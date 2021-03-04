using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly RpContext ctx;

        public BaseRepository(RpContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await ctx.AddAsync(entity);
        }

        public async Task<bool> CompleteAsync()
        {
            return await ctx.SaveChangesAsync() > 0;
        }

        public void Remove<T>(T entity) where T : class
        {
            ctx.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            ctx.Update(entity);
        }
    }
}
