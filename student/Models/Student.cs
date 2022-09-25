namespace student;

public  class Student
{
    public Guid StudentId { get; private set; } = System.Guid.NewGuid();
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Program { get; private set; } = string.Empty;
    public DateTime RegistrationDate { get; private set; }   

    public Student(string firstname,string lastname,string address, string email, string program)
    {
        FirstName = firstname;
        LastName = lastname;
        Email = email;
        Program = program;
    }   
    
}
