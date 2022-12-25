using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    [Serializable]
    public class SwordsMan : Character
    {
        private const double MaxHealth = 125.0;
        private const double MaxEnergy = 100.0;
        private const int SwordsmanCritical = 20;
        private String MagicName { get; set; }

        public SwordsMan()
        {
            HealthPoint = MaxHealth;
            EnergyPoint = MaxEnergy;
            CriticalChance = SwordsmanCritical;
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
