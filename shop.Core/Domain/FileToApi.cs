﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string? ExistingFilePath { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}
