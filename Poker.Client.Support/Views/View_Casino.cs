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
using Newtonsoft.Json;
using Poker.Shared;

namespace Poker.Client.Support.Views
{
    public partial class View_Casino : UserControl
    {
        ViewModel_Casino _casinoModel;
        public event JoinedTableHandler JoinedTableEvent;
        public View_Casino()
        {
            InitializeComponent();
        }
       public string UserName
        {
            get;set;
        }
        public void UpdateModel(ViewModel_Casino model)
        {
            _casinoModel = model;
         
            ClearTreeView();
          
          
            var groups = model.ListOfTables.GroupBy(x => x.GameName, x => new { x.GameValue , x});
            foreach (var j in groups)
            {
                TreeNode node = new TreeNode(j.Key.ToString());
                foreach (var k in j.ToList())
                {
                    TreeNode t = new TreeNode(k.GameValue);
                    t.Tag = k.x;
                    node.Nodes.Add(t);
                }
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
        public void CallBack(Poker.Shared.Message message)
        {
            if (message.MessageType == MessageType.CasinoUpdate)
            {
                ViewModel_Casino vm = JsonConvert.DeserializeObject<ViewModel_Casino>(message.Content);
                
                this.UpdateModel(vm);
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ViewModel_Table vm = (ViewModel_Table)e.Node.Tag;
            vm.UserName = this.UserName;
            View_Table vt = new View_Table(vm);
            vt.JoinedTableEvent += Vt_JoinedTableEvent;
            vt.SuspendLayout();
            vt.Height = splitContainer1.Panel2.Height;
            vt.Width = splitContainer1.Panel2.Width;
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(vt);
            vt.PerformLayout();
            
        }

        private void Vt_JoinedTableEvent(string TableNo, short SeatNo, decimal ChipCounts)
        {
            if (JoinedTableEvent != null)
                JoinedTableEvent.Invoke(TableNo, SeatNo, ChipCounts);
        }

        private void splitContainer1_Panel2_SizeChanged(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            if (p.Controls.Count > 0)
            {
                if (p.Controls[0] is Views.View_Table)
                {
                    View_Table vt = (View_Table)p.Controls[0];
                    vt.Height = p.Height;
                    vt.Width = p.Width;
                }
            }
        }

        private void splitContainer1_Panel2_DoubleClick(object sender, EventArgs e)
        {
           
            
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
