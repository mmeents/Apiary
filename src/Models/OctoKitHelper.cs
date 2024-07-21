using System;
using Octokit;
using User = Octokit.User;

namespace apiary.Models {
  public class OctoKitHelper {

    private ProductHeaderValue _ProductHeader { get; set; }
    private GitHubClient _GitHubClient { get; set; }
    public RateLimitResult? LastRateLimit { get; set; }

    public OctoKitHelper(string GithubAccessKeyName, string GithubAccessKeyValue) { 
      _ProductHeader = new ProductHeaderValue(GithubAccessKeyName);
      _GitHubClient = new GitHubClient(_ProductHeader) {
        Credentials = new Credentials(GithubAccessKeyValue)
      };
    }

    public RateLimitResult GetRateLimits() { 
      var rlc = _GitHubClient.RateLimit;
      var rl = Task.Run(async () => await rlc.GetRateLimits().ConfigureAwait(false)).GetAwaiter().GetResult();
      LastRateLimit = new RateLimitResult(rl);
      return LastRateLimit;
    }

    public User? GetUser(string login) {
      var uc = _GitHubClient.User;
      if (uc != null) { 
        User ar = Task.Run(async () => await uc.Get(login).ConfigureAwait(false)).GetAwaiter().GetResult();
        _ = GetRateLimits();
        return ar;
      }
      return null;
    }

    public bool IsFollowing (string login) {
      var uc = _GitHubClient.User;
      if (uc != null) {
        var ufc = uc.Followers;
        if (ufc != null) {
          var isfollowing = Task.Run(async () => await ufc.IsFollowingForCurrent(login).ConfigureAwait(false)).GetAwaiter().GetResult();
          return isfollowing;
        }
      }
      return false;
    }

    public bool FollowUser(string login) {
      var uc = _GitHubClient.User;
      if (uc != null) {
        var ufc = uc.Followers;
        if (ufc != null) {
          var followResult = Task.Run(async () => await ufc.Follow(login).ConfigureAwait(false)).GetAwaiter().GetResult();
          return followResult;
        }
      }
      return false;
    }

    public IReadOnlyList<User>? GetFollowing( string login) {
      var uc = _GitHubClient.User;
      if (uc != null) {
        var ufc = uc.Followers;
        if (ufc != null) {
          var listUsers = Task.Run(async () => await ufc.GetAllFollowing(login).ConfigureAwait(false)).GetAwaiter().GetResult();
          return listUsers;
        }
      }
      return null;
    }






  }
}
