using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

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

        public string GetPropertyName<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Expression is not a field or property.", "expression");
            }

            var property = memberExpression.Member as PropertyInfo;
            if (property == null)
            {
                throw new ArgumentException("Expression is not a property.", "expression");
            }

            return property.Name;
        }
    }
}