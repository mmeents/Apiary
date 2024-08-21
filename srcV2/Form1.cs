using FileTables;
using Microsoft.VisualBasic.Logging;
using Octokit;
using System.Text;
using TheadedFileTables.Models;
using static TheadedFileTables.Models.FollowedUserFileTable;

namespace TheadedFileTables {
  public partial class Form1 : Form, ILogMsg, ILogProgress {
    private SiteRateStatus _RateStatus = new SiteRateStatus(null);
    private SettingsFile _settingsPack { get; set; }
    private Settings _settings;
    private FollowedService _followedService;

    private int MaxCountdown = 0;
    private int Countdown = 0;
    private int RateRefreshCountdownStart = 1;
    private int RateRefreshCountdown = 1;

    private bool IsSaving = false;
    private bool ShouldStop = true;
    private bool InResize = false;
    private bool Importing = false;
    private bool FirstRateScheduled = false;
    public RateLimitResult? LastRateLimitResult = null;
    public Form1() {
      InitializeComponent();
      SplitMain.Panel2Collapsed = true;
      LabelStorageLocation.Text = CExt.DefaultPath;
      _settingsPack = new SettingsFile(CExt.SettingsFileName, this);
      _settings = _settingsPack.Settings;
      _followedService = new FollowedService(this, _settings);
    }

    private void Form1_Shown(object sender, EventArgs e) {
      SetLocationSettings();
    }
    private void Form1_Resize(object sender, EventArgs e) {
    }

    delegate void SetLogMsgCallback(string msg);
    public void LogMsg(string msg) {
      if (this.TextErrorLog.InvokeRequired) {
        SetLogMsgCallback d = new(LogMsg);
        this.BeginInvoke(d, new object[] { msg });
      } else {
        if (SplitMain.Panel2Collapsed) { SplitMain.Panel2Collapsed = false; }
        if (!TextErrorLog.Visible) TextErrorLog.Visible = true;
        this.TextErrorLog.Text = msg + Environment.NewLine + TextErrorLog.Text;
        if (!ShouldStop) { ShouldStop = true; }
      }
    }

    delegate void SetProgressLogCallback(string msg);
    public void LogProgress(string msg) {
      if (this.textBox1.InvokeRequired) {
        SetProgressLogCallback d = new(LogProgress);
        this.BeginInvoke(d, new object[] { msg });
      } else {
        StringBuilder sb = new StringBuilder();
        var lines = textBox1.Text.Split(Environment.NewLine);
        var lineCount = lines.Length;
        sb.AppendLine(msg);
        for(var i =0; i<200; i++) {
          if (i <= lineCount - 1) {
            sb.AppendLine(lines[i]);
          } else { 
            break;
          }          
        }
        this.textBox1.Text = sb.ToString();
      }
    }

    delegate void SetProgressBarCallback(int min, int max, int value);
    public void SetProgressBar(int min, int max, int value) {
      if (this.pbMain.InvokeRequired) {
        SetProgressBarCallback d = new(SetProgressBar);
        this.BeginInvoke(d, new object[] { min, max, value });
      } else {
        if (value == 0) {
          if (pbMain.Visible) {
            pbMain.Visible = false;
          }
          pbMain.Minimum = value > min ? min : value;
          pbMain.Maximum = value < max ? max : value;
          pbMain.Value = value;
        } else {
          pbMain.Minimum = value > min ? min : value;
          pbMain.Maximum = value < max ? max : value;
          pbMain.Value = value;
          if (!pbMain.Visible) {
            pbMain.Visible = true;
          }
        }
      }
    }

