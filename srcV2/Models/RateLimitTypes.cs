using Octokit;

namespace TheadedFileTables.Models {

  public class SiteRateStatus {
    public int CoreLimit { get; set; } = 5;
    public int CoreLast { get; set; } = 5;
    public int CoreMin { get; set; } = 3;
    public int SearchLimit { get; set; } = 5;
    public int SearchLast { get; set; } = 5;
    public int SearchMin { get; set; } = 3;
    public int GraphLimit { get; set; } = 5;
    public int GraphLast { get; set; } = 5;
    public int GraphMin { get; set; } = 3;
    public SiteRateStatus(ResourceRateLimit? aRL) {      
      if (aRL == null) return;
      CoreLast = aRL.Core.Remaining;
      CoreLimit = aRL.Core.Limit;      
      GraphLast = aRL.Graphql.Remaining;
      GraphLimit = aRL.Graphql.Limit;      
      SearchLast = aRL.Search.Remaining;
      SearchLimit = aRL.Search.Limit;
    }

    public bool IsWithinLimits { get { 
        return (CoreMin <= CoreLast) && (GraphMin <= GraphLast) && (SearchMin <= SearchLast); 
    } }
    public string AsSkipMsg { get { 
        return $"Ratelimit skip (core, graph, search): {CoreLast} {GraphLast} {SearchLast} < {CoreMin} {GraphMin} {SearchMin}";
    } }

  }
  public class RateLimitResult : SiteRateStatus {
    public bool HasResult { get; set; }
    public RateLimit? Core { get; set; } = null;
    public RateLimit? Graph { get; set; } = null;
    public RateLimit? Search { get; set; } = null;
    public RateLimitResult(MiscellaneousRateLimit? value) : base (value?.Resources) {
      HasResult = value != null;
      if (HasResult) {
        var rl2 = value.Resources;
        Core = rl2.Core;
        Graph = rl2.Graphql;
        Search = rl2.Search;
      }
    }
  }


}
