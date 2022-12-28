using System;
using System.Collections.Generic;
using System.IO;

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
            //Randomized health and damage because of replay-ability 
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
                case MonsterType.Snake:
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
            //TODO I will generate an item json file and its contents.
            //using FileStream fs = new FileStream(Directory.GetCurrentDirectory()+@"\conf\items"+Level.LevelNumber+".data",FileMode.Open);
            
            
            // method will read from item data
            Random random = new Random();
            int val = random.Next(0, 4);
            int val2 = random.Next(0, 4);
            if (val == 1 && (val2 == 2 || val2 == 0 || val2 == 3))
            {
                EquippedItems.Add(new Weapon("Weapon",0, Element, 0));
            }
            else if (val == 2 && (val2 == 1 || val2 == 0 || val2 == 3))
            {
                EquippedItems.Add(new Cloth("Armor",0, Element, 0));
            }else if (val == 3 && (val2 == 0 || val2 == 1 || val2 == 2))
            {
                EquippedItems.Add(new Potion("Potion",0,0));
            }
            else if (val2 == 1 && val == 1)
            {
                EquippedItems.Add(new Weapon("Weapon",0, Element, 0));
                EquippedItems.Add(new Potion("Potion",0,0));
            }else if (val2 == 2 && val == 2)
            {
                EquippedItems.Add(new Cloth("Armor",0, Element, 0));
                EquippedItems.Add(new Potion("Potion",0,0));
            }else if (val2 == 3 && val == 3)
            {
                EquippedItems.Add(new Potion("Potion",0,0));
                EquippedItems.Add(new Potion("Potion",0,0));
            }else if (val2 == 0 && val == 0)
            {
                EquippedItems.Add(new Weapon("Weapon",0, Element, 0));
                EquippedItems.Add(new Cloth("Armor",0, Element, 0));
                EquippedItems.Add(new Potion("Potion",0,0));
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