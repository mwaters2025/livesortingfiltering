using System.Windows.Media;

namespace WpfApp1 {
  public class User {

    public int _age;
    public int Age {
      get => this._age;
    }

    private SolidColorBrush _color;
    public SolidColorBrush Color {
      get => this._color; 
    }

    public User(int age, SolidColorBrush color) {
      this._age = age;
      this._color = color;
    }

  }
}
