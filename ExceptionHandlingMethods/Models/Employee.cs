namespace ExceptionHandlingMethods.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime StartDate { get; set; }
    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; }

    public string FullName => $"{Name} {Surname}";
}
