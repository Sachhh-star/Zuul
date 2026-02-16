using System;

class Game
{
	// Private fields
	private Parser parser;

	private Player player;
	// private Room currentRoom;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("outside the main entrance of the university");
		Room theatre = new Room("in a lecture theatre");
		Room pub = new Room("in the campus pub");
		Room lab = new Room("in a computing lab");
		Room office = new Room("in the computing admin office");

		Room kitchen = new Room("in the kitchen");
		Room cellar = new Room("in the cellar");
		Room classroom = new Room("in the classroom");
		Room hallways = new Room("in the hallways");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);

		theatre.AddExit("west", outside);

		pub.AddExit("east", outside);

		lab.AddExit("north", cellar);
		lab.AddExit("east", theatre);

		office.AddExit("west", lab);

		kitchen.AddExit("up", cellar);
		cellar.AddExit("down", office);
		classroom.AddExit("up", hallways);
		hallways.AddExit("down", kitchen);

		// Create your Items here
		Item potion = new Item(5, "healing potion");
		Item bag = new Item(1, "a black bag");
		Item sword = new Item(1, "a sword");
		Item key = new Item(1, "a rusty key!");
		Item axe = new Item(1, "a Axe");
		// ...
		// And add them to the Rooms
		kitchen.Chest.Put("potion", potion);
		kitchen.Chest.Put("sword", sword);
		kitchen.Chest.Put("bag", bag);
		lab.Chest.Put("key", key);
		lab.Chest.Put("potion", potion);

		classroom.Chest.Put("potion", potion);


		office.Chest.Put("key", key);
		office.Chest.Put("axe", axe);
		// ...
		// Add the guard 
		cellar.CurrentGuard = new Guard(100);
		hallways.CurrentGuard = new Guard(100);

		// add lock room
		lab.SetLock("key");

		// Start game outside
		// currentRoom = outside;
		player.CurrentRoom = classroom;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			if (!player.IsAlive())
			{
				Console.WriteLine("Try again!");
				return;
			}
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if (command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "look":
				PrintLook();
				break;
			case "status":
				PrintStatus();
				break;
			case "take":
				Take(command);
				break;
			case "drop":
				Drop(command);
				break;
			case "attack":
				Attack(command);
				break;
			case "use":
				Use(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}
	//  take and drop command!
	private void Take(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Take what?");
			return;
		}
		string itemName = command.SecondWord;
		player.TakeFromChest(itemName);
	}
	private void Drop(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Drop what?");
			return;
		}
		string itemName = command.SecondWord;
		player.DropFromChest(itemName);
	}

	//Use 
	private void Use(Command command)
	{

		if (!command.HasSecondWord())
		{
			Console.WriteLine("Use what?!");
			return;
		}



		string itemName = command.SecondWord;
		// string direction = command.ThridWord;

		Console.WriteLine(player.Use(itemName));
		player.Use(itemName);
		// this is not done!!!!
	}

	private void PrintLook()
	{
		Console.WriteLine(player.CurrentRoom.GetLongDescription());

		// items
	}

	private void PrintStatus()
	{
		Console.WriteLine("Your health is: " + player.GetHealth());
		Console.WriteLine("Inventory: " + player.GetInventoryString());
	}
	private void Attack(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Attack who?");
			return;
		}
		if (command.SecondWord.ToLower() != "guard" && command.SecondWord.ToLower() != "gaurd")
		{
			Console.WriteLine("You need to attack the guard!");
			return;
		}
		// string description = command.SecondWord;
		Room currentRoom = player.CurrentRoom;
		if (currentRoom.CurrentGuard == null)
		{
			Console.WriteLine("There is no Guard here.");
			return;
		}
		player.Attack(20);
	}
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}
		if (player.CurrentRoom.CurrentGuard != null && player.CurrentRoom.CurrentGuard.IsAlive())
		{
			Console.WriteLine("A guard blocks your path! You must defeat him first");
			return;
		}

		string direction = command.SecondWord;
		string itemName = command.ThridWord;

		// use the key 
		if (itemName == "key")
		{
			if (string.IsNullOrEmpty(direction))
			{
				Console.WriteLine("Which door do you want to unlock? (e.g. 'use key east')");
			}
			return;
		}
		Room targetRoom = player.CurrentRoom.GetExit(direction);
		if (targetRoom == null)
		{
			Console.WriteLine("There is no door in that direction");
		}
		else
		{
			if (!string.IsNullOrEmpty(itemName))
			{
				targetRoom.Unlock(itemName);
				Console.WriteLine($"Click! you unlock the door to the {direction}.");
			}

		}
		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}
		if (nextRoom.IsLock())
		{
			Console.WriteLine("The door is locked! You need to use the key!");
			return;
		}

		player.CurrentRoom = nextRoom;
		player.Damage(5);
		Console.WriteLine("You tripped and lost  health");
		Console.WriteLine("Your health: " + player.GetHealth());
		Console.WriteLine(player.CurrentRoom.GetLongDescription());

		if (player.CurrentRoom.GetShortDescription() == "outside the main entrance of the university")
		{
			Console.WriteLine("Congratulation! YOu have escaped the boring university!");
			Console.WriteLine("You WIN!!");

			//game is stop!
			Environment.Exit(0);
		}

		// if (player.IsAlive()== false)
		// {
		// 	Console.WriteLine("You are died!");
		// }
	}
}
