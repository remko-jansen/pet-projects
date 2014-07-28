using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ImageShrinker
{
    public abstract class NotificationModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void Set<T>(string propertyName, ref T field, T value)
        {
            if (Equals(field, value))
            {
                return;
            }

            field = value;
            OnPropertyChanged(propertyName);
        }

        protected void Set<T>(Expression<Func<T>> propertySelector, ref T field, T value)
        {
            if (Equals(field, value))
            {
                return;
            }

            field = value;

            var propertyName = GetPropertyName(propertySelector);
            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetPropertyName(LambdaExpression expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new InvalidOperationException();
            }

            return memberExpression.Member.Name;
        }

    }
}