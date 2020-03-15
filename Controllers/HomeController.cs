using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentMvc.Models;
using StudentMvc.ViewModels;

namespace StudentMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
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
    }
}