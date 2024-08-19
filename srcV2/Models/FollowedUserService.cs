using FileTables;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheadedFileTables.Models {
  public class FollowedService {
    public Form1 Mainform { get; set; }
    private ILogMsg _logMsg;
    private ILogProgress _logProgress;
    private Settings _settings;    
    private OctoKitHelper _octoKitHelper;
    public FollowedUserFileTable FollowedUserTable { get; set; }

    public LmtSchedule OpSchedule { get; set; }
    public LmtOps LimitedOps;
    public FollowedService(Form1 mainform, Settings settings) {
      if (mainform == null) throw new ArgumentNullException(nameof(mainform));      
      if (settings == null) throw new ArgumentNullException(nameof(settings));
      Mainform = mainform;
      OpSchedule = new LmtSchedule(this, mainform);
      LimitedOps = new LmtOps(this, mainform);
      _logMsg = mainform;
      _logProgress = mainform;
      _settings = settings;            
      FollowedUserTable = new FollowedUserFileTable(CExt.FollowedUserFileName, mainform);
      _logProgress.LogProgress("Followed Users Loaded.  on file: " + FollowedUserTable.Rows.Count().AsString());
      var AccessTokenName = _settings["AccessTokenName"].Value;
      var AccessToken = _settings["AccessToken"].Value;
      _octoKitHelper = new OctoKitHelper(AccessTokenName, AccessToken);
    }


    public void GetRateLimits() {
      RateLimitResult limits = _octoKitHelper.GetRateLimits();
      Mainform.LastRateLimitResult = limits;
      Mainform.ReloadLbSchedule(0);
      _logProgress.LogProgress($"{DateTime.Now} RateCheck");
    }

    public void GetUserLimited(string login) {
      if (_octoKitHelper != null) {
        var octoUser = _octoKitHelper.GetUser(login);
        _logProgress.LogProgress($"{DateTime.Now} GetUser {login}");
        Mainform.MinusOneCoreApprox();
        if (octoUser != null) {
          var followedUser = octoUser.ToFollowedUser();
          if (followedUser != null) {
            FollowedUserTable.AddOp(FuOptype.Insert, followedUser);
          }          
        }
      }      
    }

    public bool CheckIsFollowing(string login) {
      Mainform.MinusOneCoreApprox();      
      bool isFollowing = _octoKitHelper.IsFollowing(login);
      if (isFollowing ) { 
        var dbU = FollowedUserTable.Get(login);
        _logProgress.LogProgress($"{DateTime.Now} CheckFollowing {login} {dbU.FollowCount}");
        if (dbU != null) {
          FollowedUserTable.AddOp(FuOptype.MarkFollowed, dbU);
        }
      }
      return isFollowing;
    }

    public void DoFollowUnfollowed(string login) {
      Mainform.MinusOneCoreApprox();      
      _octoKitHelper.FollowUser(login);      
      var dbU = FollowedUserTable.Get(login);
      if (dbU != null) {
        _logProgress.LogProgress($"{DateTime.Now} Followed {login} {dbU.FollowCount}");
        FollowedUserTable.AddOp(FuOptype.MarkFollowed, dbU);
      }      
    }

    public void DoAddFollowing(string login) {
      Mainform.MinusOneCoreApprox();
      var listUsers = _octoKitHelper.GetFollowing(login);
      if (listUsers != null) {        
        var iX = 8;
        var iMaxAddLimit = 1000;
        
        foreach (var userX in listUsers) {
          var adbu = FollowedUserTable.Get(userX.Login);
          if (adbu != null) {
            adbu.FollowCount = adbu.FollowCount + 1;            
            FollowedUserTable.AddOp(FuOptype.Update, adbu);
          } else {
            var anu = new FollowedUser() {
              Login = userX.Login,
              Id = userX.Id
            };            
            FollowedUserTable.AddOp(FuOptype.Insert, anu);
          }          
          iX++;
          if (iMaxAddLimit < iX) {  // some users have 200K+ users...
            break;
          }
        }
        _logProgress.LogProgress($"{login} Added {listUsers.Count()}");


      }
    }

    public FollowedUser? LookupUser(string login) {
      var dbU = FollowedUserTable.Get(login);
      if (dbU != null) {
        return dbU;
      }
      return null;
    }

    public void SaveAll() {
      try { 
        FollowedUserTable.Save();
        _logProgress.LogProgress($"{DateTime.Now} Saved FollowUsers Table.");
      } catch(Exception ex) {
        _logMsg.LogMsg($"{DateTime.Now} Error: "+ex.Message);
      }
    }

   
    public IEnumerable<FollowedUser>? GetNextBatch() {
      var listUsers = FollowedUserTable.GetNextBatch(5);
      if (listUsers != null) {
        return listUsers;
      }
      return null;
    }


  }
  

}
