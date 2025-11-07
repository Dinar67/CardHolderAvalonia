using System;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using CardHolder.Services;
using CardHolder.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CardHolder.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private static MainViewModel _instance;
    public UserControl CurrentControl { get; private set; }
    public MainViewModel()
    {
        _instance = this;
        Navigate(new ListCards());
    }
    public static void Navigate(UserControl userControl)
    {
        if (_instance == null) throw new Exception("MainViewModel not initialized!");
        _instance.CurrentControl = userControl;
    }
}