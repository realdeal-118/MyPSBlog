using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPSBlog.Models
{
    public class Comment
    {
        public Comment()
        {
            this.CreatedOn = DateTime.UtcNow; //This fixed the date time issue//
        }

        [Key]
        public int Id { get; set; }

        public int BlogId { get; set; }
        public string AuthorID { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Body { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog{ get; set; }
        [ForeignKey("AuthorID")]
        public virtual ApplicationUser Author { get; set; }

    }
}