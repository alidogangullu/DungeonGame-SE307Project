using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SE307Project
{
    [Serializable] 
    public class User
    {
        public long ID { get; set; }
        private List<Character> CharacterList { get; set; }
        public Character CurrentCharacter { get; set; }
        private List<int> ScoreList { get; set; }
        
        public User(long UserID)
        {
            ID = UserID;
            CharacterList = new List<Character>();
            ScoreList = new List<int>();
        }
        
        private void ChooseCharacter()
        {
            void newCharacter()
            {
                int input;
                bool inputValid = false;

                while (!inputValid)
                {
                    try
                    {
                        Console.WriteLine("Select Faction Type to create new character.");
                        Console.WriteLine("1- " + FactionType.Archer.ToString());
                        Console.WriteLine("2- " + FactionType.Mage.ToString());
                        Console.WriteLine("3- " + FactionType.Swordsman.ToString());

                        input = Convert.ToInt32(Console.ReadLine());

                        if (input == 1)
                        {
                            CharacterList.Add(new Archer());
                            inputValid = true;
                        }
                        else if (input == 2)
                        {
                            CharacterList.Add(new Mage());
                            inputValid = true;
                        }
                        else if (input == 3)
                        {
                            CharacterList.Add(new SwordsMan());
                            inputValid = true;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong Input Try Again");
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Wrong Input Try Again");
                    }
                }


            }

            int input = -1;
            do
            {
                if (CharacterList.Count == 0)
                {
                    newCharacter();
                }
                else
                {
                    foreach (Character character in CharacterList)
                    {
                        if (character.GetType() == typeof(Archer))
                        {
                            Console.WriteLine(CharacterList.IndexOf(character) + " Archer");
                        }
                        else if (character.GetType() == typeof(Mage))
                        {
                            Console.WriteLine(CharacterList.IndexOf(character) + " Mage");
                        }
                        else if (character.GetType() == typeof(SwordsMan))
                        {
                            Console.WriteLine(CharacterList.IndexOf(character) + " SwordsMan");
                        }
                        
                    }

                    bool inputValid = false;

                    while (!inputValid)
                    {
                        Console.WriteLine("Select a character or '-1' to create new.");

                        try
                        {
                            input = Convert.ToInt32(Console.ReadLine());
                            if (input<=CharacterList.Count-1)
                            {
                                inputValid = true;
                            }
                            else
                            {
                                Console.WriteLine("Wrong Input, Try Again..");  
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Wrong Input, Try Again..");   
                        }

                        if (input != -1 && inputValid)
                        {
                            CurrentCharacter = CharacterList[input];
                            Console.WriteLine("You selected " + CurrentCharacter.GetType());
                            //Item class FactionType attribute configuration  
                            if (CurrentCharacter.GetType() == typeof(Mage))
                            {
                                Item.Faction = FactionType.Mage;
                            }
                            else if (CurrentCharacter.GetType() == typeof(Archer))
                            {
                                Item.Faction = FactionType.Archer;
                            }
                            else if (CurrentCharacter.GetType() == typeof(SwordsMan))
                            {
                                Item.Faction = FactionType.Swordsman;
                            }
                        }
                        else if(input == -1)
                        {
                            newCharacter();
                        }
                    }

                }
            } while (input == -1);
        }
        
        public void LoadGame()
        {
            if (CurrentCharacter.LastCheckpointLvl > Level.LevelNumber)
            {
                Level.LevelNumber = CurrentCharacter.LastCheckpointLvl;
            }
        }

        private void SaveGame()
        {
            //save last finished level's number
            CurrentCharacter.LastCheckpointLvl = Level.LevelNumber;

            // Open the file
            using FileStream fs = new FileStream(Directory.GetCurrentDirectory()+@"\users\"+ID + ".bin", FileMode.Create);
            
            // Save this object
            BinaryFormatter formatter = new BinaryFormatter();
            fs.Position = 0;
            formatter.Serialize(fs, this);
        }
        public void SaveUser()
        {
            // Open the file
            using FileStream fs = new FileStream(Directory.GetCurrentDirectory()+@"\users\"+ID + ".bin", FileMode.Create);
            
            // Save this object
            BinaryFormatter formatter = new BinaryFormatter();
            fs.Position = 0;
            formatter.Serialize(fs, this);
        }

        // When user wants exit without finishing the level
        private void ThrowItems(DateTime genDate)
        {
            List<Item> items = new List<Item>();
            items = CurrentCharacter.ItemList;
            foreach (var item in CurrentCharacter.ItemList)
            {
                if (item.Date.CompareTo(genDate) > 0)
                {
                    items.Remove(item);
                }
            }
        }

        public void StartGame()
        {
            
            ChooseCharacter();
            // First we define level so that loading level number can work
            Level level = new Level();
            LoadGame(); //Load lastCheckpointLvl.
            
            level.GenerateLevel();

            int choice = 0;
            
            while (choice != -1)
            {
                // Get the current room
                Room room = level.GetCurrentRoom();
                
                Console.Clear();

                for (int i = 0; i < level.RoomList.Count; i++)
                {
                    Console.WriteLine(level.RoomList[i].Count);
                }

                // Print the room number and the number of monsters in the room
                Console.WriteLine("--------------Level "+ Level.LevelNumber +"-------------------");
                Console.WriteLine("You are in Room {0}", level.CurrentRoom);
                Console.WriteLine("You are in Corridor {0}", level.CurrentCorridor);
                Console.WriteLine("There are {0} monsters in this room", room.Monsters.Count);
                Console.WriteLine("---------------------------------------------");

                foreach (Monster monster in room.Monsters)
                {
                    Console.WriteLine(monster.Description(room.Monsters.IndexOf(monster)));
                }
                
                Console.WriteLine(" ");

                foreach (Item droppedItem in room.DroppedItems)
                {
                    Console.WriteLine(droppedItem.Element+" "+droppedItem.Name);
                }

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("-1. Exit to menu");
                Console.WriteLine("0. Show Inventory");
                Console.WriteLine("1. Move");

                if (room.DroppedItems.Count > 0)
                {
                    Console.WriteLine("2. Add a dropped item to inventory");
                }
                
                if (level.GetCurrentRoom().Monsters.Count != 0)
                {
                    Console.WriteLine("3. Attack");
                }
                
                CurrentCharacter.Description();
                
                wrongChoice:
                Boolean inptValid = false;
                while (!inptValid)
                {
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                        inptValid = true;
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Wrong input, try again!");
                    }
                }
                
                switch (choice)
                {
                    case 0:
                        CurrentCharacter.ShowItemList();
                        break;
                    case 1:
                        var isExitPossible= level.Movement();
                        if (isExitPossible)
                        {
                            Console.WriteLine("You complete level " + Level.LevelNumber +
                                              "press '0' for save and exit, '-1' for exit without saving.");
                            bool inputValid = false;
                            while (!inputValid)
                            {
                                try
                                {
                                    choice = int.Parse(Console.ReadLine());

                                    if (choice == 0)
                                    {
                                        inputValid = true;
                                        SaveGame();
                                        Score();
                                        //saves last completed level number, it can be used for difficulty (monsters and generateLevel calculations).
                            
                                    }

                                    else if (choice == -1)
                                    {
                                        inputValid = true;
                                        Score();
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Invalid input format. Please enter a valid integer.");
                                }
                            }
                        }
                        break;
                    case 2:
                    {
                        foreach (Item droppedItem in room.DroppedItems)
                        {
                            Console.WriteLine(room.DroppedItems.IndexOf(droppedItem)+" "+droppedItem.Element+" "+droppedItem.Name);
                        }
                        Console.WriteLine("Enter an item index:");

                        try
                        {
                            int itemNo = int.Parse(Console.ReadLine());

                            if (CurrentCharacter.ItemList.Count >= CurrentCharacter.ItemList.Capacity)
                            {
                                Console.WriteLine("Full, you can not add this item!");
                            }
                            else
                            {
                                CurrentCharacter.ItemList.Add(room.DroppedItems[itemNo]);
                                room.DroppedItems.RemoveAt(itemNo);
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid integer.");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Invalid item index. Please enter a valid index for an item in the list.");
                        }
                        
                        break;
                    }
                    case 3 when level.GetCurrentRoom().Monsters.Count != 0:
                    {
                        foreach (Monster monster in room.Monsters)
                        {
                            Console.WriteLine(monster.Description(room.Monsters.IndexOf(monster)));
                        }
                        Console.WriteLine("Select a monster for Attack.");

                        try
                        {
                            int mChoice = int.Parse(Console.ReadLine());
                            bool isWon = CurrentCharacter.Attack(room.Monsters[mChoice]);

                            if (isWon)
                            {
                                room.Monsters[mChoice].DropItems(room);
                                level.GetCurrentRoom().Monsters.RemoveAt(mChoice);
                            }
                            else
                            {
                                Console.WriteLine("You are dead, nice try!");
                                SaveGame();
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input format. Please enter a valid integer.");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Invalid monster index. Please enter a valid index for a monster in the list.");
                        }

                        break;
                    }
                    case -1 :
                        SaveGame();
                        ThrowItems(level.GenerationDateTime);
                        break;
                    default:
                        Console.WriteLine("Wrong Input. Try Again.");
                        goto wrongChoice;
                }
            }
        }

        public void Score()
        {
            int score = 0;
            foreach (Item item in CurrentCharacter.ItemList)
            {
                score += item.Value;
            }
            
            Console.WriteLine("Your total score is " + score);
        }
    }
}