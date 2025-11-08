using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CardHolder.Services;

namespace CardHolder.Views;

public partial class MessageControl : UserControl
{
    public MessageControl()
    {
        InitializeComponent();
    }
    public MessageControl Initialize(string text)
    {
        MessageTb.Text = text;
        return this;
    }

    private void OkBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Logger.CloseMessage();
    }
}