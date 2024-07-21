using apiary.Models;
using FileTables;
using System;
using System.Linq;
using System.Windows.Forms;

namespace apiary {
  public partial class Form1 : Form, ILogMsg, ILogProgress {

    private FollowedService _followedService;
    private SettingsFile _settingsPack { get; set; }
    private Settings _settings;
    private string _defaultDir = "";
    private string _defaultSettings = "";
    private int Countdown = 0;
    private bool ShouldStop = true;
    private bool Importing = false;

    public Form1() {
      InitializeComponent();
      lbCountdown.Text = "";
      lbTrackBarValue.Text = "";
      _defaultDir = _defaultDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\PrompterFiles";
      if (!Directory.Exists(_defaultDir)) {
        Directory.CreateDirectory(_defaultDir);
      }
      _defaultSettings = _defaultDir + CExt.CommonPathAdd;
      _settingsPack = new SettingsFile(_defaultSettings, this);
      _settings = _settingsPack.Settings;
    }

    private void Form1_Shown(object sender, EventArgs e) {
      isErrorVisible = false;
      SetLocationSettings();
    }

    private void SetLocationSettings() {
      if (_settings.ContainsKey("FormTop")) {
        this.Top = _settings["FormTop"].Value.AsInt();
      }
      if (_settings.ContainsKey("FormLeft")) {
        this.Left = _settings["FormLeft"].Value.AsInt();
      }
      if (_settings.ContainsKey("FormHeight")) {
        this.Height = _settings["FormHeight"].Value.AsInt();
      }
      if (_settings.ContainsKey("FormWidth")) {
        this.Width = _settings["FormWidth"].Value.AsInt();
      }
      if (_settings.ContainsKey("AccessToken")) {
        TbAccessToken.Text = _settings["AccessToken"].Value;
      }
      if (_settings.ContainsKey("AccessTokenName")) {
        TbAccessTokenName.Text = _settings["AccessTokenName"].Value;
      }
    }

    private void SaveLocationSettings() {
      if (_settings == null) return;
      if (!_settings.ContainsKey("FormTop")) {
        _settings["FormTop"] = new SettingProperty() { Key = "FormTop", Value = this.Top.AsString() };
      } else {
        _settings["FormTop"].Value = this.Top.AsString();
      }

      if (!_settings.ContainsKey("FormLeft")) {
        _settings["FormLeft"] = new SettingProperty() { Key = "FormLeft", Value = this.Left.AsString() };
      } else {
        _settings["FormLeft"].Value = this.Left.AsString();
      }

      if (!_settings.ContainsKey("FormHeight")) {
        _settings["FormHeight"] = new SettingProperty() { Key = "FormHeight", Value = this.Height.AsString() };
      } else {
        _settings["FormHeight"].Value = this.Height.AsString();
      }

      if (!_settings.ContainsKey("FormWidth")) {
        _settings["FormWidth"] = new SettingProperty() { Key = "FormWidth", Value = this.Width.AsString() };
      } else {
        _settings["FormWidth"].Value = this.Width.AsString();
      }

      if (!_settings.ContainsKey("AccessTokenName")) {
        _settings["AccessTokenName"] = new SettingProperty() { Key = "AccessTokenName", Value = TbAccessTokenName.Text };
      } else {
        _settings["AccessTokenName"].Value = TbAccessTokenName.Text;
      }

      if (!_settings.ContainsKey("AccessToken")) {
        _settings["AccessToken"] = new SettingProperty() { Key = "AccessToken", Value = TbAccessToken.Text };
      } else {
        _settings["AccessToken"].Value = TbAccessToken.Text;
      }

      _settingsPack.Settings = _settings;
      _settingsPack.Save();
    }

    delegate void SetLogMsgCallback(string msg);
    public void LogMsg(string msg) {
      if (this.TextErrorLog.InvokeRequired) {
        SetLogMsgCallback d = new SetLogMsgCallback(LogMsg);
        this.BeginInvoke(d, new object[] { msg });
      } else {
        if (!isErrorVisible) { isErrorVisible = true; }
        if (!TextErrorLog.Visible) TextErrorLog.Visible = true;
        this.TextErrorLog.Text = msg + Environment.NewLine + TextErrorLog.Text;
      }
    }

    delegate void SetProgressLogCallback(string msg);
    public void LogProgress(string msg) {
      if (this.textBox1.InvokeRequired) {
        SetProgressLogCallback d = new SetProgressLogCallback(LogMsg);
        this.BeginInvoke(d, new object[] { msg });
      } else {
        this.textBox1.Text = msg + Environment.NewLine + textBox1.Text;
      }
    }

    private bool isErrorVisible {
      get { return PanelError.Visible; }
      set {
        if (PanelError.Visible != value) { PanelError.Visible = value; }
      }
    }


    private void TabOnOff_SelectedIndexChanged(object sender, EventArgs e) {
      if (TabOnOff.SelectedIndex == 0) {
        // turn off

      } else {
        // save and startup api. 
        ProcessRateLimits();


      }
    }


    private void TabOnOff_Selecting(object sender, TabControlCancelEventArgs e) {
      if (e.TabPageIndex == 0) {

      } else if (e.TabPageIndex == 1) {
        try {
          SaveLocationSettings();
          _followedService = new FollowedService(this, this, _settings);
          e.Cancel = false;
        } catch (Exception ex) {
          LogMsg(ex.Message);
          e.Cancel = true;
        }
      }
    }

