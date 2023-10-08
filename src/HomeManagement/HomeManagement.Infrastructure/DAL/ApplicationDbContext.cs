﻿using HomeManagement.Business.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HomeManagement.Infrastructure.DAL
{
    public class ApplicationDbContext : DbContext, IDataContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public void Migrate()
        {
            Database.Migrate();
        }

        public IQueryable<T> GetData<T>(bool trackingChanges = false, bool ignoreQueryFilters = false) where T : class
        {
            var set = Set<T>().AsQueryable();

            if (ignoreQueryFilters)
            {
                set = set.IgnoreQueryFilters();
            }

            return trackingChanges ? set.AsTracking() : set.AsNoTrackingWithIdentityResolution();
        }

        public ValueTask<T> GetAsync<T>(params object[] keyValues) where T : class
            => Set<T>().FindAsync(keyValues);

        public void Insert<T>(T entity) where T : class => Set<T>().Add(entity);

        public void Delete<T>(T entity) where T : class => Set<T>().Remove(entity);

        public void Delete<T>(IEnumerable<T> entities) where T : class => Set<T>().RemoveRange(entities);

        public Task SaveAsync(CancellationToken cancellationToken)
            => SaveChangesAsync(cancellationToken);

        public Task ExecuteTransactionAsync(Func<Task> action)
        {
            var strategy = Database.CreateExecutionStrategy();
            return strategy.ExecuteAsync(async () =>
            {
                using var transaction = await Database.BeginTransactionAsync().ConfigureAwait(false);
                await action.Invoke().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);
            });
        }
    }
}
