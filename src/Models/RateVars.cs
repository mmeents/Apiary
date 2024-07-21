using System;
using Octokit;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiary.Models {

  public class RateLimitResult { 
    public bool HasResult { get; set; }

    public RateLimit? Core { get; set; } = null;
    public RateLimit? Graph { get; set; } = null;
    public RateLimit? Search { get; set; } = null;

    public int CoreLimit { get; set; } = 0;
    public int CoreLast { get; set; } = 0;
    public int CoreMin { get; set; } = 3;    

    public int SearchLimit { get; set; } = 0;
    public int SearchLast { get; set; } = 0;
    public int SearchMin { get; set; } = 3;

    public int GraphLimit { get; set; } = 0;
    public int GraphLast { get; set; } = 0;
    public int GraphMin { get; set; } = 3;


    public RateLimitResult(MiscellaneousRateLimit? value ) {
      HasResult = value != null;
      if ( HasResult ) {
        var rl2 = value.Resources;
        Core = rl2.Core;
        Graph = rl2.Graphql;
        Search = rl2.Search;

        CoreMin = 3;
        CoreLast = Core.Remaining;
        CoreLimit = Core.Limit;
        GraphMin = 3;
        GraphLast = Graph.Remaining;
        GraphLimit = Graph.Limit;
        SearchMin = 3;
        SearchLast = Search.Remaining;
        SearchLimit = Search.Limit;
      }

    }
  }     
}
