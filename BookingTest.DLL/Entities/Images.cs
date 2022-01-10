using BookingTest.DLL.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookingTest.DLL.Entities
{
    public class Images:BaseEntity
    {
        [Required]
        public DateTime DateTime { get; set; }
         
        public long RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public Room Room { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public long Size { get; set; }

        [StringLength(127 + 1 + 127)]  
        public string MimeType { get; set; }
          
    }
}
