using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using CardHolder.Services;
using CardHolder.Views;
using CommunityToolkit.Mvvm.Input;

namespace CardHolder.ViewModels;

public class CardViewModel : ViewModelBase
{
    private Card _card;

    private IBrush _background;

    public IBrush Background
    {
        get => _background;
        set
        {
            _background = value;
            OnPropertyChanged();
        }
    }
    private Bitmap _logoImage;

    public Bitmap LogoImage
    {
        get => _logoImage;
        private set
        {
            _logoImage = value;
            OnPropertyChanged();
        }
    }
    
    public ICommand CardTappedCommand { get; private set; }
    public ICommand EditCommand { get; private set; }
    public ICommand DeleteCommand { get; private set; }
    public CardViewModel()
    {
        CardTappedCommand = new RelayCommand(CardTapped);
        EditCommand = new RelayCommand(Edit);
        DeleteCommand = new RelayCommand(Delete);
    }
    public Card Card
    {
        get => _card;
        set
        {
            _card = value;
            OnPropertyChanged();
        }
    }

    public void SetCard(Card card)
    {
        Card = card;
        Background = SolidColorBrush.Parse(card.Color);
        if (Card.CardImageBytes != null)
            LogoImage = ImageHelper.GetImageFromBytes(Card.CardImageBytes);
    }

    private void CardTapped()
    {
        var page = new ShowCard();
        page.ShowCardViewModel.Initialize(Card);
        MainView.Navigate(page);
    }
    private void Edit()
    {
        var page = new AddEditCard();
        page.AddEditCardViewModel.Initialize(Card);
        MainView.Navigate(page);
    }
    private void Delete()
    {
        Logger.ShowYesNo($"Вы точно хотите удалить карту {Card.Name}?", () =>
        {
            App.db.Delete(Card);
            App.db.SaveChanges();
            MainView.Navigate(new ListCards());
        });
    }
}