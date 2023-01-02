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
                    temp.CreateUser();
                }
                else
                {
                    try
                    {
                        currentUser = temp.FindUser(input);
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

    }
    
}
