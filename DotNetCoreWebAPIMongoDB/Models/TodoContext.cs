namespace NetCoreWebAPIMongoDB.Models
{
    using NetCoreWebAPIMongoDB;
    using MongoDB.Driver;
    using System;
    using NetCoreWebAPIMongoDB.Config;

    public class TodoContext: ITodoContext
    {
        private readonly IMongoDatabase _db;
        public TodoContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Todo> Todos => _db.GetCollection<Todo>("Todos");
    }
}