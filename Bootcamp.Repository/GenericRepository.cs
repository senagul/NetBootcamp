using Microsoft.EntityFrameworkCore;


namespace Bootcamp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity<int>
    {
        public DbSet<T> DbSet { get; set; } // dbset vritabanıyla etkileşime geçmemizi sağlayan sınıftır. Örneğin products entitysinde add, update, delete metotlarını kullanabilmemize yarar
        protected AppDbContext Context;
        public GenericRepository(AppDbContext context)
        {
            DbSet = context.Set<T>(); // T ye hangi classı verirsek DbSet o classın DbSeti olur
            Context = context;
        }
        public async Task<T> Create(T entity)
        {
            await DbSet.AddAsync(entity);
            return entity;

        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity!);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            var list = await DbSet.ToListAsync();
            return list.AsReadOnly();
        }

        public async Task<IReadOnlyList<T>> GetAllByPage(int page, int pageSize)
        {
            var list = await DbSet.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return list.AsReadOnly();
        }

        public async Task<T?> GetById(int id)
        {
            var result = await DbSet.FindAsync(id);
            return result;
        }

        public Task Update(T entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public Task<bool> HasExist(int id)
        {
            return DbSet.AnyAsync(x => x.Id == id);
        }
    }
}
