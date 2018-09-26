using Demo.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Demo.Domain.Data;

namespace Demo.Domain
{
    /// <summary>
    /// 实体
    /// </summary>
    [Serializable]
    public abstract class Entity : TrackableBase, IEntity
    {
        IFieldData _fieldData;

        /// <summary>
        /// 字段数据,属性的值保存在
        /// </summary>
        protected IFieldData FieldData
        {
            get { return _fieldData ?? (_fieldData = DataFactory.Instance.Create(GetType())); }
        }

        object IEntity.Id
        {
            get => GetId();
            set => SetId(value);
        }

        /// <summary>
        /// 重写此方法获取实体主键<see cref="IEntity.Id"/>的值
        /// </summary>
        /// <returns></returns>
        protected abstract object GetId();

        /// <summary>
        /// 重写此方法设置实体主键<see cref="IEntity.Id"/>的值
        /// </summary>
        /// <param name="id"></param>
        protected abstract void SetId(object id);

        [NonSerialized]
        IPropertyContainer _propertyContainer;
        /// <summary>
        /// 属性容器
        /// </summary>
        public IPropertyContainer PropertyContainer
        {
            get { return _propertyContainer ?? (_propertyContainer = PropertyContainerFactory.Instance.Get(GetType())); }
        }

        public T Get<T>([CallerMemberName]string propertyName = null)
        {
            var property = PropertyContainer.Get(propertyName);
            return Get<T>(property);
        }

        public T Get<T>(IProperty property)
        {
            if (property is ICaculateProperty)
                return Get<T>(property as ICaculateProperty);
            if (property is IListProperty)
                return Get<T>(property as IListProperty);
            if (property is IRefEntityProperty)
                return Get<T>(property as IRefEntityProperty);
            return default(T);
        }

        public T Get<T>(IProperty<T> property)
        {
            return Get<T>((IProperty)property);
        }

        public T Get<T>(IRefEntityProperty property)
        {
            return default(T);
        }

        public T Get<T>(IRefEntityProperty<T> property)
        {
            return Get<T>((IRefEntityProperty)property);
        }

        public T Get<T>(IListProperty property)
        {
            return default(T);
        }

        public T Get<T>(IListProperty<T> property)
        {
            return Get<T>((IListProperty)property);
        }

        public T Get<T>(ICaculateProperty property)
        {
            return default(T);
        }

        public T Get<T>(ICaculateProperty<T> property)
        {
            return Get<T>((ICaculateProperty)property);
        }

        public void Set<T>(T value, [CallerMemberName]string propertyName = null)
        {
            var property = PropertyContainer.Get(propertyName);
            Set<T>(value, property);
        }

        public void Set<T>(T value, IProperty property)
        {
            if (property is IRefProperty)
                Set(value, property as IRefProperty);
        }

        public void Set<T>(T value, IProperty<T> property)
        {
            Set<T>(value, (IProperty)property);
        }

        public void Set<T>(T value, IRefProperty property)
        {

        }

        public void Set<T>(T value, IRefProperty<T> property)
        {
            Set<T>(value, (IRefProperty)property);
        }
    }
}
