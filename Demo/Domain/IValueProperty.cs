using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain
{
    /// <summary>
    /// 值属性声明
    /// </summary>
    public interface IValueProperty : IProperty
    {
    }

    /// <summary>
    /// 值属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IValueProperty<T> : IValueProperty
    {
    }
}
