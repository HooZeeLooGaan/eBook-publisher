namespace EBookPlatform.Contracts;

public class DraftSubmittedEvent: IEvent
{
    public Guid SubmissionId { get; set; }
    public string Title { get; set; }
    public string AuthorEmail { get; set; }
    public int PublishYear { get; set; }
    public string Genre { get; set; }
}