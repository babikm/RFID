using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class MaterialSchema
    {
        public string Id { get; set; }
        public string SchemaName { get; set; }
        public string MaterialType { get; set; }
        public string Characteristic { get; set; }
    }
}