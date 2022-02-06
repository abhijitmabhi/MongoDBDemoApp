using System;
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

            var recs = db.LoadRecords<PersonModel>("Users");

            foreach (var rec in recs)
            {
                Console.WriteLine($"{rec.Id}: {rec.FirstName} {rec.LastName}");

                if (rec.PermanentAddress != null)
                {
                    Console.WriteLine(rec.PermanentAddress.Street);
                }
                Console.WriteLine();
            }

            //var oneRec = db.LoadRecordById<PersonModel>("Users", new Guid("c73a6ced-7298-4738-b83c-fbfcc6ea1297"));
            //oneRec.DateOfBirth = new DateTime(1995,12,15,0,0,0, DateTimeKind.Utc);

            //db.UpsertRecord("Users", oneRec.Id, oneRec);

            //db.DeleteReord<PersonModel>("Users", oneRec.Id);


            Console.ReadLine(); 
        }
    }
}
