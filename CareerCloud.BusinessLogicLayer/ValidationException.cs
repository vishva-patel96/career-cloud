﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
    public class ValidationException : Exception
    {
        public int code;

        public ValidationException(int Code, string Message) : base(Message)
        {
            code = Code;
        }
    }
}
