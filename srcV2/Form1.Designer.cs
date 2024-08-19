namespace TheadedFileTables {
  partial class Form1 {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      components = new System.ComponentModel.Container();
      SplitMain = new SplitContainer();
      tabControl1 = new TabControl();
      tabPage1 = new TabPage();
      LabelStorageLocation = new Label();
      LabelSettingsLabel = new Label();
      label4 = new Label();
      TbAccessTokenName = new TextBox();
      label3 = new Label();
      label2 = new Label();
      label1 = new Label();
      TbAccessToken = new TextBox();
      tabPage2 = new TabPage();
      btnManualSave = new Button();
      btnManualRefresh = new Button();
      lbLmtQueue = new ListBox();
      lbFtQueue = new ListBox();
      lbSchedule = new ListBox();
      btnRunSchd = new Button();
      cbAddFollows = new CheckBox();
      btnSetNext = new Button();
      lbNextUp = new ListBox();
      edFollows = new TextBox();
      btnSchedule = new Button();
      textBox1 = new TextBox();
      label5 = new Label();
      label6 = new Label();
      label7 = new Label();
      pbMain = new ProgressBar();
      lbTrackBarValue = new Label();
      trackBar1 = new TrackBar();
      cbRunTimers = new CheckBox();
      lbCountdown = new Label();
      LabelErrorCaption = new Label();
      TextErrorLog = new TextBox();
      HideErrorPanel = new Button();
      TimerRateLimit = new System.Windows.Forms.Timer(components);
      ((System.ComponentModel.ISupportInitialize)SplitMain).BeginInit();
      SplitMain.Panel1.SuspendLayout();
      SplitMain.Panel2.SuspendLayout();
      SplitMain.SuspendLayout();
      tabControl1.SuspendLayout();
      tabPage1.SuspendLayout();
      tabPage2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
      SuspendLayout();
      // 
      // SplitMain
      // 
      SplitMain.Dock = DockStyle.Fill;
      SplitMain.Location = new Point(0, 0);
      SplitMain.Name = "SplitMain";
      SplitMain.Orientation = Orientation.Horizontal;
      // 
      // SplitMain.Panel1
      // 
      SplitMain.Panel1.Controls.Add(tabControl1);
      // 
      // SplitMain.Panel2
      // 
      SplitMain.Panel2.Controls.Add(LabelErrorCaption);
      SplitMain.Panel2.Controls.Add(TextErrorLog);
      SplitMain.Panel2.Controls.Add(HideErrorPanel);
      SplitMain.Size = new Size(903, 798);
      SplitMain.SplitterDistance = 627;
      SplitMain.TabIndex = 0;
      // 
      // tabControl1
      // 
      tabControl1.Controls.Add(tabPage1);
      tabControl1.Controls.Add(tabPage2);
      tabControl1.Dock = DockStyle.Fill;
      tabControl1.Location = new Point(0, 0);
      tabControl1.Name = "tabControl1";
      tabControl1.SelectedIndex = 0;
      tabControl1.Size = new Size(903, 627);
      tabControl1.TabIndex = 0;
      // 
      // tabPage1
      // 
      tabPage1.BackColor = SystemColors.Control;
      tabPage1.Controls.Add(LabelStorageLocation);
      tabPage1.Controls.Add(LabelSettingsLabel);
      tabPage1.Controls.Add(label4);
      tabPage1.Controls.Add(TbAccessTokenName);
      tabPage1.Controls.Add(label3);
      tabPage1.Controls.Add(label2);
      tabPage1.Controls.Add(label1);
      tabPage1.Controls.Add(TbAccessToken);
      tabPage1.Location = new Point(4, 29);
      tabPage1.Name = "tabPage1";
      tabPage1.Padding = new Padding(3);
      tabPage1.Size = new Size(895, 594);
      tabPage1.TabIndex = 0;
      tabPage1.Text = "Configure";
      // 
      // LabelStorageLocation
      // 
      LabelStorageLocation.AutoSize = true;
      LabelStorageLocation.Location = new Point(43, 37);
      LabelStorageLocation.Name = "LabelStorageLocation";
      LabelStorageLocation.Size = new Size(239, 20);
      LabelStorageLocation.TabIndex = 14;
      LabelStorageLocation.Text = "c:\\Some\\path\\to\\computed\\folder";
      // 
      // LabelSettingsLabel
      // 
      LabelSettingsLabel.AutoSize = true;
      LabelSettingsLabel.Location = new Point(17, 13);
      LabelSettingsLabel.Name = "LabelSettingsLabel";
      LabelSettingsLabel.Size = new Size(192, 20);
      LabelSettingsLabel.TabIndex = 13;
      LabelSettingsLabel.Text = "Settings and storage folder:";
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Location = new Point(17, 70);
      label4.Name = "label4";
      label4.Size = new Size(247, 20);
      label4.TabIndex = 12;
      label4.Text = "Github Personal Access Token Name";
      // 
      // TbAccessTokenName
      // 
      TbAccessTokenName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      TbAccessTokenName.Location = new Point(43, 107);
      TbAccessTokenName.Name = "TbAccessTokenName";
      TbAccessTokenName.Size = new Size(845, 27);
      TbAccessTokenName.TabIndex = 11;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.Location = new Point(572, 70);
      label3.Name = "label3";
      label3.Size = new Size(316, 20);
      label3.TabIndex = 10;
      label3.Text = "You need to create this in your Github account.";
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.Location = new Point(17, 244);
      label2.Name = "label2";
      label2.Size = new Size(876, 20);
      label2.TabIndex = 9;
      label2.Text = "Account Settings, Developer Settings... So far need to add Read Write for Followers, Watching, Starring.  Interaction limits read-only";
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(17, 150);
      label1.Name = "label1";
      label1.Size = new Size(203, 20);
      label1.TabIndex = 8;
      label1.Text = "Github Personal Access Token";
      // 
      // TbAccessToken
      // 
      TbAccessToken.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      TbAccessToken.Location = new Point(43, 187);
      TbAccessToken.Name = "TbAccessToken";
      TbAccessToken.Size = new Size(845, 27);
      TbAccessToken.TabIndex = 7;
      // 
      // tabPage2
      // 
      tabPage2.BackColor = SystemColors.Control;
      tabPage2.Controls.Add(btnManualSave);
      tabPage2.Controls.Add(btnManualRefresh);
      tabPage2.Controls.Add(lbLmtQueue);
      tabPage2.Controls.Add(lbFtQueue);
      tabPage2.Controls.Add(lbSchedule);
      tabPage2.Controls.Add(btnRunSchd);
      tabPage2.Controls.Add(cbAddFollows);
      tabPage2.Controls.Add(btnSetNext);
      tabPage2.Controls.Add(lbNextUp);
      tabPage2.Controls.Add(edFollows);
      tabPage2.Controls.Add(btnSchedule);
      tabPage2.Controls.Add(textBox1);
      tabPage2.Controls.Add(label5);
      tabPage2.Controls.Add(label6);
      tabPage2.Controls.Add(label7);
      tabPage2.Controls.Add(pbMain);
      tabPage2.Controls.Add(lbTrackBarValue);
      tabPage2.Controls.Add(trackBar1);
      tabPage2.Controls.Add(cbRunTimers);
      tabPage2.Controls.Add(lbCountdown);
      tabPage2.Location = new Point(4, 29);
      tabPage2.Name = "tabPage2";
      tabPage2.Padding = new Padding(3);
      tabPage2.Size = new Size(895, 594);
      tabPage2.TabIndex = 1;
      tabPage2.Text = "Explore";
      // 
      // btnManualSave
      // 
      btnManualSave.Location = new Point(9, 433);
      btnManualSave.Name = "btnManualSave";
      btnManualSave.Size = new Size(93, 28);
      btnManualSave.TabIndex = 38;
      btnManualSave.Text = "Save DB";
      btnManualSave.UseVisualStyleBackColor = true;
      btnManualSave.Click += btnManualSave_Click;
      // 
      // btnManualRefresh
      // 
      btnManualRefresh.Location = new Point(9, 389);
      btnManualRefresh.Name = "btnManualRefresh";
      btnManualRefresh.Size = new Size(93, 28);
      btnManualRefresh.TabIndex = 37;
      btnManualRefresh.Text = "Refresh";
      btnManualRefresh.UseVisualStyleBackColor = true;
      btnManualRefresh.Click += btnManualRefresh_Click;
      // 
      // lbLmtQueue
      // 
      lbLmtQueue.FormattingEnabled = true;
      lbLmtQueue.ItemHeight = 20;
      lbLmtQueue.Location = new Point(118, 479);
      lbLmtQueue.Name = "lbLmtQueue";
      lbLmtQueue.Size = new Size(260, 84);
      lbLmtQueue.TabIndex = 36;
      // 
      // lbFtQueue
      // 
      lbFtQueue.FormattingEnabled = true;
      lbFtQueue.ItemHeight = 20;
      lbFtQueue.Location = new Point(117, 389);
      lbFtQueue.Name = "lbFtQueue";
      lbFtQueue.Size = new Size(260, 84);
      lbFtQueue.TabIndex = 35;
      // 
      // lbSchedule
      // 
      lbSchedule.FormattingEnabled = true;
      lbSchedule.ItemHeight = 20;
      lbSchedule.Location = new Point(118, 299);
      lbSchedule.Name = "lbSchedule";
      lbSchedule.Size = new Size(259, 84);
      lbSchedule.TabIndex = 34;
      // 
      // btnRunSchd
      // 
      btnRunSchd.Location = new Point(9, 307);
      btnRunSchd.Name = "btnRunSchd";
      btnRunSchd.Size = new Size(93, 51);
      btnRunSchd.TabIndex = 33;
      btnRunSchd.Text = "Run Next Scheduled";
      btnRunSchd.UseVisualStyleBackColor = true;
      btnRunSchd.Click += btnRunSchd_Click;
      // 
      // cbAddFollows
      // 
      cbAddFollows.AutoSize = true;
      cbAddFollows.Checked = true;
      cbAddFollows.CheckState = CheckState.Checked;
      cbAddFollows.Location = new Point(117, 136);
      cbAddFollows.Name = "cbAddFollows";
      cbAddFollows.Size = new Size(170, 24);
      cbAddFollows.TabIndex = 32;
      cbAddFollows.Text = "Add Follow's Follows";
      cbAddFollows.UseVisualStyleBackColor = true;
      // 
      // btnSetNext
      // 
      btnSetNext.Location = new Point(9, 203);
      btnSetNext.Name = "btnSetNext";
      btnSetNext.Size = new Size(93, 28);
      btnSetNext.TabIndex = 31;
      btnSetNext.Text = "Set Next";
      btnSetNext.UseVisualStyleBackColor = true;
      btnSetNext.Click += btnSetNext_Click;
      // 
      // lbNextUp
      // 
      lbNextUp.FormattingEnabled = true;
      lbNextUp.ItemHeight = 20;
      lbNextUp.Location = new Point(118, 203);
      lbNextUp.Name = "lbNextUp";
      lbNextUp.Size = new Size(259, 84);
      lbNextUp.TabIndex = 30;
      // 
      // edFollows
      // 
      edFollows.Location = new Point(117, 166);
      edFollows.Name = "edFollows";
      edFollows.Size = new Size(257, 27);
      edFollows.TabIndex = 29;
      // 
      // btnSchedule
      // 
      btnSchedule.Location = new Point(9, 164);
      btnSchedule.Name = "btnSchedule";
      btnSchedule.Size = new Size(93, 28);
      btnSchedule.TabIndex = 28;
      btnSchedule.Text = "Schedule";
      btnSchedule.UseVisualStyleBackColor = true;
      btnSchedule.Click += btnSchedule_Click;
      // 
      // textBox1
      // 
      textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      textBox1.Location = new Point(396, 134);
      textBox1.Multiline = true;
      textBox1.Name = "textBox1";
      textBox1.ScrollBars = ScrollBars.Vertical;
      textBox1.Size = new Size(491, 454);
      textBox1.TabIndex = 27;
      // 
      // label5
      // 
      label5.AutoSize = true;
      label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label5.Location = new Point(396, 81);
      label5.Name = "label5";
      label5.Size = new Size(424, 28);
      label5.TabIndex = 26;
      label5.Text = "Run Next simulates auto; Save is Manual invoke";
      // 
      // label6
      // 
      label6.AutoSize = true;
      label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label6.Location = new Point(396, 44);
      label6.Name = "label6";
      label6.Size = new Size(377, 28);
      label6.TabIndex = 25;
      label6.Text = "Type Github user next to Schedule to start";
      // 
      // label7
      // 
      label7.AutoSize = true;
      label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label7.Location = new Point(395, 8);
      label7.Name = "label7";
      label7.Size = new Size(342, 28);
      label7.TabIndex = 24;
      label7.Text = "This is the Rage limit notification area ";
      // 
      // pbMain
      // 
      pbMain.Location = new Point(19, 51);
      pbMain.Name = "pbMain";
      pbMain.Size = new Size(368, 10);
      pbMain.TabIndex = 23;
      // 
      // lbTrackBarValue
      // 
      lbTrackBarValue.AutoSize = true;
      lbTrackBarValue.Location = new Point(19, 96);
      lbTrackBarValue.Name = "lbTrackBarValue";
      lbTrackBarValue.Size = new Size(36, 20);
      lbTrackBarValue.TabIndex = 22;
      lbTrackBarValue.Text = "13.7";
      // 
      // trackBar1
      // 
      trackBar1.Location = new Point(8, 56);
      trackBar1.Maximum = 660;
      trackBar1.Name = "trackBar1";
      trackBar1.Size = new Size(388, 56);
      trackBar1.TabIndex = 21;
      trackBar1.TickFrequency = 10;
      trackBar1.Value = 130;
      trackBar1.ValueChanged += trackBar1_ValueChanged;
      // 
      // cbRunTimers
      // 
      cbRunTimers.AutoSize = true;
      cbRunTimers.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
      cbRunTimers.Location = new Point(99, 15);
      cbRunTimers.Name = "cbRunTimers";
      cbRunTimers.Size = new Size(194, 35);
      cbRunTimers.TabIndex = 20;
      cbRunTimers.Text = "Enable Autorun";
      cbRunTimers.UseVisualStyleBackColor = true;
      cbRunTimers.CheckedChanged += cbRunTimers_CheckedChanged;
      // 
      // lbCountdown
      // 
      lbCountdown.AutoSize = true;
      lbCountdown.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
      lbCountdown.Location = new Point(8, 9);
      lbCountdown.Name = "lbCountdown";
      lbCountdown.Size = new Size(73, 41);
      lbCountdown.TabIndex = 19;
      lbCountdown.Text = "12.9";
      // 
      // LabelErrorCaption
      // 
      LabelErrorCaption.AutoSize = true;
      LabelErrorCaption.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
      LabelErrorCaption.Location = new Point(7, 7);
      LabelErrorCaption.Name = "LabelErrorCaption";
      LabelErrorCaption.Size = new Size(93, 25);
      LabelErrorCaption.TabIndex = 14;
      LabelErrorCaption.Text = "Errors Log";
      // 
      // TextErrorLog
      // 
      TextErrorLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      TextErrorLog.Location = new Point(3, 37);
      TextErrorLog.Multiline = true;
      TextErrorLog.Name = "TextErrorLog";
      TextErrorLog.Size = new Size(896, 127);
      TextErrorLog.TabIndex = 13;
      // 
      // HideErrorPanel
      // 
      HideErrorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      HideErrorPanel.Location = new Point(834, 9);
      HideErrorPanel.Name = "HideErrorPanel";
      HideErrorPanel.Size = new Size(64, 27);
      HideErrorPanel.TabIndex = 12;
      HideErrorPanel.Text = "Hide";
      HideErrorPanel.UseVisualStyleBackColor = true;
      HideErrorPanel.Click += HideErrorPanel_Click;
      // 
      // TimerRateLimit
      // 
      TimerRateLimit.Tick += TimerRateLimit_Tick;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      AutoSizeMode = AutoSizeMode.GrowAndShrink;
      ClientSize = new Size(903, 798);
      Controls.Add(SplitMain);
      Name = "Form1";
      SizeGripStyle = SizeGripStyle.Show;
      StartPosition = FormStartPosition.WindowsDefaultBounds;
      Text = "Apiary 2";
      FormClosing += Form1_FormClosing;
      Shown += Form1_Shown;
      Resize += Form1_Resize;
      SplitMain.Panel1.ResumeLayout(false);
      SplitMain.Panel2.ResumeLayout(false);
      SplitMain.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)SplitMain).EndInit();
      SplitMain.ResumeLayout(false);
      tabControl1.ResumeLayout(false);
      tabPage1.ResumeLayout(false);
      tabPage1.PerformLayout();
      tabPage2.ResumeLayout(false);
      tabPage2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private SplitContainer SplitMain;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private Label LabelErrorCaption;
    public TextBox TextErrorLog;
    private Button HideErrorPanel;
    private Label label4;
    private TextBox TbAccessTokenName;
    private Label label3;
    private Label label2;
    private Label label1;
    private TextBox TbAccessToken;
    private Label LabelStorageLocation;
    private Label LabelSettingsLabel;
    private ProgressBar pbMain;
    private Label lbTrackBarValue;
    private TrackBar trackBar1;
    private CheckBox cbRunTimers;
    private Label lbCountdown;
    private System.Windows.Forms.Timer TimerRateLimit;
    private TextBox textBox1;
    private Label label5;
    private Label label6;
    private Label label7;
    private CheckBox cbAddFollows;
    private Button btnSetNext;
    private ListBox lbNextUp;
    private TextBox edFollows;
    private Button btnSchedule;
    private Button btnRunSchd;
    private ListBox lbSchedule;
    private ListBox lbFtQueue;
    private ListBox lbLmtQueue;
    private Button btnManualSave;
    private Button btnManualRefresh;
  }
}