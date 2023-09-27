using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Administrationsapplication.MVVM.ViewModels;
using Administrationsapplication.MVVM.Models;

namespace Administrationsapplication;


public partial class MainWindow : Window
{

    public ObservableCollection<DeviceItem> Devices { get; set; } = new ObservableCollection<DeviceItem>()
    {
            new DeviceItem {DeviceId = "1",DeviceType="light", Placement = "Kicken", IsActive = true},
            new DeviceItem {DeviceId = "2",DeviceType="ledstrip" ,Placement = "Livingroom", IsActive = false},
            new DeviceItem {DeviceId = "3",DeviceType="fan", Placement = "Toa", IsActive = false}
     };



    public MainWindow(MainWindowViewModel mainWindowViewModel)
    {
        InitializeComponent();
        DataContext = mainWindowViewModel;
        DeviceList.ItemsSource = Devices;
    }
    private void Window_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            this.DragMove();
        }

    }
}
