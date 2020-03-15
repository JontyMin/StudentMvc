using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentMvc.Models
{
    /// <summary>
    /// 学生模型
    /// </summary>
    public class Student
    {

        public int Id { get; set; }

        [Display(Name="姓名")]
        [Required(ErrorMessage = "请输入学生姓名")]
        [MaxLength(20),MinLength(2)]
        public string Name { get; set; }

        [Display(Name = "班级")]
        public ClassNameEnum  ClassName { get; set; }

        [Display(Name="邮箱地址")]
        [DataType(DataType.EmailAddress,ErrorMessage = "邮箱地址错误")]
        public string Email { get; set; }
    }
}
