using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailsToViyar
{
    public class SupListEnumerator
    {
        int nIndex;
        SuppliersList collection;
        public SupListEnumerator(SuppliersList coll)
        {
            collection = coll;
            nIndex = -1;
        }

        public bool MoveNext()
        {
            nIndex++;
            return nIndex < collection.supplierList.Length;
        }

        public Supplier Current => collection.supplierList[nIndex];
    }
}
