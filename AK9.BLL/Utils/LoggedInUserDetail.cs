using AK9.AppHelper.Utils;
using Microsoft.AspNetCore.Http;

namespace AK9.BLL.Utils
{
    public class LoggedInUserDetail
    {
        HttpContext _httpContext;

        public LoggedInUserDetail()
        {
            _httpContext = AppHttpContext.Current;
        }

        public int LoggedInUserId
        {
            get
            {
                return _httpContext.User.GetUserId();
            }
        }
    }
}
