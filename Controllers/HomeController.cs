using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using StudentMvc.Models;
using StudentMvc.ViewModels;

namespace StudentMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IStudentRepository studentRepository, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var ls = _studentRepository.GetStudents();
            return View(ls);
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Details(int id)
        {
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Student = _studentRepository.GetStudent(id),
                PageTitle = "学生信息"
            };
            //ViewBag.Student = ls;
            return View(homeDetailsViewModel);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 创建学生
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel student)
        {

            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(student);
                //if (student.Photo != null)//&& student.Photos.Count > 0 多文件上传
                //{
                //    //foreach (var photo in student.Photos)
                //    //{
                //    //文件上传到wwwroot中的images文件夹中
                //    //获取wwwroot路径需要注入IWebHostEnvironment，通过WebRootPath获取
                //    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                //    //确保文件名统一，附加新的GUID值和下划线
                //    uniqueFileName = Guid.NewGuid() + "_" + student.Photo.FileName;
                //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //    //使用IFromFile接口提供的CopyTo()方法将文件复制到wwwroot/images文件夹
                //    student.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                //    //}
                //}
                var newStu = new Student()
                {
                    Name = student.Name,
                    Email = student.Email,
                    ClassName = student.ClassName,
                    PhotoPath = uniqueFileName
                };
                _studentRepository.Create(newStu);
                //var stu = _studentRepository.Create(student);
                return RedirectToAction("Details", new { id = newStu.Id });
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            var ls = _studentRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var stu = _studentRepository.GetStudent(id);
            StudentEditViewModel studentEditView = new StudentEditViewModel()
            {
                Id = stu.Id,
                Name = stu.Name,
                Email = stu.Email,
                ClassName = stu.ClassName,
                ExistingPhotoPath = stu.PhotoPath
            };
            return View(studentEditView);
        }
        [HttpPost]
        public IActionResult Edit(StudentEditViewModel studentEditView)
        {
            //检查数据
            if (ModelState.IsValid)
            {
                var stu = _studentRepository.GetStudent(studentEditView.Id);
                stu.Name = studentEditView.Name;
                stu.Email = studentEditView.Email;
                stu.ClassName = studentEditView.ClassName;

                //存在则删除
                if (studentEditView.ExistingPhotoPath != null)
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images",
                        studentEditView.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                stu.PhotoPath = ProcessUploadedFile(studentEditView);
                var newStudent = _studentRepository.Update(stu);

                return RedirectToAction("Index");
            }

            return View();
        }

        /// <summary>
        /// 图片保存
        /// </summary>
        /// <returns>唯一文件名</returns>
        private string ProcessUploadedFile(StudentCreateViewModel model)
        {
            if (model.Photo!=null)
            {
                string uniqueFileName = null;
                //文件上传到wwwroot中的images文件夹中
                //获取wwwroot路径需要注入IWebHostEnvironment，通过WebRootPath获取
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                //确保文件名统一，附加新的GUID值和下划线
                uniqueFileName = Guid.NewGuid() + "_" + model.Photo.FileName;
                string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);
                //释放
                using (var fileStream = new FileStream(newFilePath, FileMode.Create))
                {
                    //使用IFromFile接口提供的CopyTo()方法将文件复制到wwwroot / images文件夹
                    model.Photo.CopyTo(fileStream);
                }
                //model.Photo.CopyTo(new FileStream(newFilePath, FileMode.Create));
                return uniqueFileName;
            }

            return null;
        }
    }
}