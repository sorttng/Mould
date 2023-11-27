using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using FAS.Common;
namespace FAS.Model
{
    public class LogHelper
    {
        public static Logger Logger = LogManager.GetCurrentClassLogger();

        public class LogInfo
        {
            private string _UserName = UserInfo.LogedUserInfo.UserName;
            /// <summary>
            /// 用户名
            /// </summary>
            public string UserName
            {
                get { return _UserName; }
                set { _UserName = value; }
            }


            private string _Describe;
            /// <summary>
            /// 描述
            /// </summary>
            public string Describe
            {
                get { return _Describe; }
                set { _Describe = value; }
            }
        }

        public static string JasonLog(LogInfo logInfo)
        {
            return JasonSerialization.ObjectToJson(logInfo);
        }
    }


}
