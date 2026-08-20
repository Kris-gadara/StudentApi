using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.Data;
using StudentApi.Models;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetStudent),
                new { id = student.Id },
                student);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(
    int id,
    Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}