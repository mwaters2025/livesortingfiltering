using System;
using System.Windows.Media;

namespace WpfApp1 {
  public class RandomService {

    private Random _random;

    public int GetAge() {
      return this._random.Next(1, 100);
    }

    public SolidColorBrush GetBrush() {
      int color = this._random.Next(0, 3);
      switch(color) {
        case 0:
          return Brushes.Red;
        case 1:
          return Brushes.Green;
      }
      return Brushes.Blue;
    }

    public RandomService() {
      this._random = new Random();
    }
  }
}
