using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Level
    {
        private int Number { get; set; }
        private Dictionary<int, List<Room>> RoomList { get; set; }
        private int CurrentRoom { get; set; }

        public Level()
        {
            RoomList = new Dictionary<int, List<Room>>();
        }
        
        private void GenerateLevel()
        {
        
        }
    }
    
    public class Room
    {
        private int Number { get; set; }
        private List<Monster> Monsters { get; set; }
        private List<Item> DroppedItems { get; set; }

        public Room()
        {
            DroppedItems = new List<Item>();
            Monsters = new List<Monster>();
        }
        
        private void GenerateMonster()
        {
            
        }
    }

}