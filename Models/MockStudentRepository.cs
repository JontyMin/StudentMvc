using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMvc.Models
{
    public class MockStudentRepository : IStudentRepository
    {
        private List<Student> _students;    

        public MockStudentRepository()
        {
            _students = new List<Student>()
            {
                new Student() {Id = 1, Name = "昊佳", ClassName = ClassNameEnum.FirstGrade, Email = "501211312@qq.com"},
                new Student() {Id = 2, Name = "罗鑫", ClassName = ClassNameEnum.SecondGrade, Email = "5014561312@qq.com"},
                new Student() {Id = 3, Name = "柳聪", ClassName = ClassNameEnum.ThreeGrade, Email = "501231312@qq.com"}
            };
        }

        public Student Create(Student student)
        {
            student.Id = _students.Max(s => s.Id + 1);
            _students.Add(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _students.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}
