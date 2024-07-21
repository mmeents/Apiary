using FileTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiary.Models {

  public static class CExt { 
    public const string CommonPathAdd = "\\PrompterFiles";
    public const string SettingsAdd = "\\ApiarySettings.sft";
    public const string FollowedUserFileName = "\\FollowsUser.ftx";
    public const string FollowsFileName = "\\Follows.ftx";
  }

  public class FollowedService {  
    private ILogMsg _logMsg;
    private ILogProgress _logProgress;
    private Settings _settings;
    private string _defaultDir; 
    private FollowedUserFileTable _followedUserFileTable;
    private OctoKitHelper _octoKitHelper;
    public string DefaultDir { get{ return _defaultDir; } }
    public string FollowedUserFileName { get { return _defaultDir + CExt.FollowedUserFileName;} }
    public FollowedService(ILogMsg logMeg, ILogProgress logProgress, Settings settings) {      
      _logMsg = logMeg;
      _logProgress = logProgress;
      _settings = settings;
      _defaultDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + CExt.CommonPathAdd;
      if (!Directory.Exists(DefaultDir)) {
        Directory.CreateDirectory(DefaultDir);
      }
      _logProgress.LogProgress("Default Dir: '"+DefaultDir+"'");
      _followedUserFileTable = new FollowedUserFileTable(FollowedUserFileName);
      _logProgress.LogProgress("Followed Users Loaded.  on file: "+ _followedUserFileTable.Rows.Count().AsString() );      
      var AccessTokenName = _settings["AccessTokenName"].Value;
      var AccessToken = _settings["AccessToken"].Value;
      _octoKitHelper = new OctoKitHelper(AccessTokenName, AccessToken);
    }

    public FollowedUser? GetUser(string username) {
      var dbU = _followedUserFileTable.Get(username);
      if (dbU != null) {
        return dbU;
      }
      if (_octoKitHelper != null) {
        var octoUser = _octoKitHelper.GetUser(username);
        if (octoUser != null) {
          var followedUser = octoUser.ToFollowedUser();
          if (followedUser != null) { 
            var AddedUid = _followedUserFileTable.Insert(followedUser);
            var resultUser = _followedUserFileTable.Get(AddedUid);
            return resultUser;
          }
        }
      }
      return null;
    }

    public void Update(FollowedUser? user) {
      if (user == null) return;
      _followedUserFileTable.Update(user);
    }

    public void Follow(FollowedUser user) { 
      if (user != null) {
        if (! _octoKitHelper.IsFollowing(user.Login)){
          _octoKitHelper.FollowUser(user.Login);          
        }
      }
    }

    public void GetAllFollowing(FollowedUser user) { 
      var listUsers = _octoKitHelper.GetFollowing(user.Login);
      if (listUsers != null) { 
        _logProgress.LogProgress($"{user.Login} following {listUsers.Count()}");
        foreach(var userX in listUsers) {
          var adbu = _followedUserFileTable.Get(userX.Login);
          if (adbu != null) {
            adbu.FollowCount = adbu.FollowCount + 1;
            _followedUserFileTable.Update(adbu);
          } else { 
            var anu = new FollowedUser() { 
              Login = userX.Login,
              Id = userX.Id
            };
            _followedUserFileTable.Insert(anu);
          }
        }
      }

    }
    public void SaveAll() { 
      _followedUserFileTable.Save();
    }

    public RateLimitResult GetRateLimits() { 
      RateLimitResult limits = _octoKitHelper.GetRateLimits();
      return limits;      
    }

    public IEnumerable<FollowedUser>? GetNextBatch() { 
      var listUsers = _followedUserFileTable.GetNextBatch(5);
      if (listUsers != null) {
        return listUsers;
      }
      return null;
    }


  }

}
