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
    [Serializable]
    public class Item
    {
        protected String Name { get; set; }
        protected int Value { get; set; }
        public static FactionType Faction { get; set; }
        protected ElementType Element { get; set; }
        // for sorting other items according to date
        private DateTime Date { get; set; }

        public Item() {Date = DateTime.Now; } 
        public Item(String name, int value)
        {
            Date = DateTime.Now;
            Name = name;
            Value = value;
        }

        public Item(String name, int value, ElementType element)
        {
            Date = DateTime.Now;
            Name = name;
            Value = value;
            Element = element;
        }

        public override string ToString()
        {
            return Name + " Score :"+ Value ;
        }
    }
    [Serializable]
    public class Potion : Item
    {
        public double Amount { get; set; }

        public Potion() : base()
        {
            
        }
        public Potion(String name, int value, double amount) : base(name, value)
        {
            Amount = amount;
        }
        public double Heal(double health)
        {
            return health += Amount;
        }
    }
    [Serializable]
    public class Weapon : Item
    {
        private double Damage { get; set; }
        public Weapon(String name, int val, ElementType elementType,double damage) :
            base(name, val, elementType)
        {
            Damage = damage;
        }
        public double CalculateDamage()
        {
            return Damage;
        }
    }
    [Serializable]
    public class Cloth : Item
    {
        private double Defence { get; set; }

        public Cloth(String name, int val, ElementType elementType, double defence) :
            base(name, val, elementType)
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