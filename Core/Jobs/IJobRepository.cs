﻿using Core.Jobs.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Jobs
{
    public interface IJobRepository
    {
        Task Add(Job job, CancellationToken token);
        Task<IEnumerable<Job>> GetJobs(CancellationToken token);
        Task<IEnumerable<Job>> GetJobsPage(int pageNumber, int pageSize, CancellationToken token);
        Task<int> GetJobsCount(CancellationToken token);
        Task<Job?> GetJob(Expression<Func<Job, bool>> predicate, CancellationToken token);
        Task<IEnumerable<Job>> GetJobsOfAuthor(Guid uuid, CancellationToken token);
        Task CreateApplication(JobApplication jobApplication, CancellationToken token);
        Task<JobApplication> GetMyApplicationForJob(Guid jobUuid, Guid userUuid, CancellationToken token);
        Task<IEnumerable<JobApplication>> GetMyApplicationsWithJobs(Guid userUuid, CancellationToken token);
        Task<JobApplication?> GetApplication(Expression<Func<JobApplication, bool>> predicate, CancellationToken token);
        Task<IEnumerable<JobApplication?>> GetApplications(Guid jobUuid, CancellationToken token);
        void Remove<T>(T entity) where T : class;
        Task Save(CancellationToken token);
    }
}