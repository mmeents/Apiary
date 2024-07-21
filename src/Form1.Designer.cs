namespace apiary {
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
      TabOnOff = new TabControl();
      TabConfigure = new TabPage();
      label4 = new Label();
      TbAccessTokenName = new TextBox();
      label3 = new Label();
      label2 = new Label();
      label1 = new Label();
      TbAccessToken = new TextBox();
      TabExplore = new TabPage();
      btnSetNext = new Button();
      lbNextUp = new ListBox();
      edFollows = new TextBox();
      btnFollows = new Button();
      textBox1 = new TextBox();
      pbMain = new ProgressBar();
      label5 = new Label();
      label6 = new Label();
      label7 = new Label();
      lbTrackBarValue = new Label();
      trackBar1 = new TrackBar();
      cbRunTimers = new CheckBox();
      lbCountdown = new Label();
      PanelError = new Panel();
      LabelErrorCaption = new Label();
      TextErrorLog = new TextBox();
      HideErrorPanel = new Button();
      timer1 = new System.Windows.Forms.Timer(components);
      TabOnOff.SuspendLayout();
      TabConfigure.SuspendLayout();
      TabExplore.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
      PanelError.SuspendLayout();
      SuspendLayout();
      // 
      // TabOnOff
      // 
      TabOnOff.Controls.Add(TabConfigure);
      TabOnOff.Controls.Add(TabExplore);
      TabOnOff.Dock = DockStyle.Fill;
      TabOnOff.Location = new Point(0, 0);
      TabOnOff.Name = "TabOnOff";
      TabOnOff.SelectedIndex = 0;
      TabOnOff.Size = new Size(960, 794);
      TabOnOff.TabIndex = 0;
      TabOnOff.SelectedIndexChanged += TabOnOff_SelectedIndexChanged;
      TabOnOff.Selecting += TabOnOff_Selecting;
      // 
      // TabConfigure
      // 
      TabConfigure.BackColor = SystemColors.Control;
      TabConfigure.BorderStyle = BorderStyle.FixedSingle;
      TabConfigure.Controls.Add(label4);
      TabConfigure.Controls.Add(TbAccessTokenName);
      TabConfigure.Controls.Add(label3);
      TabConfigure.Controls.Add(label2);
      TabConfigure.Controls.Add(label1);
      TabConfigure.Controls.Add(TbAccessToken);
      TabConfigure.Location = new Point(4, 29);
      TabConfigure.Name = "TabConfigure";
      TabConfigure.Padding = new Padding(3);
      TabConfigure.Size = new Size(952, 761);
      TabConfigure.TabIndex = 0;
      TabConfigure.Text = "Configure";
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Location = new Point(17, 26);
      label4.Name = "label4";
      label4.Size = new Size(247, 20);
      label4.TabIndex = 6;
      label4.Text = "Github Personal Access Token Name";
      // 
      // TbAccessTokenName
      // 
      TbAccessTokenName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      TbAccessTokenName.Location = new Point(43, 63);
      TbAccessTokenName.Name = "TbAccessTokenName";
      TbAccessTokenName.Size = new Size(839, 27);
      TbAccessTokenName.TabIndex = 5;
      // 
      // label3
      // 
      label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      label3.AutoSize = true;
      label3.Location = new Point(566, 26);
      label3.Name = "label3";
      label3.Size = new Size(316, 20);
      label3.TabIndex = 3;
      label3.Text = "You need to create this in your Github account.";
      // 
      // label2
      // 
      label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      label2.AutoSize = true;
      label2.Location = new Point(17, 200);
      label2.Name = "label2";
      label2.Size = new Size(876, 20);
      label2.TabIndex = 2;
      label2.Text = "Account Settings, Developer Settings... So far need to add Read Write for Followers, Watching, Starring.  Interaction limits read-only";
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(17, 106);
      label1.Name = "label1";
      label1.Size = new Size(203, 20);
      label1.TabIndex = 1;
      label1.Text = "Github Personal Access Token";
      // 
      // TbAccessToken
      // 
      TbAccessToken.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      TbAccessToken.Location = new Point(43, 143);
      TbAccessToken.Name = "TbAccessToken";
      TbAccessToken.Size = new Size(839, 27);
      TbAccessToken.TabIndex = 0;
      // 
      // TabExplore
      // 
      TabExplore.BackColor = SystemColors.Control;
      TabExplore.Controls.Add(btnSetNext);
      TabExplore.Controls.Add(lbNextUp);
      TabExplore.Controls.Add(edFollows);
      TabExplore.Controls.Add(btnFollows);
      TabExplore.Controls.Add(textBox1);
      TabExplore.Controls.Add(pbMain);
      TabExplore.Controls.Add(label5);
      TabExplore.Controls.Add(label6);
      TabExplore.Controls.Add(label7);
      TabExplore.Controls.Add(lbTrackBarValue);
      TabExplore.Controls.Add(trackBar1);
      TabExplore.Controls.Add(cbRunTimers);
      TabExplore.Controls.Add(lbCountdown);
      TabExplore.Location = new Point(4, 29);
      TabExplore.Name = "TabExplore";
      TabExplore.Padding = new Padding(3);
      TabExplore.Size = new Size(952, 761);
      TabExplore.TabIndex = 1;
      TabExplore.Text = "Explore";
      // 
      // btnSetNext
      // 
      btnSetNext.Location = new Point(21, 194);
      btnSetNext.Name = "btnSetNext";
      btnSetNext.Size = new Size(93, 29);
      btnSetNext.TabIndex = 23;
      btnSetNext.Text = "Set Next";
      btnSetNext.UseVisualStyleBackColor = true;
      btnSetNext.Click += btnSetNext_Click;
      // 
      // lbNextUp
      // 
      lbNextUp.FormattingEnabled = true;
      lbNextUp.ItemHeight = 20;
      lbNextUp.Location = new Point(120, 194);
      lbNextUp.Name = "lbNextUp";
      lbNextUp.Size = new Size(269, 384);
      lbNextUp.TabIndex = 22;
      // 
      // edFollows
      // 
      edFollows.Location = new Point(120, 150);
      edFollows.Name = "edFollows";
      edFollows.Size = new Size(269, 27);
      edFollows.TabIndex = 21;
      // 
      // btnFollows
      // 
      btnFollows.Location = new Point(21, 150);
      btnFollows.Name = "btnFollows";
      btnFollows.Size = new Size(93, 29);
      btnFollows.TabIndex = 20;
      btnFollows.Text = "Follows";
      btnFollows.UseVisualStyleBackColor = true;
      btnFollows.Click += btnFollows_Click;
      // 
      // textBox1
      // 
      textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      textBox1.Location = new Point(405, 148);
      textBox1.Multiline = true;
      textBox1.Name = "textBox1";
      textBox1.Size = new Size(537, 603);
      textBox1.TabIndex = 19;
      // 
      // pbMain
      // 
      pbMain.Location = new Point(21, 58);
      pbMain.Name = "pbMain";
      pbMain.Size = new Size(368, 10);
      pbMain.TabIndex = 18;
      // 
      // label5
      // 
      label5.AutoSize = true;
      label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label5.Location = new Point(405, 95);
      label5.Name = "label5";
      label5.Size = new Size(65, 28);
      label5.TabIndex = 17;
      label5.Text = "label5";
      // 
      // label6
      // 
      label6.AutoSize = true;
      label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label6.Location = new Point(405, 58);
      label6.Name = "label6";
      label6.Size = new Size(65, 28);
      label6.TabIndex = 16;
      label6.Text = "label6";
      // 
      // label7
      // 
      label7.AutoSize = true;
      label7.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
      label7.Location = new Point(404, 22);
      label7.Name = "label7";
      label7.Size = new Size(65, 28);
      label7.TabIndex = 15;
      label7.Text = "label7";
      // 
      // lbTrackBarValue
      // 
      lbTrackBarValue.AutoSize = true;
      lbTrackBarValue.Location = new Point(21, 103);
      lbTrackBarValue.Name = "lbTrackBarValue";
      lbTrackBarValue.Size = new Size(36, 20);
      lbTrackBarValue.TabIndex = 12;
      lbTrackBarValue.Text = "13.7";
      // 
      // trackBar1
      // 
      trackBar1.Location = new Point(10, 63);
      trackBar1.Maximum = 660;
      trackBar1.Name = "trackBar1";
      trackBar1.Size = new Size(388, 56);
      trackBar1.TabIndex = 11;
      trackBar1.TickFrequency = 10;
      trackBar1.Value = 130;
      trackBar1.ValueChanged += trackBar1_ValueChanged;
      // 
      // cbRunTimers
      // 
      cbRunTimers.AutoSize = true;
      cbRunTimers.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
      cbRunTimers.Location = new Point(101, 22);
      cbRunTimers.Name = "cbRunTimers";
      cbRunTimers.Size = new Size(194, 35);
      cbRunTimers.TabIndex = 10;
      cbRunTimers.Text = "Enable Autorun";
      cbRunTimers.UseVisualStyleBackColor = true;
      cbRunTimers.CheckedChanged += cbRunTimers_CheckedChanged;
      // 
      // lbCountdown
      // 
      lbCountdown.AutoSize = true;
      lbCountdown.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
      lbCountdown.Location = new Point(10, 16);
      lbCountdown.Name = "lbCountdown";
      lbCountdown.Size = new Size(73, 41);
      lbCountdown.TabIndex = 9;
      lbCountdown.Text = "12.9";
      // 
      // PanelError
      // 
      PanelError.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      PanelError.BorderStyle = BorderStyle.Fixed3D;
      PanelError.Controls.Add(LabelErrorCaption);
      PanelError.Controls.Add(TextErrorLog);
      PanelError.Controls.Add(HideErrorPanel);
      PanelError.Dock = DockStyle.Bottom;
      PanelError.Location = new Point(0, 569);
      PanelError.Name = "PanelError";
      PanelError.Size = new Size(960, 225);
      PanelError.TabIndex = 1;
      // 
      // LabelErrorCaption
      // 
      LabelErrorCaption.AutoSize = true;
      LabelErrorCaption.Font = new Font("Segoe UI", 10.8F, FontStyle.Regular, GraphicsUnit.Point);
      LabelErrorCaption.Location = new Point(12, 11);
      LabelErrorCaption.Name = "LabelErrorCaption";
      LabelErrorCaption.Size = new Size(93, 25);
      LabelErrorCaption.TabIndex = 11;
      LabelErrorCaption.Text = "Errors Log";
      // 
      // TextErrorLog
      // 
      TextErrorLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      TextErrorLog.Location = new Point(12, 42);
      TextErrorLog.Multiline = true;
      TextErrorLog.Name = "TextErrorLog";
      TextErrorLog.Size = new Size(932, 167);
      TextErrorLog.TabIndex = 10;
      // 
      // HideErrorPanel
      // 
      HideErrorPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      HideErrorPanel.Location = new Point(886, 7);
      HideErrorPanel.Name = "HideErrorPanel";
      HideErrorPanel.Size = new Size(58, 29);
      HideErrorPanel.TabIndex = 9;
      HideErrorPanel.Text = "Hide";
      HideErrorPanel.UseVisualStyleBackColor = true;
      HideErrorPanel.Click += HideErrorPanel_Click;
      // 
      // timer1
      // 
      timer1.Tick += timer1_Tick;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(960, 794);
      Controls.Add(PanelError);
      Controls.Add(TabOnOff);
      Name = "Form1";
      Text = "Apiary";
      Shown += Form1_Shown;
      TabOnOff.ResumeLayout(false);
      TabConfigure.ResumeLayout(false);
      TabConfigure.PerformLayout();
      TabExplore.ResumeLayout(false);
      TabExplore.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
      PanelError.ResumeLayout(false);
      PanelError.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private TabControl TabOnOff;
    private TabPage TabConfigure;
    private TabPage TabExplore;
    private Label label1;
    private TextBox TbAccessToken;
    private Label label2;
    private Label label3;
    private Label label4;
    private TextBox TbAccessTokenName;
    private Panel PanelError;
    private Button HideErrorPanel;
    public TextBox TextErrorLog;
    private Label LabelErrorCaption;
    private System.Windows.Forms.Timer timer1;
    private Label lbTrackBarValue;
    private TrackBar trackBar1;
    private CheckBox cbRunTimers;
    private Label lbCountdown;
    private ProgressBar pbMain;
    private Label label5;
    private Label label6;
    private Label label7;
    private TextBox textBox1;
    private Button btnSetNext;
    private ListBox lbNextUp;
    private TextBox edFollows;
    private Button btnFollows;
  }
}