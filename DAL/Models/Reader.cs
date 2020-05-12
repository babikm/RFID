using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace DAL.Models
{
    public class Reader
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ReaderNumber { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Description { get; set; }

        [DllImport("function.dll")]
        public static extern int GetVersionNum([In]byte[] strVersionNum);

        [DllImport("function.dll")]
        public static extern int MF_Read(byte mode, byte blk_add, byte num_blk, [In]byte[] snr, [In]byte[] buffer);

        [DllImport("function.dll")]
        public static extern int MF_Write(byte mode, byte blk_add, byte num_blk, [In]byte[] snr, [In]byte[] buffer);

        [DllImport("function.dll")]
        public static extern int ControlBuzzer(int freq, int duration, [In]byte[] buffer);
    }
}
