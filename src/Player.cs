class Player
{
    public Room CurrentRoom { get; set; }
    
    private int health;
    private Inventory backpack;
    public Player()
    {
        CurrentRoom = null;
        health = 100;
        backpack = new Inventory(25);
    }
    public void Damage(int amount)
    {
        health = health - amount;
    }
    public void Health(int amount)
    {
        health = health + amount;
    }

    public bool IsAlive()
    {
        return health > 0;
    }
    // bool is short for Boolean. it's specific data type in the program that only can hold 2 values: ture or false 
    public int GetHealth()
    {
        return health;
    }
    // to take the item from the room!
    public bool TakeFromChest(string itemName)
    {
        Item item = CurrentRoom.Chest.Get(itemName);
        if (item == null)
        {
            System.Console.WriteLine("Item not found in the room.");
            return false;
        }
        backpack.Put(itemName, item);
        System.Console.WriteLine("You took the: " + itemName);
        return true;
    }

    public bool DropFromChest(string itemName)
    {
        Item item = backpack.Get(itemName);
        if (item == null)
        {
            System.Console.WriteLine("You don't have that items!");
            return false;
        }
        CurrentRoom.Chest.Put(itemName, item);
        System.Console.WriteLine("You dropped the: " + itemName);
        return true;
    }
    public string GetInventoryString()
    {
        return backpack.Show();
    }
}