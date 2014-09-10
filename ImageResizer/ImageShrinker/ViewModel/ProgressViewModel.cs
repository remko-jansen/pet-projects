using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace ImageShrinker.ViewModel
{
    public class ProgressViewModel : BaseNotificationModel
    {
        private int _maximumSteps;
        private int _currentStep;
        private bool _isIndeterminate;
        private readonly List<string> _messages;
        private readonly Object _messagesLock = new Object();

        public ProgressViewModel()
        {
            _messages = new List<string>();
            _maximumSteps = 0;
            _currentStep = 0;
            _isIndeterminate = false;
        }

        public void Reset()
        {
            MaximumSteps = 0;
            CurrentStep = 0;
            IsIndeterminate = false;
            ClearMessages();
        }
        
        public int MaximumSteps
        {
            get { return _maximumSteps; }
            set { Set(() => MaximumSteps, ref _maximumSteps, value); }
        }

        public int CurrentStep
        {
            get { return _currentStep; }
            set { Set(() => CurrentStep, ref _currentStep, value); }
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

    }

}
