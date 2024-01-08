using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.Validation;

namespace EasyTools.MySqlDatabaseTools
{
    public interface IDataRepo<T>
    {
        IValidationResponse Add(T item);
        IValidationResponse Change(T item);
        IValidationResponse Remove(T item);
        IReadOnlyList<T> GetAll();
        IReadOnlyList<T> GetWhere<TArg>(TArg arg);
    }

}
