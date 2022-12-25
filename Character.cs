using System;
using System.Collections.Generic;

namespace SE307Project
{
    [Serializable]
    public abstract class Character
    {
        protected double HealthPoint;
        protected double EnergyPoint { get; set; }
        protected List<Item> ItemList { get; set; }
        protected int CriticalChance { get; set; }
        protected int Cooldown { get; set; }

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

        public virtual void Attack()
        {
            
        }
    }
    
    
    
}