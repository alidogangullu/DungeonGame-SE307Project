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

        }
    }
}