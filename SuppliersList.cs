using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailsToViyar
{
    public class SuppliersList
    {

        public Supplier[] supplierList;

        public SuppliersList()
        {

            supplierList = new Supplier[]
            {
                new Supplier("Viyar"), // SP1
                new Supplier("MT"), // SP2
                new Supplier("Вибор"), // SP3
                new Supplier("Hafele"), // SP4
                new Supplier("ADS"), // SP5
                new Supplier("VDM"), // SP6
                new Supplier("KRONAS") // SP7
            };
        }

        public SupListEnumerator GetEnumerator()
        {
            return new SupListEnumerator(this);
        }

        public void AddDetail(int index, string a, string n, string u, string q, string c)
        {
            supplierList[index].AddDetail(a, n, u, q, c);
        }

        public int Length()
        {
            return supplierList.Length;
        }
        public Supplier GetSupplier(int index)
        {
            return supplierList[index];
        }

        public int GetSupplierDetCount (int index)
        {
            return supplierList[index].DetailsCount();
        }      
    }
}
