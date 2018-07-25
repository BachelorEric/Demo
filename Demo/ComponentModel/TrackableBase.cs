using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Demo.ComponentModel
{
    [Serializable]
    public class TrackableBase : INotifyPropertyChanged, ITrackable
    {
        bool _suppressNotifyChanged;

        [NonSerialized]
        PropertyChangedEventHandler _propertyChanged;
        [NonSerialized]
        EventHandler<ValueChangedEventArgs> _valueChanged;
        
        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged = (PropertyChangedEventHandler)Delegate.Combine(_propertyChanged, value); }
            remove { _propertyChanged = (PropertyChangedEventHandler)Delegate.Remove(_propertyChanged, value); }
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged
        {
            add { _valueChanged = (EventHandler<ValueChangedEventArgs>)Delegate.Combine(_valueChanged, value); }
            remove { _valueChanged = (EventHandler<ValueChangedEventArgs>)Delegate.Remove(_valueChanged, value); }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (!_suppressNotifyChanged)
                _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnValueChanged(string propertyName, object newValue, object oldValue)
        {
            if (!_suppressNotifyChanged)
                _valueChanged?.Invoke(this, new ValueChangedEventArgs(propertyName, newValue, oldValue));
        }

        public void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        public void RaiseValueChanged(string propertyName, object newValue, object oldValue)
        {
            OnValueChanged(propertyName, newValue, oldValue);
        }

        public void ResumeNotifyChanged()
        {
            _suppressNotifyChanged = false;
        }

        public void SuppressNotifyChanged()
        {
            _suppressNotifyChanged = true;
        }
    }
}
