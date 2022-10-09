﻿using System;
namespace UserRegistrationTask.Utilities
{
    public class JsonResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ResponseData { get; set; }
        public object ErrorList { get; set; }
    }
}

