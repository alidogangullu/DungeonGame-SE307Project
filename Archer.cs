using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    [Serializable]
    public class Archer : Character
    {
        private const double MaxHealth = 85.0;
        private const double MaxEnergy = 150.0;
        private const int ArcherCritical = 40;
        private String MagicName { get; set; }

        public Archer()
        {
            HealthPoint = MaxHealth;
            EnergyPoint = MaxEnergy;
            CriticalChance = ArcherCritical;
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
