using System;

namespace SE307Project
{
    [Serializable]
    public class Archer : Character
    {
        private const double MaxHealth = 85.0;
        private const double MaxEnergy = 150.0;
        private String MagicName { get; set; }

        public Archer() : base()
        {
            HealthPoint = MaxHealth;
            EnergyPoint = MaxEnergy;
            Weapon = new Weapon("Old Short Bow", 0, ElementType.Normal, 5);
            Cloth = new Cloth("Dusty Outfit", 0, ElementType.Normal, 5);
            MagicName = "Turn into shadow";
        }
        
        public override void UseMagic()
        {
            EnergyPoint -= 50;
            Cooldown = 6;
        }

        public override bool Attack(Monster monster)
        {
            bool isWon = false;
            bool isMagicUsed = false;
            while (monster.HealthPoint > 0  && HealthPoint > 0)
            {
                Description();
                Console.WriteLine(monster.Description(1));
                Console.WriteLine("\n");
                
                if (isMagicUsed)
                {
                    Console.WriteLine(MagicName + ": Active");
                }
                else
                {
                    Console.WriteLine(MagicName + ": Not Active");
                }
                
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Shoot an arrow");
                if (EnergyPoint >= 20)
                {
                    Console.WriteLine("2.Shoot two arrows");
                }
                Console.WriteLine("3.Drink Potion");
                if (EnergyPoint >= 50 && Cooldown == 0)
                {
                    Console.WriteLine("4."+MagicName);
                }
             
                int movement = Convert.ToInt32(Console.ReadLine());
                
                if (movement == 1)
                {
                    string formValue ;
                    int time = 0;
                    double damage = Weapon.CalculateDamage(monster.Element, false,isMagicUsed);
                    Console.WriteLine("Enter any prediction value you have 5 seconds to gain critic chance: ");
                    (time,formValue) = Timer();
                    int isPredicted = Prediction(time,Convert.ToDouble(formValue),damage);
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
                }
                else if (movement == 2 && EnergyPoint >= 20)
                {
                    string formValue ;
                    int time = 0;
                    double damage = Weapon.CalculateDamage(monster.Element, true,isMagicUsed);
                    Console.WriteLine("Enter any prediction value you have 5 seconds to gain critic chance: ");
                    (time,formValue) = Timer();
                    int isPredicted = Prediction(time,Convert.ToDouble(formValue),damage);
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
                    EnergyPoint -= 20;
                }
                else if (movement == 3)
                {
                    var count = 0;
                    foreach (var item in ItemList)
                    {
                        count++;
                        if (typeof(Potion) == item.GetType())
                        {
                            Console.WriteLine(ItemList.IndexOf(item) + "( " +item.ToString());
                            count = 0;
                        }
                    }// this is here for if there is no potion users don't have to use their turn
                    if (count == 0)
                    {
                        Console.WriteLine("There is no potion in your inventory \n");
                        continue;
                    }
                    //Healing
                    Console.WriteLine("Which one do you want to choose ?");
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
                    
                }
                else if (movement == 4 && EnergyPoint >= 40 && Cooldown == 0)
                {
                    UseMagic();
                    isMagicUsed = true;
                }
                else
                {
                    Console.WriteLine("Passed!");
                }
                monster.Attack(this,isMagicUsed);
                EnergyPoint += 10;
                if (Cooldown > 0)
                {
                    Cooldown -= 1;
                }
                if (Cooldown == 4)
                {
                    isMagicUsed = false;
                }
            }
            if (HealthPoint <= 0)
            {
                Console.WriteLine("Game Over");
            }
            else if (monster.HealthPoint <= 0)
            {
                isWon = true;
                Cooldown = 0;
            }
            return isWon;
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
