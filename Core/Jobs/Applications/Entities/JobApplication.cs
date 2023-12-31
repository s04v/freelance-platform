﻿using Core.Common;
using Core.Jobs.Entities;
using Core.Users.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Applications.Entities
{
    public class JobApplication
    {
        public Guid Uuid { get; set; }
        public Guid UserUuid { get; set; }
        public Guid JobUuid { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string? Message { get; set; }
        public ApplicationStatus Status { get; set; }
        public User? User { get; set; }
        public Job? Job { get; set; }
    }
}