    delegate void SetReloadSchedule(int unused);
    public void ReloadLbSchedule(int unused) {
      if (lbSchedule.InvokeRequired) {
        SetReloadSchedule d = new(ReloadLbSchedule);
        this.BeginInvoke(d, new Object[] { 0 });
      } else {
        try {
          
          lbSchedule.Items.Clear();
          var res0 = _followedService.OpSchedule.Values.ToList();
          foreach (var schedtask in res0) {            
            lbSchedule.Items.Add(schedtask.Optype.AsString() + " " + schedtask.Login);
          }

          lbFtQueue.Items.Clear();          
          var todoCount = _followedService.FollowedUserTable.ToDoQueue.Keys.Count;
          var keys = _followedService.FollowedUserTable.ToDoQueue.Keys.ToList();
          if (todoCount-1 >= 0 ) {
            var op = _followedService.FollowedUserTable.ToDoQueue[keys[0]];
            lbFtQueue.Items.Add(op.OperationType + " " + op.User?.Login ?? "");
          }
          if (todoCount - 1 >= 1) {
            var op = _followedService.FollowedUserTable.ToDoQueue[keys[1]];
            lbFtQueue.Items.Add(op.OperationType + " " + op.User?.Login ?? "");
          }
          if (todoCount - 1 >= 2) {
            var op = _followedService.FollowedUserTable.ToDoQueue[keys[2]];
            lbFtQueue.Items.Add(op.OperationType + " " + op.User?.Login ?? "");
          }
          if (todoCount - 1 >= 3) {
            var op = _followedService.FollowedUserTable.ToDoQueue[keys[3]];
            lbFtQueue.Items.Add(op.OperationType + " " + op.User?.Login ?? "");
          }
          

          lbLmtQueue.Items.Clear();
          var res2 = _followedService.LimitedOps.Values.ToList();
          foreach (var op in res2) {
            lbLmtQueue.Items.Add(op.OpLabel);
          }

          if (LastRateLimitResult != null) {
            WriteRateLimits(LastRateLimitResult);
          }
        } catch (Exception ex) {
          // skip stopping things on a drawing error...
        }
      }
    }



    delegate void SetSyncSaveFollows(int unused);

    public void SyncSaveFollows(int unused) {
      if (this.TextErrorLog.InvokeRequired) {
        SetSyncSaveFollows d = new(SyncSaveFollows);
        this.BeginInvoke(d, new object[] { 0 });
      } else {
        if (!IsSaving) {
          IsSaving = true;
          _followedService.SaveAll();
          IsSaving = false;
        }
      }
    }

    private void SetLocationSettings() {
      if (_settings == null) return;
      if (InResize) return;
      InResize = true;
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
      InResize = false;
    }

    private void SaveLocationSettings() {
      if (_settings == null) return;
      if (InResize) return;

      InResize = true;
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
      InResize = false;

    }

    private void HideErrorPanel_Click(object sender, EventArgs e) {
      SplitMain.Panel2Collapsed = true;
    }

    private void trackBar1_ValueChanged(object sender, EventArgs e) {
      lbTrackBarValue.Text = "Every " + (Convert.ToDecimal((trackBar1.Value + 7.0) / 10.0)).AsStr1P() + " sec or " +
        $"{Convert.ToDecimal(60.0 / ((trackBar1.Value + 7.0) / 10)).AsStr1P()} /min or " +
        $"{Convert.ToDecimal((60.0 * 60.0) / ((trackBar1.Value + 7.0) / 10)).AsStr1P()} /hour or so ";
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
      e.Cancel = false;
      SaveLocationSettings();
    }
    private void cbRunTimers_CheckedChanged(object sender, EventArgs e) {
      Countdown = 2 + trackBar1.Value;
      MaxCountdown = Countdown;
      if (cbRunTimers.Checked == true) {
        TimerRateLimit.Enabled = true;
        TimerFollowTblUpdates.Enabled = true;
        ShouldStop = false;
        lbCountdown.Text = (Convert.ToDecimal(Countdown / 10.0)).AsStr1P();
        SetProgressBar(0, MaxCountdown, Countdown);
        LogProgress($"Autorun Enabled {DateTime.Now}");
      } else {
        TimerRateLimit.Enabled = false;
        TimerFollowTblUpdates.Enabled = false;
        lbCountdown.Text = "";
        LogProgress($"Autorun Disabled {DateTime.Now}");
        SetProgressBar(0, MaxCountdown, 0);
      }
    }

