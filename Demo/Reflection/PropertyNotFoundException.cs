using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Reflection
{
    [Serializable]
    public class PropertyNotFoundException : AppException
    {
        public PropertyNotFoundException(string property) : base(property) { }
    }
}
