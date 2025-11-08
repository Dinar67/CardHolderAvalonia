using System.Collections.Generic;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using CardHolder.Services;
using CardHolder.Views;
using CommunityToolkit.Mvvm.Input;

namespace CardHolder.ViewModels;

public class ListCardsViewModel : ViewModelBase
{
    public static ListCardsViewModel Instance { get; private set; }
    public List<Control> Cards { get; private set; } = new List<Control>();
    public ICommand AddCommand { get; private set; }
    public ListCardsViewModel()
    {
        Instance = this;
        AddCommand = new RelayCommand(Add);
        Refresh();
    }

    public void Refresh()
    {
        var cards = App.db.Get<Card>();
        foreach (var card in cards)
        {
            var cardControl =  new CardUserControl();
            cardControl.CardViewModel.SetCard(card);
            Cards.Add(cardControl);
        }
    }

    private void Add()
    {
        var page = new AddEditCard();
        page.AddEditCardViewModel.Initialize(new Card("", "","", null, null));
        MainView.Navigate(page);
    }
}