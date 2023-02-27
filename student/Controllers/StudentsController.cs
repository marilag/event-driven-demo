using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eventschool;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
  

    private readonly ILogger<StudentsController> _logger;
    private readonly IMediator _mediator;
    private readonly IStudentRepository _studentRepo;
    private readonly IEventStoreRepository<DomainEvent<Student>> _eventRepo;

    public StudentsController(ILogger<StudentsController> logger, 
    IMediator mediator, 
    IStudentRepository studentRepo,
    IEventStoreRepository<DomainEvent<Student>> eventRepo)
    {
        _logger = logger;
        _mediator = mediator;
        _studentRepo = studentRepo;
        this._eventRepo = eventRepo;
    }

    [HttpPost]
    public async Task<ActionResult> PostStudent(RegisterStudent enrolStudent)
    {
        
        var result = await _mediator.Send(enrolStudent);
        return CreatedAtAction(nameof(GetStudentsById),new { id = enrolStudent.Id.ToString()}, enrolStudent.Id.ToString());
    } 

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _mediator.Send<List<Student>>(new GetResgistrations());
        
    } 

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Student>> GetStudentsById(string id)
    {
        return (await _studentRepo.Get(id));
    } 

     [HttpGet]
    [Route("/student-events")]
    public ActionResult<List<DomainEvent<Student>>> GetStudentEvents()
    {
        return _eventRepo.GetStream().ToList();
    } 

    
}
