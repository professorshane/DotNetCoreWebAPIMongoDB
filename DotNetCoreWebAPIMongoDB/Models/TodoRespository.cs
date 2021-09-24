namespace NetCoreWebAPIMongoDB.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using System.Linq;

    //MVC controllers interact with repositories to load and persist an application business model.
    //a repository allows you to populate data in memory that comes from the database in the form of the domain entities. Once the entities are in memory, they can be changed and then persisted back to the database through transactions.
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoContext _context;
        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            
            return await _context
                            .Todos
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Todo> GetTodo(long id)
        {
            FilterDefinition<Todo> filter = Builders<Todo>.Filter.Eq(m => m.Id, id);
            return _context
                    .Todos
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(Todo todo)
        {
            await _context.Todos.InsertOneAsync(todo);
        }
        public async Task<bool> Update(Todo todo)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Todos
                        .ReplaceOneAsync(
                            filter: g => g.Id == todo.Id,
                            replacement: todo);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(long id)
        {
            FilterDefinition<Todo> filter = Builders<Todo>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Todos
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Todos.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}