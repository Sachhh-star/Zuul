class Guard
{
    public int HP;
    // public Guard loot;
    public Item Loot {get; set;}

    public Guard(int hp, Item loot = null)
    {
        HP = 100;
        Loot = loot;
    }
    public void TakeDamage(int amount)
    {
        HP -= amount;
    }
    public bool IsAlive()
    {
        return HP > 0;
    }
    
}