    private void TimerFollowTblUpdates_Tick(object sender, EventArgs e) {
      try { 
        if (_followedService.FollowedUserTable.ToDoQueue.Count > 0) { 
          if (Countdown > 7) { 
            _followedService.FollowedUserTable.ProcessScheduledOps(25);
          }
        }
      } catch (Exception ex) {
        LogMsg($"{DateTime.Now} Error "+ex.ToString());
      }
    }
    private void TimerRateLimit_Tick(object sender, EventArgs e) {
      if (cbRunTimers.Checked == true) {
        TimerRateLimit.Enabled = false;
        try {
          if (Countdown >= 1) {
            Countdown = Countdown - 1;
            SetProgressBar(0, MaxCountdown, Countdown);
            if (Countdown == 6) {
              if (_followedService.OpSchedule.Count == 0) {

                if (!FirstRateScheduled) {
                  _followedService.OpSchedule.AddToSchedule(LmtOptype.UpdateRateLimits, "");
                  FirstRateScheduled = true;
                }
                if (edFollows.Text == "") {
                  btnSetNext_Click(sender, e);
                }
                string login = edFollows.Text;
                var user = _followedService.LookupUser(login);
                if (user == null) {
                  LogProgress($"{DateTime.UtcNow} Scheduling {login}");
                  _followedService.OpSchedule.AddToSchedule(LmtOptype.GetUser, login);
                } else {
                  LogProgress($"{DateTime.UtcNow} Scheduling {login}");
                  _followedService.OpSchedule.AddToSchedule(LmtOptype.CheckFollow, login);
                  if (cbAddFollows.Checked) {
                    _followedService.OpSchedule.AddToSchedule(LmtOptype.AddFollowing, login);
                  }
                }
                if (RateRefreshCountdown > 0) {
                  RateRefreshCountdown--;
                } else {
                  _followedService.OpSchedule.AddToSchedule(LmtOptype.UpdateRateLimits, "");
                  _followedService.FollowedUserTable.AddOp(FuOptype.Save, null);
                  RateRefreshCountdown = RateRefreshCountdownStart;
                }
                btnSetNext_Click(sender, e);                
              }
            }
          } else {
            Countdown = (7 + trackBar1.Value);
            MaxCountdown = Countdown;
            if (_RateStatus.IsWithinLimits) {
              var Scheduled = _followedService.OpSchedule.Pop();
              if (Scheduled != null){
                _followedService.LimitedOps.AddOp(new LmtOp(_followedService.LimitedOps, 0, Scheduled.Optype, Scheduled.Login, cbAddFollows.Checked));
              } else {
                LogMsg($"{DateTime.UtcNow} Skipping Nothing Scheduled");
              }
            } else {
              LogMsg(_RateStatus.AsSkipMsg);
            }
          }

          lbCountdown.Text = (Convert.ToDecimal(Countdown / 10.0)).AsStr1P();
          ReloadLbSchedule(0);

        } catch (Exception ex) {
          SetProgressBar(0, 10, 0);
          LogMsg(ex.ToString());
        }
        if (!ShouldStop) {
          TimerRateLimit.Enabled = true;
        } else {
          cbRunTimers.Checked = false;
        }
        SetProgressBar(0, 10, 0);
      }
    }



    private void btnSchedule_Click(object sender, EventArgs e) {
      ShouldStop = false;
      Importing = true;
      btnSchedule.Enabled = true;
      try {
        if (_RateStatus.IsWithinLimits) {

          if (!FirstRateScheduled) {
            _followedService.OpSchedule.AddToSchedule(LmtOptype.UpdateRateLimits, "");
            FirstRateScheduled = true;
          }
          if (edFollows.Text == "") {
            btnSetNext_Click(sender, e);
          }

          string login = edFollows.Text;
          var user = _followedService.LookupUser(login);
          if (user == null) {
            LogProgress($"{DateTime.UtcNow} Scheduling {login}");
            _followedService.OpSchedule.AddToSchedule(LmtOptype.GetUser, login);
          } else {
            LogProgress($"{DateTime.UtcNow} Scheduling {login}");
            _followedService.OpSchedule.AddToSchedule(LmtOptype.CheckFollow, login);
            if (cbAddFollows.Checked) {
              _followedService.OpSchedule.AddToSchedule(LmtOptype.AddFollowing, login);
            }
          }

          btnSetNext_Click(sender, e);

        } else {
          LogMsg(_RateStatus.AsSkipMsg);
        }

      } catch (Exception ee) {
        LogMsg("Error: " + ee.Message);

      } finally {
        Importing = false;
        ShouldStop = false;
        pbMain.Maximum = 0;
        btnSchedule.Enabled = true;
        ReloadLbSchedule(0);
      }
    }

