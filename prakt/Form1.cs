using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prakt
{
    public partial class Form1 : Form
    {
        int count = 1;
        Point start;
        Point end;
        Point startMouse;
        Point endMouse;



        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(MouseDounPosision);
            this.MouseUp += new MouseEventHandler(MouseUpPosition);
            this.MouseMove += new MouseEventHandler(MousPosition);

        }

        public void MouseDounPosision(Object sender, MouseEventArgs e)
        {
            
            startMouse.X = e.X;
            startMouse.Y = e.Y;

        }

        public void MouseUpPosition(Object sender, MouseEventArgs e)
        {
            endMouse.X = e.X;
            endMouse.Y = e.Y;
            if(e.Button == MouseButtons.Left)
            {
                CreateButton();
            }
            
        }

        public void CreateButton()
        {
            Abs();
            int X1 = end.X - start.X;
            int Y1 = end.Y - start.Y;
            
            
            if(X1 > 10 || Y1 > 10)
            {
                Button button = new Button();
                button.Location = start;
                button.Name = "button"+count.ToString();
                button.Size = new System.Drawing.Size(X1, Y1);
                button.TabIndex = count;
                button.Text = count.ToString();
                button.UseVisualStyleBackColor = true;
                this.Controls.Add(button);
                button.MouseDown += new MouseEventHandler(RemoveButton);
                button.MouseUp += new MouseEventHandler(StaticMove);
                
                
                
            }
            else
            {
                Button button = new Button();
                button.Location = start;
                button.Name = "button" + count.ToString();
                button.Size = new System.Drawing.Size(50, 50);
                button.TabIndex = 0;
                button.Text = count.ToString();
                button.UseVisualStyleBackColor = true;
                this.Controls.Add(button);
                button.MouseDown += new MouseEventHandler(RemoveButton);
                button.MouseUp += new MouseEventHandler(StaticMove);
            }
            count++;
        }

        public void Abs()
        {
            start.X = (startMouse.X < endMouse.X)? startMouse.X : endMouse.X;
            start.Y = (startMouse.Y < endMouse.Y) ? startMouse.Y : endMouse.Y;
            end.X = (startMouse.X > endMouse.X) ? startMouse.X : endMouse.X;
            end.Y = (startMouse.Y > endMouse.Y) ? startMouse.Y : endMouse.Y;
        }

        public void RemoveButton(Object sender, MouseEventArgs e)
        {

            if(e.Button == MouseButtons.Right)
            {
                Button button = (Button)sender;
                this.Controls.Remove(button);
                button.Dispose();
            }

        }


        public void StaticMove(Object sender, MouseEventArgs e)
        {
            Abs();
            int X1;
            int Y1;

            if (e.Button == MouseButtons.Left)
            {
                Random random = new Random();
                Button button = (Button)sender;
                X1 = button.Size.Width;
                Y1 = button.Size.Height;
                int n = random.Next(0,2);
                if(n == 0)
                {
                    int X = random.Next(0, this.Width - X1);
                    int Y = random.Next(0, this.Height - Y1);
                    button.Location = new Point(X,Y);
                }else if(n == 1)
                {
                    int X = button.Location.X;
                    int Y = random.Next(0, this.Height - Y1);
                    button.Location = new Point(X, Y);
                }
                else
                {
                    int X = random.Next(0, this.Width - X1);
                    int Y = button.Location.Y;
                    button.Location = new Point(X, Y);
                }
                
            }

        }

        public void MousPosition(Object sender, MouseEventArgs e)
        {
            this.Text = e.Location.ToString();
            
            foreach(var viev in this.Controls)
            {
                if (viev is Button)
                {

                    Button button = (Button)viev;
                    Point point = button.Location;

                    if(isRound(button, e))
                    {
                        if (point.X - 50 < e.X && e.X < point.X)
                        {
                            point.X++;
                            button.Location = point;
                        }
                        if (point.X + button.Width + 50 < e.X && e.X > point.X + button.Width + 50)
                        {
                            point.X--;
                            button.Location = point;
                        }
                        if (point.Y - 50 < e.Y && e.Y < point.Y)
                        {
                            point.Y++;
                            button.Location = point;
                        }
                        if (point.Y + button.Height + 50 < e.Y && e.Y > point.Y + button.Height + 50)
                        {
                            point.Y--;
                            button.Location = point;
                        }
                    }

                    
                }
            }
            

        }

        private bool isRound(Button button, MouseEventArgs mouse)
        {
            if(button.Location.X - 50 > mouse.X || button.Location.X + button.Width + 50 < mouse.X ||
                button.Location.Y - 50 > mouse.Y || button.Location.Y + button.Height + 50 < mouse.Y)
            {
                return true;
            }
            return false;
        }

    }
}
