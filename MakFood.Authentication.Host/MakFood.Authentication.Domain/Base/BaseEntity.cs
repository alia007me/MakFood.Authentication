using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Base
{
    public class BaseEntity<Tid>
    {
        public Tid Id { get; private set; }
    }
}
