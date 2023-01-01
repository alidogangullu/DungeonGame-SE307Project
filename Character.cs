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
        

    }
}