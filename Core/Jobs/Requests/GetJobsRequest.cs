﻿using Core.Jobs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Jobs.Requests
{
    public class GetJobsRequest : IRequest<JobListResponse>
    {
        public int PageNumber { get; set; }
    }
}
