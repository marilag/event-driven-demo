using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eventschool.Controllers;

[ApiController]
[Route("api/classes")]
public class ClassesController : ControllerBase
{
  

    private readonly ILogger<ClassesController> _logger;
    private readonly IMediator _mediator;
    private readonly IClassesRepository _classesRepo;

    public ClassesController(ILogger<ClassesController> logger, IMediator mediator, IClassesRepository classesRepo)
    {
        _logger = logger;
        _mediator = mediator;
        _classesRepo = classesRepo;
    }

    [HttpPost]
    [Route("{classid}/students")]
    public async Task<Class> EnrolStudent(string classid, [FromBody] string studentid)
    {
        var result = await _mediator.Send<Class>(new EnrolToClass() {ClassId = classid, StudentId = studentid});
        return result;
    } 

    [HttpGet]
    public async Task<IEnumerable<Class>> GetClasses()
    {
        return (await _classesRepo.Get());
    } 

    [HttpGet]
    [Route("{id}")]
    public async Task<Class> GetClassById(string id)
    {
        return (await _classesRepo.Get(id));
    } 

    
}
