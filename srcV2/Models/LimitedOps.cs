using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheadedFileTables.Models {
  public enum LmtOptype {    
    UpdateRateLimits, 
    GetUser, 
    CheckFollow,
    Follow,
    AddFollowing
  }

  public class LmtSchItem { 
    public long Id { get; set;}
    public LmtOptype Optype { get; set;}
    public string Login { get; set;}
    public LmtSchItem(LmtOptype optype, string login) {
      Id = 0;
      Optype = optype;
      Login = login;
    }
  }

  public class LmtSchedule : ConcurrentDictionary<long, LmtSchItem> {
    public Int64 Nonce = 1;
    public FollowedService Owner;
    public Form1 Mainform;
    public LmtSchedule(FollowedService owner, Form1 mainform) {
      Owner = owner;
      Mainform = mainform;
    }
    public LmtSchItem AddToSchedule(LmtOptype opType, string login) {
      Nonce++;
      var op = new LmtSchItem(opType, login );
      op.Id = Nonce;
      base[Nonce] = op;
      return op;
    }
    public void Remove(Int64 aKey) {
      if (ContainsKey(aKey)) {
        base.TryRemove(aKey, out _);
      }
    }
    public LmtSchItem? Pop() {
      LmtSchItem? aR = null;
      if (Keys.Count > 0) {
        base.TryRemove(base.Keys.OrderBy(x => x).First(), out aR);
      }
      return aR;
    }
  }

  public class LmtOp {
    public long Id = 0;
    private BackgroundWorker BgWorker = new BackgroundWorker();
    private LmtOptype OpType;
    private string _login;
    private bool _addFollowing;
    public LmtOps Owner { get; set; }
    public LmtOp(LmtOps owner, long id, LmtOptype optype, string login, bool AddFollowing) {
      Id = id;
      Owner = owner;
      OpType = optype;
      _login = login;
      _addFollowing = AddFollowing;      
      BgWorker.DoWork += DoWorkHandeler;
      BgWorker.RunWorkerCompleted += DoWorkCompleteHandeler;
      BgWorker.RunWorkerAsync();
    }
    private void DoWorkCompleteHandeler(object? sender, RunWorkerCompletedEventArgs e) {
      try { 
        Owner.Owner.Mainform.ReloadLbSchedule(0);
      } catch (Exception ex) { 
        Owner.Owner.Mainform.LogMsg($"{DateTime.Now} Error0 {ex.Message}");
      }
      BgWorker.Dispose();
      Owner.Remove(this.Id);
    }
    private void DoWorkHandeler(object? sender, DoWorkEventArgs e) {
      try { 
        switch (OpType) {
          case LmtOptype.UpdateRateLimits:
            Owner.Owner.GetRateLimits();          
            break;
          case LmtOptype.GetUser:
            Owner.Owner.GetUserLimited(_login);
            Owner.Owner.OpSchedule.AddToSchedule(LmtOptype.CheckFollow, _login);          
            if (_addFollowing) {
              Owner.Owner.OpSchedule.AddToSchedule(LmtOptype.AddFollowing, _login);            
            }          
            break;
          case LmtOptype.CheckFollow:
            if (!Owner.Owner.CheckIsFollowing(_login)) {             
              Owner.Owner.OpSchedule.AddToSchedule(LmtOptype.Follow, _login);
            }            
            break;
          case LmtOptype.Follow:
            Owner.Owner.DoFollowUnfollowed(_login);
            break;
          case LmtOptype.AddFollowing:
            Owner.Owner.DoAddFollowing(_login);
            break;        
        }
      } catch (Exception ex) {
        Owner.Mainform.LogMsg($"{DateTime.Now} Error1 {ex.Message}");
      }
    }

    public string OpLabel { get {
        string opLbl = "";
        switch (OpType) { 
          case LmtOptype.UpdateRateLimits: opLbl = "UpdateRateLimits"; break; 
          case LmtOptype.GetUser: opLbl = "GetUser"; break;
          case LmtOptype.CheckFollow: opLbl = "CheckFollow"; break;
          case LmtOptype.Follow: opLbl = "Follow"; break;
          case LmtOptype.AddFollowing: opLbl = "AddFollowing"; break;
        }
      return opLbl+ " " + _login; 
    } }
  }
  public class LmtOps : ConcurrentDictionary<long, LmtOp> {
    public Int64 Nonce = 1;
    public FollowedService Owner;
    public Form1 Mainform;
    public LmtOps(FollowedService owner, Form1 mainform) {
      Owner = owner;
      Mainform = mainform;
    }
    public LmtOp AddOp(LmtOp op) {
      Nonce++;
      op.Id = Nonce;
      base[Nonce] = op;
      return op;
    }
    public void Remove(Int64 aKey) {
      if (ContainsKey(aKey)) {
        base.TryRemove(aKey, out _);        
      }
    }
  }

}
