using System;
using System.Linq.Expressions;

namespace FluentBinder
{
    public interface IFluentBindInstance
    {
        void Unbind(string propName);
        void Unbind(string propName, Action handler);
        void Reset();
    }

    public interface IFluentBindInstance<T> : IFluentBindInstance
    {
        void Bind<TProperty>(Expression<Func<T, TProperty>> viewModelProp, Action action);
        void Bind<TProperty>(Expression<Func<T, TProperty>> viewModelProp, Action action, Func<T, bool> condition);
        void Unbind(string propName, Action handler, Func<T, bool> predicate);
    }
}
