using Microsoft.AspNetCore.Mvc;
using MSIT141Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index(User user)
        {
            //return Content("Ajax,您好","text/plain",System.Text.Encoding.UTF8);
            if(string.IsNullOrEmpty(user.name))
            {
                user.name = "Ajax";
            }
            return Content($"{user.name}您好,你的年紀是{user.age}", "text/plain", System.Text.Encoding.UTF8);
        }
    }
}
