using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Level
    {
        //TODO: When its public static also you can change its value (cheat) we need to change that somehow ???
        public static int Number;
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
    
    

}