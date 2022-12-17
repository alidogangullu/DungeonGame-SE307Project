using System;
using System.Collections.Generic;

namespace SE307Project
{
    public abstract class Character
    {
        protected double HealthPoint { get; set; }
        protected double EnergyPoint { get; set; }
        protected List<Item> ItemList { get; set; }
        protected int CriticalChance { get; set; }
        protected int Cooldown { get; set; }

        public Character()
        {
            ItemList = new List<Item>();
        }

        public double CalculateHealthPoint()
        {
            return HealthPoint;
        }
        
        public double CalculateEnergyPoint()
        {
            return EnergyPoint;
        }
        
        public int CalculateCriticalChance()
        {
            return CriticalChance;
        }

        private void DefineMagic()
        {
            
        }

        public void UseMagic()
        {
            
        }

        public void Attack()
        {
            
        }
    }
    
    public class SwordsMan : Character
    {
        private String MagicName { get; set; }

        public double CalculateHealthPoint()
        {
            return HealthPoint;
        }
        
        public double CalculateEnergyPoint()
        {
            return EnergyPoint;
        }
        
        public int CalculateCriticalChance()
        {
            return CriticalChance;
        }
        
        private void DefineMagic()
        {
            
        }

        public void UseMagic()
        {
            
        }

        public void Attack()
        {
            
        }
    }
    
    public class Mage : Character
    {
        private String MagicName { get; set; }

        public double CalculateHealthPoint()
        {
            return HealthPoint;
        }
        
        public double CalculateEnergyPoint()
        {
            return EnergyPoint;
        }
        
        public int CalculateCriticalChance()
        {
            return CriticalChance;
        }
        
        private void DefineMagic()
        {
            
        }

        public void UseMagic()
        {
            
        }

        public void Attack()
        {
            
        }
    }
    
    public class Archer : Character
    {
        private String MagicName { get; set; }

        public double CalculateHealthPoint()
        {
            return HealthPoint;
        }
        
        public double CalculateEnergyPoint()
        {
            return EnergyPoint;
        }
        
        public int CalculateCriticalChance()
        {
            return CriticalChance;
        }
        
        private void DefineMagic()
        {
            
        }

        public void UseMagic()
        {
            
        }

        public void Attack()
        {
            
        }
    }
}