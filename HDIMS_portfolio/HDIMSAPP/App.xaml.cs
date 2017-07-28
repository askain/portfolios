using System;
using System.Windows;
using System.Windows.Controls;
using HDIMSAPP.Models;
using HDIMSAPP.Views.Verify;

namespace HDIMSAPP
{
    public partial class App : Application
    {
        public App()
        {
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //EmpData empdata = new EmpData();
            
            //MainPage mp = new MainPage();
            //mp.ContentFrame.Source = new Uri("/Main/RealMonitor", UriKind.Relative);

            

            //HDIMSAPP.Views.DataSearch.QueryBrowser mp = new HDIMSAPP.Views.DataSearch.QueryBrowser();

            //HDIMSAPP.Views.Evaluation.AbnormChkInput mp = new Views.Evaluation.AbnormChkInput();



            WaterLevelSearch_Telerik mp = new WaterLevelSearch_Telerik();

            this.RootVisual = mp;
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // 응용 프로그램을 디버거 외부에서 실행 중인 경우 ChildWindow 컨트롤을
            // 사용하여 예외를 보고합니다.
            if (!System.Diagnostics.Debugger.IsAttached)
            {
                // 참고: 예외가 throw된 후 처리되지 않아도 응용 프로그램은 계속
                // 실행됩니다. 
                // 프로덕션 응용 프로그램의 경우 이 오류 처리는 웹 사이트로 오류를 보고하고 응용 프로그램을 
                // 중지하는 다른 방식으로 대체되어야 합니다.
                e.Handled = true;
                ChildWindow errorWin = new ErrorWindow(e.ExceptionObject);
                errorWin.Show();
            }
        }
    }
}