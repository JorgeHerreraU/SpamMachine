namespace SpamMachine.DTOs;

public class MessageQueueReadDTO
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public string Schedule { get; set; } = null!;
    public string Status { get; set; } = null!;
}