﻿using System.ComponentModel;

namespace ServiceDisabler
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        //basic ViewModelBase
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        //Extra Stuff, shows why a base ViewModel is useful
        //bool? _CloseWindowFlag;
        //public bool? CloseWindowFlag
        //{
        //    get { return _CloseWindowFlag; }
        //    set
        //    {
        //        _CloseWindowFlag = value;
        //        RaisePropertyChanged("CloseWindowFlag");
        //    }
        //}

        //public virtual void CloseWindow(bool? result = true)
        //{
        //    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
        //    {
        //        CloseWindowFlag = CloseWindowFlag == null
        //            ? true
        //            : !CloseWindowFlag;
        //    }));
        //}
    }
}
