using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SE307Project
{
    class Program
    {
        static void Main(string[] args)
        {
            User currentUser = null;
            long input = 0;
            
            do
            {
                Console.WriteLine("Enter a user ID to load game or " +
                                  "Enter '-1' to create new user.");
                bool inputValid = false;

                while (!inputValid)
                {
                    try
                    {
                        input = long.Parse(Console.ReadLine());
                        inputValid = true;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Wrong Input Try Again");
                    }
                }
                
                if (input == -1)
                {
                    CreateUser();
                }
                else
                {
                    try
                    {
                         currentUser = FindUser(input);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        if (currentUser == null)
                        {
                            Console.WriteLine("User not found.");
                            input = -1;
                        }
                    }
                }

            } while (input == -1 );

            input = 0;
            while (input != -1)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("-1.Exit");
                Console.WriteLine("1.Select a character");
                Console.WriteLine("2.Tutorial ");

                input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        currentUser.StartGame();
                        break;
                    case 2:
                        Console.WriteLine("Factions\n" +
                                          "Each faction have one normal attack one heavy attack; heavy attack consumes 40 energy points.\n" +
                                          "Every faction has it's own unique type of magic abilities. It consumes 50 energy points\n" +

                                          "Archer: \n" +
                                          "Can dodge every incoming attack from monster for 2 turns. Cooldown:\n" +

                                          "Mage:\n" +
                                          "Can increase 20 percent of the elemantal advantage for 3 turns. Cooldow\n:" +

                                          "Swordsman:\n" +
                                          "Can heal 10 health points for 2 turns\n" +
                                          " ------------------------------‐-----\n" +
                                          "Elements \n" +
                                          "Dark is 30% more effective to Holy\n" +
                                          "Holy is 30% more  effective to Dark\n" +

                                          "Fire is 40% more effective to Nature\n" +
                                          "Nature is 40% more effective to Lighting\n" +
                                          "Lightning is 40% more effective to Water\n" +
                                          "Water is 40% more effective to Water \n" +
                                          "Elements is only effective for weapons\n" +
                                          "In attack phase you have to calculate the damage which is going to be happen.\n" +
                                          " If you want to make an critical attack you should predict around exact value +-0.5 in 5 seconds.\n" +
                                          " If you want your attack to reach its exact value you should predict around +-1\n"+
                        "Each time you are collecting items which have certain value. At the level's end you can see on the screen your score");
                        break;
                    default:
                        input = -1;
                        break;
                }
                
            }
            
            
        }
        public static void CreateUser()
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
            new User(newUserID).SaveUser();
            Console.WriteLine("Your ID is " + newUserID);
        }
        public static User FindUser(long ID)
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
