using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Worker
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //[Required]
        public string FirstName { get; set; }
        //[Required]
        public string SecondName { get; set; }

        public string Name
        {
            get
            {
                return string.Format("{0} {1}", FirstName, SecondName);
            }
        }
    }
}
