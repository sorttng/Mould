using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using FAS.Model;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro.Controls;
using ControlzEx.Theming;
using System.Windows.Media;
using System.Windows;
using System.Configuration;
using FAS.Common;
namespace FAS.ViewModel
{
    public class Setting_ViewModel: ViewModelBase
    {
        private Setting_Model _mSetting_Model;

        public Setting_Model mSetting_Model
        {
            get { return _mSetting_Model; }
            set { _mSetting_Model = value; RaisePropertyChanged(() => mSetting_Model); }
        }


        public Setting_ViewModel()
        {
            mSetting_Model = new Setting_Model()
            {
                // create accent color menu items for the demo
                AccentColors = ThemeManager.Current.Themes
                                            .GroupBy(x => x.ColorScheme)
                                            .OrderBy(a => a.Key)
                                            .Select(a => new AccentColorMenuData { Name = a.Key, ColorBrush = a.First().ShowcaseBrush })
                                            .ToList(),
                // create metro theme color menu items for the demo
                AppThemes = ThemeManager.Current.Themes
                                         .GroupBy(x => x.BaseColorScheme)
                                         .Select(x => x.First())
                                         .Select(a => new AppThemeMenuData { Name = a.BaseColorScheme, BorderColorBrush = a.Resources["MahApps.Brushes.ThemeForeground"] as Brush, ColorBrush = a.Resources["MahApps.Brushes.ThemeBackground"] as Brush })
                                         .ToList(),
            };
            mSetting_Model.Sel_Theme = mSetting_Model.AppThemes.Where(a => a.Name == Theme_Model.Theme).First();
            mSetting_Model.Sel_AccentColors = mSetting_Model.AccentColors.Where(a => a.Name == Theme_Model.Colors).First();
        }

        private RelayCommand select_Command;
        public RelayCommand Select_Command
        {
            get
            {
                if (select_Command == null)
                {
                    select_Command = new RelayCommand(_Select);
                }
                return select_Command;
            }
            set { select_Command = value; }
        }

        private void _Select()
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, "Amber");
        }

        #region 设置主题颜色
        private RelayCommand _ChangeColors_Command;
        public RelayCommand ChangeColors_Command
        {
            get
            {
                if (_ChangeColors_Command == null)
                {
                    _ChangeColors_Command = new RelayCommand(_ChangeColors);
                }
                return _ChangeColors_Command;
            }
            set { _ChangeColors_Command = value; }
        }

        private void _ChangeColors()
        {
            string colors = mSetting_Model.Sel_AccentColors.Name;
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, colors);

            ConfigFileHelper.ConfigSet("Colors", colors);
        }
        #endregion

        #region 设置主题
        private RelayCommand<ToggleSwitch> _ChangeThemes_Command;
        public RelayCommand<ToggleSwitch> ChangeThemes_Command
        {
            get
            {
                if (_ChangeThemes_Command == null)
                {
                    _ChangeThemes_Command = new RelayCommand<ToggleSwitch>(_ChangeThemes);
                }
                return _ChangeThemes_Command;
            }
            set { _ChangeThemes_Command = value; }
        }

        private void _ChangeThemes(ToggleSwitch mtoggleSwitch)
        {
            string theme = string.Empty;
            if (mtoggleSwitch.IsOn)
                theme = mtoggleSwitch.OnContent.ToString();
            else
                theme = mtoggleSwitch.OffContent.ToString();
            ThemeManager.Current.ChangeThemeBaseColor(Application.Current, theme);

            ConfigFileHelper.ConfigSet("Theme", theme);
        }
        #endregion
    }
}
