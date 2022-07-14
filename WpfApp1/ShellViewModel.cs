using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1 {

  public enum SortType {
    ASC,
    DESC
  }

  public enum FilterType {
    ALL_COLORS,
    RED_COLOR,
    GREEN_COLOR,
    BLUE_COLOR
  }

  public class ShellViewModel : INotifyPropertyChanged {

    


    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged(string property) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }

    private object _originalViewLock = new object();
    private ObservableCollection<User> _originalView;
    public ObservableCollection<User> OriginalView {
      get => this._originalView;
      set {
        this._originalView = value;
        BindingOperations.EnableCollectionSynchronization(this._originalView, this._originalViewLock);
        OnPropertyChanged(nameof(OriginalView));
      }
    }

    private object _sortedViewLock = new object();
    private ObservableCollection<User> _sortedView;
    public ObservableCollection<User> SortedView {
      get => this._sortedView;
      set {
        this._sortedView = value;
        BindingOperations.EnableCollectionSynchronization(this._sortedView, this._sortedViewLock);
        OnPropertyChanged(nameof(SortedView));
      }
    }

    private ICommand _startThreadCommand;
    public ICommand StartThreadCommand {
      get => this._startThreadCommand;
      set {
        this._startThreadCommand = value;
        OnPropertyChanged(nameof(StartThreadCommand));
      }
    }



    private event EventHandler<User> _onUserCreated;
    public EventHandler<User> OnUserCreated {
      get => this._onUserCreated;
      set {
        this._onUserCreated = value;
      }
    }

    private event EventHandler<User> _onUserRemoved;
    public EventHandler<User> OnUserRemoved {
      get => this._onUserRemoved;
      set {
        this._onUserRemoved = value;
      }
    }

    private SortType _sortType;
    public SortType SortType {
      get => this._sortType;
      set {
        this._sortType = value;
        OnPropertyChanged(nameof(this.SortType));
        lock(this._originalViewLock) {
          this.Refresh();
        }
      }
    }

    private FilterType _filterType;
    public FilterType FilterType {
      get => this._filterType;
      set {
        this._filterType = value;
        OnPropertyChanged(nameof(this.FilterType));
        lock(this._originalViewLock) {
          this.Refresh();
        } 
      }
    }

    private void Refresh() { 
      User[] users = this.OriginalView.ToArray();
      IEnumerable<User> filtered = null; 
      switch(FilterType) {
        case FilterType.ALL_COLORS: {
          filtered = users;
          break;
        }
        case FilterType.RED_COLOR: {
          filtered = users.Where(x => x.Color.Equals(Brushes.Red));
          break;
        }
        case FilterType.GREEN_COLOR: {
          filtered = users.Where(x => x.Color.Equals(Brushes.Green));
          break;
        }
        case FilterType.BLUE_COLOR: {
          filtered = users.Where(x => x.Color.Equals(Brushes.Blue));
          break;
        }
      }
      if(SortType == SortType.ASC) {
        filtered = filtered.OrderBy(x => x.Age);
      } else {
        filtered = filtered.OrderByDescending(x => x.Age);
      }
      this.SortedView = new ObservableCollection<User>(filtered);
    }

    private void Subsribe() {
      lock(this._originalViewLock) {
        this.OnUserRemoved += new EventHandler<User>(OnUserRemovedHandle);
        this.OnUserCreated += new EventHandler<User>(OnUserCreatedHandle);
        this.Refresh();
      } 
    }
   


    public ShellViewModel() {
      this.OriginalView = new ObservableCollection<User>();
      RandomService randomService = new RandomService();
      this.Subsribe();
      
      
      
      this.StartThreadCommand = new RelayCommand((a) => {
        Task.Run(() => {
          while(true) { 
            User @new = new User(randomService.GetAge(), randomService.GetBrush());
            User removed = null;
            lock(this._originalViewLock) {
              if(this.OriginalView.Count > 1000) {
                removed = this.OriginalView[0];
                this.OriginalView.Remove(removed);
              }
              this.OriginalView.Add(@new);
            }
            this.OnUserCreated.Invoke(this, @new);
            if(removed != null) {
              this.OnUserRemoved.Invoke(this, removed);
            } 
            Thread.Sleep(1);
          }
        });
        Task.Run(() => {
          while(true) { 
            User @new = new User(randomService.GetAge(), randomService.GetBrush());
            User removed = null;
            lock(this._originalViewLock) {
              if(this.OriginalView.Count > 1000) {
                removed = this.OriginalView[0];
                this.OriginalView.Remove(removed);
              }
              this.OriginalView.Add(@new);
            }
            this.OnUserCreated.Invoke(this, @new);
            if(removed != null) {
              this.OnUserRemoved.Invoke(this, removed);
            } 
            Thread.Sleep(1);
          }
        });
      }, p => true); 
    }

    private void Sort(User e) {
      if(this.SortType == SortType.ASC) {
        User first = this.SortedView.FirstOrDefault(x => x.Age > e.Age);
        if(first != null) {
          this.SortedView.Insert(this.SortedView.IndexOf(first), e);
        } else {
          this.SortedView.Add(e);
        }
      } else {
        User first = this.SortedView.FirstOrDefault(x => x.Age < e.Age);
        if(first != null) {
          this.SortedView.Insert(this.SortedView.IndexOf(first), e);
        } else {
          this.SortedView.Add(e);
        }
      }
    }

    private void OnUserCreatedHandle(object sender, User e) {
      lock(this._sortedViewLock) {
        switch(this.FilterType) {
          case FilterType.ALL_COLORS: {
            Sort(e);
            break;
          }
          case FilterType.RED_COLOR: {
            if(e.Color.Equals(Brushes.Red)) {
              Sort(e);
            }
            break;
          }
          case FilterType.GREEN_COLOR: {
            if(e.Color.Equals(Brushes.Green)) {
              Sort(e);
            }
            break;
          }
          case FilterType.BLUE_COLOR: {
            if(e.Color.Equals(Brushes.Blue)) {
              Sort(e);
            }
            break;
          }
        }
      }
    } 
    private void OnUserRemovedHandle(object sender, User e) {
      lock(this._sortedViewLock) { 
        if(this.SortedView.IndexOf(e) != -1) {
          this.SortedView.Remove(e);
        } 
      }
    }
  }
}
