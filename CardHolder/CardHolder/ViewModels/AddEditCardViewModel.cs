using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using CardHolder.Services;
using CardHolder.Views;
using CommunityToolkit.Mvvm.Input;

namespace CardHolder.ViewModels;

public class AddEditCardViewModel : ViewModelBase
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
    public ICommand EditQrImageCommand { get; private set; }
    public ICommand EditLogoImageCommand { get; private set; }
    public ICommand SaveCommand { get; private set; }

    public AddEditCardViewModel()
    {
        BackCommand = new RelayCommand(Back);
        EditQrImageCommand = new AsyncRelayCommand(EditQrImage);
        EditLogoImageCommand = new AsyncRelayCommand(EditLogoImage);
        SaveCommand = new RelayCommand(Save);
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

    private async Task EditQrImage()
    {
        QrImage = await FileSelector.SelectImage();
        var bytes = ImageHelper.GetBytesFromImage(QrImage);
        Card.CodeImageBytes = bytes;
    }
    private async Task EditLogoImage()
    {
        var bytes = ImageHelper.GetBytesFromImage(await FileSelector.SelectImage());
        Card.CardImageBytes = bytes;
    }
    private void Save()
    {
        if (string.IsNullOrWhiteSpace(Card.Id))
        {
            Card.Id = Guid.NewGuid().ToString();
            Card.Color = "#ffffff";
            App.db.Add(Card);
        }
        App.db.SaveChanges();
        MainView.Navigate(new ListCards());
    }
}