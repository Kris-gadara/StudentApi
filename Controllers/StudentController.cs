using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDto>>> GetStudents()
        {
            var students = await _service.GetAllAsync();

            return Ok(students);
        }

        // GET: api/Student/1
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetStudent(int id)
        {
            var student = await _service.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // POST: api/Student
        [HttpPost]
        public async Task<ActionResult<StudentResponseDto>> CreateStudent(
            StudentCreateDto dto)
        {
            var student = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                student);
        }

        // PUT: api/Student/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(
            int id,
            StudentUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Student/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}