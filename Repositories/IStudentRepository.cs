using StudentApi.Models;

namespace StudentApi.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();

        Task<Student?> GetByIdAsync(int id);

        Task<Student> AddAsync(Student student);

        Task<bool> UpdateAsync(Student student);

        Task<bool> DeleteAsync(int id);
    }
}