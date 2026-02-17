class Inventory
{
    private int MaxWeight;
    private Dictionary<string, Item> items;

    public Inventory(int maxWeight)
    {
        this.MaxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }
    public bool Put(string itemName, Item item)
    {
        if (FreeWeight() < item.Weight)
        {
            return false; //if it too heavy maybe???
        }
        if (items.ContainsKey(itemName))
        {
            Console.WriteLine($"You already have a {itemName}!");
            return false;
        }
        // this is when you get the item for your inventory??? 
        items.Add(itemName, item);
        return true;
    }
    public Item Get(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            Item foundItem = items[itemName];
            Item item = items[itemName];
            items.Remove(itemName);
            return foundItem;
        }
        return null; //it will tell that you dont have it 
    }

    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName);
    }
    //  help methods
    public int TotalWeight()
    {
        int total = 0;
        foreach (Item item in items.Values)
        {
            total = total + item.Weight;
        }
        return total;
        //its will loop through all items and add their weights
    }
    public int FreeWeight()
    {
        return MaxWeight - TotalWeight();
        // return MaxWeight; 
        // testing :D
    }

    // this help to look at the item, idk if its need it or not 
    public string Show()
    {
        if (items.Count == 0) return "nothing.";
        return string.Join(", ", items.Keys);
    }
}