using Azure.Messaging.ServiceBus;
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
    private readonly ServiceBusClient _sbClient;

    public ClassesController(ILogger<ClassesController> logger, 
    IMediator mediator, 
    IClassesRepository classesRepo,
    ServiceBusClient sbClient)
    {
        _logger = logger;
        _mediator = mediator;
        _classesRepo = classesRepo;
        _sbClient = sbClient;
    }

    [HttpPost]
    [Route("{classid}/students")]
    public async Task<IActionResult> EnrolStudent(string classid, [FromBody] string studentid)
    {
        var result = await _mediator.Send(new EnrolToClass() {ProgramId = classid, StudentId = studentid});
        return new AcceptedResult();
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
