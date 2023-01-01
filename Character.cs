using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SE307Project
{
    [Serializable]
    public abstract class Character
    {
        protected double HealthPoint;
        protected double EnergyPoint { get; set; }
        protected List<Item> ItemList { get; set; }
        // ability cooldown
        protected int Cooldown { get; set; }
        public Weapon Weapon { get; set; }
        public Cloth Cloth { get; set; }
        public Character()
        {
            ItemList = new List<Item>();
        }

        public abstract double CalculateHealthPoint();
        public abstract void SetHealth(double health);

        public abstract double CalculateEnergyPoint();

        public abstract int CalculateCriticalChance();

        public virtual void DefineMagic()
        {

        }


        public virtual void UseMagic()
        {
            
        }

        public abstract void Attack(Monster monster);


        public virtual void ShowItemList()
        {
            Console.WriteLine("---Inventory---");
            foreach (Item item in ItemList)
            {
                Console.WriteLine(ItemList.IndexOf(item) + "- " + item.Name);
            }

            Console.WriteLine(" ");
            Console.WriteLine("Select an item or '-1' to quit");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (ItemList[choice].GetType() == typeof(Weapon))
            {
                ItemList.Add(this.Weapon);
                this.Weapon = ItemList[choice] as Weapon;
            }
            
            
            else if (ItemList[choice].GetType() == typeof(Cloth))
            {
                ItemList.Add(this.Cloth);
                this.Cloth = ItemList[choice] as Cloth;
            }

            else if (choice == -1)
            {
                
            }
        }
        
    }
}