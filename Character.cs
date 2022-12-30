using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SE307Project
{
    [Serializable]
    public abstract class Character
    {
        protected double HealthPoint;
        protected double EnergyPoint { get; set; }
        protected List<Item> ItemList { get; set; }
        protected int CriticalChance { get; set; }
        protected int Cooldown { get; set; }

        public Character()
        {
            ItemList = new List<Item>();
        }

        public abstract double CalculateHealthPoint();
        public abstract void SetHealth(double health);

        public abstract double CalculateEnergyPoint();

        public abstract int CalculateCriticalChance();

        public virtual void DefineMagic()
        {

        }
        

        public virtual void UseMagic()
        {
            
        }

        public virtual void Attack(Room room)
        {
            
            foreach (Monster monster in room.Monsters)
            {
                monster.Description(room.Monsters.IndexOf(monster));
            }
            
            Console.WriteLine("Select a monster for Attack.");
            int choice = int.Parse(Console.ReadLine());
            
            Form form = new Form();

            // Set the form's properties
            form.Text = room.Monsters[choice].MType.ToString();
            

            // Add a button to the form
            Button button = new Button();
            button.Text = "Attack "+room.Monsters[choice].MType;
            button.Location = new Point(100, 50);
            button.AutoSize = true;
            
            button.Click += (sender, e) =>
            {
                room.Monsters.RemoveAt(choice);
                form.Close();
            };
            
            form.Controls.Add(button);
            
            // Create a new label
            Label label = new Label();

            // Set the label's properties
            label.Text = room.Monsters[choice].Description(choice);
            label.Location = new Point(100, 0);
            label.AutoSize = true;

            // Add the label to the form
            form.Controls.Add(label);

            // Start the GUI's message loop
            Application.Run(form);
        }
    }
}