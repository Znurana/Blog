using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class AddProfileImage
    {
        public int WriterID { get; set; }
        public string WriterName { get; set; }
        public string WriterAbout { get; set; }
        public IFormFile WriteImage { get; set; }
        public string WriteMail { get; set; }
        public string WritePassword { get; set; }
        public bool WriterStatus { get; set; }
    }
}
