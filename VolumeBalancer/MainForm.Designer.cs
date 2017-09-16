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
            this.labelHotkeyActivateMainFocusApplication = new System.Windows.Forms.Label();
            this.textBoxHotkeyActivateMainFocusApplication = new System.Windows.Forms.TextBox();
            this.labelHotkeySetAndActivateTemporaryFocusApplication = new System.Windows.Forms.Label();
            this.textBoxHotkeySetAndActivateTemporaryFocusApplication = new System.Windows.Forms.TextBox();
            this.labelHotkeyResetAllVolumes = new System.Windows.Forms.Label();
            this.labelHotkeyResetBalance = new System.Windows.Forms.Label();
            this.textBoxHotkeyResetAllVolumes = new System.Windows.Forms.TextBox();
            this.textBoxHotkeyResetBalance = new System.Windows.Forms.TextBox();
            this.textBoxHotkeyIncreaseOtherApplicationVolume = new System.Windows.Forms.TextBox();
            this.labelHotkeyIncreaseOtherApplicationsVolume = new System.Windows.Forms.Label();
            this.textBoxHotkeyIncreaseFocusApplicationVolume = new System.Windows.Forms.TextBox();
            this.labelHotkeyIncreaseFocusApplicationVolume = new System.Windows.Forms.Label();
            this.labelTemporaryFocusApplication = new System.Windows.Forms.Label();
            this.textBoxTemporaryFocusApplication = new System.Windows.Forms.TextBox();
            this.groupBoxApplications = new System.Windows.Forms.GroupBox();
            this.checkBoxBalanceSystemSounds = new System.Windows.Forms.CheckBox();
            this.panelTrayIconColor = new System.Windows.Forms.Panel();
            this.groupBoxMisc = new System.Windows.Forms.GroupBox();
            this.labelFormIconColor = new System.Windows.Forms.Label();
            this.labelTrayIconColor = new System.Windows.Forms.Label();
            this.panelFormIconColor = new System.Windows.Forms.Panel();
            this.checkBoxAutostart = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarBalance)).BeginInit();
            this.groupBoxBalance.SuspendLayout();
            this.groupBoxHotkeys.SuspendLayout();
            this.groupBoxApplications.SuspendLayout();
            this.groupBoxMisc.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarBalance
            // 
            this.trackBarBalance.LargeChange = 1;
            this.trackBarBalance.Location = new System.Drawing.Point(81, 29);
            this.trackBarBalance.Maximum = 40;
            this.trackBarBalance.Name = "trackBarBalance";
            this.trackBarBalance.Size = new System.Drawing.Size(435, 45);
            this.trackBarBalance.TabIndex = 1;
            this.trackBarBalance.Value = 20;
            this.trackBarBalance.ValueChanged += new System.EventHandler(this.trackBarBalance_ValueChanged);
            // 
            // comboAudioApplications
            // 
            this.comboAudioApplications.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboAudioApplications.FormattingEnabled = true;
            this.comboAudioApplications.Location = new System.Drawing.Point(241, 55);
            this.comboAudioApplications.MaxDropDownItems = 12;
            this.comboAudioApplications.Name = "comboAudioApplications";
            this.comboAudioApplications.Size = new System.Drawing.Size(345, 21);
            this.comboAudioApplications.TabIndex = 4;
            this.comboAudioApplications.DropDown += new System.EventHandler(this.comboAudioApplications_DropDown);
            this.comboAudioApplications.SelectedIndexChanged += new System.EventHandler(this.comboAudioApplications_SelectedIndexChanged);
            // 
            // labelMainFocusApplication
            // 
            this.labelMainFocusApplication.AutoSize = true;
            this.labelMainFocusApplication.Location = new System.Drawing.Point(13, 32);
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
            this.groupBoxBalance.Location = new System.Drawing.Point(16, 158);
            this.groupBoxBalance.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBoxBalance.Name = "groupBoxBalance";
            this.groupBoxBalance.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxBalance.Size = new System.Drawing.Size(602, 115);
            this.groupBoxBalance.TabIndex = 1;
            this.groupBoxBalance.TabStop = false;
            this.groupBoxBalance.Text = "Balance";
            // 
            // buttonResetBalance
            // 
            this.buttonResetBalance.Location = new System.Drawing.Point(227, 76);
            this.buttonResetBalance.Name = "buttonResetBalance";
            this.buttonResetBalance.Size = new System.Drawing.Size(150, 23);
            this.buttonResetBalance.TabIndex = 5;
            this.buttonResetBalance.Text = "Reset Balance";
            this.buttonResetBalance.UseVisualStyleBackColor = true;
            this.buttonResetBalance.Click += new System.EventHandler(this.buttonResetBalance_Click);
            // 
            // buttonIncreaseOtherApplicationVolume
            // 
            this.buttonIncreaseOtherApplicationVolume.Location = new System.Drawing.Point(402, 76);
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
            this.labelBalanceOtherApplications.Location = new System.Drawing.Point(522, 31);
            this.labelBalanceOtherApplications.Name = "labelBalanceOtherApplications";
            this.labelBalanceOtherApplications.Size = new System.Drawing.Size(64, 26);
            this.labelBalanceOtherApplications.TabIndex = 3;
            this.labelBalanceOtherApplications.Text = "Other\r\nApplications";
            this.labelBalanceOtherApplications.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // labelBalanceCenter
            // 
            this.labelBalanceCenter.AutoSize = true;
            this.labelBalanceCenter.Location = new System.Drawing.Point(294, 20);
            this.labelBalanceCenter.Name = "labelBalanceCenter";
            this.labelBalanceCenter.Size = new System.Drawing.Size(10, 13);
            this.labelBalanceCenter.TabIndex = 2;
            this.labelBalanceCenter.Text = "-";
            // 
            // textBoxMainFocusApplication
            // 
            this.textBoxMainFocusApplication.Location = new System.Drawing.Point(139, 29);
            this.textBoxMainFocusApplication.Name = "textBoxMainFocusApplication";
            this.textBoxMainFocusApplication.ReadOnly = true;
            this.textBoxMainFocusApplication.Size = new System.Drawing.Size(447, 20);
            this.textBoxMainFocusApplication.TabIndex = 1;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(138, 54);
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
            this.labelOr.Location = new System.Drawing.Point(219, 59);
            this.labelOr.Name = "labelOr";
            this.labelOr.Size = new System.Drawing.Size(16, 13);
            this.labelOr.TabIndex = 3;
            this.labelOr.Text = "or";
            // 
            // groupBoxHotkeys
            // 
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyActivateMainFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyActivateMainFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeySetAndActivateTemporaryFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeySetAndActivateTemporaryFocusApplication);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyResetAllVolumes);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyResetBalance);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyResetAllVolumes);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyResetBalance);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyIncreaseOtherApplicationVolume);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyIncreaseOtherApplicationsVolume);
            this.groupBoxHotkeys.Controls.Add(this.textBoxHotkeyIncreaseFocusApplicationVolume);
            this.groupBoxHotkeys.Controls.Add(this.labelHotkeyIncreaseFocusApplicationVolume);
            this.groupBoxHotkeys.Location = new System.Drawing.Point(16, 286);
            this.groupBoxHotkeys.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.groupBoxHotkeys.Name = "groupBoxHotkeys";
            this.groupBoxHotkeys.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxHotkeys.Size = new System.Drawing.Size(394, 195);
            this.groupBoxHotkeys.TabIndex = 2;
            this.groupBoxHotkeys.TabStop = false;
            this.groupBoxHotkeys.Text = "Hotkeys";
            // 
            // labelHotkeyActivateMainFocusApplication
            // 
            this.labelHotkeyActivateMainFocusApplication.AutoSize = true;
            this.labelHotkeyActivateMainFocusApplication.Location = new System.Drawing.Point(16, 110);
            this.labelHotkeyActivateMainFocusApplication.Name = "labelHotkeyActivateMainFocusApplication";
            this.labelHotkeyActivateMainFocusApplication.Size = new System.Drawing.Size(162, 13);
            this.labelHotkeyActivateMainFocusApplication.TabIndex = 6;
            this.labelHotkeyActivateMainFocusApplication.Text = "Activate Main Focus Application:";
            // 
            // textBoxHotkeyActivateMainFocusApplication
            // 
            this.textBoxHotkeyActivateMainFocusApplication.Location = new System.Drawing.Point(200, 107);
            this.textBoxHotkeyActivateMainFocusApplication.Name = "textBoxHotkeyActivateMainFocusApplication";
            this.textBoxHotkeyActivateMainFocusApplication.ReadOnly = true;
            this.textBoxHotkeyActivateMainFocusApplication.Size = new System.Drawing.Size(177, 20);
            this.textBoxHotkeyActivateMainFocusApplication.TabIndex = 7;
            this.textBoxHotkeyActivateMainFocusApplication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyActivateMainFocusApplication_KeyDown);
            // 
            // labelHotkeySetAndActivateTemporaryFocusApplication
            // 
            this.labelHotkeySetAndActivateTemporaryFocusApplication.AutoSize = true;
            this.labelHotkeySetAndActivateTemporaryFocusApplication.Location = new System.Drawing.Point(16, 136);
            this.labelHotkeySetAndActivateTemporaryFocusApplication.Name = "labelHotkeySetAndActivateTemporaryFocusApplication";
            this.labelHotkeySetAndActivateTemporaryFocusApplication.Size = new System.Drawing.Size(166, 13);
            this.labelHotkeySetAndActivateTemporaryFocusApplication.TabIndex = 8;
            this.labelHotkeySetAndActivateTemporaryFocusApplication.Text = "Set Temporary Focus Application:";
            // 
            // textBoxHotkeySetAndActivateTemporaryFocusApplication
            // 
            this.textBoxHotkeySetAndActivateTemporaryFocusApplication.Location = new System.Drawing.Point(200, 133);
            this.textBoxHotkeySetAndActivateTemporaryFocusApplication.Name = "textBoxHotkeySetAndActivateTemporaryFocusApplication";
            this.textBoxHotkeySetAndActivateTemporaryFocusApplication.ReadOnly = true;
            this.textBoxHotkeySetAndActivateTemporaryFocusApplication.Size = new System.Drawing.Size(177, 20);
            this.textBoxHotkeySetAndActivateTemporaryFocusApplication.TabIndex = 9;
            this.textBoxHotkeySetAndActivateTemporaryFocusApplication.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeySetAndActivateTemporaryFocusApplication_KeyDown);
            // 
            // labelHotkeyResetAllVolumes
            // 
            this.labelHotkeyResetAllVolumes.AutoSize = true;
            this.labelHotkeyResetAllVolumes.Location = new System.Drawing.Point(16, 162);
            this.labelHotkeyResetAllVolumes.Name = "labelHotkeyResetAllVolumes";
            this.labelHotkeyResetAllVolumes.Size = new System.Drawing.Size(95, 13);
            this.labelHotkeyResetAllVolumes.TabIndex = 10;
            this.labelHotkeyResetAllVolumes.Text = "Reset All Volumes:";
            // 
            // labelHotkeyResetBalance
            // 
            this.labelHotkeyResetBalance.AutoSize = true;
            this.labelHotkeyResetBalance.Location = new System.Drawing.Point(16, 84);
            this.labelHotkeyResetBalance.Name = "labelHotkeyResetBalance";
            this.labelHotkeyResetBalance.Size = new System.Drawing.Size(80, 13);
            this.labelHotkeyResetBalance.TabIndex = 4;
            this.labelHotkeyResetBalance.Text = "Reset Balance:";
            // 
            // textBoxHotkeyResetAllVolumes
            // 
            this.textBoxHotkeyResetAllVolumes.Location = new System.Drawing.Point(200, 159);
            this.textBoxHotkeyResetAllVolumes.Name = "textBoxHotkeyResetAllVolumes";
            this.textBoxHotkeyResetAllVolumes.ReadOnly = true;
            this.textBoxHotkeyResetAllVolumes.Size = new System.Drawing.Size(177, 20);
            this.textBoxHotkeyResetAllVolumes.TabIndex = 11;
            this.textBoxHotkeyResetAllVolumes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyResetAllVolumes_KeyDown);
            // 
            // textBoxHotkeyResetBalance
            // 
            this.textBoxHotkeyResetBalance.Location = new System.Drawing.Point(200, 81);
            this.textBoxHotkeyResetBalance.Name = "textBoxHotkeyResetBalance";
            this.textBoxHotkeyResetBalance.ReadOnly = true;
            this.textBoxHotkeyResetBalance.Size = new System.Drawing.Size(177, 20);
            this.textBoxHotkeyResetBalance.TabIndex = 5;
            this.textBoxHotkeyResetBalance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHotkeyResetBalance_KeyDown);
            // 
            // textBoxHotkeyIncreaseOtherApplicationVolume
            // 
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Location = new System.Drawing.Point(200, 55);
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Name = "textBoxHotkeyIncreaseOtherApplicationVolume";
            this.textBoxHotkeyIncreaseOtherApplicationVolume.ReadOnly = true;
            this.textBoxHotkeyIncreaseOtherApplicationVolume.Size = new System.Drawing.Size(177, 20);
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
            this.textBoxHotkeyIncreaseFocusApplicationVolume.Location = new System.Drawing.Point(200, 29);
            this.textBoxHotkeyIncreaseFocusApplicationVolume.Name = "textBoxHotkeyIncreaseFocusApplicationVolume";
            this.textBoxHotkeyIncreaseFocusApplicationVolume.ReadOnly = true;
            this.textBoxHotkeyIncreaseFocusApplicationVolume.Size = new System.Drawing.Size(177, 20);
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
            this.labelTemporaryFocusApplication.Location = new System.Drawing.Point(13, 93);
            this.labelTemporaryFocusApplication.Name = "labelTemporaryFocusApplication";
            this.labelTemporaryFocusApplication.Size = new System.Drawing.Size(92, 13);
            this.labelTemporaryFocusApplication.TabIndex = 5;
            this.labelTemporaryFocusApplication.Text = "Temporary Focus:";
            // 
            // textBoxTemporaryFocusApplication
            // 
            this.textBoxTemporaryFocusApplication.Location = new System.Drawing.Point(139, 90);
            this.textBoxTemporaryFocusApplication.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.textBoxTemporaryFocusApplication.Name = "textBoxTemporaryFocusApplication";
            this.textBoxTemporaryFocusApplication.ReadOnly = true;
            this.textBoxTemporaryFocusApplication.Size = new System.Drawing.Size(447, 20);
            this.textBoxTemporaryFocusApplication.TabIndex = 6;
            // 
            // groupBoxApplications
            // 
            this.groupBoxApplications.Controls.Add(this.labelMainFocusApplication);
            this.groupBoxApplications.Controls.Add(this.textBoxTemporaryFocusApplication);
            this.groupBoxApplications.Controls.Add(this.comboAudioApplications);
            this.groupBoxApplications.Controls.Add(this.labelTemporaryFocusApplication);
            this.groupBoxApplications.Controls.Add(this.textBoxMainFocusApplication);
            this.groupBoxApplications.Controls.Add(this.buttonBrowse);
            this.groupBoxApplications.Controls.Add(this.labelOr);
            this.groupBoxApplications.Location = new System.Drawing.Point(16, 16);
            this.groupBoxApplications.Name = "groupBoxApplications";
            this.groupBoxApplications.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxApplications.Size = new System.Drawing.Size(602, 129);
            this.groupBoxApplications.TabIndex = 0;
            this.groupBoxApplications.TabStop = false;
            this.groupBoxApplications.Text = "Applications";
            // 
            // checkBoxBalanceSystemSounds
            // 
            this.checkBoxBalanceSystemSounds.AutoSize = true;
            this.checkBoxBalanceSystemSounds.Location = new System.Drawing.Point(16, 52);
            this.checkBoxBalanceSystemSounds.Name = "checkBoxBalanceSystemSounds";
            this.checkBoxBalanceSystemSounds.Size = new System.Drawing.Size(141, 17);
            this.checkBoxBalanceSystemSounds.TabIndex = 1;
            this.checkBoxBalanceSystemSounds.Text = "Balance System Sounds";
            this.checkBoxBalanceSystemSounds.UseVisualStyleBackColor = true;
            this.checkBoxBalanceSystemSounds.CheckedChanged += new System.EventHandler(this.checkBoxBalanceSystemSounds_CheckedChanged);
            // 
            // panelTrayIconColor
            // 
            this.panelTrayIconColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTrayIconColor.Location = new System.Drawing.Point(16, 98);
            this.panelTrayIconColor.Name = "panelTrayIconColor";
            this.panelTrayIconColor.Size = new System.Drawing.Size(13, 13);
            this.panelTrayIconColor.TabIndex = 4;
            this.panelTrayIconColor.Click += new System.EventHandler(this.panelTrayIconColor_Click);
            // 
            // groupBoxMisc
            // 
            this.groupBoxMisc.Controls.Add(this.labelFormIconColor);
            this.groupBoxMisc.Controls.Add(this.labelTrayIconColor);
            this.groupBoxMisc.Controls.Add(this.panelFormIconColor);
            this.groupBoxMisc.Controls.Add(this.panelTrayIconColor);
            this.groupBoxMisc.Controls.Add(this.checkBoxBalanceSystemSounds);
            this.groupBoxMisc.Controls.Add(this.checkBoxAutostart);
            this.groupBoxMisc.Location = new System.Drawing.Point(423, 286);
            this.groupBoxMisc.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.groupBoxMisc.Name = "groupBoxMisc";
            this.groupBoxMisc.Padding = new System.Windows.Forms.Padding(13);
            this.groupBoxMisc.Size = new System.Drawing.Size(195, 195);
            this.groupBoxMisc.TabIndex = 4;
            this.groupBoxMisc.TabStop = false;
            this.groupBoxMisc.Text = "Misc";
            // 
            // labelFormIconColor
            // 
            this.labelFormIconColor.AutoSize = true;
            this.labelFormIconColor.Location = new System.Drawing.Point(32, 76);
            this.labelFormIconColor.Name = "labelFormIconColor";
            this.labelFormIconColor.Size = new System.Drawing.Size(97, 13);
            this.labelFormIconColor.TabIndex = 3;
            this.labelFormIconColor.Text = "Window Icon Color";
            this.labelFormIconColor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelFormIconColor.Click += new System.EventHandler(this.panelFormIconColor_Click);
            // 
            // labelTrayIconColor
            // 
            this.labelTrayIconColor.AutoSize = true;
            this.labelTrayIconColor.Location = new System.Drawing.Point(32, 98);
            this.labelTrayIconColor.Name = "labelTrayIconColor";
            this.labelTrayIconColor.Size = new System.Drawing.Size(79, 13);
            this.labelTrayIconColor.TabIndex = 5;
            this.labelTrayIconColor.Text = "Tray Icon Color";
            this.labelTrayIconColor.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelTrayIconColor.Click += new System.EventHandler(this.panelTrayIconColor_Click);
            // 
            // panelFormIconColor
            // 
            this.panelFormIconColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFormIconColor.Location = new System.Drawing.Point(16, 76);
            this.panelFormIconColor.Name = "panelFormIconColor";
            this.panelFormIconColor.Size = new System.Drawing.Size(13, 13);
            this.panelFormIconColor.TabIndex = 2;
            this.panelFormIconColor.Click += new System.EventHandler(this.panelFormIconColor_Click);
            // 
            // checkBoxAutostart
            // 
            this.checkBoxAutostart.AutoSize = true;
            this.checkBoxAutostart.Location = new System.Drawing.Point(16, 29);
            this.checkBoxAutostart.Name = "checkBoxAutostart";
            this.checkBoxAutostart.Size = new System.Drawing.Size(68, 17);
            this.checkBoxAutostart.TabIndex = 0;
            this.checkBoxAutostart.Text = "Autostart";
            this.checkBoxAutostart.UseVisualStyleBackColor = true;
            this.checkBoxAutostart.CheckedChanged += new System.EventHandler(this.checkBoxAutostart_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 495);
            this.Controls.Add(this.groupBoxMisc);
            this.Controls.Add(this.groupBoxApplications);
            this.Controls.Add(this.groupBoxHotkeys);
            this.Controls.Add(this.groupBoxBalance);
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
            this.groupBoxApplications.ResumeLayout(false);
            this.groupBoxApplications.PerformLayout();
            this.groupBoxMisc.ResumeLayout(false);
            this.groupBoxMisc.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Label labelHotkeyResetBalance;
        private System.Windows.Forms.Label labelHotkeyResetAllVolumes;
        private System.Windows.Forms.Label labelTemporaryFocusApplication;
        private System.Windows.Forms.TextBox textBoxTemporaryFocusApplication;
        private System.Windows.Forms.Label labelHotkeySetAndActivateTemporaryFocusApplication;
        private System.Windows.Forms.TextBox textBoxHotkeySetAndActivateTemporaryFocusApplication;
        private System.Windows.Forms.Label labelHotkeyActivateMainFocusApplication;
        private System.Windows.Forms.TextBox textBoxHotkeyActivateMainFocusApplication;
        private System.Windows.Forms.GroupBox groupBoxApplications;
        private System.Windows.Forms.CheckBox checkBoxBalanceSystemSounds;
        private System.Windows.Forms.GroupBox groupBoxMisc;
        private System.Windows.Forms.CheckBox checkBoxAutostart;
        private System.Windows.Forms.Panel panelTrayIconColor;
        private System.Windows.Forms.Label labelTrayIconColor;
        private System.Windows.Forms.Label labelFormIconColor;
        private System.Windows.Forms.Panel panelFormIconColor;
    }
}

