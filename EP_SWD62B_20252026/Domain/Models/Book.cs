using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public  class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public double WholesalePrice { get; set; }
        public int Section { get; set; }
        public string OrderedFrom { get; set; }
    }
}
