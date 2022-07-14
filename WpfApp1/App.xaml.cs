using System.Windows;

namespace WpfApp1 {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    private void Application_Startup(object sender, StartupEventArgs e) {
      this.MainWindow = new ShellView();
      this.MainWindow.DataContext = new ShellViewModel();
      this.MainWindow.Show();
    }
  }
}
