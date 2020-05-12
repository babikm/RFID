using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Required]
        public string TagId { get; set; }
        [Required(ErrorMessage = "{0} to pole jest wymagane")]
        public string CompanyName { get; set; }
        public string SupplierCode { get; set; }
        [Required(ErrorMessage = "{0} to pole jest wymagane")]
        public string MaterialType { get; set; }
        [Required(ErrorMessage = "{0} to pole jest wymagane")]
        [Range(1, 2000, ErrorMessage = "Podaj wartość pomiędzy 1 a 2000")]
        public int Weight { get; set; }
        [Required(ErrorMessage = "{0} to pole jest wymagane")]
        public string Characeteristic { get; set; }
        [Required(ErrorMessage = "{0} to pole jest wymagane")]
        public string Comments { get; set; }
        [Required(ErrorMessage = "{0} to pole jest wymagane")]
        public DateTime Date { get; set; }
        //[Required(ErrorMessage = "{0} to pole jest wymagane")]
        public Worker Worker1 { get; set; }
        //[Required(ErrorMessage = "{0} to pole jest wymagane")]
        public Worker Worker2 { get; set; }

        public Extruder ExtruderName { get; set; }
        public string Status { get; set; }
    }
}
