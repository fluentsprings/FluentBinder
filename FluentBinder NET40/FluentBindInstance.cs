using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace FluentBinder
{
    public class FluentBindInstance<T> : IFluentBindInstance<T> where T : INotifyPropertyChanged
    {
        #region Fields

        private readonly T _instance;
        private readonly IDictionary<string, List<KeyValuePair<Action, Func<T, bool>>>> _bindings = new Dictionary<string, List<KeyValuePair<Action, Func<T, bool>>>>();

        #endregion

        #region Constructors

        public FluentBindInstance(T instance)
        {
            _instance = instance;
            _instance.PropertyChanged += PropertyChanged;
        }

        #endregion

        #region Implementation of IFluentBindInstance<T>

        public void Bind<TProperty>(Expression<Func<T, TProperty>> viewModelProp, Action action)
        {
            BindHandler(viewModelProp, action, null);
        }

        public void Bind<TProperty>(Expression<Func<T, TProperty>> viewModelProp, Action action, Func<T, bool> condition)
        {
            BindHandler(viewModelProp, action, condition);
        }

        public void Unbind(string propName)
        {
            if (_bindings.ContainsKey(propName))
            {
                _bindings.Remove(propName);
            }
        }

        public void Unbind(string propName, Action handler)
        {
            Unbind(propName, (z => z.Key == handler));
        }

        public void Unbind(string propName, Action handler, Func<T, bool> predicate)
        {
            Unbind(propName, (z => z.Key == handler && z.Value == predicate));
        }

        public void Reset()
        {
            _bindings.Clear();
        }

        private void Unbind(string propName, Func<KeyValuePair<Action, Func<T, bool>>, bool> predicate)
        {
            if (_bindings.ContainsKey(propName))
            {
                var result = _bindings[propName].Where(predicate);
                if (result.Any())
                {
                    foreach (var item in result)
                    {
                        _bindings[propName].Remove(item);
                    }
                }
            }
        }

        private void BindHandler<TProperty>(Expression<Func<T, TProperty>> viewModelProp, Action action,
            Func<T, bool> predicate)
        {
            var propExp = viewModelProp.Body as MemberExpression;
            if (propExp != default(MemberExpression))
            {
                if (!_bindings.ContainsKey(propExp.Member.Name))
                {
                    _bindings[propExp.Member.Name] = new List<KeyValuePair<Action, Func<T, bool>>>();
                }

                _bindings[propExp.Member.Name].Add(new KeyValuePair<Action, Func<T, bool>>(action, predicate));
            }
        }

        private void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_bindings.ContainsKey(e.PropertyName))
            {
                foreach (var handler in _bindings[e.PropertyName])
                {
                    if (handler.Value == null || (handler.Value != null && handler.Value(_instance)))
                    {
                        handler.Key();
                    }
                }
            }
        }

        #endregion
    }
}
