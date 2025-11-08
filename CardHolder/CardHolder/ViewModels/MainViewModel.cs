using System.Threading.Tasks;
using System.Windows.Input;
using CardHolder.Services;
using CardHolder.Views;
using CommunityToolkit.Mvvm.Input;

namespace CardHolder.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private bool _isPaneOpen;

    public bool IsPaneOpen
    {
        get => _isPaneOpen;
        set
        {
            _isPaneOpen = value;
            OnPropertyChanged();
        }
    }
    public ICommand ImportCommand { get; private set; }
    public ICommand ExportCommand { get; private set; }
    public ICommand OpenPaneCommand { get; private set; }
    public MainViewModel()
    {
        ImportCommand = new AsyncRelayCommand(Import);
        ExportCommand = new AsyncRelayCommand(Export);
        OpenPaneCommand = new RelayCommand(OpenPane);
    }

    private async Task Import()
    {
        await App.db.Improt<Card>();
        MainView.Navigate(new ListCards());
    }
    private async Task Export()
    {
        await App.db.Export<Card>();
    }
    private void OpenPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }
}