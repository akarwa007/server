﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerClient
{
    public partial class TestForm : Form
    {
        int tabcount = 1;
        Color[] bk_color = new Color[]{Color.AliceBlue,Color.Coral};
        int color_index = 0;

        public TestForm()
        {
            InitializeComponent();
            tabControl1.TabPages.Clear();
            
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            TestPlayer newplayer = new TestPlayer();

            this.tabControl1.TabPages.Add("Player_" + tabcount.ToString());
            TabPage newpage = tabControl1.TabPages[tabcount - 1];
            newpage.BackColor = bk_color[color_index%2];
            color_index++;
            newpage.Controls.Add(newplayer);
            tabcount++;

        }

        private void TestForm_Load(object sender, EventArgs e)
        {

        }
    }
}
