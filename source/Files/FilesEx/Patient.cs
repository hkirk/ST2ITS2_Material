namespace FilesEx;

public enum Status
{
    Alive,
    Deceased,
    Ill,
}

public class Patient
{

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Status Status { get; set; }
}