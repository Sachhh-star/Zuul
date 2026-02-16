using System.Reflection.Metadata;

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
        health = Math.Min(health + amount, 100);
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
        if (backpack.Put(itemName, item))
        {
            Console.WriteLine("You took the:" + itemName);
            return true;
        }
        else
        {
            CurrentRoom.Chest.Put(itemName, item);
            return false;
        }
        // backpack.Put(itemName, item);
        // System.Console.WriteLine("You took the: " + itemName);
        // return true;
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

    //use method 

    public string Use(string itemName)
    {
        Item item = backpack.Get(itemName); //you can check of the player have this item in the backpack
        if (item == null) // you can try to take and use it 
        {
            return "You don't have this item!";
        }
        String massages = "";

        switch (itemName.ToLower())
        {
            case "potion":
                Health(25);
                massages = "You have drink the potion! And your heal is now " + health;
                break;
            case "sword":
                Attack(20);
                massages = "You have attack the guard! he health is: ";
                backpack.Put(itemName, item);
                break;
            case "axe":
                Attack(25);
                backpack.Put(itemName, item);
                break;
            case "key":
                bool UnlockSomething = false;
                foreach (Room neighbor in CurrentRoom.GetExits().Values)
                {
                    if (neighbor.IsLock())
                    {
                        neighbor.Unlock("key");
                        UnlockSomething = true;
                    }
                }
                if (UnlockSomething)
                {
                    massages = "You have unlock the door!";
                }
                else
                {
                    massages = "You need to use key to unlock the door!";

                }
                backpack.Put(itemName, item);
                break;

            default:
                massages = "You can't is that!";
                backpack.Put(itemName, item);
                break;
        }

        // if (itemName == "potion")
        // {
        //     Health(20); // it give you 20 heal
        //     backpack.Get(itemName); // this means that when you use the item it will remove permenently
        //     return "You has use potion";
        // }
        return massages;

    }
    public void Attack(int damage)
    {
        Guard enemy = CurrentRoom.CurrentGuard;
        if (enemy == null)
        {
            Console.WriteLine("There is no one to be attack!");
            return;
        }
        enemy.TakeDamage(damage);
        Console.WriteLine($"You hit the guard for {damage} damage!");
        Console.WriteLine($"Guard HP: {enemy.HP}");
        // if (!CurrentRoom.HasAliveGuard())
        // {
        //     this.Damage(10);
        //     Console.WriteLine("The guard hits you back");
        // }
        if (CurrentRoom.HasAliveGuard())
        {
            // CurrentRoom.CurrentGuard.TakeDamage(damage); 
            Damage(10);
            Console.WriteLine("The guard hits you back!");
            if (!IsAlive())
            {
                Console.WriteLine("The guard has hit you and you have died!");
            }
        }
        else
        {
            Console.WriteLine("You defeated the guard! The path is now clear!");
        }
    }

}