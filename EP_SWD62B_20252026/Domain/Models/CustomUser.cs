using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    //so what we will be adding here will be added
    //into the table: ASPNETUSERS
    public class CustomUser: IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
