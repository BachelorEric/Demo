using Demo.Domain;
using Demo.Module;
using Demo.Primitive.Domain;
using System;
[assembly: Demo.Module.ModuleInfo(typeof(Demo.Primitive.Module))]

namespace Demo.Primitive
{
    class Module : IModule
    {
        public void Init(IApp app)
        {
            DomainFactory.SetDomainInterceptor(new DomainInterceptor());
            PropertyContainerFactory.SetFactory(new PropertyContainerFactoryImpl());
        }
    }
}
