using System;
using System.Collections.Generic;

namespace SE307Project
{
    public enum MonsterType
    {
        Skeleton,
        Undead,
        Angel,
        Spirit,
        Imp,
        Naga,
        Dragon,
        Harpy,
        Snake,
        Tyrant,
        Phoenix
    }
    
    public class Monster
    {
        public double HealthPoint { get; set; }
        public ElementType Element { get; set; }
        public MonsterType MType { get; set; }
        // immutable used for calculations in attack method
        public int BaseDamage { get;  private set; }
        public List<Item> EquippedItems { get; private set; }

        public Monster()
        {
            //Randomized Monster creation without any constructor parameter.
            Random random = new Random();
            MonsterType[] values = (MonsterType[])Enum.GetValues(typeof(MonsterType));
            int index = random.Next(values.Length);
            MonsterType randomMonster = values[index];
            MType = randomMonster;
            
            CalculateBaseDamage();
            GenerateItems();
        }

        private void CalculateBaseDamage()
        {
            //Randomized because of replay-ability 
            Random random = new Random();
            //Base damage calculations dependent to the level 
            switch (MType)
            {
                case MonsterType.Skeleton:
                    Element = ElementType.Dark;
                    BaseDamage = Level.LevelNumber * random.Next(5, 7) ;
                    HealthPoint = Level.LevelNumber * random.Next(10, 21);
                    break;
                case MonsterType.Undead:
                    Element = ElementType.Dark;
                    BaseDamage = Level.LevelNumber * random.Next(5, 9);
                    HealthPoint = Level.LevelNumber * random.Next(15, 21);
                    break;
                case MonsterType.Angel:
                    Element = ElementType.Holy;
                    BaseDamage = Level.LevelNumber * random.Next(3, 6);
                    HealthPoint = Level.LevelNumber * random.Next(20, 26);
                    break;
                case MonsterType.Spirit:
                    Element = ElementType.Holy;
                    BaseDamage = Level.LevelNumber * random.Next(10, 16);
                    HealthPoint = Level.LevelNumber * random.Next(5, 11);
                    break;
                case MonsterType.Imp:
                    Element = ElementType.Fire;
                    BaseDamage = Level.LevelNumber * random.Next(7, 11);
                    HealthPoint = Level.LevelNumber * random.Next(15, 21);
                    break;
                case MonsterType.Naga:
                    Element = ElementType.Water;
                    BaseDamage = Level.LevelNumber * random.Next(7, 11);
                    HealthPoint = Level.LevelNumber * random.Next(20, 26);
                    break;
                case MonsterType.Dragon:
                    int dragonElement = random.Next(1, 5);
                    if (dragonElement == 1)
                    {
                        Element = ElementType.Lightning;
                    }else if (dragonElement == 2)
                    {
                        Element = ElementType.Water;
                    }else if (dragonElement == 3)
                    {
                        Element = ElementType.Fire;
                    }else if (dragonElement == 4)
                    {
                        Element = ElementType.Normal;
                    }
                    BaseDamage = Level.LevelNumber * random.Next(20, 41);
                    HealthPoint = Level.LevelNumber * random.Next(60, 81);
                    break;
                case MonsterType.Harpy:
                    Element = ElementType.Lightning;
                    BaseDamage = Level.LevelNumber * random.Next(7, 11);
                    HealthPoint = Level.LevelNumber * random.Next(20, 26);
                    break;
                case  MonsterType.Snake:
                    Element = ElementType.Normal;
                    BaseDamage = Level.LevelNumber * random.Next(3, 5);
                    HealthPoint = Level.LevelNumber * random.Next(10, 21);
                    break;
                case MonsterType.Tyrant:
                    Element = ElementType.Nature;
                    BaseDamage = Level.LevelNumber  * random.Next(10, 21);
                    HealthPoint = Level.LevelNumber * random.Next(30, 41);
                    break;
                case MonsterType.Phoenix:
                    Element = ElementType.Fire;
                    BaseDamage = Level.LevelNumber * random.Next(10, 21);
                    HealthPoint = Level.LevelNumber * random.Next(20, 41);
                    break;
            }
        }
        // This method will generate items according to the level and monster's element type 
        private void GenerateItems()
        {
            Random random = new Random();
            // if 0 comes there is no item 1 comes generate weapon if 2, generate cloth  
            int val = random.Next(0,3);
            // how should faction replace maybe there can be static function to get or when user starts automatically item fucntion is assigned
            // Item item = new Item();
            switch (Element)
            {
                case ElementType.Dark :
                    
                    break;
                case ElementType.Fire :
                    break;
                case ElementType.Holy :
                    break;
                case ElementType.Lightning :
                    break;
                case ElementType.Nature :
                    break;
                case ElementType.Normal :
                    break;
                case ElementType.Water :
                    break;
            }
        }

        public void Description()
        {
            Console.WriteLine(MType);
            Console.WriteLine(BaseDamage);
            Console.WriteLine(HealthPoint);
        }
        
        public void Attack()
        {
            
        }

        public void DropItems(Room room)
        {
            
        }
    }
    
}