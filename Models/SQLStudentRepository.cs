using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMvc.Models
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public SQLStudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public Student Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Delete(int id)
        {
            var stu = _context.Students.Find(id);
            if (stu != null)
            {
                _context.Students.Remove(stu);
                _context.SaveChanges();
            }

            return stu;
        }

        public Student GetStudent(int id)
        {
            return _context.Students.Find(id);
        }

        public IEnumerable<Student> GetStudents()
        {
            return _context.Students;
        }

        public Student Update(Student updateStudent)
        {
            var stu = _context.Students.Attach(updateStudent);
            stu.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return updateStudent;
        }
    }
}
