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

        public override void Attack(Monster monster)
        {
            base.Attack(monster);
            while (monster.HealthPoint > 0 )
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Slash");
                Console.WriteLine("2.Mighty Slash");
                Console.WriteLine("3.Drink Potion");
                Console.WriteLine("4.Encourage");
                Console.WriteLine("Any character to pass");
                int movement = Convert.ToInt32(Console.ReadLine());
                if (movement == 1)
                {
                    monster.HealthPoint -= Weapon.CalculateDamage();
                }else if (movement == 2 && EnergyPoint > 0)
                {
                    monster.HealthPoint -= Weapon.CalculateDamage() * 1.5;
                    EnergyPoint -= 40;
                }else if (movement == 3)
                {
                    foreach (var item in ItemList)
                    {
                        if (typeof(Potion) == item.GetType())
                        {
                            Console.WriteLine(ItemList.IndexOf(item) + "( " +item.ToString());
                        }
                    }
                    //Healing
                    Console.WriteLine("Which one are you want to choose ?");
                    try
                    {
                        var pChoice = Convert.ToInt32(Console.ReadLine());
                        var selection = (Potion) ItemList[pChoice];
                        SetHealth(selection.Heal(HealthPoint));
                        ItemList.RemoveAt(pChoice);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                    
                }else if (movement == 4)
                {
                    UseMagic();
                }
                else
                {
                    Console.WriteLine("Passed!");
                }
                monster.Attack(this);
            }
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
