using System;
using System.Collections.Generic;
using System.Text;

namespace SE307Project
{
    [Serializable]
    public class Mage : Character
    {
        private const double MaxHealth = 100.0;
        private const double MaxMana = 200.0;
        private String MagicName { get; set; }

        public Mage() 
        {
            HealthPoint = MaxHealth;
            EnergyPoint = MaxMana;
        }

        protected override void DefineMagic()
        {
            // elemantal damage +20 ekliiyor
            //
        }

        public override void UseMagic()
        {
            throw new NotImplementedException();
        }

        public override void Attack(Monster monster)
        {
            /*var elementAbility = 
            var grandElement = */
            while (monster.HealthPoint > 0 )
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Basic Attack");
                Console.WriteLine("2.");
                Console.WriteLine("3.Drink Potion");
                Console.WriteLine("4.Encourage");
                Console.WriteLine("Any character to pass");
                int movement = Convert.ToInt32(Console.ReadLine());
                if (movement == 1)
                {
                    double formValue = 0;
                    double damage = Weapon.CalculateDamage(monster.Element, false);
                    int isPredicted = Prediction(formValue,damage);
                    // Checks whatever prediction is non-predicted, normal or critical respectively.
                    if (isPredicted == 0)
                    {
                        monster.HealthPoint -= damage/ 2;   
                    }
                    else if (isPredicted == 1 )
                    {
                        monster.HealthPoint -= damage;
                    }else if (isPredicted == 2 )
                    {
                        monster.HealthPoint -= damage * 2  ;
                    }
                }else if (movement == 2 && EnergyPoint > 0)
                {
                    double formValue = 0;
                    double damage = Weapon.CalculateDamage(monster.Element, true);
                    int isPredicted = Prediction(formValue,damage);
                    // Checks whatever prediction is non-predicted, normal or critical respectively.
                    if (isPredicted == 0)
                    {
                        monster.HealthPoint -= damage/ 2;   
                    }
                    else if (isPredicted == 1 )
                    {
                        monster.HealthPoint -= damage;
                    }else if (isPredicted == 2 )
                    {
                        monster.HealthPoint -= damage * 2  ;
                    }
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

    }
}
