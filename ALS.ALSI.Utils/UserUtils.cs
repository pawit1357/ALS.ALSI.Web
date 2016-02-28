using ALS.ALSI.Biz.Constant;
using System.Web;

namespace ALS.ALSI.Utils
{
    public class UserUtils
    {
        public static bool isLogin()
        {
            if (HttpContext.Current.Session[Constants.SESSION_USER]!=null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
