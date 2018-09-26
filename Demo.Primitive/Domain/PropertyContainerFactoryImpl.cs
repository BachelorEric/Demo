using Demo.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Primitive.Domain
{
    class PropertyContainerFactoryImpl : PropertyContainerFactory
    {
        public override IPropertyContainer Get(Type type)
        {
            var container = new PropertyContainer();
            return container;
        }
    }
}
