using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        [Key] //to make it auto-generated
              //1) either manually using Guid.NewGuid();
              //2) OR we configure this in the Context 
        public Guid Id { get; set; } //9413215A-A30E-461D-9ABE-F0431025F360
        public string Username { get; set; }
        public DateTime DatePlaced { get; set; }



    }
}
