﻿using System.Collections.Generic;

namespace Sat.Recruitment.Api.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
