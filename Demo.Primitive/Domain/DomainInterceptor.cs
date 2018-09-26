using Castle.DynamicProxy;
using Castle.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Domain;

namespace Demo.Primitive.Domain
{
    /// <summary>
    /// <see cref="Entity"/>的拦截器，实现属性变更通知
    /// </summary>
    class DomainInterceptor : IInterceptor
    {
        void IInterceptor.Intercept(IInvocation invocation)
        {
            System.Diagnostics.Debug.WriteLine(invocation.Method.Name);
            //通过拦截器实现事件变更通知
            if (invocation.Method.IsPublic && invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("set_"))
            {
                var target = invocation.InvocationTarget as Entity;
                Action<object, string> method = target.Set<object>;
                object[] args = new object[2];
                args[0] = invocation.Arguments[0];
                args[1] = invocation.Method.Name.Substring(4);
                invocation.ReturnValue = method.Method.GetGenericMethodDefinition().MakeGenericMethod(invocation.Method.ReturnType).Invoke(target, args);
            }
            else
                invocation.Proceed();
        }
    }
}
