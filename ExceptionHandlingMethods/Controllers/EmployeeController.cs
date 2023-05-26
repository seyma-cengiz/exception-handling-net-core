using ExceptionHandlingMethods.Exceptions;
using ExceptionHandlingMethods.Exceptions.Filters;
using ExceptionHandlingMethods.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlingMethods.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly List<Employee> _employeeList = new List<Employee>
    {
        new Employee { Id = 1, Name = "Aa", Surname = "AAA", DepartmentId = 1, StartDate= new DateTime(2012, 1, 1), Department = new Department { Id = 1, Name = "Department1" } },
        new Employee { Id = 1, Name = "Bb", Surname = "BBB", DepartmentId = 1, StartDate= new DateTime(2014, 1, 1), Department = new Department { Id = 1, Name = "Department1" } },
        new Employee { Id = 2, Name = "Cc", Surname = "CCC", DepartmentId = 1, StartDate= new DateTime(2016, 1, 1), Department = new Department { Id = 1, Name = "Department1" } },
        new Employee { Id = 3, Name = "Dd", Surname = "DDD", DepartmentId = 1, StartDate= new DateTime(2018, 1, 1), Department = new Department { Id = 1, Name = "Department1" } },
        new Employee { Id = 4, Name = "Ee", Surname = "EEE", DepartmentId = 2, StartDate= new DateTime(2010, 1, 1), Department = new Department { Id = 2, Name = "Department2" } },
        new Employee { Id = 5, Name = "Ff", Surname = "FFF", DepartmentId = 2, StartDate= new DateTime(2020, 1, 1), Department = new Department { Id = 2, Name = "Department2" } },
    };

    [HttpGet]
    [CustomExceptionFilter]
    public IActionResult GetEmployee(int id, bool isSenior = false)
    {
        try
        {
            var employee = _employeeList.Single(t => t.Id == id);

            if (isSenior && ExmployeeHasLessThan5YearsExperience(employee.StartDate))
            {
                throw new EmployeeExperienceException($"The employee named {employee.FullName} has less than 5 years experience.", employee.FullName);
            }

            throw new Exception("Error occured");

            return Ok(employee);
        }
        //catch (ArgumentNullException ex)
        //{
        //    return BadRequest(ex.Message);
        //}
        //catch (InvalidOperationException ex)
        //{
        //    return BadRequest(ex.Message);
        //}
        catch (Exception ex) when (ex is ArgumentNullException ||
                                   ex is InvalidOperationException) //Built-in Exceptions
        {
            return BadRequest(ex.Message);
        }
        catch (EmployeeExperienceException ex) //Custom Exception
        {
            return BadRequest($"The employee named {ex.FullName} founded, but {ex.FullName} is not a senior.");
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            //Do other things
        }
    }

    private bool ExmployeeHasLessThan5YearsExperience(DateTime graduationDate)
    {
        return (DateTime.Today.Year - graduationDate.Year) < 5;
    }
}
