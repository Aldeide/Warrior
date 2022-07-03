using BlazorWorker.Core;
using BlazorWorker.BackgroundServiceFactory;
using BlazorWorker.WorkerBackgroundService;

namespace Warrior
{
	public class ProgressRef { public int Progress { get; set; } }

	public class SimulationManager
	{
		public int numWorkers { get; set; } = 15;
		string dps = "";
        List<IWorker> workers = new List<IWorker>();
        public List<IWorkerBackgroundService<Simulation>> backgroundServices { get; set; } = new List<IWorkerBackgroundService<Simulation>>();
		public List<ProgressRef> simulationProgress = new List<ProgressRef>();
		public bool ready { get; set; }
		public bool isRunning { get; set; }

		public event EventHandler Ready;

		public event EventHandler Completed;

        public SimulationManager()
        {
        }

        public async Task Initialize(IWorkerFactory workerFactory)
        {
			try
			{
				while (workers.Count() < numWorkers)
				{
					Console.WriteLine("Initializing a worker in simulation manager.");
					var worker = await workerFactory.CreateAsync();
					workers.Add(worker);
					var service = await worker.CreateBackgroundServiceAsync<Simulation>(options => options
								.AddAssemblies("Warrior.dll")
								.AddAssemblies("System.Text.Json.dll")
								.AddAssemblies("System.Text.Encodings.Web.dll"));
					backgroundServices.Add(service);
					var progressRef = new ProgressRef();
					simulationProgress.Add(progressRef);
					await service.RegisterEventListenerAsync(nameof(Simulation.Progress),
						(object s, SimulationProgress eventInfo) =>
						{
							progressRef.Progress = (int)Math.Floor((100 * ((decimal)eventInfo.progress)));
						});
				}
				Ready?.Invoke(this, null);
				ready = true;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public async void Start(string config, int totalIterations)
        {
			if (!ready) return;
			if (isRunning) return;
			isRunning = true;
			int iterationsPerWorker = (int)Math.Floor((float)totalIterations / numWorkers);
			var allTasks = new List<Task<Warrior.Results.SimulationResults>>();
			var servicesStarted = 0;

			foreach (var backgroundService in backgroundServices.Take(numWorkers))
			{
				var task = backgroundService.RunAsync(s => s.SimulateWithSettings(config, iterationsPerWorker));
				allTasks.Add(task);
				servicesStarted++;
			}

			var result = await Task.WhenAll(allTasks.ToArray()).ContinueWith(t =>
			{
				return t.Result;
			});

			float dpsResults = 0;
			foreach (var r in result)
			{
				dpsResults += r.dps;
			}
			dpsResults /= numWorkers;

			dps = dpsResults.ToString();
			isRunning = false;
		}

		public void NotifyCompletion()
        {
			Completed?.Invoke(this, null);
        }
    }




}
