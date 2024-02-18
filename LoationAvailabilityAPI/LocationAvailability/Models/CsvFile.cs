using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocationAvailability.Models
{
    [Table("CsvFiles")]
    public class CsvFile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FileName { get; set; }

        [Required]
        public byte[] CsvFileData { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; }
    }
}