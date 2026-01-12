class Player
{
    public Room CurrentRoom { get; set; }

    private int health;

    public Player()
    {
        CurrentRoom = null;
        health = 100;

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
}