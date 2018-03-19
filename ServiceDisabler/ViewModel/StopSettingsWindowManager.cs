﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceDisabler
{
    internal class StopSettingsWindowManager : BaseViewModel
    {
        public StopSettingsWindowManager()
        {
            Visibility = Visibility.Collapsed;
            Content = null;
        }

        private static StopSettingsWindowManager _instance;

        public static StopSettingsWindowManager Instance =>
            _instance ??
            (_instance = new StopSettingsWindowManager());

        private FrameworkElement _content;
        public FrameworkElement Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged(nameof(Content));
            }
        }

        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                RaisePropertyChanged(nameof(Visibility));
            }
        }

        public void ShowSettingsWindow(FrameworkElement content)
        {
            Content = content;
            //RaisePropertyChanged();
            Visibility = Visibility.Visible;
            //RaisePropertyChanged();
        }

        public void CloseSettingsWindow()
        {
            Visibility = Visibility.Collapsed;
            //RaisePropertyChanged();
            Content = null;
            //RaisePropertyChanged();
        }
    }
}
