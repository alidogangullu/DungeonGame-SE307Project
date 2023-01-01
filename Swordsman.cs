using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        public override void Attack(Monster monster,)
        {
            Form form = new Form();

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
            Application.Run(form);
            while (monster.HealthPoint > 0 && HealthPoint >0 )
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1.Slash");
                Console.WriteLine("2.Mighty Slash");
                Console.WriteLine("3.Drink Potion");
                Console.WriteLine("4.Encourage"); // magic name
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
                }
                else
                {
                    Console.WriteLine("Passed!");
                }

                if (monster.HealthPoint > 0)
                {
                    monster.Attack(this);
                }
                EnergyPoint += 10;
            }

            if (HealthPoint <= 0)
            {
                // levela döner
                Console.WriteLine("Game Over");
            }else if (monster.HealthPoint <= 0)
            {
                //Mimarisi güzel olanı yap return olan
                monster.DropItems();
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
