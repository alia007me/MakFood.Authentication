using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Domain.Model.Base
{
    public class BaseEntity<TId>
    {
        public TId Id { get; protected set; }
    }
}
