using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CardHolder.ViewModels;

namespace CardHolder.Views;

public partial class ShowCard : UserControl
{
    public ShowCard()
    {
        InitializeComponent();
    }
    public ShowCardViewModel ShowCardViewModel => (DataContext as ShowCardViewModel)!;
}