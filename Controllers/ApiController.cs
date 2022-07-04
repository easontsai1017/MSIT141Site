using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(string name,int age=0)
        {
            //return Content("Ajax,您好","text/plain",System.Text.Encoding.UTF8);
            if(string.IsNullOrEmpty(name))
            {
                name = "Ajax";
            }
            return Content($"{name}您好,你的年紀是{age}", "text/plain", System.Text.Encoding.UTF8);
        }
    }
}
