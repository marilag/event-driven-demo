using Microsoft.AspNetCore.Mvc;

namespace student.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private static readonly string[] Students = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
    }

    

    
}
