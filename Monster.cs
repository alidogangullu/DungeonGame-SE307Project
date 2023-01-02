using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
            EquippedItems = new List<Item>();
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
            List<Weapon> classW =selectWeaponForClass();

            List<Potion> myPotions = new List<Potion>();
            myPotions.Add(new Potion("Potion of Minor Healing",10,10));
            myPotions.Add(new Potion("Potion of Adequate Healing",15,20));
            myPotions.Add(new Potion("Potion of Plentiful  Healing",20,40));
            myPotions.Add(new Potion("Potion of Plentiful  Healing",25,60));
            myPotions.Add(new Potion("Potion of Ultimate Healing",40,150));
            

            List<Cloth> clothes = new List<Cloth>();
            clothes.Add(new Cloth("Fine Scaled Armor",10,Element,10));
            clothes.Add(new Cloth("Superior Scaled Armor",15,Element,15));
            clothes.Add(new Cloth("Exquisite Scaled Armor",20,Element,20));
            clothes.Add(new Cloth("Flawless Scaled Armor",25,Element,25));
            clothes.Add(new Cloth("Legendary Scaled Armor",30,Element,30));
            
            // method will read from item data
            Random random = new Random();
            int val = random.Next(0, 4);
            int val2 = random.Next(0, 4);
            if (val == 1 && (val2 == 2 || val2 == 0 || val2 == 3))
            {
                int weaponNo = random.Next(0, classW.Count);
                EquippedItems.Add(classW[weaponNo]);
            }
            else if (val == 2 && (val2 == 1 || val2 == 0 || val2 == 3))
            {
                int cNo = random.Next(0, clothes.Count);
                EquippedItems.Add(clothes[cNo]);
            }else if (val == 3 && (val2 == 0 || val2 == 1 || val2 == 2))
            {
                int pNo = random.Next(0, myPotions.Count);
                EquippedItems.Add(myPotions[pNo]);
            }
            else if (val2 == 1 && val == 1)
            {
                int weaponNo = random.Next(0, classW.Count);
                EquippedItems.Add(classW[weaponNo]);
                int pNo = random.Next(0, myPotions.Count);
                EquippedItems.Add(myPotions[pNo]);
            }else if (val2 == 2 && val == 2)
            {
                int cNo = random.Next(0, clothes.Count);
                EquippedItems.Add(clothes[cNo]);
                int pNo = random.Next(0, myPotions.Count);
                EquippedItems.Add(myPotions[pNo]);
            }else if (val2 == 3 && val == 3)
            {
                int pNo1 = random.Next(0, myPotions.Count);
                EquippedItems.Add(myPotions[pNo1]);
                int pNo2 = random.Next(0, myPotions.Count);
                EquippedItems.Add(myPotions[pNo2]);
            }else if (val2 == 0 && val == 0)
            {
                int weaponNo = random.Next(0, classW.Count);
                EquippedItems.Add(classW[weaponNo]);
                int cNo = random.Next(0, clothes.Count);
                EquippedItems.Add(clothes[cNo]);
                int pNo = random.Next(0, myPotions.Count);
                EquippedItems.Add(myPotions[pNo]);
            }
        }
        
        public String Description(int i)
        {
            String description = i+ "- " + MType + "\n" 
                                 + "Monster Damage: " + BaseDamage + "\n"
                                 + "Monster Health: "+HealthPoint;
            return description;
        }
        
        public void Attack(Character enemy, bool isSkill)
        {
            if (isSkill && enemy.GetType() == typeof(Archer))
            {
                return;
            }
            Random random = new Random();
            var attackType = random.Next(1, 3);
            // Normal Attack
            if (attackType == 1)
            {
                Console.WriteLine("Normal Attack!");
                enemy.SetHealth(enemy.CalculateHealthPoint()+enemy.Cloth.Defence - BaseDamage );
            }//Heavy Attack
            else if (attackType == 2)
            {
                Console.WriteLine("Heavy Attack!");
                enemy.SetHealth(enemy.CalculateHealthPoint()+enemy.Cloth.Defence-BaseDamage*1.2);
            }
        }

        public void DropItems(Room room)
        {
            foreach (Item equippedItem in EquippedItems)
            {
                room.DroppedItems.Add(equippedItem);
            }
        }

        private List<Weapon> selectWeaponForClass()
        {
            List<Weapon> classWeapon = new List<Weapon>();
            if (Item.Faction == FactionType.Archer)
            {
                classWeapon.Add(new Weapon("Shortbow",10,Element,10));
                classWeapon.Add(new Weapon("Longbow",15,Element,15));
                classWeapon.Add(new Weapon("Crossbow",20,Element,20));
            }else if (Item.Faction == FactionType.Swordsman)
            {
                classWeapon.Add(new Weapon("Short Sword",10,Element,10));
                classWeapon.Add(new Weapon("Battleaxe",15,Element,15));
                classWeapon.Add(new Weapon("Dagger",5,Element,5));
                classWeapon.Add(new Weapon("Mace",20,Element,20));
                classWeapon.Add(new Weapon("Greatsword",25,Element,25));
            }
            else
            {
                classWeapon.Add(new Weapon("Wand",10,Element,10));
                classWeapon.Add(new Weapon("Staff",15,Element,20));
            }

            return classWeapon;
        }
    }
    
}