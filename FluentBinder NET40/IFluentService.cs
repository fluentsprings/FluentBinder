using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FluentBinder
{
    public interface IFluentService
    {
        void Bind<T, TProperty>(T obj, Expression<Func<T, TProperty>> propExpression, Action action)
            where T : INotifyPropertyChanged;

        void Bind<T, TProperty>(T obj, Expression<Func<T, TProperty>> propExpression, Action action,
            Func<T, bool> predicate)
            where T : INotifyPropertyChanged;

        void Unbind<T>(T obj, string propName, Action handler, Func<T, bool> predicate)
            where T : INotifyPropertyChanged;

        void Unbind<T>(T obj, string propName)
            where T : INotifyPropertyChanged;

        void Unbind<T>(T obj, string propName, Action handler)
            where T : INotifyPropertyChanged;

        void Reset<T>(T obj)
            where T : INotifyPropertyChanged;

        void ResetAll();
    }
}
