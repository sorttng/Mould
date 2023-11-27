using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using FAS.Model;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using FAS.Common;
using MahApps.Metro.Controls.Dialogs;
namespace FAS.ViewModel
{
    public class Login_Window_ViewModel: ViewModelBase
    {
        private Login_Window_Model _mLogin_Window_Model;

        public Login_Window_Model mLogin_Window_Model
        {
            get { return _mLogin_Window_Model; }
            set { _mLogin_Window_Model = value; RaisePropertyChanged(() => mLogin_Window_Model); }
        }

        public Login_Window_ViewModel()
        {
            WindowManager.Register<MainWindow>("MainWindow");
            _dialogCoordinator = DialogCoordinator.Instance;

            mLogin_Window_Model = new Login_Window_Model()
            {
                UserName = "user1",
                Password = "123456",
            };
        }

        #region Message
        private readonly IDialogCoordinator _dialogCoordinator;
        // Simple method which can be used on a Button
        public async void FooMessage(string title, string msg)
        {
            await _dialogCoordinator.ShowMessageAsync(this, title, msg);
        }
        #endregion


        #region 登录
        private RelayCommand _Login_Command;
        public RelayCommand Login_Command
        {
            get
            {
                if (_Login_Command == null)
                {
                    _Login_Command = new RelayCommand(_Login);
                }
                return _Login_Command;
            }
            set { _Login_Command = value; }
        }

        private void _Login()
        {
            #region 卫语句
            if (mLogin_Window_Model.UserName == string.Empty
                || mLogin_Window_Model.Password == string.Empty
                || mLogin_Window_Model.UserName == null
                || mLogin_Window_Model.Password == null)
            {FooMessage("提示！", "用户名或密码不能为空！");return; }

            var userInfo = SqlSugarHelper.mDB.Queryable<SqlSugarModel.User_Table>().
                Where(a => a.UserName == mLogin_Window_Model.UserName);
            if (userInfo.Count() == 0 || userInfo == null)
            {
                FooMessage("提示！", "用户名或密码错误！");return;
            }

            SqlSugarModel.User_Table info = userInfo.First();
            if (info.Password != mLogin_Window_Model.Password)
            {
                FooMessage("提示！", "用户名或密码错误！"); return;
            }
            #endregion

            //登录用户全局使用
            UserInfo.LogedUserInfo = info;
            //关闭本窗口打开新窗口
            //WindowManager.Show("HomeWindows", new HomeWindows_ViewModel());
            WindowManager.Show("MainWindow", new MainWindow_ViewModel());
            //login_Model.ISvisible = Visibility.Hidden;
            LogHelper.LogInfo logInfo = new LogHelper.LogInfo()
            { Describe = "登录成功" };
            LogHelper.Logger.Info(LogHelper.JasonLog(logInfo));
            ToClose = true;
        }
        #endregion

        #region 关闭窗口函数
        private bool toClose = false;
        /// <summary>
        /// 是否要关闭窗口
        /// </summary>
        public bool ToClose
        {
            get
            {
                return toClose;
            }
            set
            {
                toClose = value;
                if (toClose)
                {
                    this.RaisePropertyChanged("ToClose");
                }
            }
        }
        #endregion
    }
}
