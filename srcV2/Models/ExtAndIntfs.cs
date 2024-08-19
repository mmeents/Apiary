using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheadedFileTables.Models {
  public static class CExt {
    public const string CommonPathAdd = "\\PrompterFiles";
    public const string SettingsAdd = "\\ApiarySettings.sft";
    public const string FollowedUserFileNameAdd = "\\FollowsUser.ftx";
    public const string FollowsFileName = "\\Follows.ftx";
    public static string DefaultPath { get{ 
        var DefaultDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + CExt.CommonPathAdd;
        if (!Directory.Exists(DefaultDir)) {
          Directory.CreateDirectory(DefaultDir);
        }
        return DefaultDir;
    } }
    public static string SettingsFileName { get { return DefaultPath + SettingsAdd; } } 
    public static string FollowedUserFileName { get { return DefaultPath + CExt.FollowedUserFileNameAdd; } }
  }

  public static class Ext {
  
    public static string AsStr1P(this decimal x) {
      return String.Format(CultureInfo.InvariantCulture, "{0:0.0}", x);
    }
    public static FollowedUser? ToFollowedUser(this Octokit.User? user) {
      if (user == null) { return null; }
      return new FollowedUser() {
        Id = user?.Id ?? 0,
        Login = user?.Login ?? "",
      };
    }
  }

  public interface ILogProgress {
    public void LogProgress(string msg);
    public void SetProgressBar(int min, int max, int value);

  }


}
