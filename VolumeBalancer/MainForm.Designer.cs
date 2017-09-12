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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trackBarBalance = new System.Windows.Forms.TrackBar();
            this.comboAudioApplications = new System.Windows.Forms.ComboBox();
            this.labelMainFocusApplication = new System.Windows.Forms.Label();
            this.labelBalanceFocusApplication = new System.Windows.Forms.Label();
            this.groupBoxBalance = new System.Windows.Forms.GroupBox();
            this.buttonResetBalance = new System.Windows.Forms.Button();
            this.buttonIncreaseOtherApplicationVolume = new System.Windows.Forms.Button();
            this.buttonIncreaseFocusApplicationVolume = new System.Windows.Forms.Button();
            this.labelBalanceOtherApplications = new System.Windows.Forms.Label();
            this.labelBalanceCenter = new System.Windows.Forms.Label();
            this.textBoxMainFocusApplication = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelOr = new System.Windows.Forms.Label();
            this.groupBoxHotkeys = new System.Windows.Forms.GroupBox();
            this.labelActivateMainFocusApplication = new System.Windows.Forms.Label();
            this.textBoxHotkeyActivateMainFocusApplication = new System.Windows.Forms.TextBox();
            this.labelActivateTemporaryFocusApplication = new System.Windows.Forms.Label();
            this.textBoxHotkeyActivateTemporaryFocusApplication = new System.Windows.Forms.TextBox();
            this.labelResetAllVolumes = new System.Windows.Forms.Label();
            this.labelResetBalance = new System.Windows.Forms.Label();
            this.textBoxHotkeyResetAllVolumes = new System.Windows.Forms.TextBox();
            this.textBoxHotkeyResetBalance = new System.Windows.Forms.TextBox();
            this.textBoxHotkeyIncreaseOtherApplicationVolume = new System.Windows.Forms.TextBox();
            this.labelHotkeyIncreaseOtherApplicationsVolume = new System.Windows.Forms.Label();
            this.textBoxHotkeyIncreaseFocusApplicationVolume = new System.Windows.Forms.TextBox();
            this.labelHotkeyIncreaseFocusApplicationVolume = new System.Windows.Forms.Label();
            this.labelTemporaryFocusApplication = new System.Windows.Forms.Label();
            this.textBoxTemporaryFocusApplication = new System.Windows.Forms.TextBox();
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
            this.trackBarBalance.Size = new System.Drawing.Size(440, 45);
            this.trackBarBalance.TabIndex = 1;
            this.trackBarBalance.Value = 20;
            this.trackBarBalance.ValueChanged += new System.EventHandler(this.trackBarBalance_ValueChanged);
            // 
            // comboAudioApplications
            // 
            this.comboAudioApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAudioApplications.FormattingEnabled = true;
            this.comboAudioApplications.Location = new System.Drawing.Point(244, 39);
            this.comboAudioApplications.MaxDropDownItems = 12;
            this.comboAudioApplications.Name = "comboAudioApplications";
            this.comboAudioApplications.Size = new System.Drawing.Size(374, 21);
            this.comboAudioApplications.TabIndex = 4;
            this.comboAudioApplications.DropDown += new System.EventHandler(this.comboAudioApplications_DropDown);
            this.comboAudioApplications.SelectedIndexChanged += new System.EventHandler(this.comboAudioApplications_SelectedIndexChanged);
            // 
            // labelMainFocusApplication
            // 
            this.labelMainFocusApplication.AutoSize = true;
            this.labelMainFocusApplication.Location = new System.Drawing.Point(16, 16);
            this.labelMainFocusApplication.Name = "labelMainFocusApplication";
            this.labelMainFocusApplication.Size = new System.Drawing.Size(120, 13);
            this.labelMainFocusApplication.TabIndex = 0;
            this.labelMainFocusApplication.Text = "Main Focus Application:";
            // 
            // labelBalanceFocusApplication
            // 
            this.labelBalanceFocusApplication.AutoSize = true;
            this.labelBalanceFocusApplication.Location = new System.Drawing.Point(16, 31);
            this.labelBalanceFocusApplication.Name = "labelBalanceFocusApplication";
            this.labelBalanceFocusApplication.Size = new System.Drawing.Size(59, 26);
            this.labelBalanceFocusApplication.TabIndex = 0;
            this.labelBalanceFocusApplication.Text = "Focus\r\nApplication";
            this.labelBalanceFocusApplication.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBoxBalance
            // 
            this.groupBoxBalance.Controls.Add(this.buttonResetBalance);
            this.groupBoxBalance.Controls.Add(this.buttonIncreaseOtherApplicationVolume);
            this.groupBoxBalance.Controls.Add(this.buttonIncreaseFocusApplicationVolume);
            this.groupBoxBalance.Controls.Add(this.labelBalanceOtherApplications);
            this.groupBoxBalance.Controls.Add(this.labelBalanceFocusApplication);
            this.groupBoxBalance.Controls.Add(this.trackBarBalance);
            this.groupBoxBalance.Controls.Add(this.labelBalanceCenter);
            this.groupBoxBalance.Location = new System.Drawing.Point(19, 109);
            this.groupBoxBalance.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBoxBalance.Name = "groupBoxBalance";
            this.groupBoxBalance.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxBalance.Size = new System.Drawing.Size(599, 115);
            this.groupBoxBalance.TabIndex = 7;
            this.groupBoxBalance.TabStop = false;
            this.groupBoxBalance.Text = "Balance";
            // 
            // buttonResetBalance
            // 
            this.buttonResetBalance.Location = new System.Drawing.Point(225, 76);
            this.buttonResetBalance.Name = "buttonResetBalance";
            this.buttonResetBalance.Size = new System.Drawing.Size(150, 23);
            this.buttonResetBalance.TabIndex = 5;
            this.buttonResetBalance.Text = "Reset Balance";
            this.buttonResetBalance.UseVisualStyleBackColor = true;
            this.buttonResetBalance.Click += new System.EventHandler(this.buttonResetBalance_Click);
            // 
            // buttonIncreaseOtherApplicationVolume
            // 
            this.buttonIncreaseOtherApplicationVolume.Location = new System.Drawing.Point(398, 76);
            this.buttonIncreaseOtherApplicationVolume.Name = "buttonIncreaseOtherApplicationVolume";
            this.buttonIncreaseOtherApplicationVolume.Size = new System.Drawing.Size(185, 23);
            this.buttonIncreaseOtherApplicationVolume.TabIndex = 6;
            this.buttonIncreaseOtherApplicationVolume.Text = "Increase Other Applications Volume";
            this.buttonIncreaseOtherApplicationVolume.UseVisualStyleBackColor = true;
            this.buttonIncreaseOtherApplicationVolume.Click += new System.EventHandler(this.buttonIncreaseOtherApplicationVolume_Click);
            // 
            // buttonIncreaseFocusApplicationVolume
            // 
            this.buttonIncreaseFocusApplicationVolume.Location = new System.Drawing.Point(16, 76);
            this.buttonIncreaseFocusApplicationVolume.Name = "buttonIncreaseFocusApplicationVolume";
            this.buttonIncreaseFocusApplicationVolume.Size = new System.Drawing.Size(185, 23);
            this.buttonIncreaseFocusApplicationVolume.TabIndex = 4;
            this.buttonIncreaseFocusApplicationVolume.Text = "Increase Focus Application Volume";
            this.buttonIncreaseFocusApplicationVolume.UseVisualStyleBackColor = true;
            this.buttonIncreaseFocusApplicationVolume.Click += new System.EventHandler(this.buttonIncreaseFocusApplicationVolume_Click);
            // 
            // labelBalanceOtherApplications
            // 
            this.labelBalanceOtherApplications.AutoSize = true;
            this.labelBalanceOtherApplications.Location = new System.Drawing.Point(519, 31);
            this.labelBalanceOtherApplications.Name = "labelBalanceOtherApplications";
            this.labelBalanceOtherApplications.Size = new System.Drawing.Size(64, 26);
            this.labelBalanceOtherApplications.TabIndex = 3;
            this.labelBalanceOtherApplications.Text = "Other\r\nApplications";
            this.labelBalanceOtherApplications.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelBalanceCenter
            // 
            this.labelBalanceCenter.AutoSize = true;
            this.labelBalanceCenter.Location = new System.Drawing.Point(289, 20);
            this.labelBalanceCenter.Name = "labelBalanceCenter";
            this.labelBalanceCenter.Size = new System.Drawing.Size(10, 13);
            this.labelBalanceCenter.TabIndex = 2;
            this.labelBalanceCenter.Text = "-";
            // 
            // textBoxMainFocusApplication
            // 
            this.textBoxMainFocusApplication.Location = new System.Drawing.Point(142, 13);
            this.textBoxMainFocusApplication.Name = "textBoxMainFocusApplication";
            this.textBoxMainFocusApplication.ReadOnly = true;
            this.textBoxMainFocusApplication.Size = new System.Drawing.Size(476, 20);
            this.textBoxMainFocusApplication.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(141, 38);
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
            this.labelOr.Location = new System.Drawing.Point(222, 43);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(16, 13);
            this.labelOr.TabIndex = 3;
            this.labelOr.Text = "or";
            // 
            // groupBoxHotkeys
            // 
            this.groupBoxHotkeys.Controls.Add(this.labelActivateMainFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyActivateMainFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.labelActivateTemporaryFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyActivateTemporaryFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.labelResetAllVolumes);
            this.groupBoxHotkeys.Controls.Add(this.labelResetBalance);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyResetAllVolumes);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyResetBalance);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyIncreaseOtherApplicationVolume);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyIncreaseOtherApplicationsVolume);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyIncreaseFocusApplicationVolume);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyIncreaseFocusApplicationVolume);
            this.groupBoxHotkeys.Location = new System.Drawing.Point(19, 237);
            this.groupBoxHotkeys.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBoxHotkeys.Name = "groupBoxHotkeys";
            this.groupBoxHotkeys.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxHotkeys.Size = new System.Drawing.Size(599, 196);
            this.groupBoxHotkeys.TabIndex = 8;
            this.groupBoxHotkeys.TabStop = false;
            this.groupBoxHotkeys.Text = "Hotkeys";
            // 
            // labelActivateMainFocusApplication
            // 
            this.labelActivateMainFocusApplication.AutoSize = true;
            this.labelActivateMainFocusApplication.Location = new System.Drawing.Point(16, 110);
            this.labelActivateMainFocusApplication.Name = "labelActivateMainFocusApplication";
            this.labelActivateMainFocusApplication.Size = new System.Drawing.Size(162, 13);
            this.labelActivateMainFocusApplication.TabIndex = 6;
            this.labelActivateMainFocusApplication.Text = "Activate Main Focus Application:";
            // 
            // textBoxHotkeyActivateMainFocusApplication
            // 
            this.textBoxHotkeyActivateMainFocusApplication.Location = new System.Drawing.Point(216, 107);
            this.textBoxHotkeyActivateMainFocusApplication.Name = "textBoxHotkeyActivateMainFocusApplication";
            this.textBoxHotkeyActivateMainFocusApplication.ReadOnly = true;
            this.textBoxHotkeyActivateMainFocusApplication.Size = new System.Drawing.Size(213, 20);
            this.textBoxHotkeyActivateMainFocusApplication.TabIndex = 7;
            this.textBoxHotkeyActivateMainFocusApplication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyActivateMainFocusApplication_KeyDown);
            // 
            // labelActivateTemporaryFocusApplication
            // 
            this.labelActivateTemporaryFocusApplication.AutoSize = true;
            this.labelActivateTemporaryFocusApplication.Location = new System.Drawing.Point(16, 136);
            this.labelActivateTemporaryFocusApplication.Name = "labelActivateTemporaryFocusApplication";
            this.labelActivateTemporaryFocusApplication.Size = new System.Drawing.Size(166, 13);
            this.labelActivateTemporaryFocusApplication.TabIndex = 8;
            this.labelActivateTemporaryFocusApplication.Text = "Set Temporary Focus Application:";
            // 
            // textBoxHotkeyActivateTemporaryFocusApplication
            // 
            this.textBoxHotkeyActivateTemporaryFocusApplication.Location = new System.Drawing.Point(216, 133);
            this.textBoxHotkeyActivateTemporaryFocusApplication.Name = "textBoxHotkeyActivateTemporaryFocusApplication";
            this.textBoxHotkeyActivateTemporaryFocusApplication.ReadOnly = true;
            this.textBoxHotkeyActivateTemporaryFocusApplication.Size = new System.Drawing.Size(213, 20);
            this.textBoxHotkeyActivateTemporaryFocusApplication.TabIndex = 9;
            this.textBoxHotkeyActivateTemporaryFocusApplication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyActivateTemporaryFocusApplication_KeyDown);
            // 
            // labelResetAllVolumes
            // 
            this.labelResetAllVolumes.AutoSize = true;
            this.labelResetAllVolumes.Location = new System.Drawing.Point(16, 162);
            this.labelResetAllVolumes.Name = "labelResetAllVolumes";
            this.labelResetAllVolumes.Size = new System.Drawing.Size(95, 13);
            this.labelResetAllVolumes.TabIndex = 10;
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
            this.textBoxHotkeyResetAllVolumes.Location = new System.Drawing.Point(216, 159);
            this.textBoxHotkeyResetAllVolumes.Name = "textBoxHotkeyResetAllVolumes";
            this.textBoxHotkeyResetAllVolumes.ReadOnly = true;
            this.textBoxHotkeyResetAllVolumes.Size = new System.Drawing.Size(213, 20);
            this.textBoxHotkeyResetAllVolumes.TabIndex = 11;
            this.textBoxHotkeyResetAllVolumes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyResetAllVolumes_KeyDown);
            // 
            // textBoxHotkeyResetBalance
            // 
            this.textBoxHotkeyResetBalance.Location = new System.Drawing.Point(216, 81);
            this.textBoxHotkeyResetBalance.Name = "textBoxHotkeyResetBalance";
            this.textBoxHotkeyResetBalance.ReadOnly = true;
            this.textBoxHotkeyResetBalance.Size = new System.Drawing.Size(213, 20);
            this.textBoxHotkeyResetBalance.TabIndex = 5;
            this.textBoxHotkeyResetBalance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyResetBalance_KeyDown);
            // 
            // textBoxHotkeyIncreaseOtherApplicationVolume
            // 
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Location = new System.Drawing.Point(216, 55);
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Name = "textBoxHotkeyIncreaseOtherApplicationVolume";
            this.textBoxHotkeyIncreaseOtherApplicationVolume.ReadOnly = true;
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Size = new System.Drawing.Size(213, 20);
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
            // textBoxHotkeyIncreaseFocusApplicationVolume
            // 
            this.textBoxHotkeyIncreaseFocusApplicationVolume.Location = new System.Drawing.Point(216, 29);
            this.textBoxHotkeyIncreaseFocusApplicationVolume.Name = "textBoxHotkeyIncreaseFocusApplicationVolume";
            this.textBoxHotkeyIncreaseFocusApplicationVolume.ReadOnly = true;
            this.textBoxHotkeyIncreaseFocusApplicationVolume.Size = new System.Drawing.Size(213, 20);
            this.textBoxHotkeyIncreaseFocusApplicationVolume.TabIndex = 1;
            this.textBoxHotkeyIncreaseFocusApplicationVolume.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxShortcutIncreaseFocusApplicationVolume_KeyDown);
            // 
            // labelHotkeyIncreaseFocusApplicationVolume
            // 
            this.labelHotkeyIncreaseFocusApplicationVolume.AutoSize = true;
            this.labelHotkeyIncreaseFocusApplicationVolume.Location = new System.Drawing.Point(16, 32);
            this.labelHotkeyIncreaseFocusApplicationVolume.Name = "labelHotkeyIncreaseFocusApplicationVolume";
            this.labelHotkeyIncreaseFocusApplicationVolume.Size = new System.Drawing.Size(176, 13);
            this.labelHotkeyIncreaseFocusApplicationVolume.TabIndex = 0;
            this.labelHotkeyIncreaseFocusApplicationVolume.Text = "Increase Focus Application Volume:";
            // 
            // labelTemporaryFocusApplication
            // 
            this.labelTemporaryFocusApplication.AutoSize = true;
            this.labelTemporaryFocusApplication.Location = new System.Drawing.Point(16, 79);
            this.labelTemporaryFocusApplication.Name = "labelTemporaryFocusApplication";
            this.labelTemporaryFocusApplication.Size = new System.Drawing.Size(92, 13);
            this.labelTemporaryFocusApplication.TabIndex = 5;
            this.labelTemporaryFocusApplication.Text = "Temporary Focus:";
            // 
            // textBoxTemporaryFocusApplication
            // 
            this.textBoxTemporaryFocusApplication.Location = new System.Drawing.Point(142, 76);
            this.textBoxTemporaryFocusApplication.Name = "textBoxTemporaryFocusApplication";
            this.textBoxTemporaryFocusApplication.ReadOnly = true;
            this.textBoxTemporaryFocusApplication.Size = new System.Drawing.Size(476, 20);
            this.textBoxTemporaryFocusApplication.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 448);
            this.Controls.Add(this.textBoxTemporaryFocusApplication);
            this.Controls.Add(this.labelTemporaryFocusApplication);
            this.Controls.Add(this.groupBoxHotkeys);
            this.Controls.Add(this.labelOr);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxMainFocusApplication);
            this.Controls.Add(this.labelMainFocusApplication);
            this.Controls.Add(this.groupBoxBalance);
            this.Controls.Add(this.comboAudioApplications);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private System.Windows.Forms.Label labelMainFocusApplication;
        private System.Windows.Forms.Label labelBalanceFocusApplication;
        private System.Windows.Forms.GroupBox groupBoxBalance;
        private System.Windows.Forms.Label labelBalanceOtherApplications;
        private System.Windows.Forms.Button buttonIncreaseOtherApplicationVolume;
        private System.Windows.Forms.Button buttonIncreaseFocusApplicationVolume;
        private System.Windows.Forms.Button buttonResetBalance;
        private System.Windows.Forms.Label labelBalanceCenter;
        private System.Windows.Forms.TextBox textBoxMainFocusApplication;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelOr;
        private System.Windows.Forms.GroupBox groupBoxHotkeys;
        private System.Windows.Forms.Label labelHotkeyIncreaseFocusApplicationVolume;
        private System.Windows.Forms.TextBox textBoxHotkeyIncreaseFocusApplicationVolume;
        private System.Windows.Forms.TextBox textBoxHotkeyIncreaseOtherApplicationVolume;
        private System.Windows.Forms.Label labelHotkeyIncreaseOtherApplicationsVolume;
        private System.Windows.Forms.TextBox textBoxHotkeyResetAllVolumes;
        private System.Windows.Forms.TextBox textBoxHotkeyResetBalance;
        private System.Windows.Forms.Label labelResetBalance;
        private System.Windows.Forms.Label labelResetAllVolumes;
        private System.Windows.Forms.Label labelTemporaryFocusApplication;
        private System.Windows.Forms.TextBox textBoxTemporaryFocusApplication;
        private System.Windows.Forms.Label labelActivateTemporaryFocusApplication;
        private System.Windows.Forms.TextBox textBoxHotkeyActivateTemporaryFocusApplication;
        private System.Windows.Forms.Label labelActivateMainFocusApplication;
        private System.Windows.Forms.TextBox textBoxHotkeyActivateMainFocusApplication;
    }
}

