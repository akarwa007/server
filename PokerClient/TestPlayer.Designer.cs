﻿namespace PokerClient
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
            this.txtReceiever = new System.Windows.Forms.RichTextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(492, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(105, 31);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtReceiever
            // 
            this.txtReceiever.Location = new System.Drawing.Point(343, 13);
            this.txtReceiever.Name = "txtReceiever";
            this.txtReceiever.Size = new System.Drawing.Size(143, 26);
            this.txtReceiever.TabIndex = 4;
            this.txtReceiever.Text = "";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(12, 23);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(64, 13);
            this.lblUsername.TabIndex = 5;
            this.lblUsername.Text = "Username : ";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(178, 21);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(62, 13);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password : ";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(72, 16);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 7;
            this.txtUsername.Text = "Alok";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(237, 13);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(3, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(599, 235);
            this.panel1.TabIndex = 15;
            // 
            // TestPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtReceiever);
            this.Controls.Add(this.btnConnect);
            this.Name = "TestPlayer";
            this.Size = new System.Drawing.Size(606, 283);
            this.Load += new System.EventHandler(this.TestPlayer_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestPlayer_Paint);
            this.Resize += new System.EventHandler(this.TestPlayer_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.RichTextBox txtReceiever;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Panel panel1;
    }
}
