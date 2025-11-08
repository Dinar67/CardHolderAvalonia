using System.Windows.Input;
using Avalonia.Media.Imaging;
using CardHolder.Services;
using CardHolder.Views;
using CommunityToolkit.Mvvm.Input;

namespace CardHolder.ViewModels;

public class ShowCardViewModel : ViewModelBase
{
    private Bitmap _qrImage;

    public Bitmap QrImage
    {
        get => _qrImage;
        private set
        {
            _qrImage = value;
            OnPropertyChanged();
        }
    }
    private Card _card;

    public Card Card
    {
        get => _card;
        set
        {
            _card = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand BackCommand { get; private set; }

    public ShowCardViewModel()
    {
        BackCommand = new RelayCommand(Back);
    }
    public void Initialize(Card card)
    {
        Card = card;
        if (Card.CodeImageBytes != null)
            QrImage = ImageHelper.GetImageFromBytes(Card.CodeImageBytes);
    }
    private void Back()
    {
        MainView.Navigate(new ListCards());
    }
}