    private void btnRunSchd_Click(object sender, EventArgs e) {

      ShouldStop = false;
      Importing = true;
      btnRunSchd.Enabled = false;
      if (_followedService.OpSchedule.Count == 0) {
        if (!FirstRateScheduled) {
          _followedService.OpSchedule.AddToSchedule(LmtOptype.UpdateRateLimits, "");
          FirstRateScheduled = true;
        }
        if (edFollows.Text == "") {
          btnSetNext_Click(sender, e);
        }
        string login = edFollows.Text;
        if (!String.IsNullOrEmpty(login)) {
          var user = _followedService.LookupUser(login);
          if (user == null) {
            LogProgress($"{DateTime.UtcNow} Scheduling {login}");
            _followedService.OpSchedule.AddToSchedule(LmtOptype.GetUser, login);
          } else {
            LogProgress($"{DateTime.UtcNow} Scheduling {login}");
            _followedService.OpSchedule.AddToSchedule(LmtOptype.CheckFollow, login);
            if (cbAddFollows.Checked) {
              _followedService.OpSchedule.AddToSchedule(LmtOptype.AddFollowing, login);
            }
          }
          btnSetNext_Click(sender, e);
        }
      }

      try {

        if (_RateStatus.IsWithinLimits) {

          var Scheduled = _followedService.OpSchedule.Pop();
          if (Scheduled != null) {
            _followedService.LimitedOps.AddOp(new LmtOp(_followedService.LimitedOps, 0, Scheduled.Optype, Scheduled.Login, cbAddFollows.Checked));
          } else {
            LogMsg($"{DateTime.UtcNow} Skipping Nothing Scheduled");
          }

        } else {
          LogMsg(_RateStatus.AsSkipMsg);
        }

      } catch (Exception ee) {
        LogMsg("Error: " + ee.Message);

      } finally {
        Importing = false;
        ShouldStop = false;
        pbMain.Maximum = 0;
        btnRunSchd.Enabled = true;
        ReloadLbSchedule(0);
      }

    }

    private void btnSetNext_Click(object sender, EventArgs e) {
      if (lbNextUp.Items.Count > 0) {
        var anItem = lbNextUp.Items[0];
        edFollows.Text = anItem.AsString();
        lbNextUp.Items.Remove(anItem);
      } else {

        var nextBatch = _followedService.GetNextBatch();
        if (nextBatch != null) {
          foreach (var userX in nextBatch) {
            lbNextUp.Items.Add(userX.Login);
          }
        }
        if (lbNextUp.Items.Count > 0) {
          var anItem = lbNextUp.Items[0];
          edFollows.Text = anItem.AsString();
          lbNextUp.Items.Remove(anItem);
        } else {
          edFollows.Text = "";
        }
      }
    }

    public void MinusOneCoreApprox() {
      _RateStatus.CoreLast = _RateStatus.CoreLast - 1;
    }
    public void WriteRateLimits(RateLimitResult x) {
      int FileTableTodo = _followedService.FollowedUserTable.ToDoQueue.Count;
      label7.Text = "Core " + x.CoreLast.AsString() + " of " + x.CoreLimit.AsString() + " per hour; FileTblTodo: " + FileTableTodo.AsString();
      label6.Text = "Graph " + x.GraphLast.AsString() + " of " + x.GraphLimit.AsString() + " per hour ProtectedTodo: " + _followedService.OpSchedule.Count.AsString();
      label5.Text = "Search " + x.SearchLast.AsString() + " of " + x.SearchLimit.AsString();
      _RateStatus.CoreMin = x.CoreMin;
      _RateStatus.CoreLast = x.CoreLast;
      _RateStatus.GraphMin = x.GraphMin;
      _RateStatus.GraphLast = x.GraphLast;
      _RateStatus.SearchMin = x.SearchMin;
      _RateStatus.SearchLast = x.SearchLast;
    }

    private void btnManualRefresh_Click(object sender, EventArgs e) {
      ReloadLbSchedule(0);
    }

    private void btnManualSave_Click(object sender, EventArgs e) {
      _followedService.FollowedUserTable.ToDoQueue.Clear();
      _followedService.LimitedOps.Clear();
      SyncSaveFollows(0);
      ReloadLbSchedule(0);
    }


  }
}