using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace HDIMSAPP.Common
{
    public class IdleEventArgs : EventArgs
    {
        public bool IsIdle { get; private set; }
        public TimeSpan IdleTime { get; private set; }

        public IdleEventArgs(bool isIdle, TimeSpan idleTime)
        {
            IsIdle = isIdle;
            IdleTime = idleTime;
        }
    }

    /// <summary>
    /// http://forums.silverlight.net/t/225936.aspx/1
    /// hollay for Magikos
    /// 
    /// 1. Add following code in App.xaml
    /// <Application.ApplicationLifetimeObjects>        
    ///     <local:UserInteractionMonitor />
    /// </Application.ApplicationLifetimeObjects>
    /// 
    /// 2. Add following code in the page 
    ///   UserInteractionMonitor.Current.RegisterIdleNotification(TimeSpan.FromMinutes(5), OnUserIdle);
    /// 
    /// </summary>
    public class UserInteractionMonitor : IApplicationService, IApplicationLifetimeAware, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected MouseButtonEventHandler OnMouseDownHandler;
        protected KeyEventHandler OnKeyDownHandler;

        protected readonly IList<Popup> _attachedPopup = new List<Popup>();
        internal readonly IList<IdleNotification> _idleNotifications = new List<IdleNotification>();
        protected readonly DispatcherTimer _userStateTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(500) };
        public static UserInteractionMonitor Current { get; protected set; }

        private long _totalMouseClicks;
        public virtual long TotalMouseClicks
        {
            get { return _totalMouseClicks; }
            protected set
            {
                if (value == _totalMouseClicks) { return; }

                _totalMouseClicks = value;
                OnPropertyChanged("TotalMouseClicks");
            }
        }

        private long _totalMouseMoves;
        public virtual long TotalMouseMoves
        {
            get { return _totalMouseMoves; }
            protected set
            {
                if (value == _totalMouseMoves) { return; }

                _totalMouseMoves = value;
                OnPropertyChanged("TotalMouseMoves");
            }
        }

        private long _totalKeyPresses;
        public virtual long TotalKeyPresses
        {
            get { return _totalKeyPresses; }
            protected set
            {
                if (value == _totalKeyPresses) { return; }

                _totalKeyPresses = value;
                OnPropertyChanged("TotalKeyPresses");
            }
        }
        #region == 추가 ==
        private Point _lastMouseLeftDownPosistion;
        public virtual Point LastMouseLeftDownPosistion
        {
            get { return _lastMouseLeftDownPosistion; }
            protected set
            {
                if (value == _lastMouseLeftDownPosistion) { return; }

                _lastMouseLeftDownPosistion = value;
                OnPropertyChanged("LastMouseLeftDownPosistion");
            }
        }

        public virtual Rect LastMouseDragingRect
        {
            get {
                if (LastMouseLeftDownPosistion != null && CurrentMousePosistion != null)
                {
                    return new Rect(LastMouseLeftDownPosistion, CurrentMousePosistion);
                }
                return Rect.Empty;
            }
        }

        #endregion
        private Point _currentMousePosistion;
        public virtual Point CurrentMousePosistion
        {
            get { return _currentMousePosistion; }
            protected set
            {
                if (value == _currentMousePosistion) { return; }

                _currentMousePosistion = value;
                OnPropertyChanged("CurrentMousePosistion");
            }
        }

        private DateTime _lastInteraction;
        public virtual DateTime LastInteraction
        {
            get { return _lastInteraction; }
            protected set
            {
                if (value == _lastInteraction) { return; }

                _lastInteraction = value;
                OnPropertyChanged("LastInteraction");
            }
        }

        private TimeSpan _idleTime;
        public virtual TimeSpan IdleTime
        {
            get { return _idleTime; }
            protected set
            {
                if (value == _idleTime) { return; }

                _idleTime = value;
                OnPropertyChanged("IdleTime");
            }
        }

        private bool _isMouseOverApplication;
        public virtual bool IsMouseOverApplication
        {
            get { return _isMouseOverApplication; }
            protected set
            {
                if (value == _isMouseOverApplication) { return; }

                _isMouseOverApplication = value;
                OnPropertyChanged("IsMouseOverApplication");
            }
        }

        public virtual void StartService(ApplicationServiceContext context)
        {
            OnMouseDownHandler = OnMouseDown;
            OnKeyDownHandler = OnKeyDown;

            Current = this;
            LastInteraction = DateTime.Now;
        }

        public virtual void StopService() { return; }

        public virtual void Starting() { return; }
        public virtual void Started()
        {
            UIElement root = Application.Current.RootVisual;
            if (root == null) { throw new InvalidOperationException("A application root visual is required by the UserInteractionMonitor."); }

            AddHandlers(root);
            root.MouseLeave += OnMouseLeave;

            _userStateTimer.Tick += OnTimerTick;
            _userStateTimer.Start();
        }

        public virtual void Exiting() { return; }
        public virtual void Exited()
        {
            UIElement root = Application.Current.RootVisual;
            if (root == null) { return; }

            RemoveHandlers(root);
            root.MouseLeave -= OnMouseLeave;

            _userStateTimer.Stop();
        }

        protected virtual void OnMouseLeave(object sender, MouseEventArgs e) { IsMouseOverApplication = false; }
        protected virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
            IsMouseOverApplication = true;
            LastInteraction = DateTime.Now;

            Point delta = CurrentMousePosistion;
            CurrentMousePosistion = e.GetPosition(Application.Current.RootVisual);

            delta = new Point(delta.X - CurrentMousePosistion.X, delta.Y - CurrentMousePosistion.Y);
            TotalMouseMoves += Math.Abs((long)delta.X) + Math.Abs((long)delta.Y);
        }


        protected virtual void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            #region == 정광일 추가 - 왼쪽 클릭은 아니지만 귀찮아서 그냥 넘어감(나중에 수정?) ==
            LastMouseLeftDownPosistion = e.GetPosition(null);
            #endregion

            LastInteraction = DateTime.Now;
            TotalMouseClicks++;
        }

        protected virtual void OnKeyDown(object sender, KeyEventArgs e)
        {
            LastInteraction = DateTime.Now;
            TotalKeyPresses++;
        }

        protected virtual void OnTimerTick(object sender, EventArgs e)
        {
            IEnumerable<Popup> popups = VisualTreeHelper.GetOpenPopups();
            OnAttachPopups(popups);
            OnDetachPopups(popups);

            if (_idleNotifications.Count == 0) { return; }

            IdleTime = DateTime.Now.Subtract(LastInteraction);
            foreach (IdleNotification idleNotification in _idleNotifications)
            {
                idleNotification.Notify(IdleTime);
            }
        }

        public virtual void RegisterIdleNotification(TimeSpan timeout, EventHandler<IdleEventArgs> callback)
        {
            if (timeout == TimeSpan.Zero) { throw new ArgumentException("The timeout value cannot be TimeSpan.Zero", "timeout"); }
            if (callback == null) { throw new ArgumentNullException("callback"); }

            IdleNotification idleNotification = _idleNotifications.FirstOrDefault(i => i.Timeout == timeout && i.Callback == callback);
            if (idleNotification != null) { throw new InvalidOperationException("IdleNotification is already registered."); }

            _idleNotifications.Add(new IdleNotification { Timeout = timeout, Callback = callback });
        }

        public virtual void UnregisterIdleNotification(TimeSpan timeout, EventHandler<IdleEventArgs> callback)
        {
            IdleNotification idleNotification = _idleNotifications.FirstOrDefault(i => i.Timeout == timeout && i.Callback == callback);
            if (idleNotification == null) { return; }

            _idleNotifications.Remove(idleNotification);
        }

        protected virtual void OnAttachPopups(IEnumerable<Popup> popups)
        {
            Popup[] newPopups = popups.Where(p => !_attachedPopup.Contains(p)).ToArray();  //ToArray() forces LINQ to evaluate, needed because the collection will be modified below.
            foreach (Popup newPopup in newPopups)
            {
                _attachedPopup.Add(newPopup);
                AddHandlers(newPopup.Child);
            }
        }

        protected virtual void OnDetachPopups(IEnumerable<Popup> popups)
        {
            Popup[] oldPopups = _attachedPopup.Where(p => !popups.Contains(p)).ToArray();  //ToArray() forces LINQ to evaluate, needed because the collection will be modified below.
            foreach (Popup oldPopup in oldPopups)
            {
                _attachedPopup.Remove(oldPopup);
                RemoveHandlers(oldPopup.Child);
            }
        }

        protected virtual void AddHandlers(UIElement uiElement)
        {
            if (uiElement == null) { return; }

            uiElement.MouseMove += OnMouseMove;
            uiElement.MouseRightButtonDown += OnMouseDown;
            uiElement.AddHandler(UIElement.KeyDownEvent, OnKeyDownHandler, true);
            uiElement.AddHandler(UIElement.MouseLeftButtonDownEvent, OnMouseDownHandler, true);
        }

        protected virtual void RemoveHandlers(UIElement uiElement)
        {
            if (uiElement == null) { return; }

            uiElement.MouseMove -= OnMouseMove;
            uiElement.MouseRightButtonDown -= OnMouseDown;
            uiElement.RemoveHandler(UIElement.KeyDownEvent, OnKeyDownHandler);
            uiElement.RemoveHandler(UIElement.MouseLeftButtonDownEvent, OnMouseDownHandler);
        }

        protected virtual void OnPropertyChanged(string propertyName) { if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); } }

        internal class IdleNotification
        {
            private bool _isIdle;

            internal TimeSpan Timeout { get; set; }
            internal EventHandler<IdleEventArgs> Callback { get; set; }
            public void Notify(TimeSpan idleTime)
            {
                if (!_isIdle && idleTime >= Timeout) { Callback(Current, new IdleEventArgs(_isIdle = true, idleTime)); }
                if (_isIdle && idleTime <= Timeout) { Callback(Current, new IdleEventArgs(_isIdle = false, idleTime)); }
            }
        }
    }  
}
