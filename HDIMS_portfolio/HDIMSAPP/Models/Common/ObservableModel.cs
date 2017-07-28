using System.ComponentModel;
using System.Windows;
using HDIMSAPP.Utils;

namespace HDIMSAPP.Models.Common
{
    public abstract class ObservableModel : INotifyPropertyChanged, IDataErrorInfo
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool HasPropertyChangedHandler()
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            return handler != null;
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            OnPropertyChanged(this, e);
        }
        protected void OnPropertyChanged(object sender, string propertyName)
        {
            OnPropertyChanged(sender, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected delegate void PropertyChangedDelegate(object sender, string propertyName);


#if SILVERLIGHT
        public void OnAsyncPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler == null) return;

            Deployment.Current.Dispatcher.BeginInvoke(new PropertyChangedDelegate(OnPropertyChanged), this, propertyName);
            //Deployment.Current.Dispatcher.BeginInvoke(() =>
            //{
            //    OnPropertyChanged(this, propertyName);
            //});
        }
#endif
        #endregion
        #region IDataErrorInfo Members
        public ValidationHandler ValidationHandler = new ValidationHandler();
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                if (this.ValidationHandler.BrokenRuleExists(columnName))
                {
                    return this.ValidationHandler[columnName];
                }
                return null;
            }
        }
        #endregion
    }
}
