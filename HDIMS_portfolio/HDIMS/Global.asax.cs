using System.Collections;
using System.Web.Mvc;
using System.Web.Routing;
using Common.Logging;
using HDIMS.Utils.Login;

namespace HDIMS
{
    // 참고: IIS6 또는 IIS7 클래식 모드를 사용하도록 설정하는 지침을 보려면 
    // http://go.microsoft.com/?LinkId=9394801을 방문하십시오.

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MvcApplication));

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{*silverlight}", new { silverlight = @".*\.xap(/.*)?" });
            routes.IgnoreRoute("UploaderControlHandler.ashx");
            //routes.IgnoreRoute("/mrd/{*pathInfo}");
            //routes.IgnoreRoute("/kml/{*pathInfo}");
            //routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" }); 

            routes.MapRoute(
                "Default", // 경로 이름
                "{controller}/{action}/{id}", // 매개 변수가 있는 URL
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // 매개 변수 기본값
            );
        }

        protected void Application_Start()
        {
            //IObjectFactory factory = SpringContext.GetObjectFactory();
            //IScheduler _scheduler = (StdScheduler)factory.GetObject("MakeJsSchedulerFactory");
            //_scheduler.Start();

            //HttpContext.Current.Application["current_users"] = new Hashtable();
            //ControllerBuilder.Current.SetControllerFactory(typeof(WEBSOLTOOL.Spring.SpringControllerFactory));

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_End()
        {
            EmpData emp = new EmpData();
            string userId = emp.GetEmpData(0);
            if (log.IsDebugEnabled)
            {
                log.Debug("user id : " + userId + " : session_end");
            }
            //Hashtable current_users = (Hashtable)HttpContext.Current.Application["current_users"];
            //current_users.Remove(userId);
            Hashtable param = new Hashtable();
            param.Add("USERID", userId);
            SessionUtil.DeleteSession(param);
        }
    }
}