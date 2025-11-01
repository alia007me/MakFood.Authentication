using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Application.Command.CommandHandler.DeclaringGroup
{
    public class DeclaringGroupCommandResponse
    {
        public static DeclaringGroupCommandResponse Succeeded => new()
        {
            Success = true,
        };
        public bool Success { get; set; }

    }
}
