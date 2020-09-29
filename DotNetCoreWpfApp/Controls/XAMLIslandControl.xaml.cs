﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DotNetCoreWpfApp.Contracts.Services;
using DotNetCoreWpfApp.Models;
using DotNetCoreWpfApp.XamlIsland;

using Microsoft.Toolkit.Wpf.UI.XamlHost;

namespace DotNetCoreWpfApp.Controls
{
    // For info about hosting a custom UWP control in a WPF app using XAML Islands read this doc
    // https://docs.microsoft.com/windows/apps/desktop/modernize/host-custom-control-with-xaml-islands
    public partial class XAMLIslandControl : UserControl
    {
        private readonly IThemeSelectorService _themeSelectorService;

        private bool _useDarkTheme;
        private SolidColorBrush _backgroundColor;
        private XAMLIslandControlUniversal _universalControl;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // TODO WTS: Add any Dependency properties you need to add to your control
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(XAMLIslandControl), new PropertyMetadata(string.Empty));

        public XAMLIslandControl()
        {
            InitializeComponent();
            _themeSelectorService = ((App)Application.Current).GetService<IThemeSelectorService>();
            _themeSelectorService.ThemeChanged += OnThemeChanged;
            GetColors();
        }

        private void OnThemeChanged(object sender, System.EventArgs e)
        {
            GetColors();
            ApplyColors();
        }

        private void OnChildChanged(object sender, EventArgs e)
        {
            if (sender is WindowsXamlHost host && host.GetUwpInternalObject() is XAMLIslandControlUniversal xamlIsland)
            {
                _universalControl = xamlIsland;
                ApplyColors();

                // TODO WTS: Set bindings to your UWP DependencyProperty XAMLIsland control
                _universalControl.SetBinding(XAMLIslandControlUniversal.TextProperty, new Windows.UI.Xaml.Data.Binding() { Path = new Windows.UI.Xaml.PropertyPath(nameof(Text)), Mode = Windows.UI.Xaml.Data.BindingMode.TwoWay });
            }
        }

        private void GetColors()
        {
            _backgroundColor = _themeSelectorService.GetColor("MahApps.Brushes.Control.Background");
            _useDarkTheme = _themeSelectorService.GetCurrentTheme() == AppTheme.Dark;
        }

        private void ApplyColors()
        {
            _universalControl.BackgroundColor = Converters.ColorConverter.FromSystemColor(_backgroundColor);
            _universalControl.UseDarkTheme = _useDarkTheme;
        }
    }
}
