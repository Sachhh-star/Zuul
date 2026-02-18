class Command
{
	public string CommandWord { get; init; }
	public string SecondWord { get; init; }
	public string ThirdWord { get; init; } //added!
	
	// Create a command object. First and second word must be supplied, but
	// either one (or both) can be null. See Parser.GetCommand()
	public Command(string first, string second, string thrid)
	{
		CommandWord = first;
		SecondWord = second;
		ThirdWord = thrid;
	}

	
	// Return true if this command was not understood.
	public bool IsUnknown()
	{
		return CommandWord == null;
	}

	
	// Return true if the command has a second word.
	public bool HasSecondWord()
	{
		return SecondWord != null;
	}
	// return ture if the command has a third word
	public bool HasThridWord()
	{
		return ThirdWord != null;
	}
}
