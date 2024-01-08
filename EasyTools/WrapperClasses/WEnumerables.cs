using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.WrapperClasses
{
    public static class WEnumerables
    {
        public static void PopulateRandom(IEnumerable<int> enumerable, int min, int max)
        {
            if (enumerable.Any())
            {
                return;
            }
            for(int i = 0; i < enumerable.Count(); i++)
            {
                Random rng = new Random();
            }
                throw new NotImplementedException();
        }
    }
}
