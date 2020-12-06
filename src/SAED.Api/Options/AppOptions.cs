﻿using System.Collections.Generic;

namespace SAED.Api.Options
{
    public class AppOptions
    {
        public TokenOptions Token { get; set; }
        public ICollection<string> Roles { get; set; }
        public ICollection<UserOptions> Users { get; set; }
    }
}