using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
namespace FAS.Model
{
    public class Setting_Model: ObservableObject
    {
        private List<AppThemeMenuData> _AppThemes;
        /// <summary>
        /// 主题
        /// </summary>
        public List<AppThemeMenuData> AppThemes
        {
            get { return _AppThemes; }
            set { _AppThemes = value; RaisePropertyChanged(() => AppThemes); }
        }


        private List<AccentColorMenuData> _AccentColors;
        /// <summary>
        /// 颜色
        /// </summary>
        public List<AccentColorMenuData> AccentColors
        {
            get { return _AccentColors; }
            set { _AccentColors = value; RaisePropertyChanged(() => AccentColors); }
        }

        private AppThemeMenuData _Sel_Theme;
        /// <summary>
        /// 选中主题
        /// </summary>
        public AppThemeMenuData Sel_Theme
        {
            get { return _Sel_Theme; }
            set { _Sel_Theme = value; RaisePropertyChanged(() => Sel_Theme); }
        }

        private AccentColorMenuData _Sel_AccentColors;
        /// <summary>
        /// 选中颜色
        /// </summary>
        public AccentColorMenuData Sel_AccentColors
        {
            get { return _Sel_AccentColors; }
            set { _Sel_AccentColors = value; RaisePropertyChanged(() => Sel_AccentColors); }
        }
    }
}
