using Microsoft.FSharp.Data.UnitSystems.SI.UnitNames;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UristBot
{
    internal class MongoConnector
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<SielomUser> collUsers;

        public MongoConnector()
        {
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("UristBot");
            collUsers = database.GetCollection<SielomUser>("users");
        }

        public async Task AddUserAsync(SielomUser user)
        {
            await collUsers.InsertOneAsync(user);
        }

        public async Task<List<SielomUser>> GetUsers()
        {
            using var cursor = await collUsers.FindAsync(new BsonDocument());
            return cursor.ToList();
        }

    }
}
