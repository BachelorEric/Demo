using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain
{
    /// <summary>
    /// 引用属性声明
    /// </summary>
    public interface IRefProperty : IProperty
    {
    }

    /// <summary>
    /// 引用属性声明
    /// </summary>
    /// <typeparam name="T">属性值类型</typeparam>
    public interface IRefProperty<T> : IRefProperty
    {

    }
}
