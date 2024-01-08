﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    public interface IExecutable
    {
        void Execute();
    }
    public interface IExecutable<TReturn>
    {
        TReturn Execute();
    }
}
