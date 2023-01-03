using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SE307Project
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //User temp = new User(0);
            //we can call createUser, FindUser methods with using this temp.
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
            
            
            currentUser.StartGame();
            
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
