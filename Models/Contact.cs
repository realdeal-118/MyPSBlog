using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPSBlog.Models
{
    public class Contact
    {
        [required]
        public DateTime Created;

        [required]
        public int Id { get; set; }

        [required]
        public string SenderName { get; set; }

        [required]
        public string SenderEmail { get; set; }

        [required]
        public string SenderMessage { get; set; }



    }

}