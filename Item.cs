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
        public String Name { get; set; }
        protected int Value { get; set; }
        public static FactionType Faction { get; set; }
        public ElementType Element { get; set; }
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

        public double CalculateDamage(ElementType monsterElement,bool isHeavy, bool isSkill)
        {
            int percentage;
            if (monsterElement == ElementType.Dark && Element == ElementType.Holy )
            {
                percentage = 30;
            }else if (monsterElement == ElementType.Holy && Element == ElementType.Dark)
            {
                percentage = 30;
            }
            else if (monsterElement == ElementType.Water && Element == ElementType.Lightning )
            {
                percentage = 40;
            }else if (monsterElement == ElementType.Lightning && Element == ElementType.Nature)
            {
                percentage = 40;
            }else if (monsterElement == ElementType.Nature && Element == ElementType.Fire)
            {
                percentage = 40;
            }else if (monsterElement == ElementType.Fire && Element == ElementType.Water)
            {
                percentage = 40;
            }
            else
            {
                return CalculateDamage();
            }
            
            if (Faction == FactionType.Mage && isSkill)
            {
                return isHeavy ? (Damage + Damage * ((percentage+20) / 100.0))*1.2 :
                    (Damage + Damage * ((percentage+20) / 100.0)) ;
            }
            else if (Faction == FactionType.Swordsman && isSkill)
            {
                return 1 ;
            }
            else if (Faction == FactionType.Archer && isSkill)
            {
                
            }
            return isHeavy ? (Damage + Damage * ((percentage) / 100.0))*1.2 :
                (Damage + Damage * ((percentage) / 100.0)) ;
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