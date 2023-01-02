using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Room
    {
        public int Number { get; set; }
        public int corridorConnectionUp { get; set; }
        public int corridorConnectionDown { get; set; }
        
        public Boolean Left { get; set; }
        public Boolean Right { get; set; }
        public List<Monster> Monsters { get; set; }
        public List<Item> DroppedItems { get; set; }

        public Room(int number)
        {
            Number = number;
            corridorConnectionUp = -1;
            corridorConnectionDown = -1;
            Left = false;
            Right = false;
            DroppedItems = new List<Item>();
            Monsters = new List<Monster>();
            GenerateMonsters();
        }
         
        private void GenerateMonsters()
        {
            Random random = new Random();
            
            int numMonsters = random.Next(1, 4);

            for (int i = 0; i < numMonsters; i++)
            {
                // Create a new monster and add it to the Monsters list
                Monster monster = new Monster();
                Monsters.Add(monster);
            }
        }

    }
    
}
