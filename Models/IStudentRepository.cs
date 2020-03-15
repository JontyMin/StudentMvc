using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMvc.Models
{
    public interface IStudentRepository
    {
        /// <summary>
        /// 通过id查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student GetStudent(int id);
        /// <summary>
        /// 查询所有信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<Student> GetStudents();
        /// <summary>
        /// 创建学生信息
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        Student Create(Student student);
        /// <summary>
        /// 根据id修改学生信息
        /// </summary>
        /// <param name="updateStudent"></param>
        /// <returns></returns>
        Student Update(Student updateStudent);
        /// <summary>
        /// 根据id删除学生信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student Delete(int id);

    }
}
