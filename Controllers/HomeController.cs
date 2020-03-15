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

        public HomeController(IStudentRepository studentRepository,  IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var ls = _studentRepository.GetStudents();
            return View(ls);
        }

       
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
        [HttpPost]
        public IActionResult Create(StudentCreateViewModel student)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (student.Photo!=null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath,"images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + student.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    student.Photo.CopyTo(new FileStream(filePath,FileMode.Create));

                }
                var newStu=new Student()
                {
                    Name=student.Name,
                    Email= student.Email, 
                    ClassName= student.ClassName,
                    PhotoPath = uniqueFileName
                };
                _studentRepository.Create(newStu);
                //var stu = _studentRepository.Create(student);
                return RedirectToAction("Details", new {id = newStu.Id});
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
          var ls=  _studentRepository.Delete(id);
          return RedirectToAction("Index");
        }
    }
}