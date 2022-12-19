using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    public class Mage : Character
    {
        private const double MaxHealth = 100.0;
        private const double MaxMana = 200.0;
        private const int MageCritical = 20;
        private String MagicName { get; set; }

        public Mage() 
        {
            HealthPoint = MaxHealth;
            EnergyPoint = MaxMana;
            CriticalChance = MageCritical;
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

        public override double CalculateHealthPoint()
        {
            return HealthPoint;
        }

        public override void SetHealth(double health)
        {
            HealthPoint = health >= MaxHealth ? MaxHealth : health;
        }


        public override double CalculateEnergyPoint()
        {
            return EnergyPoint;
        }

        public override int CalculateCriticalChance()
        {
            return CriticalChance;
        }
    }
}
