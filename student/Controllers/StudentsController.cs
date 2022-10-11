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
    private readonly IEventStoreRepository<StudentRegistered> _eventRepo;

    public StudentsController(ILogger<StudentsController> logger, 
    IMediator mediator, 
    IStudentRepository studentRepo,
    IEventStoreRepository<StudentRegistered> eventRepo)
    {
        _logger = logger;
        _mediator = mediator;
        _studentRepo = studentRepo;
        this._eventRepo = eventRepo;
    }

    [HttpPost]
    public async Task<ActionResult<Student>> PostStudent(RegisterStudent enrolStudent)
    {
        var result = await _mediator.Send<Student>(enrolStudent);
        return CreatedAtAction(nameof(GetStudents), new { id = enrolStudent }, result);
    } 

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return (await _studentRepo.Get()).ToList();
    } 

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Student>> GetStudentsById(string id)
    {
        return (await _studentRepo.Get(id));
    } 

     [HttpGet]
    [Route("/student-events")]
    public async Task<ActionResult<List<StudentRegistered>>> GetStudentEvents()
    {
        return (await _eventRepo.Get()).ToList();
    } 

    
}
