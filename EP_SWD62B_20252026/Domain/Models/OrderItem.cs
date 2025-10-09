using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class OrderItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //navigational properly:
        //1) they allow you to navigate through property to get the data about book
        //2) they save you the time to make another request to the database
        //3) they save you the time to forge another inner join request
        [ForeignKey("BookFK")]
        public virtual Book Book { get; set; }
        public int BookFK { get; set; }
        public int Qty { get; set; }

        [ForeignKey("OrderFK")]
        public virtual Order Order { get; set; }
        public Guid OrderFK { get; set; }

    }
}
