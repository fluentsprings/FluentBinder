using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace FluentBinder
{
    public class FluentService : IFluentService
    {
        #region Fields

        private static readonly IDictionary<INotifyPropertyChanged, IFluentBindInstance> Binders =
            new Dictionary<INotifyPropertyChanged, IFluentBindInstance>();

        #endregion

        #region Implementation of IFluentService

        public void Bind<T, TProperty>(T obj, Expression<Func<T, TProperty>> propExpression, Action action)
            where T : INotifyPropertyChanged
        {
            CreateIfDoesNotExist<T>(obj);

            var binder = Binders[obj] as IFluentBindInstance<T>;
            binder?.Bind(propExpression, action);
        }

        public void Bind<T, TProperty>(T obj, Expression<Func<T, TProperty>> propExpression, Action action,
            Func<T, bool> predicate)
            where T : INotifyPropertyChanged
        {
            CreateIfDoesNotExist<T>(obj);
            var binder = Binders[obj] as IFluentBindInstance<T>;
            binder?.Bind(propExpression, action, predicate);
        }

        public void Unbind<T>(T obj, string propName, Action handler, Func<T, bool> predicate)
            where T : INotifyPropertyChanged
        {
            if (Binders.ContainsKey(obj))
            {
                var fluentBinder = Binders[obj] as IFluentBindInstance<T>;
                fluentBinder?.Unbind(propName, handler, predicate);
            }
        }

        public void Unbind<T>(T obj, string propName)
            where T : INotifyPropertyChanged
        {
            if (Binders.ContainsKey(obj))
            {
                Binders[obj]?.Unbind(propName);
            }
        }

        public void Unbind<T>(T obj, string propName, Action handler)
            where T : INotifyPropertyChanged
        {
            if (Binders.ContainsKey(obj))
            {
                Binders[obj]?.Unbind(propName, handler);
            }
        }

        public void Reset<T>(T obj)
            where T : INotifyPropertyChanged
        {
            Binders[obj].Reset();
        }

        public void ResetAll()
        {
            Binders.Clear();
        }

        #endregion

        #region Helper methods

        private static void CreateIfDoesNotExist<T>(T obj) where T : INotifyPropertyChanged
        {
            if (!Binders.ContainsKey(obj))
            {
                var fluentBinder = new FluentBindInstance<T>(obj);
                Binders[obj] = fluentBinder;
            }
        }

        #endregion
    }
}
