using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDA.Domain.Identity
{
    public class IdentityRoles
    {
        public static readonly string AdminId = Guid.NewGuid().ToString();
        public const string AdminName = "Admin";
    }
}
