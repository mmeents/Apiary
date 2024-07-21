using FileTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace apiary.Models {


  public class FollowedUser {
    public FollowedUser() { }
    public long Uid { get; set; } = 0;  // this is for the row ID.
    public int FollowCount { get; set; } = 0;
    public int FollowStatus { get; set; } = 0;
    public long Id { get; set; } = 0;
    public string Login { get; set; } = "";
  }

  public static class FExt { 
    public static FollowedUser? ToFollowedUser(this Octokit.User? user) {
      if (user == null) { return null; }
      return new FollowedUser() {
        Id = user?.Id ?? 0,        
        Login = user?.Login ?? "",        
      };
    }
  }

  public class FollowedUserFileTable {
    private readonly FileTable _table;
    public Columns Columns { get { return _table.Columns; } }
    public Rows Rows { get { return _table.Rows; } }
    public FollowedUserFileTable(string fileName) {
      _table = new FileTable(fileName);
      _table.Active = true;
      if (_table.Columns.Count() == 0) {
        _table.AddColumn("Uid", ColumnType.Int64);
        _table.AddColumn("FollowCount", ColumnType.Int32);
        _table.AddColumn("FollowStatus", ColumnType.Int32);
        _table.AddColumn("Id", ColumnType.Int64);
        _table.AddColumn("Login", ColumnType.String);
      }
    }
    public FollowedUser? Get(long Uid) {
      if (_table.Rows.Contains(Uid)) {
        return new FollowedUser() {
          Uid = _table.Rows[Uid]["Uid"].Value.AsInt64(),
          FollowCount = _table.Rows[Uid]["FollowCount"].Value.AsInt32(),
          FollowStatus = _table.Rows[Uid]["FollowStatus"].Value.AsInt32(),
          Id = _table.Rows[Uid]["Id"].Value.AsInt64(),
          Login = _table.Rows[Uid]["Login"].Value,
        };
      } else { return null; }
    }

    public FollowedUser? Get(string login) {
      var aresult = Rows.Select(x => x.Value)
        .Where(x => x["Login"].Value == login)
        .OrderByDescending(x => x["Uid"].Value.AsInt64())
        .Take(1);
      if (aresult.Any()) {
        FollowedUser? result = null;
        foreach (var row in aresult) {
          if (row != null) {
            result = new FollowedUser() {
              Uid = row["Uid"].Value.AsInt64(),
              FollowCount = row["FollowCount"].Value.AsInt32(),
              FollowStatus = row["FollowStatus"].Value.AsInt32(),
              Id = row["Id"].Value.AsInt64(),
              Login = row["Login"].Value
            };
          }
          break;
        }
        return result;
      }
      return null;
    }


    public IEnumerable<FollowedUser> GetNextBatch(int batchCount) {
      var aresult = Rows.Select(x => x.Value)
        .Where(x => x["FollowStatus"].Value == "0")
        .OrderByDescending(x => x["FollowCount"].Value.AsInt32())
        .Take(batchCount);
      if (aresult.Any()) {
        List<FollowedUser> result = new List<FollowedUser>();
        foreach (var row in aresult) {
          result.Add(new FollowedUser() {
            Uid = row["Uid"].Value.AsInt64(),
            FollowCount = row["FollowCount"].Value.AsInt32(),
            FollowStatus = row["FollowStatus"].Value.AsInt32(),
            Id = row["Id"].Value.AsInt64(),
            Login = row["Login"].Value
          });
        }
        return result;
      }
      return null;
    }




    public long Insert(FollowedUser item) {
      long RowKey = _table.AddRow();
      _table.Rows[RowKey]["Uid"].Value = RowKey.AsString();
      _table.Rows[RowKey]["FollowCount"].Value = item.FollowCount.AsString();
      _table.Rows[RowKey]["FollowStatus"].Value = item.FollowStatus.AsString();
      _table.Rows[RowKey]["Id"].Value = item.Id.AsString();
      _table.Rows[RowKey]["Login"].Value = item.Login;
      return RowKey;
    }
    public void Update(FollowedUser item) {
      long RowKey = item.Uid;
      _table.Rows[RowKey]["Uid"].Value = item.Uid.AsString();
      _table.Rows[RowKey]["FollowCount"].Value = item.FollowCount.AsString();
      _table.Rows[RowKey]["FollowStatus"].Value = item.FollowStatus.AsString();
      _table.Rows[RowKey]["Id"].Value = item.Id.AsString();
      _table.Rows[RowKey]["Login"].Value = item.Login;      
    }
    public void Delete(FollowedUser item) {
      long RowKey = item.Uid;
      _table.Rows.Remove(RowKey, out Row? _);      
    }
    public void Save() {
      _table.Save();
    }
  }

}
