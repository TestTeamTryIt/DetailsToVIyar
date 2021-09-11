using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailsToViyar
{
    public class Detail
    {
        public string Article { set; get; }
        public string Name { set; get; }
        public string Units { set; get; }
        public string Quantity { set; get; }
        public string Code { set; get; }
        public string Produser { set; get; }

        public Detail(string a, string n, string u, string q, string c)
        {
            Article = a;
            Name = n;
            Units = u;
            Quantity = q;
            Code = c;
        }
    }

}
