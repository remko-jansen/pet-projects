using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media;
using System.Windows.Shell;
using System.Windows.Threading;

namespace ImageShrinker.ViewModel
{
    public class ProgressViewModel : BaseNotificationModel
    {
        private int _maximumSteps;
        private int _currentStep;
        private bool _isIndeterminate;
        private readonly List<string> _messages;
        private readonly Object _messagesLock = new Object();

        private TaskbarItemInfo _taskbarItemInfo;

        public ProgressViewModel()
        {
            _messages = new List<string>();
            _maximumSteps = 1;
            _currentStep = 0;
            _isIndeterminate = false;
        }

        public void Reset()
        {
            MaximumSteps = 1;
            CurrentStep = 0;
            IsIndeterminate = false;
            ClearMessages();
            ResetTaskBarProgress();
        }

        public TaskbarItemInfo TaskbarItemInfo
        {
            get { return _taskbarItemInfo; }
            set { _taskbarItemInfo = value; }
        }

        public int MaximumSteps
        {
            get { return _maximumSteps; }
            set { Set(() => MaximumSteps, ref _maximumSteps, value); }
        }

        public int CurrentStep
        {
            get { return _currentStep; }
            set
            {
                Set(() => CurrentStep, ref _currentStep, value);
                UpdateTaskBarProgress();
            }
        }

        public bool IsIndeterminate
        {
            get { return _isIndeterminate; }
            set { Set(() => IsIndeterminate, ref _isIndeterminate, value); }
        }

        public string Messages
        {
            get
            {
                lock (_messagesLock)
                {
                    return string.Join(Environment.NewLine, _messages);
                }
            }
        }

        public void AddMessage(string msg)
        {
            lock (_messagesLock)
            {
                _messages.Add(msg);
            }
            OnPropertyChanged(GetPropertyName(() => Messages));
        }

        public void AddMessage(string msg, params object[] args)
        {
            var formatedMsg = string.Format(msg, args);
            AddMessage(formatedMsg);
        }

        public void ClearMessages()
        {
            lock (_messagesLock)
            {
                _messages.Clear();
            }
            OnPropertyChanged(GetPropertyName(() => Messages));
        }

        private void UpdateTaskBarProgress()
        {
            if (_taskbarItemInfo != null)
            {
                var state = TaskbarItemProgressState.None;
                double progress = 0;

                if (_currentStep != 0)
                {
                    progress = _currentStep / ((double)_maximumSteps);
                    state = TaskbarItemProgressState.Normal;
                }

                _taskbarItemInfo.Dispatcher.Invoke(new Action(() => {
                                                                        _taskbarItemInfo.ProgressState = state;
                                                                        _taskbarItemInfo.ProgressValue = progress;
                                                                    }
                                                             ));
            }
        }

        private void ResetTaskBarProgress()
        {
            if (_taskbarItemInfo != null)
            {
                _taskbarItemInfo.Dispatcher.Invoke(new Action(() => _taskbarItemInfo.ProgressState = TaskbarItemProgressState.None));
            }
        }

    }

}
