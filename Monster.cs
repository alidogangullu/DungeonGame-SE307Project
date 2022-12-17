using System;
using System.Collections.Generic;

namespace SE307Project
{
    public enum MonsterType
    {
        Skeleton,
        Undead,
        Angel,
        Sprit,
        Imp,
        Naga,
        Dragon,
        Harpie,
        Snake,
        Tyrant,
        Pheonix
    }
    
    public class Monster
    {
        private String Name { get; set; }
        private double HealthPoint { get; set; }
        private int Level { get; set; }
        private ElementType Element { get; set; }
        private MonsterType MonsterType { get; set; }
        private int BaseDamage { get; set; }
        private List<Item> EquipedItems { get; set; }

        public Monster(ElementType element, MonsterType monsterType)
        {
            Element = element;
            MonsterType = monsterType;
        }

        private void CalculateBaseDamage()
        {
            
        }

        public void Attack()
        {
            
        }

        public void DropItems(Room room)
        {
            
        }
    }

}