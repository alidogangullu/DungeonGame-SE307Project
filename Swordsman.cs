using System;

namespace SE307Project
{
    [Serializable]
    public class SwordsMan : Character
    {
        private const double MaxHealth = 125.0;
        private const double MaxEnergy = 100.0;
        private String MagicName { get; set; }

        public SwordsMan(): base()
        {
            HealthPoint = MaxHealth;
            EnergyPoint = MaxEnergy;
            Weapon = new Weapon("Stick", 0, ElementType.Normal, 5);
            Cloth = new Cloth("Dusty Outfit", 0, ElementType.Normal, 5);
            MagicName = "Divine's Blessing";
        }
        public override void UseMagic()
        {
            EnergyPoint -= 50;
            Cooldown = 7;
        }

        public override bool Attack(Monster monster)
        {
            bool isWon = false;
            bool isMagicUsed = false;
            while (monster.HealthPoint > 0 && HealthPoint >0 )
            {
                
                Description();
                Console.WriteLine(monster.Description(1));;
                
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
                Console.WriteLine("1.Slash");
                if (EnergyPoint >= 40)
                {
                    Console.WriteLine("2.Mighty Slash");
                }
                Console.WriteLine("3.Drink Potion");
                if (EnergyPoint >= 50 && Cooldown == 0 )
                {
                    Console.WriteLine(MagicName); 
                }
                Console.WriteLine("Any character to pass");
                int movement = Convert.ToInt32(Console.ReadLine());
                
                //Normal Attack
                if (movement == 1)
                {
                    string formValue ;
                    int time = 0;
                    double damage = Weapon.CalculateDamage(monster.Element, false,isMagicUsed);
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
                }
                // Heavy Attack
                else if (movement == 2 && EnergyPoint > 0)
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
                    EnergyPoint -= 40;
                }
                // Potion Selection
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

                }else if (movement == 4)
                {
                    UseMagic();
                    isMagicUsed = true;
                    
                }
                else
                {
                    Console.WriteLine("Passed!");
                }

                if (monster.HealthPoint > 0)
                {
                    Console.WriteLine("Monster's Turn: ");
                    monster.Attack(this,isMagicUsed);
                }
                EnergyPoint += 10;
                if (Cooldown > 0)
                {
                    Cooldown -= 1;
                }
                if (Cooldown == 3) 
                {
                    isMagicUsed = false;
                }
                if (Cooldown > 3 )
                {
                    SetHealth(HealthPoint+10);
                }
            }

            if (HealthPoint <= 0)
            {
                Console.WriteLine("Game Over");
            }else if (monster.HealthPoint <= 0)
            {
                Console.WriteLine(monster.MType + " is dead!");
                isWon = true;
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
        
        /*Form form = new Form();

            // Set the form's properties
            form.Text = monster.MType.ToString();
            
            // Add a button to the form
            Button button = new Button();
            button.Text = "Attack "+monster.MType;
            button.Location = new Point(100, 50);
            button.AutoSize = true;
            
            form.Controls.Add(button);
            
            // Create a new label
            Label label = new Label();

            // Set the label's properties
            label.Text = monster.Description(1);
            label.Location = new Point(100, 0);
            label.AutoSize = true;

            // Add the label to the form
            form.Controls.Add(label);

            // Start the GUI's message loop
            Application.Run(form);*/
    }
}
