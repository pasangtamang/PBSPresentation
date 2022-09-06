namespace PbsUI.Models
{
    public partial class EventModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string EmailId { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string Profession { get; set; } = null!;
        public string? FeedBack { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
