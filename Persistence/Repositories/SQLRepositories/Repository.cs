using SharedKernel.Domain.Entities.Primitives;

namespace Persistence.Repositories.SQLRepositories;

internal abstract class Repository<TEntity> where TEntity : Entity
{
    //protected readonly SharedDbContext DbContext;

    //protected Repository(SharedDbContext dbContext)
    //{
    //    DbContext = dbContext;
    //}

    ////public async Task<TEntity?> GetByIdAsync(int Id)
    ////{
    ////    return await DbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == Id);
    ////}

    //public async Task Add(TEntity entity)
    //{
    //    DbContext.Set<TEntity>().Add(entity);
    //    await DbContext.SaveChangesAsync();
    //}

    //public async Task Update(TEntity entity)
    //{
    //    DbContext.Set<TEntity>().Update(entity);
    //    await DbContext.SaveChangesAsync();
    //}

    //public async Task Delete(TEntity entity)
    //{
    //    DbContext.Set<TEntity>().Remove(entity);
    //    await DbContext.SaveChangesAsync();
    //}
}
