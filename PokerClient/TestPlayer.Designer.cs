namespace PokerClient
{
    partial class TestPlayer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.txtWriter = new System.Windows.Forms.RichTextBox();
            this.txtReceiever = new System.Windows.Forms.RichTextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnSignin = new System.Windows.Forms.Button();
            this.btnSignout = new System.Windows.Forms.Button();
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnLeaveGame = new System.Windows.Forms.Button();
            this.btnAction = new System.Windows.Forms.Button();
            this.btnCasino = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(15, 14);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(213, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(15, 241);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(213, 23);
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            // 
            // txtWriter
            // 
            this.txtWriter.Location = new System.Drawing.Point(15, 90);
            this.txtWriter.Name = "txtWriter";
            this.txtWriter.Size = new System.Drawing.Size(283, 145);
            this.txtWriter.TabIndex = 3;
            this.txtWriter.Text = "";
            // 
            // txtReceiever
            // 
            this.txtReceiever.Location = new System.Drawing.Point(304, 90);
            this.txtReceiever.Name = "txtReceiever";
            this.txtReceiever.Size = new System.Drawing.Size(251, 145);
            this.txtReceiever.TabIndex = 4;
            this.txtReceiever.Text = "";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(234, 22);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(64, 13);
            this.lblUsername.TabIndex = 5;
            this.lblUsername.Text = "Username : ";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(399, 22);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(62, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password : ";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(293, 19);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 7;
            this.txtUsername.Text = "Alok";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(455, 19);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // btnSignin
            // 
            this.btnSignin.Location = new System.Drawing.Point(15, 61);
            this.btnSignin.Name = "btnSignin";
            this.btnSignin.Size = new System.Drawing.Size(75, 23);
            this.btnSignin.TabIndex = 9;
            this.btnSignin.Text = "Signin";
            this.btnSignin.UseVisualStyleBackColor = true;
            this.btnSignin.Click += new System.EventHandler(this.btnSignin_Click);
            // 
            // btnSignout
            // 
            this.btnSignout.Location = new System.Drawing.Point(96, 61);
            this.btnSignout.Name = "btnSignout";
            this.btnSignout.Size = new System.Drawing.Size(75, 23);
            this.btnSignout.TabIndex = 10;
            this.btnSignout.Text = "Signout";
            this.btnSignout.UseVisualStyleBackColor = true;
            this.btnSignout.Click += new System.EventHandler(this.btnSignout_Click);
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.Location = new System.Drawing.Point(177, 61);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(75, 23);
            this.btnJoinGame.TabIndex = 11;
            this.btnJoinGame.Text = "Join Game";
            this.btnJoinGame.UseVisualStyleBackColor = true;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            // 
            // btnLeaveGame
            // 
            this.btnLeaveGame.Location = new System.Drawing.Point(257, 61);
            this.btnLeaveGame.Name = "btnLeaveGame";
            this.btnLeaveGame.Size = new System.Drawing.Size(75, 23);
            this.btnLeaveGame.TabIndex = 12;
            this.btnLeaveGame.Text = "Leave Game";
            this.btnLeaveGame.UseVisualStyleBackColor = true;
            this.btnLeaveGame.Click += new System.EventHandler(this.btnLeaveGame_Click);
            // 
            // btnAction
            // 
            this.btnAction.Location = new System.Drawing.Point(338, 61);
            this.btnAction.Name = "btnAction";
            this.btnAction.Size = new System.Drawing.Size(75, 23);
            this.btnAction.TabIndex = 13;
            this.btnAction.Text = "Action";
            this.btnAction.UseVisualStyleBackColor = true;
            this.btnAction.Click += new System.EventHandler(this.btnAction_Click);
            // 
            // btnCasino
            // 
            this.btnCasino.Location = new System.Drawing.Point(480, 61);
            this.btnCasino.Name = "btnCasino";
            this.btnCasino.Size = new System.Drawing.Size(75, 23);
            this.btnCasino.TabIndex = 14;
            this.btnCasino.Text = "View Casino";
            this.btnCasino.UseVisualStyleBackColor = true;
            this.btnCasino.Click += new System.EventHandler(this.btnCasino_Click);
            // 
            // TestPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCasino);
            this.Controls.Add(this.btnAction);
            this.Controls.Add(this.btnLeaveGame);
            this.Controls.Add(this.btnJoinGame);
            this.Controls.Add(this.btnSignout);
            this.Controls.Add(this.btnSignin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtReceiever);
            this.Controls.Add(this.txtWriter);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnConnect);
            this.Name = "TestPlayer";
            this.Size = new System.Drawing.Size(605, 283);
            this.Load += new System.EventHandler(this.TestPlayer_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestPlayer_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.RichTextBox txtWriter;
        private System.Windows.Forms.RichTextBox txtReceiever;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnSignin;
        private System.Windows.Forms.Button btnSignout;
        private System.Windows.Forms.Button btnJoinGame;
        private System.Windows.Forms.Button btnLeaveGame;
        private System.Windows.Forms.Button btnAction;
        private System.Windows.Forms.Button btnCasino;
    }
}
