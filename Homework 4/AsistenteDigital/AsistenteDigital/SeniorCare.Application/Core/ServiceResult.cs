﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniorCare.Application.Core
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "")
        {
            return new ServiceResult<T> { Success = true, Data = data, Message = message };
        }

        public static ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T> { Success = false, Message = message };
        }
    }
}

