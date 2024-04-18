using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EighthWebApplication.Models
{
    public class BasketForm
    {

        [Required]
        public uint? ProductID { get; set; }

        [Required]
        public uint? Count { get; set; }


    }
}
