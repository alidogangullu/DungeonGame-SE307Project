using System;

namespace SE307Project
{
    public enum FactionType
    {
       Mage,
       Swordsman,
       Archer
    }

    public enum ElementType
    {
        Holy,
        Dark,
        Lightning,
        Nature,
        Fire,
        Water,
        Normal
    }
    public class Item
    {
        protected String Name { get; set; }
        protected int Value { get; set; }
        protected FactionType Faction { get; set; }
        protected ElementType Element { get; set; }
        private DateTime Date { get; set; }
        
    }

    public class Potion : Item
    {
        private double Amount { get; set; }

        public void Heal(Character c)
        {
            
        }
    }
    
    public class Weapon : Item
    {
        private double Amount { get; set; }

        public double CalculateDamage()
        {
            return Amount;
        }
    }
    
    public class Cloth : Item
    {
        private double Amount { get; set; }

        public double CalculateDefence()
        {
            return Amount;
        }
    }
}