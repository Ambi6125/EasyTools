using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    public delegate object[] DataSource(object connection);
    public delegate T[] GenericDataSource<T>(string path);
}
