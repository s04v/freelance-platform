using Core.Common;
using Core.Jobs;
using Core.Jobs.Applications.Entities;
using Core.Jobs.Attachment;
using Core.Jobs.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class JobRepository : IJobRepository
    {
        public BaseDbContext _dbContext;

        public JobRepository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Job job, CancellationToken token)
        {
            await _dbContext.Job.AddAsync(job, token);
        }

        public async Task<IEnumerable<Job>> GetJobs(CancellationToken token)
        {
            return await _dbContext.Job
                .ToListAsync(token);
        }

        public async Task<IEnumerable<Job>> GetJobsPage(int pageNumber, int pageSize, CancellationToken token)
        {
            return await _dbContext.Job
                .OrderByDescending(o => o.PublicationDate)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetJobsCount(CancellationToken token)
        {
            return await _dbContext.Job.CountAsync();
        }

        public async Task<Job?> GetJob(Expression<Func<Job, bool>> predicate, CancellationToken token)
        {
            return await _dbContext.Job
                .Where(predicate)
                .FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<Job>> GetJobsOfAuthor(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Job
                .Where(o => o.AuthorUuid == uuid)
                .ToListAsync(token);
        }

        public async Task CreateApplication(JobApplication jobApplication, CancellationToken token)
        {
            await _dbContext.JobApplication
                .AddAsync(jobApplication, token);
        }

        public async Task<JobApplication?> GetMyApplicationForJob(Guid jobUuid, Guid userUuid, CancellationToken token)
        {
            return await _dbContext.JobApplication
                .Where(o => o.JobUuid == jobUuid && o.UserUuid == userUuid)
                .FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<JobApplication>> GetMyApplicationsWithJobs(Guid userUuid, CancellationToken token)
        {
            return await _dbContext.JobApplication
                .Where(o => o.UserUuid == userUuid)
                    .Include(o => o.Job)
                .ToListAsync(token);
        }

        public async Task<JobApplication?> GetApplication(Expression<Func<JobApplication, bool>> predicate, CancellationToken token)
        {
            return await _dbContext.JobApplication
                .Where(predicate)
                .FirstOrDefaultAsync(token);
        }

        public async Task<IEnumerable<JobApplication?>> GetApplications(Guid jobUuid, CancellationToken token)
        {
            var q = _dbContext.JobApplication
                .AsNoTracking()
                    .Include(o => o.User)
                .Where(o => o.JobUuid == jobUuid);

            var res = await  q.ToListAsync(token);
            return res;        
        }

        public async Task AddJobAttachment(JobAttachment attachment, CancellationToken token)
        {
            await _dbContext.JobAttachment.AddAsync(attachment, token);
        }

        public async Task<IEnumerable<JobAttachment>> GetJobAttachmentByJobUuid(Guid jobUuid, CancellationToken token)
        {
            return await _dbContext.JobAttachment
                .AsNoTracking()
                .Where(o => o.JobUuid == jobUuid)
                .ToListAsync();
        }

        public async Task<JobAttachment?> GetJobAttachmentByUuid(Guid uuid, CancellationToken token)
        {
            return await _dbContext.JobAttachment
                .Where(o => o.Uuid == uuid)
                .FirstOrDefaultAsync(token);
        }

        public async Task<int> GetApplicationCountOfUser(Guid uuid, CancellationToken token)
        {
            return await _dbContext.Job
                .Where(o => o.AuthorUuid == uuid)
                .CountAsync(token);
        }

        public async Task<int> GetJobCountOfUser(Guid uuid, CancellationToken token)
        {
            return await _dbContext.JobApplication
                .Where(o => o.UserUuid == uuid)
                .CountAsync(token);
        }

        public void Remove<T>(T entity) where T : class
        {
            _dbContext.Remove(entity);
        }

        public async Task Save(CancellationToken token)
        {
            await _dbContext.SaveChangesAsync(token);
        }

    }
}
