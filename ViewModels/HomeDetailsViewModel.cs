using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentMvc.Models;

namespace StudentMvc.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Student Student { get; set; }
        public string PageTitle { get; set; }
    }
}