    private void HideErrorPanel_Click(object sender, EventArgs e) {
      isErrorVisible = false;
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e) {
      lbTrackBarValue.Text = "Every " + (Convert.ToDecimal((trackBar1.Value + 7.0) / 10.0)).AsStr1P() + " sec or " +
        $"{Convert.ToDecimal(60.0 / ((trackBar1.Value + 7.0) / 10)).AsStr1P()} /min or " +
        $"{Convert.ToDecimal((60.0 * 60.0) / ((trackBar1.Value + 7.0) / 10)).AsStr1P()} /hour or so ";
    }

    private void cbRunTimers_CheckedChanged(object sender, EventArgs e) {
      Countdown = 2 + trackBar1.Value;
      if (cbRunTimers.Checked == true) {
        timer1.Enabled = true;
        ShouldStop = false;
        lbCountdown.Text = (Convert.ToDecimal(Countdown / 10.0)).AsStr1P();
        LogProgress($"Autorun Enabled {DateTime.Now}");
      } else {
        timer1.Enabled = false;
        lbCountdown.Text = "";
        LogProgress($"Autorun Disabled {DateTime.Now}");
      }
    }

    private void timer1_Tick(object sender, EventArgs e) {
      if (cbRunTimers.Checked == true) {
        timer1.Enabled = false;
        try {
          if (Countdown >= 1) {
            Countdown = Countdown - 1;
          } else {
            LogProgress($"Running Next {DateTime.UtcNow} ");
            Countdown = (7 + trackBar1.Value);

            if ((CoreMin <= CoreLast) && (GraphMin <= GraphLast) && (SearchMin <= SearchLast)) {

              string UserLogin = edFollows.Text;
              if (!String.IsNullOrEmpty(UserLogin)) {
                var userToFollow = _followedService.GetUser(UserLogin);
                if (userToFollow != null) {
                  _followedService.Follow(userToFollow);
                  _followedService.GetAllFollowing(userToFollow);
                  userToFollow.FollowStatus = 1;
                  _followedService.Update(userToFollow);
                }
              }
              ProcessRateLimits();
              _followedService.SaveAll();
              btnSetNext_Click(sender, e);

            } else {
              LogMsg($"Ratelimit skip (core, graph, search): {CoreLast} {GraphLast} {SearchLast} < {CoreMin} {GraphMin} {SearchMin}");
            }


          }

          lbCountdown.Text = (Convert.ToDecimal(Countdown / 10.0)).AsStr1P();

        } catch (Exception ex) {
          LogMsg(ex.ToString());
        }
        if (!ShouldStop) {
          timer1.Enabled = true;
        } else {
          cbRunTimers.Checked = false;
        }

      }

    }

    private int CoreMin = 3;
    private int CoreLast = 5;
    private int GraphMin = 3;
    private int GraphLast = 5;
    private int SearchMin = 3;
    private int SearchLast = 5;
    private void ProcessRateLimits() {
      RateLimitResult limitsNow = _followedService.GetRateLimits();
      WriteRateLimits(limitsNow);
    }

    private void WriteRateLimits(RateLimitResult x) {
      label7.Text = "Core " + x.CoreLast.AsString() + " of " +
        x.CoreLimit.AsString() + " per hour";
      label6.Text = "Graph " + x.GraphLast.AsString() + " of " +
        x.GraphLimit.AsString() + " per hour";
      label5.Text = "Search " + x.SearchLast.AsString() + " of " +
        x.SearchLimit.AsString();
      CoreMin = x.CoreMin;
      CoreLast = x.CoreLast;
      GraphMin = x.GraphMin;
      GraphLast = x.GraphLast;
      SearchMin = x.SearchMin;
      SearchLast = x.SearchLast;
    }
    private void btnFollows_Click(object sender, EventArgs e) {
      if (!Importing) {
        btnFollows.Text = "Stop";
        ShouldStop = false;
        Importing = true;

        try {
          if ((CoreMin <= CoreLast) && (GraphMin <= GraphLast) && (SearchMin <= SearchLast)) {

            string UserLogin = edFollows.Text;
            if (!String.IsNullOrEmpty(UserLogin)) {
              var userToFollow = _followedService.GetUser(UserLogin);
              if (userToFollow != null) {
              //  _followedService.Follow(userToFollow);
                _followedService.GetAllFollowing(userToFollow);
                userToFollow.FollowStatus = 1;
                _followedService.Update(userToFollow);                
              }
            }
            ProcessRateLimits();
            _followedService.SaveAll();
            btnSetNext_Click(sender, e);

          } else {
            LogMsg($"Ratelimit skip (core, graph, search): {CoreLast} {GraphLast} {SearchLast} < {CoreMin} {GraphMin} {SearchMin}");
          }

        } catch (Exception ee) {
          LogMsg("Error: " + ee.Message);

        } finally {
          Importing = false;
          ShouldStop = false;          
          pbMain.Maximum = 0;
          btnFollows.Text = "Follows";
        }

      } else {
        btnFollows.Text = "Stopping";
        ShouldStop = true;
        return;
      }

    }

    private void btnSetNext_Click(object sender, EventArgs e) {
      if (lbNextUp.Items.Count > 0) {
        var anItem = lbNextUp.Items[0];
        edFollows.Text = anItem.AsString();
        lbNextUp.Items.Remove(anItem);
      } else {

        var nextBatch = _followedService.GetNextBatch();
        if (nextBatch!= null) { 
          foreach(var userX in nextBatch) { 
            lbNextUp.Items.Add(userX.Login);
          }
        }
        if (lbNextUp.Items.Count > 0) {
          var anItem = lbNextUp.Items[0];
          edFollows.Text = anItem.AsString();
          lbNextUp.Items.Remove(anItem);
        }
      }
    }


  }

  public interface ILogProgress {
    public void LogProgress(string msg);
  }

}