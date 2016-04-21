using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FluentBinder
{
    public class FluentBinder
    {
        private static readonly IFluentService FluentService = new FluentService();

        public static void Bind<T, TProperty>(T obj, Expression<Func<T, TProperty>> propExpression, Action action)
            where T : INotifyPropertyChanged
        {
            FluentService.Bind(obj, propExpression, action);
        }

        public static void Bind<T, TProperty>(T obj, Expression<Func<T, TProperty>> propExpression, Action action,
            Func<T, bool> predicate)
            where T : INotifyPropertyChanged
        {
            FluentService.Bind(obj, propExpression, action, predicate);
        }

        public static void Unbind<T>(T obj, string propName, Action handler, Func<T, bool> predicate)
            where T : INotifyPropertyChanged
        {
            FluentService.Unbind(obj, propName, handler, predicate);
        }

        public static void Unbind<T>(T obj, string propName)
            where T : INotifyPropertyChanged
        {
            FluentService.Unbind(obj, propName);
        }

        public static void Unbind<T>(T obj, string propName, Action handler)
            where T : INotifyPropertyChanged
        {
            FluentService.Unbind(obj, propName, handler);
        }

        public static void Reset<T>(T obj)
            where T : INotifyPropertyChanged
        {
            FluentService.Reset(obj);
        }

        public static void ResetAll()
        {
            FluentService.ResetAll();
        }
    }
}
