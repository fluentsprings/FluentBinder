using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FluentBinder
{
    public static class FluentBinderExtensions
    {
        public static void Bind<T, TProperty>(this T obj, Expression<Func<T, TProperty>> propExpression, Action action)
            where T : INotifyPropertyChanged
        {
            FluentBinder.Bind(obj, propExpression, action);
        }

        public static void Bind<T, TProperty>(this T obj, Expression<Func<T, TProperty>> propExpression, Action action, Func<T, bool> predicate)
            where T : INotifyPropertyChanged
        {
            FluentBinder.Bind(obj, propExpression, action, predicate);
        }

        public static void Unbind<T>(this T obj, string propName, Action handler, Func<T, bool> predicate)
            where T : INotifyPropertyChanged
        {
            FluentBinder.Unbind(obj, propName, handler, predicate);
        }

        public static void Unbind<T>(this T obj, string propName)
            where T : INotifyPropertyChanged
        {
            FluentBinder.Unbind(obj, propName);
        }

        public static void Unbind<T>(this T obj, string propName, Action handler)
            where T : INotifyPropertyChanged
        {
            FluentBinder.Unbind(obj, propName, handler);
        }

        public static void Reset<T>(this T obj)
            where T : INotifyPropertyChanged
        {
            FluentBinder.Reset(obj);
        }
    }
}
