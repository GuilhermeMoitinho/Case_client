using CaseClient.Data.CustomerContext;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;

namespace CaseClient.WebAPI.Extensions;

public static class InitialDataExtension
{
    public static void AddInitialData(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var postgresContext = scope.ServiceProvider.GetRequiredService<PostgreSqlDbContext>();

            postgresContext.Database.EnsureDeleted();
            postgresContext.Database.EnsureCreated();
        }

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            var mongoClient = new MongoClient("mongodb://my_mongo_user:my_mongo_password@localhost:27017");
            var mongoDatabase = mongoClient.GetDatabase("case_client");
        }
    }

