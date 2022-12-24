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
        // for sorting other items according to date
        private DateTime Date { get; set; }

        public Item(String name, int value)
        {
            Date = DateTime.Now;
            Name = name;
            Value = value;
        }

        public Item(String name, int value, FactionType faction, ElementType element)
        {
            Date = DateTime.Now;
            Name = name;
            Value = value;
            Faction = faction;
            Element = element;
        }
    }

    public class Potion : Item
    {
        private double Amount { get; set; }

        public Potion(String name, double amount, int value) : base(name, value)
        {
            Amount = amount;
            
        }
        public void Heal(Character c)
        {
            if (null != c)
            {
                // Adds amount, every specific character handles its setHealth method
                double mageHealth = c.CalculateHealthPoint();
                mageHealth += Amount;
                c.SetHealth(mageHealth);
            }
        }
    }
    
    public class Weapon : Item
    {
        private double Damage { get; set; }
        public Weapon(String name, int val, FactionType factionType, ElementType elementType,double damage) :
            base(name, val,factionType,elementType)
        {
            Damage = damage;
        }
        public double CalculateDamage()
        {
            return Damage;
        }
    }
    
    public class Cloth : Item
    {
        private double Defence { get; set; }

        public Cloth(String name, int val, FactionType factionType, ElementType elementType, double defence) :
            base(name, val,factionType,elementType)
        {
            Defence = defence;
        }
        public double CalculateDefence()
        {
            return Defence;
        }
    }
    
    // used later
    /*public class NullCharacterException : Exception
    {
        public void getMessage()
        {
            var color = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("Character is null, please provide correct character!");
            Console.BackgroundColor = color;
        }
    }*/
}