using FileTables;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheadedFileTables.Models {
  public static class Fc {   // column names
    public static string Uid { get { return "Uid";} }
    public static string FollowCount { get { return "FollowCount"; } }
    public static string FollowStatus { get { return "FollowStatus"; } }
    public static string Id { get { return "Id"; } }
    public static string Login { get { return "Login"; } }
  }
  public class FollowedUser {  // Followed User Entity record 
    public FollowedUser() { }
    public long Uid { get; set; } = 0;  // this is for the row ID.
    public int FollowCount { get; set; } = 0;   // When adding if it's here then inc count, get next batch order by
    public int FollowStatus { get; set; } = 0;  // Are we following?
    public long Id { get; set; } = 0;  // Github User Id
    public string Login { get; set; } = "";  // Github Login name
  }  


  public class FollowedUserFileTable {
    private readonly FileTable _table;
    public bool NeedsSave = false;
    public Columns Columns { get { return _table.Columns; } }
    public Rows Rows { get { return _table.Rows; } }
    public FuOps ToDoQueue{ get; set; }
    public Form1 Mainform { get; set; }
    public FollowedUserFileTable(string fileName, Form1 mainform) {
      _table = new FileTable(fileName);
      _table.Active = true;
      if (_table.Columns.Count() == 0) {
        _table.AddColumn(Fc.Uid, ColumnType.Int64);
        _table.AddColumn(Fc.FollowCount, ColumnType.Int32);
        _table.AddColumn(Fc.FollowStatus, ColumnType.Int32);
        _table.AddColumn(Fc.Id, ColumnType.Int64);
        _table.AddColumn(Fc.Login, ColumnType.String);
      }
      ToDoQueue = new FuOps(this);
      Mainform = mainform;
    }
    public FollowedUser? Get(long Uid) {
      if (_table.Rows.Contains(Uid)) {
        return new FollowedUser() {
          Uid = _table.Rows[Uid][Fc.Uid].Value.AsInt64(),
          FollowCount = _table.Rows[Uid][Fc.FollowCount].Value.AsInt32(),
          FollowStatus = _table.Rows[Uid][Fc.FollowStatus].Value.AsInt32(),
          Id = _table.Rows[Uid][Fc.Id].Value.AsInt64(),
          Login = _table.Rows[Uid][Fc.Login].Value,
        };
      } else { return null; }
    }

    public FollowedUser? Get(string login) {
      var aresult = Rows.Select(x => x.Value)
        .Where(x => x[Fc.Login].Value == login)
        .OrderByDescending(x => x[Fc.Uid].Value.AsInt64())
        .Take(1);
      if (aresult.Any()) {
        FollowedUser? result = null;
        foreach (var row in aresult) {
          if (row != null) {
            result = new FollowedUser() {
              Uid = row[Fc.Uid]?.Value.AsInt64() ?? 0,
              FollowCount = row[Fc.FollowCount].Value.AsInt32(),
              FollowStatus = row[Fc.FollowStatus].Value.AsInt32(),
              Id = row[Fc.Id].Value.AsInt64(),
              Login = row[Fc.Login].Value
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
        .Where(x => x[Fc.FollowStatus].Value == "0")
        .OrderByDescending(x => x[Fc.FollowCount].Value.AsInt32())
        .Take(batchCount);
      if (aresult.Any()) {
        List<FollowedUser> result = new List<FollowedUser>();
        foreach (var row in aresult) {
          result.Add(new FollowedUser() {
            Uid = row[Fc.Uid].Value.AsInt64(),
            FollowCount = row[Fc.FollowCount].Value.AsInt32(),
            FollowStatus = row[Fc.FollowStatus].Value.AsInt32(),
            Id = row[Fc.Id].Value.AsInt64(),
            Login = row[Fc.Login].Value
          });
        }
        return result;
      }
      return null;
    }

    public long Insert(FollowedUser item) {
      long RowKey = _table.AddRow();
      _table.Rows[RowKey][Fc.Uid].Value = RowKey.AsString();
      _table.Rows[RowKey][Fc.FollowCount].Value = item.FollowCount.AsString();
      _table.Rows[RowKey][Fc.FollowStatus].Value = item.FollowStatus.AsString();
      _table.Rows[RowKey][Fc.Id].Value = item.Id.AsString();
      _table.Rows[RowKey][Fc.Login].Value = item.Login;
      NeedsSave = true;
      return RowKey;
    }
    public void Update(FollowedUser item) {
      long RowKey = item.Uid;
      _table.Rows[RowKey][Fc.Uid].Value = item.Uid.AsString();
      _table.Rows[RowKey][Fc.FollowCount].Value = item.FollowCount.AsString();
      _table.Rows[RowKey][Fc.FollowStatus].Value = item.FollowStatus.AsString();
      _table.Rows[RowKey][Fc.Id].Value = item.Id.AsString();
      _table.Rows[RowKey][Fc.Login].Value = item.Login;
      NeedsSave = true;
    }
    public void Delete(FollowedUser item) {
      long RowKey = item.Uid;
      _table.Rows.Remove(RowKey, out Row? _);
      NeedsSave = true;
    }
    public void Save() {      
      _table.Save();
      NeedsSave = false;
    }

    public void AddOp(FuOptype optype, FollowedUser? user) { 
      ToDoQueue.AddOp(new FuOp(ToDoQueue, 0, optype, user ));
    }
  }

  public enum FuOptype { 
    Insert, Update, Delete, MarkFollowed, Save
  }

  public class FuOp {
    public long Id = 0;
    private BackgroundWorker BgWorker = new BackgroundWorker(); 
    public FuOptype OpType;
    public FollowedUser? User;
    public FuOps Owner { get; set; }
    public FuOp(FuOps owner, long id, FuOptype optype, FollowedUser? user ) {       
      Id = id;
      Owner = owner;
      OpType = optype;
      User = user;
      BgWorker.WorkerSupportsCancellation = true;
      BgWorker.DoWork += DoWorkHandeler;
      BgWorker.RunWorkerCompleted += DoWorkCompleteHandeler;
      BgWorker.RunWorkerAsync();
    }
    private void DoWorkCompleteHandeler(object? sender, RunWorkerCompletedEventArgs e) {
      try {
        BgWorker.Dispose();
      } catch (Exception ex) { 
        Owner.Owner.Mainform.LogMsg($"{DateTime.Now} ErrorBGD {ex.Message}");
      }
      try {
        Owner.Owner.Mainform.ReloadLbSchedule(0);
      } catch (Exception ex) {
        Owner.Owner.Mainform.LogMsg($"{DateTime.Now} ErrorRL {ex.Message}");
      }      
      Owner.Remove(this.Id);
    }
    private void DoWorkHandeler(object? sender, DoWorkEventArgs e) {
      try { 
        switch (OpType) {
          case FuOptype.Insert:
            Owner.Owner.Mainform.LogProgress($"{DateTime.Now} Adding {User.Login}");
            Owner.Owner.Insert(this.User);
            break;
          case FuOptype.Update:
            Owner.Owner.Mainform.LogProgress($"{DateTime.Now} Update {User.Login}");
            Owner.Owner.Update(this.User);
            break;
          case FuOptype.Delete:
            Owner.Owner.Mainform.LogProgress($"{DateTime.Now} Delete {User.Login}");
            Owner.Owner.Delete(this.User);
            break;
          case FuOptype.MarkFollowed:
            Owner.Owner.Mainform.LogProgress($"{DateTime.Now} MarkingFollowed {User.Login}");
            this.User.FollowStatus = 1;
            Owner.Owner.Update(this.User);          
            break;
          case FuOptype.Save: 
            Owner.Owner.Mainform.SyncSaveFollows(0);
            break;
        }
      } catch(Exception ex) {
        Owner.Owner.Mainform.LogMsg($"{DateTime.Now} Error {ex.Message}");
      }
    }
    public string OperationType { get { 
      string opStr = "";
      switch (OpType) {
        case FuOptype.Insert: opStr = "Insert";  break;
        case FuOptype.Update: opStr = "Update";  break;
        case FuOptype.Delete: opStr = "Delete";  break;
        case FuOptype.MarkFollowed: opStr = "MarkFollowed"; break;
        case FuOptype.Save: opStr = "Save"; break;
      }
      return opStr;
    } }
  }
  public class FuOps : ConcurrentDictionary<long, FuOp> {
    public Int64 Nonce = 1;
    public FollowedUserFileTable Owner;
    public FuOps(FollowedUserFileTable owner) {
      Owner = owner;
    }  
    public FuOp AddOp(FuOp op) {
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
