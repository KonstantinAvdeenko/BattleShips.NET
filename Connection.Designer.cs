namespace BattleShips
{
    partial class Connection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Connection));
            this.groupBox_ConnectionType = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_ServerIP = new System.Windows.Forms.TextBox();
            this.radioButton_Client = new System.Windows.Forms.RadioButton();
            this.radioButton_Server = new System.Windows.Forms.RadioButton();
            this.button_Connect = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel_Progress = new System.Windows.Forms.Panel();
            this.labelConnectionStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox_ConnectionType.SuspendLayout();
            this.panel_Progress.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_ConnectionType
            // 
            this.groupBox_ConnectionType.Controls.Add(this.label1);
            this.groupBox_ConnectionType.Controls.Add(this.textBox_ServerIP);
            this.groupBox_ConnectionType.Controls.Add(this.radioButton_Client);
            this.groupBox_ConnectionType.Controls.Add(this.radioButton_Server);
            this.groupBox_ConnectionType.Location = new System.Drawing.Point(12, 45);
            this.groupBox_ConnectionType.Name = "groupBox_ConnectionType";
            this.groupBox_ConnectionType.Size = new System.Drawing.Size(231, 124);
            this.groupBox_ConnectionType.TabIndex = 0;
            this.groupBox_ConnectionType.TabStop = false;
            this.groupBox_ConnectionType.Text = "Настройки подключения";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP адрес:";
            // 
            // textBox_ServerIP
            // 
            this.textBox_ServerIP.Enabled = false;
            this.textBox_ServerIP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.textBox_ServerIP.Location = new System.Drawing.Point(41, 91);
            this.textBox_ServerIP.Name = "textBox_ServerIP";
            this.textBox_ServerIP.Size = new System.Drawing.Size(149, 27);
            this.textBox_ServerIP.TabIndex = 0;
            this.textBox_ServerIP.Text = "77.47.161.56";
            this.textBox_ServerIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_ServerIP_KeyPress);
            // 
            // radioButton_Client
            // 
            this.radioButton_Client.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_Client.AutoSize = true;
            this.radioButton_Client.Location = new System.Drawing.Point(72, 45);
            this.radioButton_Client.Name = "radioButton_Client";
            this.radioButton_Client.Size = new System.Drawing.Size(84, 22);
            this.radioButton_Client.TabIndex = 1;
            this.radioButton_Client.TabStop = true;
            this.radioButton_Client.Text = "Клиент";
            this.radioButton_Client.UseVisualStyleBackColor = true;
            this.radioButton_Client.CheckedChanged += new System.EventHandler(this.radioButton_Client_CheckedChanged);
            // 
            // radioButton_Server
            // 
            this.radioButton_Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.radioButton_Server.AutoSize = true;
            this.radioButton_Server.Location = new System.Drawing.Point(72, 26);
            this.radioButton_Server.Name = "radioButton_Server";
            this.radioButton_Server.Size = new System.Drawing.Size(87, 22);
            this.radioButton_Server.TabIndex = 0;
            this.radioButton_Server.TabStop = true;
            this.radioButton_Server.Text = "Сервер";
            this.radioButton_Server.UseVisualStyleBackColor = true;
            this.radioButton_Server.CheckedChanged += new System.EventHandler(this.radioButton_Server_CheckedChanged);
            // 
            // button_Connect
            // 
            this.button_Connect.Enabled = false;
            this.button_Connect.Location = new System.Drawing.Point(105, 224);
            this.button_Connect.Name = "button_Connect";
            this.button_Connect.Size = new System.Drawing.Size(141, 33);
            this.button_Connect.TabIndex = 2;
            this.button_Connect.Text = "Подключиться";
            this.button_Connect.UseVisualStyleBackColor = true;
            this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Enabled = false;
            this.button_Cancel.Location = new System.Drawing.Point(12, 224);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(90, 33);
            this.button_Cancel.TabIndex = 3;
            this.button_Cancel.Text = "Отмена";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(11, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(208, 15);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // panel_Progress
            // 
            this.panel_Progress.Controls.Add(this.labelConnectionStatus);
            this.panel_Progress.Controls.Add(this.progressBar1);
            this.panel_Progress.Location = new System.Drawing.Point(12, 175);
            this.panel_Progress.Name = "panel_Progress";
            this.panel_Progress.Size = new System.Drawing.Size(231, 44);
            this.panel_Progress.TabIndex = 4;
            this.panel_Progress.Visible = false;
            // 
            // labelConnectionStatus
            // 
            this.labelConnectionStatus.AutoSize = true;
            this.labelConnectionStatus.Location = new System.Drawing.Point(45, 21);
            this.labelConnectionStatus.Name = "labelConnectionStatus";
            this.labelConnectionStatus.Size = new System.Drawing.Size(140, 18);
            this.labelConnectionStatus.TabIndex = 5;
            this.labelConnectionStatus.Text = "Подключение...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ваше имя:";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(105, 6);
            this.textBox_name.MaxLength = 10;
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(138, 27);
            this.textBox_name.TabIndex = 6;
            this.textBox_name.TextChanged += new System.EventHandler(this.textBox_name_TextChanged);
            this.textBox_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_name_KeyDown);
            this.textBox_name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_name_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Connection
            // 
            this.AcceptButton = this.button_Connect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cancel;
            this.ClientSize = new System.Drawing.Size(255, 263);
            this.Controls.Add(this.textBox_name);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel_Progress);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Connect);
            this.Controls.Add(this.groupBox_ConnectionType);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Connection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подключение";
            this.groupBox_ConnectionType.ResumeLayout(false);
            this.groupBox_ConnectionType.PerformLayout();
            this.panel_Progress.ResumeLayout(false);
            this.panel_Progress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_ConnectionType;
        private System.Windows.Forms.RadioButton radioButton_Client;
        private System.Windows.Forms.RadioButton radioButton_Server;
        private System.Windows.Forms.TextBox textBox_ServerIP;
        private System.Windows.Forms.Button button_Connect;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel_Progress;
        private System.Windows.Forms.Label labelConnectionStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Timer timer1;
    }
}