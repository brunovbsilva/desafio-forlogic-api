namespace Domain.Services.Requests;

public class RegisterPersonRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool Active { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }
    public string? OtherInformation { get; set; }
    public string? Interests { get; set; }
    public string? Feelings { get; set; }
    public string? Values { get; set; }
}
