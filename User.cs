using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization.Formatters.Binary;

namespace SE307Project
{
    [Serializable] 
    public class User
    {
        private long ID { get; set; }
        private List<Character> CharacterList { get; set; }
        public Character currentCharacter { get; set; }
        private List<int> ScoreList { get; set; }

        public int lastCheckpointLvl { get; set; }

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
                            currentCharacter = CharacterList[input];
                            Console.WriteLine("You selected " + currentCharacter.GetType());
                            //Item class FactionType attribute configuration  
                            if (currentCharacter.GetType() == typeof(Mage))
                            {
                                Item.Faction = FactionType.Mage;
                            }
                            else if (currentCharacter.GetType() == typeof(Archer))
                            {
                                Item.Faction = FactionType.Archer;
                            }
                            else if (currentCharacter.GetType() == typeof(SwordsMan))
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
            if (lastCheckpointLvl > Level.LevelNumber)
            {
                Level.LevelNumber = lastCheckpointLvl;
            }
        }

        private void SaveGame()
        {
            //save last finished level's number
            lastCheckpointLvl = Level.LevelNumber;
            
            // Open the file
            using FileStream fs = new FileStream(Directory.GetCurrentDirectory()+@"\users\"+ID + ".bin", FileMode.Create);
            
            // Save this object
            BinaryFormatter formatter = new BinaryFormatter();
            fs.Position = 0;
            formatter.Serialize(fs, this);
        }

        public void StartGame()
        {
            ChooseCharacter();
            
            SaveGame(); //Save new characters.
            LoadGame(); //Load lastCheckpointLvl.
            
            Level level = new Level();
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

                foreach (Item droppedItem in room.DroppedItems)
                {
                    Console.WriteLine(room.DroppedItems.IndexOf(droppedItem)+" "+droppedItem.Name);
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
                
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Your Health: " + currentCharacter.HealthPoint);
                Console.WriteLine("Your Energy: " + currentCharacter.EnergyPoint);
                Console.WriteLine("Your Attack Damage: " + currentCharacter.Weapon.Damage);
                Console.WriteLine("---------------------------------------------");

               
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
                        currentCharacter.ShowItemList();
                        break;
                    case 1:
                        level.Movement();
                        break;
                    case 2:
                    {
                        Console.WriteLine("Enter an item index:");

                        try
                        {
                            int itemNo = int.Parse(Console.ReadLine());

                            if (currentCharacter.ItemList.Count >= currentCharacter.ItemList.Capacity)
                            {
                                Console.WriteLine("Full, you can not add this item!");
                            }
                            else
                            {
                                currentCharacter.ItemList.Add(room.DroppedItems[itemNo]);
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
                            bool isWon = currentCharacter.Attack(room.Monsters[mChoice]);

                            if (isWon)
                            {
                                room.Monsters[mChoice].DropItems(room);
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
                    default:
                        Console.WriteLine("Wrong Input. Try Again.");
                        goto wrongChoice;
                }

            }
            
            bool inputValid = false;

            while (!inputValid)
            {
                Console.WriteLine("You complete level " + Level.LevelNumber +
                                  "press '0' for save and exit, '-1' for exit without saving.");
                try
                {
                    choice = int.Parse(Console.ReadLine());

                    if (choice == 0)
                    {
                        inputValid = true;
                        SaveGame();
                        //saves last completed level number, it can be used for difficulty (monsters and generateLevel calculations).
                    }

                    else if (choice == -1)
                    {
                        inputValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input format. Please enter a valid integer.");
                }
            }

            int score = 0;
            foreach (Item item in currentCharacter.ItemList)
            {
                score += item.Value;
            }
            
            Console.WriteLine("Your total score is " + score);
        }

        public void CreateUser()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory()+@"\users\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory()+@"\users\");
            }
            String[] fileNames = Directory.GetFiles(Directory.GetCurrentDirectory()+@"\users\");
            
            long biggestID = 0;
            foreach (String fileName in fileNames)
            {
                // get userID from fileName
                String[] parts = fileName.Split(@"\");
                String filename = parts.Last();  // "1.bin"
                String[] filenameParts = filename.Split('.');
                String numberString = filenameParts.First();  // "1"
                
                long readedID = long.Parse(numberString);
                
                if (readedID > biggestID)
                {
                    biggestID = readedID;
                }
            }
            
            //create a new user with unique id
            long newUserID = biggestID + 1;
            new User(newUserID).SaveGame();
            Console.WriteLine("Your ID is " + newUserID);
        }
        
        public User FindUser(long ID)
        {
            //Open the file which name is equal to this userID.
            if (File.Exists(Directory.GetCurrentDirectory()+@"\users\"+ID + ".bin"))
            {   
                // Deserialize the user from the file which created in CreateUser
                using (FileStream fs = new FileStream(Directory.GetCurrentDirectory()+@"\users\"+ID + ".bin", FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    User user= (User)formatter.Deserialize(fs);

                    // Use the deserialized object
                    Console.WriteLine("Welcome: " + user.ID);
                    return user;
                }
            }
            else
            {
                return null;
            }
        }
        
    }
}