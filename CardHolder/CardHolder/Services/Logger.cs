using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using CardHolder.Views;

namespace CardHolder.Services;

public static class Logger
{
    private static ContentControl? MessageControl { get; set; }
    private static Rectangle? Rectangle { get; set; }
    private static bool isInit = false;

    public static void Initialize(ContentControl control, Rectangle rectangle)
    {
        MessageControl = control;
        Rectangle = rectangle;
        MessageControl.IsVisible = false;
        Rectangle.IsVisible = false;
        isInit = true;
    }

    public static void ShowMessage(string text)
    {
        if (!isInit) return;
        MessageControl!.IsVisible = true;
        Rectangle!.IsVisible = true;
        MessageControl!.Content = new MessageControl().Initialize(text);
    }
    public static void ShowYesNo(string text, Action action)
    {
        if (!isInit) return;
        MessageControl!.IsVisible = true;
        Rectangle!.IsVisible = true;
        MessageControl!.Content = new YesNoControl().Initialize(text, action);
    }
    

    public static void CloseMessage()
    {
        if (!isInit) return;
        MessageControl!.IsVisible = false;
        Rectangle!.IsVisible = false;
        MessageControl!.Content = null;
    }
}