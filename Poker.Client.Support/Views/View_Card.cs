using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.Client.Support.Views
{
    public partial class View_Card : UserControl
    {
        public View_Card()
        {
            InitializeComponent();
        }

        private void View_Card_Load(object sender, EventArgs e)
        {
            rendercontrols();
        }
        private void rendercontrols()
        {
            int y1 = this.Height;
            int x1 = this.Width;

            int height = y1 / 6;
            int width = x1 / 6;

            // Add to left top corner 
            TextBox t1 = new TextBox();
            t1.Multiline = true;
            t1.Location = new Point(0, 0);
            t1.Size = new Size(width,height);

            // Add to right top corner 
            TextBox t2 = new TextBox();
            t2.Multiline = true;
            t2.Size = new Size(width, height);
            t2.Location = new Point(x1-width*3, 0);



            // Add to left bottom corner 
            TextBox t3 = new TextBox();
            t3.Multiline = true;
            t3.Size = new Size(width, height);
            t3.Location = new Point(0, y1-height);
           
            


            this.Controls.Add(t1);
            this.Controls.Add(t2);
            this.Controls.Add(t3);
        }
    }
}
