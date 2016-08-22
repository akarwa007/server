using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Poker.Client.Support;
using Poker.Client.Support.Views;
using Poker.Shared;

namespace PokerClient
{
    public partial class TestPlayer : UserControl
    {
        static int RefCount = 0;
        bool _Connected = false;
        bool _Authenticated = false;
        TcpClient _client = null;

        Poker.Shared.Message _message;

        ShellForm _shellform;
        ViewModel_Casino _casinoModel;
        View_Casino _casinoView;
        PokerUserC _pokeruser;

        PokerClientContext _context;
        public TestPlayer()
        {
            //init();
            InitializeComponent();
            txtUsername.Text = txtUsername.Text + (++TestPlayer.RefCount).ToString();
        }
        private void init(string username, string password)
        {
            _context = new PokerClientContext();
          
            _shellform = new ShellForm();
            _casinoModel = new ViewModel_Casino(username);
            
            _casinoView = new View_Casino();
            _casinoView.UserName = username;
            _casinoView.UpdateModel(_casinoModel);
            _shellform.Dock = DockStyle.Fill;
            _shellform.AutoSize = false;
            _casinoView.Dock = DockStyle.Fill;
            _casinoView.AutoSize = false;
            _shellform.Controls.Add(_casinoView);

            _pokeruser = new PokerUserC(_client, null, username, password);
            _context.PokerUser = _pokeruser;
            _context.MessageFactory = new MessageFactory(_pokeruser);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.CasinoUpdate);
            _context.MessageFactory.RegisterCallback(this.SetReceivedMessage, MessageType.GeneralPurpose);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.TableUpdate);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage,MessageType.PlayerAction);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.PlayerActionRequestBet);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.TableSendHoleCards);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.TableSendFlop);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.TableSendTurn);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.TableSendRiver);
            _context.MessageFactory.RegisterCallback(_casinoView.ProcessMessage, MessageType.TableSendWinner);
            _pokeruser.setContext(_context);
            _casinoView.JoinedTableEvent += _context.MessageFactory.SendTableJoinMessage;
            
        }

        private bool Connected
        {
            get
            {
                return _Connected;
            }
            set
            {
                _Connected = value;
                if (_Connected)
                    SetButtonText("Disconnect");
                else
                    SetButtonText("Connect");
            }
        }
        public void SetButtonText(string value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string>(SetButtonText), new object[] { value });
                return;
            }       
            this.btnConnect.Text = value;
        }
        public void SetReceivedMessage(Poker.Shared.Message value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<Poker.Shared.Message>(SetReceivedMessage), new object[] { value });
                return;
            }
            this.txtReceiever.AppendText(value.MessageType.ToString() + "-- Content Size -> " + value.Content.Length.ToString());
            this.txtReceiever.AppendText(Environment.NewLine);
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
           {
               if ((_client == null) || (_client.Connected == false))
               {
                   _client = new TcpClient("localhost", 8113);

                   if (_client.Connected)
                   {
                       this.Connected = true;
                       string content = txtUsername.Text + ":" + txtPassword.Text;
                       //_firstMessage = new Poker.Shared.Message(content, MessageType.PlayerSigningIn);
                       //_queue_outgoing.Enqueue(_firstMessage);
                       init(txtUsername.Text, txtPassword.Text);
                    }
               }
               else // you want to disconnect
                {
                   _client.Close();
                   if (_client.Connected == false)
                   {
                       this.Connected = false;
                     // do some disconnect code
                   }
               }
           });
            
        }
        
        public void AppendTextBox(RichTextBox txtbox, Poker.Shared.Message value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<RichTextBox, Poker.Shared.Message>(AppendTextBox), new object[] { txtbox, value });
                return;
            }
            txtbox.Text += value.Content;
        }
     
        private void TestPlayer_Load(object sender, EventArgs e)
        {
         
           
        }

        private void btnSignin_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerSigningIn:" + this.txtUsername.Text;
            string content = txtUsername.Text + ":" + txtPassword.Text;
            _message = new Poker.Shared.Message(content, MessageType.PlayerSigningIn);
        }

        private void btnSignout_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerSigningOut:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerSigningOut);
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerJoiningGame:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerJoiningGame);
        }

        private void btnLeaveGame_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerLeavingGame:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerLeavingGame);
        }
        

        private void btnAction_Click(object sender, EventArgs e)
        {
            this.txtWriter.Text = "PlayerAction:" + this.txtUsername.Text;
            _message = new Poker.Shared.Message(this.txtWriter.Text, MessageType.PlayerAction);
            _message.Callback += new AsyncCallback(ActionCallBack);
        }
        private void ActionCallBack(IAsyncResult result)
        {
            Poker.Shared.Message message = (Poker.Shared.Message)result;
          //  this.txtReceiever.AppendText(message.Content + Environment.NewLine);
            AppendTextBox(this.txtReceiever, message);
        }

        private void btnCasino_Click(object sender, EventArgs e)
        {
            if (_shellform != null)
            {
                _shellform.Text = "Casino View for " + _casinoView.UserName;
                _shellform.Show();
            }
        }

        private void TestPlayer_Paint(object sender, PaintEventArgs e)
        {
           
        }
    }
}
