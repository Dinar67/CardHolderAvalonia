using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using CardHolder.Services;
using CardHolder.ViewModels;

namespace CardHolder.Views;

public partial class MainView : UserControl
{
    public static IStorageProvider StorageProvider { get; private set; }
    public TopLevel? Top { get; private set; }
    private static MainView _instance;
    public MainView()
    {
        InitializeComponent();
        _instance = this;
        Logger.Initialize(MessageContentControl, MessageBackground);
        Navigate(new ListCards());
        this.Loaded += OnLoaded;
    }
    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        Top = TopLevel.GetTopLevel(this);
        StorageProvider = Top.StorageProvider;
    }
    public static void Navigate(UserControl userControl)
    {
        if (_instance == null) throw new Exception("MainViewModel not initialized!");
        _instance.Control.Content = userControl;
    }
}