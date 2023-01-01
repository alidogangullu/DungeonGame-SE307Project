using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                Console.WriteLine("Select Faction Type to create new character.");
                Console.WriteLine("1- " + FactionType.Archer.ToString());
                Console.WriteLine("2- " + FactionType.Mage.ToString());
                Console.WriteLine("3- " + FactionType.Swordsman.ToString());

                input = Convert.ToInt32(Console.ReadLine());

                if (input == 1)
                {
                    CharacterList.Add(new Archer());
                    //please choose ability
                }
                else if (input == 2)
                {
                    CharacterList.Add(new Mage());
                }
                else if (input == 3)
                {
                    CharacterList.Add(new SwordsMan());
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
                        Console.WriteLine(CharacterList.IndexOf(character) + " " + character.GetType().ToString());
                    }

                    Console.WriteLine("Select a character or '-1' to create new.");
                    input = Convert.ToInt32(Console.ReadLine());
                    if (input != -1)
                    {
                        currentCharacter = CharacterList[input];
                        Console.WriteLine("You selected " + currentCharacter.GetType());
                        //Item class FactionType attribute configuration  
                        if (currentCharacter.GetType() == typeof(Mage))
                        {
                            Item.Faction = FactionType.Mage;
                        }else if (currentCharacter.GetType() == typeof(Archer))
                        {
                            Item.Faction = FactionType.Archer;
                        }else if (currentCharacter.GetType() == typeof(SwordsMan))
                        {
                            Item.Faction = FactionType.Swordsman;
                        }
                    }
                    else
                    {
                        newCharacter();
                    }
                }
            } while (input == -1);
        }
        
        public void LoadGame()
        {
            
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
            
            SaveGame();
            
            Level level = new Level(this);
            level.GenerateLevel();

            int choice = 0;
            
            while (choice != -1)
            {
                // Get the current room
                Room room = level.GetCurrentRoom();
                
                Console.Clear();

                // Print the room number and the number of monsters in the room
                Console.WriteLine("You are in room {0}", room.Number);
                Console.WriteLine("There are {0} monsters in this room", room.Monsters.Count);

                foreach (Monster monster in room.Monsters)
                {
                    Console.WriteLine(monster.Description(room.Monsters.IndexOf(monster)));
                }

                
                Console.WriteLine("Choose an option:");
                Console.WriteLine("0. Show Inventory");
                Console.WriteLine("-1. Exit to menu");
                Console.WriteLine("1. Move to the next room");

                // Only print the previous room option if the current room is not the first room
                if (level.GetCurrentRoom().Number > 0)
                {
                    Console.WriteLine("2. Move to the previous room");
                }
                
                if (level.GetCurrentRoom().Monsters.Count != 0)
                {
                    Console.WriteLine("3. Attack");
                }

                // Read the user's choice
                choice = int.Parse(Console.ReadLine());

                if (choice == 0)
                {
                    currentCharacter.ShowItemList();
                }

                // Move to the next or previous room based on the user's choice
                else if (choice == 1)
                {
                    level.MoveToNextRoom();
                }
                else if (choice == 2 && level.GetCurrentRoom().Number > 0)
                {
                    level.MoveToPreviousRoom();
                }
                
                else if (choice == 3 && level.GetCurrentRoom().Monsters.Count != 0)
                {
                    foreach (Monster monster in room.Monsters)
                    {
                        monster.Description(room.Monsters.IndexOf(monster));
                    }
                    Console.WriteLine("Select a monster for Attack.");
                    int mChoice = int.Parse(Console.ReadLine());
                    currentCharacter.Attack(room.Monsters[mChoice]);
                }
                
                Console.WriteLine(Level.LevelNumber);
                
            }
            
            Console.WriteLine("You complete level " + Level.LevelNumber + "press '0' for save and exit, '-1' for exit without saving.");
            choice = int.Parse(Console.ReadLine());

            if (choice == 0)
            {
                SaveGame();
                //saves last completed level number, it can be used for difficulty (monsters and number of rooms calculations).
            }
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
                Console.WriteLine("User Not Found");
                return null;
            }
        }
        
    }
}