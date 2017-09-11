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
            this.labelBalanceChatApplication = new System.Windows.Forms.Label();
            this.groupBoxBalance = new System.Windows.Forms.GroupBox();
            this.buttonResetBalance = new System.Windows.Forms.Button();
            this.buttonIncreaseOtherApplicationVolume = new System.Windows.Forms.Button();
            this.buttonIncreaseChatApplicationVolume = new System.Windows.Forms.Button();
            this.labelBalanceOtherApplications = new System.Windows.Forms.Label();
            this.labelBalanceCenter = new System.Windows.Forms.Label();
            this.textBoxChatApplication = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelOr = new System.Windows.Forms.Label();
            this.groupBoxHotkeys = new System.Windows.Forms.GroupBox();
            this.labelResetAllVolumes = new System.Windows.Forms.Label();
            this.labelResetBalance = new System.Windows.Forms.Label();
            this.textBoxHotkeyResetAllVolumes = new System.Windows.Forms.TextBox();
            this.textBoxHotkeyResetBalance = new System.Windows.Forms.TextBox();
            this.textBoxHotkeyIncreaseOtherApplicationVolume = new System.Windows.Forms.TextBox();
            this.labelHotkeyIncreaseOtherApplicationsVolume = new System.Windows.Forms.Label();
            this.textBoxHotkeyIncreaseChatApplicationVolume = new System.Windows.Forms.TextBox();
            this.labelHotkeyIncreaseChatVolume = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBalance)).BeginInit();
            this.groupBoxBalance.SuspendLayout();
            this.groupBoxHotkeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarBalance
            // 
            this.trackBarBalance.LargeChange = 1;
            this.trackBarBalance.Location = new System.Drawing.Point(73, 29);
            this.trackBarBalance.Maximum = 40;
            this.trackBarBalance.Name = "trackBarBalance";
            this.trackBarBalance.Size = new System.Drawing.Size(394, 45);
            this.trackBarBalance.TabIndex = 1;
            this.trackBarBalance.Value = 20;
            this.trackBarBalance.ValueChanged += new System.EventHandler(this.trackBarBalance_ValueChanged);
            // 
            // comboAudioApplications
            // 
            this.comboAudioApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAudioApplications.FormattingEnabled = true;
            this.comboAudioApplications.Location = new System.Drawing.Point(212, 39);
            this.comboAudioApplications.MaxDropDownItems = 12;
            this.comboAudioApplications.Name = "comboAudioApplications";
            this.comboAudioApplications.Size = new System.Drawing.Size(356, 21);
            this.comboAudioApplications.TabIndex = 4;
            this.comboAudioApplications.SelectedIndexChanged += new System.EventHandler(this.comboAudioApplications_SelectedIndexChanged);
            this.comboAudioApplications.DropDownClosed += new System.EventHandler(this.comboAudioApplications_DropDownClosed);
            // 
            // labelChatApplication
            // 
            this.labelChatApplication.AutoSize = true;
            this.labelChatApplication.Location = new System.Drawing.Point(16, 16);
            this.labelChatApplication.Name = "labelChatApplication";
            this.labelChatApplication.Size = new System.Drawing.Size(87, 13);
            this.labelChatApplication.TabIndex = 0;
            this.labelChatApplication.Text = "Chat Application:";
            // 
            // labelBalanceChatApplication
            // 
            this.labelBalanceChatApplication.AutoSize = true;
            this.labelBalanceChatApplication.Location = new System.Drawing.Point(16, 31);
            this.labelBalanceChatApplication.Name = "labelBalanceChatApplication";
            this.labelBalanceChatApplication.Size = new System.Drawing.Size(59, 26);
            this.labelBalanceChatApplication.TabIndex = 0;
            this.labelBalanceChatApplication.Text = "Chat\r\nApplication";
            this.labelBalanceChatApplication.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxBalance
            // 
            this.groupBoxBalance.Controls.Add(this.buttonResetBalance);
            this.groupBoxBalance.Controls.Add(this.buttonIncreaseOtherApplicationVolume);
            this.groupBoxBalance.Controls.Add(this.buttonIncreaseChatApplicationVolume);
            this.groupBoxBalance.Controls.Add(this.labelBalanceOtherApplications);
            this.groupBoxBalance.Controls.Add(this.labelBalanceChatApplication);
            this.groupBoxBalance.Controls.Add(this.trackBarBalance);
            this.groupBoxBalance.Controls.Add(this.labelBalanceCenter);
            this.groupBoxBalance.Location = new System.Drawing.Point(19, 74);
            this.groupBoxBalance.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBoxBalance.Name = "groupBoxBalance";
            this.groupBoxBalance.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxBalance.Size = new System.Drawing.Size(549, 115);
            this.groupBoxBalance.TabIndex = 5;
            this.groupBoxBalance.TabStop = false;
            this.groupBoxBalance.Text = "Balance";
            // 
            // buttonResetBalance
            // 
            this.buttonResetBalance.Location = new System.Drawing.Point(204, 76);
            this.buttonResetBalance.Name = "buttonResetBalance";
            this.buttonResetBalance.Size = new System.Drawing.Size(150, 23);
            this.buttonResetBalance.TabIndex = 5;
            this.buttonResetBalance.Text = "Reset Balance";
            this.buttonResetBalance.UseVisualStyleBackColor = true;
            this.buttonResetBalance.Click += new System.EventHandler(this.buttonResetBalance_Click);
            // 
            // buttonIncreaseOtherApplicationVolume
            // 
            this.buttonIncreaseOtherApplicationVolume.Location = new System.Drawing.Point(383, 76);
            this.buttonIncreaseOtherApplicationVolume.Name = "buttonIncreaseOtherApplicationVolume";
            this.buttonIncreaseOtherApplicationVolume.Size = new System.Drawing.Size(150, 23);
            this.buttonIncreaseOtherApplicationVolume.TabIndex = 6;
            this.buttonIncreaseOtherApplicationVolume.Text = "Increase Other Volumes";
            this.buttonIncreaseOtherApplicationVolume.UseVisualStyleBackColor = true;
            this.buttonIncreaseOtherApplicationVolume.Click += new System.EventHandler(this.buttonIncreaseOtherApplicationVolume_Click);
            // 
            // buttonIncreaseChatApplicationVolume
            // 
            this.buttonIncreaseChatApplicationVolume.Location = new System.Drawing.Point(16, 76);
            this.buttonIncreaseChatApplicationVolume.Name = "buttonIncreaseChatApplicationVolume";
            this.buttonIncreaseChatApplicationVolume.Size = new System.Drawing.Size(150, 23);
            this.buttonIncreaseChatApplicationVolume.TabIndex = 4;
            this.buttonIncreaseChatApplicationVolume.Text = "Increase Chat Volume";
            this.buttonIncreaseChatApplicationVolume.UseVisualStyleBackColor = true;
            this.buttonIncreaseChatApplicationVolume.Click += new System.EventHandler(this.buttonIncreaseChatApplicationVolume_Click);
            // 
            // labelBalanceOtherApplications
            // 
            this.labelBalanceOtherApplications.AutoSize = true;
            this.labelBalanceOtherApplications.Location = new System.Drawing.Point(473, 31);
            this.labelBalanceOtherApplications.Name = "labelBalanceOtherApplications";
            this.labelBalanceOtherApplications.Size = new System.Drawing.Size(64, 26);
            this.labelBalanceOtherApplications.TabIndex = 3;
            this.labelBalanceOtherApplications.Text = "Other\r\nApplications";
            this.labelBalanceOtherApplications.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelBalanceCenter
            // 
            this.labelBalanceCenter.AutoSize = true;
            this.labelBalanceCenter.Location = new System.Drawing.Point(266, 21);
            this.labelBalanceCenter.Name = "labelBalanceCenter";
            this.labelBalanceCenter.Size = new System.Drawing.Size(10, 13);
            this.labelBalanceCenter.TabIndex = 2;
            this.labelBalanceCenter.Text = "-";
            // 
            // textBoxChatApplication
            // 
            this.textBoxChatApplication.Location = new System.Drawing.Point(111, 13);
            this.textBoxChatApplication.Name = "textBoxChatApplication";
            this.textBoxChatApplication.ReadOnly = true;
            this.textBoxChatApplication.Size = new System.Drawing.Size(457, 20);
            this.textBoxChatApplication.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(110, 38);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelOr
            // 
            this.labelOr.AutoSize = true;
            this.labelOr.Location = new System.Drawing.Point(191, 42);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(16, 13);
            this.labelOr.TabIndex = 3;
            this.labelOr.Text = "or";
            // 
            // groupBoxHotkeys
            // 
            this.groupBoxHotkeys.Controls.Add(this.labelResetAllVolumes);
            this.groupBoxHotkeys.Controls.Add(this.labelResetBalance);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyResetAllVolumes);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyResetBalance);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyIncreaseOtherApplicationVolume);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyIncreaseOtherApplicationsVolume);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyIncreaseChatApplicationVolume);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyIncreaseChatVolume);
            this.groupBoxHotkeys.Location = new System.Drawing.Point(19, 202);
            this.groupBoxHotkeys.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBoxHotkeys.Name = "groupBoxHotkeys";
            this.groupBoxHotkeys.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxHotkeys.Size = new System.Drawing.Size(549, 143);
            this.groupBoxHotkeys.TabIndex = 6;
            this.groupBoxHotkeys.TabStop = false;
            this.groupBoxHotkeys.Text = "Hotkeys";
            // 
            // labelResetAllVolumes
            // 
            this.labelResetAllVolumes.AutoSize = true;
            this.labelResetAllVolumes.Location = new System.Drawing.Point(16, 110);
            this.labelResetAllVolumes.Name = "labelResetAllVolumes";
            this.labelResetAllVolumes.Size = new System.Drawing.Size(95, 13);
            this.labelResetAllVolumes.TabIndex = 6;
            this.labelResetAllVolumes.Text = "Reset All Volumes:";
            // 
            // labelResetBalance
            // 
            this.labelResetBalance.AutoSize = true;
            this.labelResetBalance.Location = new System.Drawing.Point(16, 84);
            this.labelResetBalance.Name = "labelResetBalance";
            this.labelResetBalance.Size = new System.Drawing.Size(80, 13);
            this.labelResetBalance.TabIndex = 4;
            this.labelResetBalance.Text = "Reset Balance:";
            // 
            // textBoxHotkeyResetAllVolumes
            // 
            this.textBoxHotkeyResetAllVolumes.Location = new System.Drawing.Point(216, 107);
            this.textBoxHotkeyResetAllVolumes.Name = "textBoxHotkeyResetAllVolumes";
            this.textBoxHotkeyResetAllVolumes.Size = new System.Drawing.Size(317, 20);
            this.textBoxHotkeyResetAllVolumes.TabIndex = 7;
            this.textBoxHotkeyResetAllVolumes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyResetAllVolumes_KeyDown);
            // 
            // textBoxHotkeyResetBalance
            // 
            this.textBoxHotkeyResetBalance.Location = new System.Drawing.Point(216, 81);
            this.textBoxHotkeyResetBalance.Name = "textBoxHotkeyResetBalance";
            this.textBoxHotkeyResetBalance.Size = new System.Drawing.Size(317, 20);
            this.textBoxHotkeyResetBalance.TabIndex = 5;
            this.textBoxHotkeyResetBalance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyResetBalance_KeyDown);
            // 
            // textBoxHotkeyIncreaseOtherApplicationVolume
            // 
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Location = new System.Drawing.Point(216, 55);
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Name = "textBoxHotkeyIncreaseOtherApplicationVolume";
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Size = new System.Drawing.Size(317, 20);
            this.textBoxHotkeyIncreaseOtherApplicationVolume.TabIndex = 3;
            this.textBoxHotkeyIncreaseOtherApplicationVolume.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyIncreaseOtherApplicationsVolume_KeyDown);
            // 
            // labelHotkeyIncreaseOtherApplicationsVolume
            // 
            this.labelHotkeyIncreaseOtherApplicationsVolume.AutoSize = true;
            this.labelHotkeyIncreaseOtherApplicationsVolume.Location = new System.Drawing.Point(16, 58);
            this.labelHotkeyIncreaseOtherApplicationsVolume.Name = "labelHotkeyIncreaseOtherApplicationsVolume";
            this.labelHotkeyIncreaseOtherApplicationsVolume.Size = new System.Drawing.Size(178, 13);
            this.labelHotkeyIncreaseOtherApplicationsVolume.TabIndex = 2;
            this.labelHotkeyIncreaseOtherApplicationsVolume.Text = "Increase Other Applications Volume:";
            // 
            // textBoxHotkeyIncreaseChatApplicationVolume
            // 
            this.textBoxHotkeyIncreaseChatApplicationVolume.Location = new System.Drawing.Point(216, 29);
            this.textBoxHotkeyIncreaseChatApplicationVolume.Name = "textBoxHotkeyIncreaseChatApplicationVolume";
            this.textBoxHotkeyIncreaseChatApplicationVolume.Size = new System.Drawing.Size(317, 20);
            this.textBoxHotkeyIncreaseChatApplicationVolume.TabIndex = 1;
            this.textBoxHotkeyIncreaseChatApplicationVolume.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxShortcutIncreaseChatVolume_KeyDown);
            // 
            // labelHotkeyIncreaseChatVolume
            // 
            this.labelHotkeyIncreaseChatVolume.AutoSize = true;
            this.labelHotkeyIncreaseChatVolume.Location = new System.Drawing.Point(16, 32);
            this.labelHotkeyIncreaseChatVolume.Name = "labelHotkeyIncreaseChatVolume";
            this.labelHotkeyIncreaseChatVolume.Size = new System.Drawing.Size(114, 13);
            this.labelHotkeyIncreaseChatVolume.TabIndex = 0;
            this.labelHotkeyIncreaseChatVolume.Text = "Increase Chat Volume:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.groupBoxHotkeys);
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
            this.Padding = new System.Windows.Forms.Padding(13);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VolumeBalancer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBalance)).EndInit();
            this.groupBoxBalance.ResumeLayout(false);
            this.groupBoxBalance.PerformLayout();
            this.groupBoxHotkeys.ResumeLayout(false);
            this.groupBoxHotkeys.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarBalance;
        private System.Windows.Forms.ComboBox comboAudioApplications;
        private System.Windows.Forms.Label labelChatApplication;
        private System.Windows.Forms.Label labelBalanceChatApplication;
        private System.Windows.Forms.GroupBox groupBoxBalance;
        private System.Windows.Forms.Label labelBalanceOtherApplications;
        private System.Windows.Forms.Button buttonIncreaseOtherApplicationVolume;
        private System.Windows.Forms.Button buttonIncreaseChatApplicationVolume;
        private System.Windows.Forms.Button buttonResetBalance;
        private System.Windows.Forms.Label labelBalanceCenter;
        private System.Windows.Forms.TextBox textBoxChatApplication;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelOr;
        private System.Windows.Forms.GroupBox groupBoxHotkeys;
        private System.Windows.Forms.Label labelHotkeyIncreaseChatVolume;
        private System.Windows.Forms.TextBox textBoxHotkeyIncreaseChatApplicationVolume;
        private System.Windows.Forms.TextBox textBoxHotkeyIncreaseOtherApplicationVolume;
        private System.Windows.Forms.Label labelHotkeyIncreaseOtherApplicationsVolume;
        private System.Windows.Forms.TextBox textBoxHotkeyResetAllVolumes;
        private System.Windows.Forms.TextBox textBoxHotkeyResetBalance;
        private System.Windows.Forms.Label labelResetBalance;
        private System.Windows.Forms.Label labelResetAllVolumes;
    }
}

