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
    public partial class View_Casino : UserControl
    {
        ViewModel_Casino _casinoModel;
        public View_Casino()
        {
            InitializeComponent();
        }
        public void UpdateModel(ViewModel_Casino model)
        {
            _casinoModel = model;
            //refresh the ui 
           // this.treeView1.Nodes.Clear();
            ClearTreeView();
           // model.ListOfTables.Select(x => new(x.GameName,x.GameValue,x.TableNo));
           var results = from p in model.ListOfTables
                          group p.GameValue by p.GameName into g
                         select new { gamename = g.Key, gamevalues = g.ToList() };

            foreach(var x in results)
            {
                TreeNode node = new TreeNode(x.gamename);
                foreach(var c in x.gamevalues)
                {
                    node.Nodes.Add(c);
                }
               // this.treeView1.Nodes.Add(node);
                AddTreeNodes(node);
            }
           
        }
        private void ClearTreeView()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(ClearTreeView));
            }
            this.treeView1.Nodes.Clear();
        }
        private void AddTreeNodes(TreeNode node)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<TreeNode>(AddTreeNodes), new object[] { node });
                return;
            }
            this.treeView1.Nodes.Add(node);
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
