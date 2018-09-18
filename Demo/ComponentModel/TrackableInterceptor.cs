using Castle.DynamicProxy;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ComponentModel
{
    /// <summary>
    /// <see cref="TrackableBase"/>的拦截器，实现属性变更通知
    /// </summary>
    public class TrackableInterceptor : IInterceptor
    {
        static ProxyGenerator _proxyGenerator = new ProxyGenerator();
        static TrackableInterceptor Interceptor = new TrackableInterceptor();

        /// <summary>
        /// 创建<see cref="TrackableBase"/>实例的代理
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>() where T : TrackableBase
        {
            return _proxyGenerator.CreateClassProxy<T>(Interceptor);
        }

        /// <summary>
        /// 获取类型代理前的类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetRawType(Type type)
        {
            while (ProxyServices.IsDynamicProxy(type))
                type = type.BaseType;
            return type;
        }

        public void Intercept(IInvocation invocation)
        {
            System.Diagnostics.Debug.WriteLine(invocation.Method.Name);
            //通过拦截器实现事件变更通知
            if (invocation.Method.IsPublic && invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("set_"))
            {
                var property = invocation.Method.Name.Substring(4);
                var target = invocation.InvocationTarget as TrackableBase;
                var oldValue = Demo.Reflection.TypeDescriptor.GetValue(target, property);
                var newValue = invocation.Arguments[0];
                invocation.Proceed();
                if (!object.Equals(oldValue, newValue))
                {
                    target.RaisePropertyChanged(property);
                    target.RaiseValueChanged(property, newValue, oldValue);
                }
            }
            else
                invocation.Proceed();
        }
    }
}
