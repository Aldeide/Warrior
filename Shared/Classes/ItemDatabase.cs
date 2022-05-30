namespace Warrior
{
    public class ItemDatabase
    {
        public List<Item> items { get; set; }
        public ItemDatabase()     
        {
            items = new List<Item>();
        }
        public Item GetItemById(int id)
        {
            return items.Find(x => x.id == id);
        }
    }
}
