using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Journal: IPaper
    {
        [Key(), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public double WholesalePrice { get; set; }
        public int PublishedYear { get; set; }

        [ForeignKey("CategoryFK")]
        public virtual Category Category { get; set; } //this is called a navigational property
        public int CategoryFK { get; set; } //this is a foreign key
        public int Stock { get; set; }
        public string? Path { get; set; }
        public int Volume { get; set; }
        public int IssueNo { get; set; }

        public string Get()
        {
            return $"{Title} | {WholesalePrice} | {Volume}:{IssueNo}";
        }
    }
}
