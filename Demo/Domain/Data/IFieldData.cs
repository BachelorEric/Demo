﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain.Data
{
    public interface IFieldData
    {
        IField Get(IProperty property);
    }
}
