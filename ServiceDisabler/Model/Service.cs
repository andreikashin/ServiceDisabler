using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Threading;

namespace ServiceDisabler
{
    internal class Service : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        private int _processId;
        public int ProcessId
        {
            get { return _processId; }
            set
            {
                if (_processId != value)
                {
                    _processId = value;
                    RaisePropertyChanged(nameof(ProcessId));
                }
            }
        }

        private string _startMode;
        public string StartMode // Auto | Manual | Disabled
        {
            get { return _startMode; }
            set
            {
                if (_startMode != value)
                {
                    _startMode = value;
                    RaisePropertyChanged(nameof(StartMode));
                }
            }
        }

        private string _state;
        public string State // Running | Stopped
        {
            get { return _state; }
            set
            {
                if (_state != value)
                {
                    _state = value;
                    RaisePropertyChanged(nameof(State));
                }
            }
        }

        private string _status;
        public string Status // OK | UNKNOWN
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged(nameof(Status));
                }
            }
        }

        private DateTimeOffset? _stopTime;
        public DateTimeOffset? StopTime
        {
            get { return _stopTime; }
            set
            {
                if (_stopTime != value)
                {
                    _stopTime = value;
                    StopTimeDisplay = _stopTime?.ToString(CultureInfo.CurrentCulture);
                    RaisePropertyChanged(nameof(StopTimeDisplay));
                    RaisePropertyChanged(nameof(StopTime));
                }
            }
        }

        private string _stopTimeDisplay;
        public string StopTimeDisplay
        {
            get { return _stopTimeDisplay; }
            set
            {
                if (_stopTimeDisplay != value)
                {
                    _stopTimeDisplay = value;
                    RaisePropertyChanged(nameof(StopTimeDisplay));
                }
            }
        }

        public DispatcherTimer UpdateServiceListTimer => new DispatcherTimer();


        private void RaisePropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
