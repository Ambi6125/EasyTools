using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    /// <summary>
    /// Indicates this class can be converted to a DTO and provides a method definition of doing so.
    /// </summary>
    /// <typeparam name="TDataTransfer">The type of the DTO this should be converted to.</typeparam>
    public interface ITransferable<TDataTransfer>
    {
        TDataTransfer AsDTO();
    }
}
