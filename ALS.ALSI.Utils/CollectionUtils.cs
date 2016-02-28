using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALS.ALSI.Utils
{
    public static class CollectionUtils
    {
        public static Collection<T> ToCollection<T>(this IEnumerable<T> data)
        {
            return new Collection<T>(data.ToList());
        }
    }
}
