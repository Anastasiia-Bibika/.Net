using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Laboratory2.Models;
namespace Laboratory2.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }
        public bool Archived { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<CommentToPost> Comments { get; }
    }

    public class CommentToPost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PublishedOn { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public int Likes { get; set; }
        public Post Post { get; set; }
    }
}
