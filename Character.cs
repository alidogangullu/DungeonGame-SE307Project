using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SE307Project
{
    [Serializable]
    public abstract class Character
    {
        public double HealthPoint;
        public double EnergyPoint { get; set; }
        public List<Item> ItemList { get; set; }
        // ability cooldown
        protected int Cooldown { get; set; }
        public Weapon Weapon { get; set; }
        public Cloth Cloth { get; set; }
        public int LastCheckpointLvl  { get; set ; }

        protected Character()
        {
            ItemList = new List<Item>(10);
            LastCheckpointLvl = 0;
        }

        public abstract double CalculateHealthPoint();
        public abstract void SetHealth(double health);
        public abstract double CalculateEnergyPoint();
        public abstract void UseMagic();
        public abstract bool Attack(Monster monster);
        
        // Generates Timer and calculates how much time user passed given any input
        public (int, String) Timer()
        {
            var keyInfo = new ConsoleKeyInfo();
            var userInput = new StringBuilder();
            var stopWatch = new Stopwatch();
            var started = false;

            do
            {
                keyInfo = Console.ReadKey(true);

                if (started == false)
                {
                    stopWatch.Start();
                    started = true;
                }

                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    Console.Write("\b \b");
                    if(userInput.Length > 0) userInput.Remove(userInput.Length - 1, 1);
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // Do nothing
                }
                else if(Char.IsLetter(keyInfo.KeyChar) ||
                        Char.IsDigit(keyInfo.KeyChar) ||
                        Char.IsWhiteSpace(keyInfo.KeyChar) ||
                        Char.IsPunctuation(keyInfo.KeyChar))

                {
                    Console.Write(keyInfo.KeyChar);
                    userInput.Append(keyInfo.KeyChar);
                }
            } while (keyInfo.Key != ConsoleKey.Enter);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            var finalString = userInput.ToString();

            Console.WriteLine();

            //Console.WriteLine($"String entered: {finalString}.");
            //Console.WriteLine($"It took {ts.Seconds} seconds.");
            
            return (ts.Seconds, finalString) ;
        }
        public int Prediction(int predictionTime, double predict, double exactValue)
        {
            int isPredicted;
            double diff = Math.Abs(predict - exactValue);
            //Critical Chance
            if (diff <= 0.5 && predictionTime <= 5)
            {
                isPredicted = 2;
            }//Normal Damage
            else if (0.5 < diff && diff <= 1 )
            {
                isPredicted = 1;
            }
            else
            {
                isPredicted = 0;
            }
            

            return isPredicted;
        }
        public virtual void ShowItemList()
        {
            Console.WriteLine("---Inventory---");
            foreach (Item item in ItemList)
            {
                Console.WriteLine(ItemList.IndexOf(item)+1 + "- " + item.Element +" "+item.Name);
            }

            Console.WriteLine(" ");
            Boolean inputValid = false;
            while (!inputValid)
            {
                try
                {
                    Console.WriteLine("Select an item or '0' to drop item menu or '-1' to quit");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == -1)
                    {
                        return;
                    }
                    if (choice == 0)
                    {
                        Console.WriteLine("Select an item for dropping.");
                        int selection = Convert.ToInt32(Console.ReadLine());

                        if (selection <= 0 || selection > ItemList.Count)
                        {
                            throw new ArgumentOutOfRangeException();
                        }

                        ItemList.RemoveAt(selection-1);
                        inputValid = true;
                    }
                    else if (choice > 0 && choice <= ItemList.Count)
                    {
                        if (ItemList[choice-1].GetType() == typeof(Weapon))
                        {
                            ItemList.Add(Weapon);
                            Weapon = ItemList[choice-1] as Weapon;
                        }
                        else if (ItemList[choice-1].GetType() == typeof(Cloth))
                        {
                            ItemList.Add(Cloth);
                            Cloth = ItemList[choice-1] as Cloth;
                        }
                        
                        inputValid = true;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid selection. Please enter a valid item number.");
                }
            }

            
            
        }

        public void Description()
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Your Health: " +HealthPoint);
            Console.WriteLine("Your Energy: " + EnergyPoint);
            Console.WriteLine("Your Attack Damage: " + Weapon.Damage +" Element: " + Weapon.Element);
            Console.WriteLine("Your Defense: " + Cloth.Defence+" Element: " + Cloth.Element);
            Console.WriteLine("Your Magic Cooldown: "+ Cooldown);
            Console.WriteLine("---------------------------------------------");
        }
        
    }
}