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
        private DemoContext _context;
        public ApiController(DemoContext context)
        {
            _context = context;
        }
        public IActionResult Index(User user)
        {
            //return Content("Ajax,您好","text/plain",System.Text.Encoding.UTF8);
            if (string.IsNullOrEmpty(user.name))
                return Content("請輸入姓名", "text/plain", System.Text.Encoding.UTF8);
            else
            {
                Member mem = _context.Members.FirstOrDefault(m => m.Name == user.name);
                if (mem != null)
                    return Content("已有此名稱，請重新輸入", "text/plain", System.Text.Encoding.UTF8);
                else
                    return Content("可使用此名稱", "text/plain", System.Text.Encoding.UTF8);
            }
            //return Content($"{user.name}您好,你的年紀是{user.age},信箱帳號{user.email}", "text/plain", System.Text.Encoding.UTF8);
        }

        //public IActionResult CheckName(User user)
        //{

        //    if(user.name)
        //}





    }
}
