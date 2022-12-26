using System;
using System.Collections.Generic;

namespace SE307Project
{
    class Program
    {
        static void Main(string[] args)
        {
            
            User temp = new User(0);
            //we can call createUser, FindUser methods with using this temp.

            long input;
            User currentUser = null;
            
            do
            {
                Console.WriteLine("Enter a user ID to load game or " +
                                  "Enter '-1' to create new user.");
                input = long.Parse(Console.ReadLine());
                
                if (input == -1)
                {
                    temp.CreateUser();
                }
                else
                {
                    currentUser = temp.FindUser(input);
                }

            } while (input == -1 );
            
            
            currentUser.ChooseCharacter();
            
            currentUser.SaveGame();
            
            Level level = new Level(currentUser);
            level.GenerateLevel();

            int choice = 0;
            
            while (choice != -1)
            {
                // Get the current room
                Room room = level.GetCurrentRoom();

                // Print the room number and the number of monsters in the room
                Console.WriteLine("You are in room {0}", room.Number);
                Console.WriteLine("There are {0} monsters in this room", room.Monsters.Count);

                foreach (Monster monster in room.Monsters)
                {
                    monster.Description();
                }

                Console.WriteLine("Choose an option:");
                Console.WriteLine("-1. Exit to menu");
                Console.WriteLine("1. Move to the next room");

                // Only print the previous room option if the current room is not the first room
                if (level.CurrentRoom > 0)
                {
                    Console.WriteLine("2. Move to the previous room");
                }

                // Read the user's choice
                choice = int.Parse(Console.ReadLine());

                // Move to the next or previous room based on the user's choice
                if (choice == 1)
                {
                    level.MoveToNextRoom();
                }
                else if (choice == 2 && level.CurrentRoom > 0)
                {
                    level.MoveToPreviousRoom();
                }

            }
            
            Console.WriteLine("You complete level " + Level.LevelNumber + "press '0' for save and exit, '-1' for exit without saving.");
            choice = int.Parse(Console.ReadLine());

            if (choice == 0)
            {
                currentUser.SaveGame();
                //saves last completed level number, it can be used for difficulty (monsters and number of rooms calculations).
            }
            
        }

    }
    
}
