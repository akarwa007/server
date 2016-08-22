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
        TextBox Left, Right;
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

            int height = y1 / 2;
            int width = x1 / 2;

            // Add to left
            Left = new TextBox();
            Left.Multiline = true;
            Left.Location = new Point(0, 0);
            Left.Size = new Size(width,height);

            // Add to right
            Right = new TextBox();
            Right.Multiline = true;
            Right.Size = new Size(width, height);
            Right.Location = new Point(width, height);

            this.Controls.Add(Left);
            this.Controls.Add(Right);
           
        }
    }
}
