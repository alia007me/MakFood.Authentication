﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MakFood.Authentication.Infraustraucture.Substructure.Base.DomainExceptions.BaseException
{
    public class DomainException : Exception
    {
        public DomainException()
        {
        }

        public DomainException(string? message) : base(message)
        {
        }

        public DomainException(string? message, Exception? innerException) : base(message, innerException)
        {
        }


    }
}
