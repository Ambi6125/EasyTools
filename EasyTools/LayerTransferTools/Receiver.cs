using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.LayerTransferTools
{
    public class Receiver<T> : IDisposable where T : DTO
    {
        private readonly T[] obj;

       

        public Receiver(Transferer<T> transferer)
        {
           obj = (T[])transferer.Transfer();
        }

        public object[] Display()
        {
            return obj;
        }

        public void Dispose()
        {
            //Nothing
        }
    }
}
