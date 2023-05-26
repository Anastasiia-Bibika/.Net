namespace Laboratory2.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
        public DateTime SentAt { get; set; }
        public string Sender { get; set; }
        public string Content { get; set; }
    }
}
