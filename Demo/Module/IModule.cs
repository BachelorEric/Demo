using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Module
{
    /// <summary>
    /// 模块声明
    /// </summary>
    public interface IModule
    {
        void Init(IApp app);
    }
}
