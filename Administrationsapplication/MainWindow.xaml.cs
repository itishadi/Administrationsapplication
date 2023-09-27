using System.Windows;
using Administrationsapplication.MVVM.Core;

namespace Administrationsapplication;


public partial class MainWindow : Window
{
    public MainWindow(NavigationStore navigationStore)
    {
        InitializeComponent();
        DataContext = navigationStore;
    }
}
