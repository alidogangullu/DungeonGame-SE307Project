using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Level
    {
        //TODO: When its public static also you can change its value (cheat) we need to change that somehow ???
        //name change for understandability
        public static int LevelNumber;
        private List<Room> RoomList { get; set; }
        public int CurrentRoom { get; set; }

        public Level(User currentUser)
        {
            RoomList = new List<Room>();
            LevelNumber = 0;
            if (currentUser.lastCheckpointLvl > LevelNumber)
            {
                LevelNumber = currentUser.lastCheckpointLvl;
            }
        }
        
        public void GenerateLevel()
        {
            LevelNumber++;
            RoomList.Clear();
            
            // Generate a random number of rooms for the level
            int numRooms = new Random().Next(5, 10);

            //some difficulty logic
            for (int i = 0; i < numRooms; i++)
            {
                // Create a new room and add it to the RoomList list
                Room room = new Room(i);
                RoomList.Add(room);

                // Generate monsters for the room
                room.GenerateMonsters();
            }
        }
        
        public void MoveToNextRoom()
        {
            Console.WriteLine("Current room: {0}", CurrentRoom);

            // Check if there are any more rooms in the level
            if (CurrentRoom + 1 < RoomList.Count)
            {
                CurrentRoom++;
            }
            else
            {
                //TODO: remove monsters from list when they are killed.
                if (RoomList[CurrentRoom].Monsters.Count==0)
                {
                    // If there are no more rooms and monsters, generate a new level
                    GenerateLevel();
                    CurrentRoom = 0;

                }
            }
            Console.WriteLine("Next room: {0}", CurrentRoom);
        }
        
        public void MoveToPreviousRoom()
        {
            Console.WriteLine("Current room: {0}", CurrentRoom);

            // Check if there are any more rooms in the level
            if (CurrentRoom - 1 >= 0)
            {
                CurrentRoom--;
            }

            Console.WriteLine("Previous room: {0}", CurrentRoom);
        }
        
        public Room GetCurrentRoom()
        {
            return RoomList[CurrentRoom];
        }
    }
}