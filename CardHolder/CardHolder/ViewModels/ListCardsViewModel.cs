using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Controls;
using CardHolder.Views;

namespace CardHolder.ViewModels;

public class ListCardsViewModel : ViewModelBase
{
    public List<Control> Cards { get; private set; } = new List<Control>();
    public ListCardsViewModel()
    {
        Cards.Add(new CardUserControl());
        Cards.Add(new CardUserControl());
        Cards.Add(new CardUserControl());
        Cards.Add(new CardUserControl());
    }
}