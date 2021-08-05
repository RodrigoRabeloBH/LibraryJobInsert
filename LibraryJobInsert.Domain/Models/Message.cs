namespace LibraryJobInsert.Domain.Models
{
    public class Message
    {
        public int DeliveryTag { get; set; }
        public string Content { get; set; }
        public bool Executed { get; set; }
    }
}
