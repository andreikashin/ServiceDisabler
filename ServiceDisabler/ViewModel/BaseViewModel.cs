﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace ServiceDisabler
{
    internal class BaseViewModel : INotifyPropertyChanged
    {

        //basic BaseViewModel
        internal void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
