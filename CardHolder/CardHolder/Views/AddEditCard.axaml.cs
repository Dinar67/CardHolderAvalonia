using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CardHolder.ViewModels;

namespace CardHolder.Views;

public partial class AddEditCard : UserControl
{
    public AddEditCard()
    {
        InitializeComponent();
    }
    public AddEditCardViewModel AddEditCardViewModel => (DataContext as AddEditCardViewModel)!;
}