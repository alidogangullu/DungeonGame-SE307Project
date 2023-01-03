using System;
using System.Collections.Generic;

namespace SE307Project
{
    public class Level
    {
        public static int LevelNumber;
        public List<List<Room>> RoomList { get; set; }
        public int CurrentRoom { get; set; }
        public int CurrentCorridor { get; set; }

        public Level()
        {
            RoomList = new List<List<Room>>();
            LevelNumber = 0;
        }
        
        public void GenerateLevel()
        {
            LevelNumber++;
            RoomList.Clear();
            
            int numFloors = new Random().Next(1, 3);
            numFloors *= LevelNumber;

            int roomNumberToGoDown = 0;
            
            for (int i = 0; i < numFloors; i++)
            {
                RoomList.Add(new List<Room>());
                // Generate a random number of rooms for the floors
                
                int numRooms = new Random().Next(3, 5); //MINIMUM 3!
                numRooms *= LevelNumber;
                Math.Clamp(numRooms, 1, 10);

                for (int j = 0; j < numRooms; j++)
                {
                    RoomList[i].Add(new Room(j));
                    if (j == 0)
                    {
                        RoomList[i][j].Right = true;
                    }
                    else if (j == numRooms-1)
                    {
                        RoomList[i][j].Left = true;
                    }
                    else
                    {
                        RoomList[i][j].Left = true;
                        RoomList[i][j].Right = true;
                    }
                }

                if (i==0)
                {
                    roomNumberToGoDown = new Random().Next(0, RoomList[i].Count);
                }
                else
                {
                    var middleRandom = new Random().Next(0, RoomList[i].Count);
                    RoomList[i][middleRandom].corridorConnectionUp = roomNumberToGoDown;
                    RoomList[i - 1][roomNumberToGoDown].corridorConnectionDown = middleRandom;
                    
                }
                
                
            }
        }

        public void Movement()
        {
            if (RoomList[CurrentCorridor][CurrentRoom].Left)
            {
                Console.WriteLine("Go Left - A");
                
            }

            if (RoomList[CurrentCorridor][CurrentRoom].Right)
            {
                Console.WriteLine("Go Right - D");
            }

            if (RoomList[CurrentCorridor][CurrentRoom].corridorConnectionUp != -1)
            {
                Console.WriteLine("Go Up - W");
            }

            if (RoomList[CurrentCorridor][CurrentRoom].corridorConnectionDown != -1)
            {
                Console.WriteLine("Go Down - S");
            }

            int totalMonsterCount = 0;
            foreach (List<Room> rooms in RoomList)
            {
                foreach (Room room in rooms)
                {
                    totalMonsterCount += room.Monsters.Count;
                }
            }
            if (totalMonsterCount == 0)
            {
                Console.WriteLine("All monsters are killed in this level. Do you want to pass to new level? y or n");
                String input = Console.ReadLine();
                if (input.Equals("y"))
                {
                    GenerateLevel();
                }
                else if (input.Equals("n"))
                {
                    
                }
                
            }
            
            wrongChoice:
                
            String choice = Console.ReadLine().ToUpper();
            
                switch (choice)
                {
                    case "A":
                        if (CurrentRoom > 0)
                        {
                            CurrentRoom--;
                        }
                        else
                        {
                            Console.WriteLine("Cannot move in that direction.");
                        }
                        break;
                    case "D":
                        if (CurrentRoom < RoomList[CurrentCorridor].Count - 1)
                        {
                            CurrentRoom++;
                        }
                        else
                        {
                            Console.WriteLine("Cannot move in that direction.");
                        }
                        break;
                    case "W":
                        if (CurrentCorridor > 0)
                        {
                            var temp = RoomList[CurrentCorridor][CurrentRoom].corridorConnectionUp;
                            CurrentCorridor--;
                            CurrentRoom = temp;
                        }
                        else
                        {
                            Console.WriteLine("Cannot move in that direction.");
                        }
                        break;
                    case "S":
                        if (CurrentCorridor < RoomList.Count - 1)
                        {
                            var temp2 = RoomList[CurrentCorridor][CurrentRoom].corridorConnectionDown;
                            CurrentCorridor++;
                            CurrentRoom = temp2;
                        }
                        else
                        {
                            Console.WriteLine("Cannot move in that direction.");
                        }
                        break;
                    default:
                        Console.WriteLine("Wrong input, try again!");
                        goto wrongChoice;
                }
                
        }
        
        public Room GetCurrentRoom()
        {
            return RoomList[CurrentCorridor][CurrentRoom];
        }
    }
}