using StudentApi.DTOs;
using StudentApi.Models;
using StudentApi.Repositories;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<StudentResponseDto>> GetAllAsync()
        {
            var students = await _repository.GetAllAsync();

            return students.Select(student => new StudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Course = student.Course
            }).ToList();
        }

        public async Task<StudentResponseDto?> GetByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);

            if (student == null)
            {
                return null;
            }

            return new StudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Age = student.Age,
                Course = student.Course
            };
        }

        public async Task<StudentResponseDto> CreateAsync(
            StudentCreateDto dto)
        {
            var student = new Student
            {
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age,
                Course = dto.Course
            };

            var createdStudent = await _repository.AddAsync(student);

            return new StudentResponseDto
            {
                Id = createdStudent.Id,
                Name = createdStudent.Name,
                Email = createdStudent.Email,
                Age = createdStudent.Age,
                Course = createdStudent.Course
            };
        }

        public async Task<bool> UpdateAsync(
            int id,
            StudentUpdateDto dto)
        {
            var student = new Student
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email,
                Age = dto.Age,
                Course = dto.Course
            };

            return await _repository.UpdateAsync(student);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}