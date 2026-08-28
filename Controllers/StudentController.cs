using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all students. Requires authentication.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<StudentResponseDto>>> GetStudents()
        {
            var students = await _service.GetAllAsync();

            return Ok(students);
        }

        /// <summary>
        /// Get a student by ID. Requires authentication.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentResponseDto>> GetStudent(int id)
        {
            var student = await _service.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound(new { message = $"Student with ID {id} not found" });
            }

            return Ok(student);
        }

        /// <summary>
        /// Create a new student. Requires Admin role.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<StudentResponseDto>> CreateStudent(
            StudentCreateDto dto)
        {
            var student = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                student);
        }

        /// <summary>
        /// Update an existing student. Requires Admin role.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStudent(
            int id,
            StudentUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound(new { message = $"Student with ID {id} not found" });
            }

            return NoContent();
        }

        /// <summary>
        /// Delete a student. Requires Admin role.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = $"Student with ID {id} not found" });
            }

            return NoContent();
        }
    }
}