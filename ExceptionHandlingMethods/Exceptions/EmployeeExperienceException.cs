namespace ExceptionHandlingMethods.Exceptions;

public class EmployeeExperienceException : Exception
{
    public string FullName { get; set; }

    public EmployeeExperienceException() { }

    public EmployeeExperienceException(string message) : base(message) { }
  
    public EmployeeExperienceException(string message, Exception innerException) : base(message, innerException) { }

    public EmployeeExperienceException(string message, string fullName) : base(message)
    {
        FullName = fullName;
    }
}
