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
            var myPerson = new PersonModel
            {
                FirstName = "Shaswati",
                LastName = "Sarkar",
                PermanentAddress = new AddressModel
                {
                    HouseNumber = "40H",
                    Street = "Kurt-Schumacher",
                    PostalCode = 54885,
                    State = "RHPLZ"
                }
            };

            MongoCRUD db = new MongoCRUD("AddressBook");
            db.InserRecord("Users", myPerson);
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
    }
}
