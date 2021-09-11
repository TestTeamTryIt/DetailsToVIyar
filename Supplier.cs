using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailsToViyar
{
    public class Supplier
    {
        public string Name { get; }
        public List<Detail> detailsList;

        public SupEnumerator GetEnumerator()
        {
            return new SupEnumerator(this);
        }

        public Supplier(string name)
        {
            Name = name;
            detailsList = new List<Detail>();
        }

        public void AddDetail(string a, string n, string u, string q, string c)
        {
            detailsList.Add(new Detail(a, n, u, q, c));
        }

        public Detail GetDetail(int i)
        {
            return detailsList[i];
        }

        public int DetailsCount()
        {
            return detailsList.Count;
        }

    }
}
