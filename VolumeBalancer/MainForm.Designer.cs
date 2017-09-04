namespace VolumeBalancer
{
    partial class MainForm
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
            this.trackBarBalance = new System.Windows.Forms.TrackBar();
            this.comboChatApplication = new System.Windows.Forms.ComboBox();
            this.labelChatApplication = new System.Windows.Forms.Label();
            this.labelChat = new System.Windows.Forms.Label();
            this.groupBoxBalance = new System.Windows.Forms.GroupBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonIncreaseOther = new System.Windows.Forms.Button();
            this.buttonIncreaseChat = new System.Windows.Forms.Button();
            this.labelOther = new System.Windows.Forms.Label();
            this.labelBalanceCenter = new System.Windows.Forms.Label();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBalance)).BeginInit();
            this.groupBoxBalance.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarBalance
            // 
            this.trackBarBalance.LargeChange = 1;
            this.trackBarBalance.Location = new System.Drawing.Point(63, 21);
            this.trackBarBalance.Maximum = 200;
            this.trackBarBalance.Name = "trackBarBalance";
            this.trackBarBalance.Size = new System.Drawing.Size(265, 45);
            this.trackBarBalance.TabIndex = 2;
            this.trackBarBalance.ValueChanged += new System.EventHandler(this.trackBarBalance_ValueChanged);
            // 
            // comboChatApplication
            // 
            this.comboChatApplication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboChatApplication.FormattingEnabled = true;
            this.comboChatApplication.Location = new System.Drawing.Point(99, 25);
            this.comboChatApplication.MaxDropDownItems = 12;
            this.comboChatApplication.Name = "comboChatApplication";
            this.comboChatApplication.Size = new System.Drawing.Size(295, 21);
            this.comboChatApplication.Sorted = true;
            this.comboChatApplication.TabIndex = 1;
            this.comboChatApplication.SelectedIndexChanged += new System.EventHandler(this.comboChatApplication_SelectedIndexChanged);
            // 
            // labelChatApplication
            // 
            this.labelChatApplication.AutoSize = true;
            this.labelChatApplication.Location = new System.Drawing.Point(6, 28);
            this.labelChatApplication.Name = "labelChatApplication";
            this.labelChatApplication.Size = new System.Drawing.Size(87, 13);
            this.labelChatApplication.TabIndex = 0;
            this.labelChatApplication.Text = "Chat Application:";
            // 
            // labelChat
            // 
            this.labelChat.AutoSize = true;
            this.labelChat.Location = new System.Drawing.Point(6, 25);
            this.labelChat.Name = "labelChat";
            this.labelChat.Size = new System.Drawing.Size(51, 13);
            this.labelChat.TabIndex = 0;
            this.labelChat.Text = "Chat App";
            // 
            // groupBoxBalance
            // 
            this.groupBoxBalance.Controls.Add(this.buttonReset);
            this.groupBoxBalance.Controls.Add(this.buttonIncreaseOther);
            this.groupBoxBalance.Controls.Add(this.buttonIncreaseChat);
            this.groupBoxBalance.Controls.Add(this.labelOther);
            this.groupBoxBalance.Controls.Add(this.labelChat);
            this.groupBoxBalance.Controls.Add(this.trackBarBalance);
            this.groupBoxBalance.Controls.Add(this.labelBalanceCenter);
            this.groupBoxBalance.Location = new System.Drawing.Point(13, 83);
            this.groupBoxBalance.Name = "groupBoxBalance";
            this.groupBoxBalance.Size = new System.Drawing.Size(400, 112);
            this.groupBoxBalance.TabIndex = 0;
            this.groupBoxBalance.TabStop = false;
            this.groupBoxBalance.Text = "Balance";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(138, 72);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(125, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset Balance";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonIncreaseOther
            // 
            this.buttonIncreaseOther.Location = new System.Drawing.Point(319, 72);
            this.buttonIncreaseOther.Name = "buttonIncreaseOther";
            this.buttonIncreaseOther.Size = new System.Drawing.Size(75, 23);
            this.buttonIncreaseOther.TabIndex = 5;
            this.buttonIncreaseOther.Text = ">>";
            this.buttonIncreaseOther.UseVisualStyleBackColor = true;
            this.buttonIncreaseOther.Click += new System.EventHandler(this.buttonIncreaseOther_Click);
            // 
            // buttonIncreaseChat
            // 
            this.buttonIncreaseChat.Location = new System.Drawing.Point(9, 72);
            this.buttonIncreaseChat.Name = "buttonIncreaseChat";
            this.buttonIncreaseChat.Size = new System.Drawing.Size(75, 23);
            this.buttonIncreaseChat.TabIndex = 3;
            this.buttonIncreaseChat.Text = "<<";
            this.buttonIncreaseChat.UseVisualStyleBackColor = true;
            this.buttonIncreaseChat.Click += new System.EventHandler(this.buttonIncreaseChat_Click);
            // 
            // labelOther
            // 
            this.labelOther.AutoSize = true;
            this.labelOther.Location = new System.Drawing.Point(334, 25);
            this.labelOther.Name = "labelOther";
            this.labelOther.Size = new System.Drawing.Size(60, 13);
            this.labelOther.TabIndex = 0;
            this.labelOther.Text = "Other Apps";
            // 
            // labelBalanceCenter
            // 
            this.labelBalanceCenter.AutoSize = true;
            this.labelBalanceCenter.Location = new System.Drawing.Point(192, 13);
            this.labelBalanceCenter.Name = "labelBalanceCenter";
            this.labelBalanceCenter.Size = new System.Drawing.Size(9, 13);
            this.labelBalanceCenter.TabIndex = 0;
            this.labelBalanceCenter.Text = "|";
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.labelChatApplication);
            this.groupBoxSettings.Controls.Add(this.comboChatApplication);
            this.groupBoxSettings.Location = new System.Drawing.Point(13, 13);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(400, 64);
            this.groupBoxSettings.TabIndex = 0;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Settings";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 206);
            this.Controls.Add(this.groupBoxBalance);
            this.Controls.Add(this.groupBoxSettings);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "VolumeBalancer";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBalance)).EndInit();
            this.groupBoxBalance.ResumeLayout(false);
            this.groupBoxBalance.PerformLayout();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarBalance;
        private System.Windows.Forms.ComboBox comboChatApplication;
        private System.Windows.Forms.Label labelChatApplication;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.GroupBox groupBoxBalance;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.Label labelOther;
        private System.Windows.Forms.Button buttonIncreaseOther;
        private System.Windows.Forms.Button buttonIncreaseChat;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelBalanceCenter;
    }
}

