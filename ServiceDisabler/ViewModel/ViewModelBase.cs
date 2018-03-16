using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;
using ServiceDisabler.Services;

namespace ServiceDisabler
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public IScheduleService ScheduleService;
        public StopSchedule StopSchedule { get; set; }

        //basic ViewModelBase
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private bool? _closeWindowFlag;
        public bool? CloseWindowFlag
        {
            get { return _closeWindowFlag; }
            set
            {
                _closeWindowFlag = value;
                RaisePropertyChanged(nameof(CloseWindowFlag));
            }
        }

        public virtual void CloseWindow(bool? result = true)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                CloseWindowFlag = CloseWindowFlag == null
                    ? true
                    : !CloseWindowFlag;
            }));
        }
    }
}
