using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain
{
    /// <summary>
    /// 属性声明
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        string PropertyName { get; set; }
        /// <summary>
        /// 属性值类型
        /// </summary>
        Type PropertyType { get; set; }
        /// <summary>
        /// 属性所有者类型
        /// </summary>
        Type OwnerType { get; set; }
        /// <summary>
        /// 属性声明类型
        /// </summary>
        Type DeclareType { get; set; }
    }

    /// <summary>
    /// 属性声明
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IProperty<T> : IProperty
    {

    }
}
