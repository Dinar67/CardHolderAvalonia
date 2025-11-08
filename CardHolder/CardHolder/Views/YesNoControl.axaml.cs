using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CardHolder.Services;

namespace CardHolder.Views;

public partial class YesNoControl : UserControl
{
    private Action action;
    public YesNoControl()
    {
        InitializeComponent();
    }
    public YesNoControl Initialize(string text, Action action)
    {
        MessageTb.Text = text;
        this.action = action;
        return this;
    }

    private void YesBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        action.Invoke();
        Logger.CloseMessage();
    }

    private void NoBtn_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Logger.CloseMessage();
    }
}