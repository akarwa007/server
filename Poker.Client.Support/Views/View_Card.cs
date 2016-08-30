using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poker.Shared;

namespace Poker.Client.Support.Views
{
    public partial class View_Card : UserControl
    {
        private ViewModel_Card _vm_card = null;
        string _Caption = "Card";
        Bitmap _card;
        public View_Card(string caption)
        {
            _Caption = caption;
            _card = View_Deck.Instance.GetBackCard();
            InitializeComponent();
        }
        public View_Card(ViewModel_Card vm_Card)
        {
            _vm_card = vm_Card;
            _card = View_Deck.Instance.GetCard(new A_Card(_vm_card.Rank, _vm_card.Suit));
            InitializeComponent();
        }

        private void View_Card_Load(object sender, EventArgs e)
        {
            RenderControls();
        }
        private void View_Card_Paint(object sender, PaintEventArgs e)
        {
            repaint();
             /*
            if (_card != null)
            {
                e.Graphics.DrawImage(_card, 0, 0,this.Width,this.Height);
            }
            */
           // e.Graphics.DrawString(_Caption, DefaultFont, Brushes.Black, 5, this.Height / 2);

        }
        public void repaint()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                   // this.Controls.Clear();

                    RenderControls();
                });
            }
            else
            {
               // this.Controls.Clear();

                RenderControls();
            }
        }
        private void RenderControls()
        {
            if (_card != null)
            {
                _card = View_Deck.Instance.GetCard(new A_Card(_vm_card.Rank, _vm_card.Suit));
                this.CreateGraphics().DrawImage(_card, 0, 0, this.Width, this.Height);
                //Console.WriteLine("Card painted for " + this._vm_card.UserName);
            }
         
        }
    }
}
