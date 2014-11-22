namespace TeamSync
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.connectbutton = new System.Windows.Forms.Button();
            this.serverAddress = new System.Windows.Forms.TextBox();
            this.Debug = new System.Windows.Forms.ListBox();
            this.Room = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // connectbutton
            // 
            this.connectbutton.Location = new System.Drawing.Point(386, 9);
            this.connectbutton.Name = "connectbutton";
            this.connectbutton.Size = new System.Drawing.Size(154, 50);
            this.connectbutton.TabIndex = 0;
            this.connectbutton.Text = "Connect to server";
            this.connectbutton.UseVisualStyleBackColor = true;
            this.connectbutton.Click += new System.EventHandler(this.connectbutton_Click);
            // 
            // serverAddress
            // 
            this.serverAddress.Location = new System.Drawing.Point(53, 12);
            this.serverAddress.Name = "serverAddress";
            this.serverAddress.Size = new System.Drawing.Size(327, 20);
            this.serverAddress.TabIndex = 1;
            this.serverAddress.Text = "winepicgaming.de:5055";
            // 
            // Debug
            // 
            this.Debug.FormattingEnabled = true;
            this.Debug.Location = new System.Drawing.Point(12, 65);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(527, 186);
            this.Debug.TabIndex = 2;
            // 
            // Room
            // 
            this.Room.Location = new System.Drawing.Point(53, 39);
            this.Room.Name = "Room";
            this.Room.Size = new System.Drawing.Size(327, 20);
            this.Room.TabIndex = 4;
            this.Room.Text = "Test Room";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Server";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Room";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 261);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Room);
            this.Controls.Add(this.Debug);
            this.Controls.Add(this.serverAddress);
            this.Controls.Add(this.connectbutton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button connectbutton;
        private System.Windows.Forms.TextBox serverAddress;
        private System.Windows.Forms.ListBox Debug;
        private System.Windows.Forms.TextBox Room;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

