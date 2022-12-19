using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
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
