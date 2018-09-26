using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain.Data
{
    public interface IField
    {
        IProperty Property { get; }

        object Value { get; set; }
    }

    public interface IField<T> : IField
    {
        new T Value { get; set; }
    }
}
