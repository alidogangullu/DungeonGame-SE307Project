using System;

namespace SE307Project
{
    [Serializable]
    public class Mage : Character
    {
        private const double MaxHealth = 100.0;
        private const double MaxMana = 200.0;
        private String MagicName { get; set; }

        public Mage() :base ()
        {
            HealthPoint = MaxHealth;
            EnergyPoint = MaxMana;
            Weapon = new Weapon("Broken Wand", 0, ElementType.Normal, 5);
            Cloth = new Cloth("Dusty Outfit", 0, ElementType.Normal, 5);
            MagicName = "Elemental Boost";
        }
        
        public override void UseMagic()
        {
            EnergyPoint -= 50;
            Cooldown = 5;
        }

        public override bool Attack(Monster monster)
        {
            bool isWon = false;
            bool isMagicUsed = false;
            var elementAbility = DefineFirstAbility();
            var grandElement = DefineSecondAbility();
            while (monster.HealthPoint > 0 )
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1."+elementAbility);
                if (EnergyPoint >= 40)
                {
                    Console.WriteLine("2."+grandElement);
                }
                Console.WriteLine("3.Drink Potion");
                if ( EnergyPoint >= 50 && Cooldown == 0)
                {
                    Console.WriteLine("4."+MagicName);
                }
                Console.WriteLine("Any character to pass");
                int movement = Convert.ToInt32(Console.ReadLine());
                
                
                if (movement == 1)
                {
                    double formValue = 0;
                    double damage = Weapon.CalculateDamage(monster.Element, false,isMagicUsed);
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
                }// heavy
                else if (movement == 2 && EnergyPoint >= 40)
                {
                    double formValue = 0;
                    double damage = Weapon.CalculateDamage(monster.Element, true,isMagicUsed);
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
                    
                    
                }else if (movement == 4 && EnergyPoint >= 40 && Cooldown == 0)
                {
                    UseMagic();
                    isMagicUsed = true;
                }
                else
                {
                    Console.WriteLine("Passed!");
                }
                monster.Attack(this);
                EnergyPoint += 10;
                if (Cooldown > 0)
                {
                    Cooldown -= 1;
                }
                if (Cooldown == 3)
                {
                    isMagicUsed = false;
                }
            }
            if (HealthPoint <= 0)
            {
                Console.WriteLine("Game Over");
            }else if (monster.HealthPoint <= 0)
            {
                isWon = true;
            }
            return isWon;
        }

        private String DefineFirstAbility()
        {
            string ability = "";;
            switch (Weapon.Element)
            {
                case ElementType.Holy:
                    ability = "Holy Power";
                    break;
                case ElementType.Dark:
                    ability = "Soul Attack";
                    break;
                case ElementType.Lightning:
                    ability = "Lightning Bolt";
                    break;
                case ElementType.Nature:
                    ability = "Nature's Wrath";
                    break;
                case ElementType.Fire:
                    ability = "Fire Damage";
                    break;
                case ElementType.Water:
                    ability = "Frost Damage";
                    break;
                case ElementType.Normal:
                    ability = "Basic Attack";
                    break;
                default:
                    ability = "";
                    break;
            }
            return ability;
        }

        private String DefineSecondAbility()
        {
            string ability = "";;
            switch (Weapon.Element)
            {
                case ElementType.Holy:
                    ability = "Divine's Great Light";
                    break;
                case ElementType.Dark:
                    ability = "Soul Conjuring";
                    break;
                case ElementType.Lightning:
                    ability = "Lightning Storm";
                    break;
                case ElementType.Nature:
                    ability = "Gravity Pressure";
                    break;
                case ElementType.Fire:
                    ability = "Fireball";
                    break;
                case ElementType.Water:
                    ability = "Frost Spike";
                    break;
                case ElementType.Normal:
                    ability = "Heavy Attack";
                    break;
                default:
                    ability = "";
                    break;
            }
            return ability;
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
