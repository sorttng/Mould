using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using MahApps.Metro.IconPacks;
using FAS.Mvvm;
using FAS.View;
using MahApps.Metro.Controls;
using MenuItem = FAS.ViewModel.MenuItem;
using System.Windows;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using FAS.Model;
using FAS.Common;
using ControlzEx.Theming;
using MahApps.Metro.Controls.Dialogs;
using GalaSoft.MvvmLight.Command;
namespace FAS.ViewModel
{
    public class MainWindow_ViewModel : ViewModelBase//BindableBase
    {
        public ObservableCollection<MenuItem> Menu { get; } = new ObservableCollection<MenuItem>();

        public ObservableCollection<MenuItem> OptionsMenu { get; } = new ObservableCollection<MenuItem>();
        public string Title { get; } = "三维荧光分析软件";
        public MainWindow_ViewModel()
        {
            _dialogCoordinator = DialogCoordinator.Instance;

            #region 添加菜单
            // Build the menus
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.WatchmanMonitoringBrands },
                Label = "数据监控",
                NavigationType = typeof(Page1),
                NavigationDestination = new Uri("View/Page1.xaml", UriKind.RelativeOrAbsolute)
            });
            this.Menu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.SearchLocationSolid },
                Label = "数据查询",
                NavigationType = typeof(Page2),
                NavigationDestination = new Uri("View/Page2.xaml", UriKind.RelativeOrAbsolute)
            });

            this.OptionsMenu.Add(new MenuItem()
            {
                Icon = new PackIconFontAwesome() { Kind = PackIconFontAwesomeKind.CogSolid },
                Label = "设置",
                NavigationType = typeof(Setting_View),
                NavigationDestination = new Uri("View/Setting_View.xaml", UriKind.RelativeOrAbsolute)
            });
            #endregion
            
            #region 读取配置文件
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current,
                Theme_Model.Theme = ConfigFileHelper.ConfigRead("Theme"));

            ThemeManager.Current.ChangeThemeColorScheme(Application.Current,
                Theme_Model.Colors = ConfigFileHelper.ConfigRead("Colors"));
            #endregion
        }

        #region Message
        private readonly IDialogCoordinator _dialogCoordinator;
        // Simple method which can be used on a Button
        public async void FooMessage(string title, string msg)
        {
            await _dialogCoordinator.ShowMessageAsync(this, title, msg);
        }
        #endregion
    }
}
