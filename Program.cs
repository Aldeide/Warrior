using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Warrior;
using Blazored.LocalStorage;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var http = new HttpClient()
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};

builder.Services.AddScoped(sp => http);

/*
Item[]? items;
System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions
{
    Converters =
            {
                new System.Text.Json.Serialization.JsonStringEnumConverter(System.Text.Json.JsonNamingPolicy.CamelCase)
            }
};
items = await http.GetFromJsonAsync<Item[]>("data/items.json", options);
*/
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(s => new Simulation());

await builder.Build().RunAsync();
