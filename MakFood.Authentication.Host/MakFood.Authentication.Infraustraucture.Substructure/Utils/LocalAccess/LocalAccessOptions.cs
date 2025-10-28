using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Utils.LocalAccess
{
    public class LocalAccessOptions
    {
        public string AllowedHost { get; set; } 
        public int AllowedPort { get; set; }
    }
}
