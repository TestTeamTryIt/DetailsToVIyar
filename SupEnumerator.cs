using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailsToViyar
{
    public class SupEnumerator
    {
        int nIndex;
        Supplier collection;
        public SupEnumerator(Supplier coll)
        {
            collection = coll;
            nIndex = -1;
        }

        public bool MoveNext()
        {
            nIndex++;
            return nIndex < collection.detailsList.Count;
        }

        public Detail Current => collection.detailsList[nIndex];
    }
}
