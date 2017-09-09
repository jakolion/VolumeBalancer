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
            this.comboAudioApplications = new System.Windows.Forms.ComboBox();
            this.labelChatApplication = new System.Windows.Forms.Label();
            this.labelChat = new System.Windows.Forms.Label();
            this.groupBoxBalance = new System.Windows.Forms.GroupBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonIncreaseOther = new System.Windows.Forms.Button();
            this.buttonIncreaseChat = new System.Windows.Forms.Button();
            this.labelOther = new System.Windows.Forms.Label();
            this.labelBalanceCenter = new System.Windows.Forms.Label();
            this.textBoxChatApplication = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelOr = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBalance)).BeginInit();
            this.groupBoxBalance.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarBalance
            // 
            this.trackBarBalance.LargeChange = 1;
            this.trackBarBalance.Location = new System.Drawing.Point(70, 26);
            this.trackBarBalance.Maximum = 200;
            this.trackBarBalance.Name = "trackBarBalance";
            this.trackBarBalance.Size = new System.Drawing.Size(358, 45);
            this.trackBarBalance.TabIndex = 2;
            this.trackBarBalance.Value = 100;
            this.trackBarBalance.ValueChanged += new System.EventHandler(this.trackBarBalance_ValueChanged);
            // 
            // comboAudioApplications
            // 
            this.comboAudioApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAudioApplications.FormattingEnabled = true;
            this.comboAudioApplications.Location = new System.Drawing.Point(214, 47);
            this.comboAudioApplications.MaxDropDownItems = 12;
            this.comboAudioApplications.Name = "comboAudioApplications";
            this.comboAudioApplications.Size = new System.Drawing.Size(306, 21);
            this.comboAudioApplications.TabIndex = 1;
            this.comboAudioApplications.SelectedIndexChanged += new System.EventHandler(this.comboAudioApplications_SelectedIndexChanged);
            // 
            // labelChatApplication
            // 
            this.labelChatApplication.AutoSize = true;
            this.labelChatApplication.Location = new System.Drawing.Point(18, 21);
            this.labelChatApplication.Name = "labelChatApplication";
            this.labelChatApplication.Size = new System.Drawing.Size(87, 13);
            this.labelChatApplication.TabIndex = 0;
            this.labelChatApplication.Text = "Chat Application:";
            // 
            // labelChat
            // 
            this.labelChat.AutoSize = true;
            this.labelChat.Location = new System.Drawing.Point(13, 28);
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
            this.groupBoxBalance.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxBalance.Size = new System.Drawing.Size(507, 115);
            this.groupBoxBalance.TabIndex = 0;
            this.groupBoxBalance.TabStop = false;
            this.groupBoxBalance.Text = "Balance";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(178, 77);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(150, 23);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset Balance";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonIncreaseOther
            // 
            this.buttonIncreaseOther.Location = new System.Drawing.Point(394, 77);
            this.buttonIncreaseOther.Name = "buttonIncreaseOther";
            this.buttonIncreaseOther.Size = new System.Drawing.Size(100, 23);
            this.buttonIncreaseOther.TabIndex = 5;
            this.buttonIncreaseOther.Text = ">>";
            this.buttonIncreaseOther.UseVisualStyleBackColor = true;
            this.buttonIncreaseOther.Click += new System.EventHandler(this.buttonIncreaseOther_Click);
            // 
            // buttonIncreaseChat
            // 
            this.buttonIncreaseChat.Location = new System.Drawing.Point(16, 77);
            this.buttonIncreaseChat.Name = "buttonIncreaseChat";
            this.buttonIncreaseChat.Size = new System.Drawing.Size(100, 23);
            this.buttonIncreaseChat.TabIndex = 3;
            this.buttonIncreaseChat.Text = "<<";
            this.buttonIncreaseChat.UseVisualStyleBackColor = true;
            this.buttonIncreaseChat.Click += new System.EventHandler(this.buttonIncreaseChat_Click);
            // 
            // labelOther
            // 
            this.labelOther.AutoSize = true;
            this.labelOther.Location = new System.Drawing.Point(434, 28);
            this.labelOther.Name = "labelOther";
            this.labelOther.Size = new System.Drawing.Size(60, 13);
            this.labelOther.TabIndex = 0;
            this.labelOther.Text = "Other Apps";
            // 
            // labelBalanceCenter
            // 
            this.labelBalanceCenter.AutoSize = true;
            this.labelBalanceCenter.Location = new System.Drawing.Point(245, 15);
            this.labelBalanceCenter.Name = "labelBalanceCenter";
            this.labelBalanceCenter.Size = new System.Drawing.Size(10, 13);
            this.labelBalanceCenter.TabIndex = 0;
            this.labelBalanceCenter.Text = "-";
            // 
            // textBoxChatApplication
            // 
            this.textBoxChatApplication.Location = new System.Drawing.Point(111, 18);
            this.textBoxChatApplication.Name = "textBoxChatApplication";
            this.textBoxChatApplication.ReadOnly = true;
            this.textBoxChatApplication.Size = new System.Drawing.Size(409, 20);
            this.textBoxChatApplication.TabIndex = 2;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(111, 45);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 3;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelOr
            // 
            this.labelOr.AutoSize = true;
            this.labelOr.Location = new System.Drawing.Point(192, 50);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(16, 13);
            this.labelOr.TabIndex = 6;
            this.labelOr.Text = "or";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 211);
            this.Controls.Add(this.labelOr);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxChatApplication);
            this.Controls.Add(this.labelChatApplication);
            this.Controls.Add(this.groupBoxBalance);
            this.Controls.Add(this.comboAudioApplications);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VolumeBalancer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBalance)).EndInit();
            this.groupBoxBalance.ResumeLayout(false);
            this.groupBoxBalance.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarBalance;
        private System.Windows.Forms.ComboBox comboAudioApplications;
        private System.Windows.Forms.Label labelChatApplication;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.GroupBox groupBoxBalance;
        private System.Windows.Forms.Label labelOther;
        private System.Windows.Forms.Button buttonIncreaseOther;
        private System.Windows.Forms.Button buttonIncreaseChat;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelBalanceCenter;
        private System.Windows.Forms.TextBox textBoxChatApplication;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelOr;
    }
}

