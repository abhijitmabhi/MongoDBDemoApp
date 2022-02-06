using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var myPerson = new PersonModel
            //{
            //    FirstName = "Shaswati",
            //    LastName = "Sarkar",
            //    PermanentAddress = new AddressModel
            //    {
            //        HouseNumber = "40H",
            //        Street = "Kurt-Schumacher",
            //        PostalCode = 54885,
            //        State = "RHPLZ"
            //    }
            //};

            MongoCRUD db = new MongoCRUD("AddressBook");
            //db.InserRecord("Users", myPerson);

            //var recs = db.LoadRecords<PersonModel>("Users");

            //foreach (var rec in recs)
            //{
            //    Console.WriteLine($"{rec.Id}: {rec.FirstName} {rec.LastName}");

            //    if(rec.PermanentAddress != null)
            //    {
            //        Console.WriteLine(rec.PermanentAddress.Street);
            //    }
            //    Console.WriteLine();    
            //}

            var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("c73a6ced-7298-4738-b83c-fbfcc6ea1297"));
            oneRec.DateOfBirth = new DateTime(1995,12,15,0,0,0, DateTimeKind.Utc);

            db.UpsertRecord("Users", oneRec.Id, oneRec);


            Console.ReadLine(); 
        }
    }

    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel PermanentAddress { get; set; }
        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
    }

    public class AddressModel
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public int PostalCode { get; set; }
        public string State { get; set; }
    }

    public class MongoCRUD
    {
        private readonly IMongoDatabase db;

        public MongoCRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InserRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T> (string table)
        {
            var collection = db.GetCollection<T>(table);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id),
                record);
        }

        public void DeleteReord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}
