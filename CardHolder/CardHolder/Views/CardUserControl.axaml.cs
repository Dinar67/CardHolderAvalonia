using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CardHolder.ViewModels;

namespace CardHolder.Views;

public partial class CardUserControl : UserControl
{
    public CardUserControl()
    {
        InitializeComponent();
    } 
    public CardViewModel CardViewModel => (DataContext as CardViewModel)!;
}