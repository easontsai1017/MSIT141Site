using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSIT141Site.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MSIT141Site.Controllers
{
    public class ApiController : Controller
    {
        private DemoContext _context;
        private readonly IWebHostEnvironment _host;
        public ApiController(DemoContext conetxt, IWebHostEnvironment hostEnvironment)
        {
            _context = conetxt;
            _host = hostEnvironment;
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

        public IActionResult Register(Member member, IFormFile file)
        {
            string path = Path.Combine(_host.WebRootPath, "uploads", file.FileName); //取得專案資料夾下wwwroot的實際路徑
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream); //儲存檔案到uploads資料夾中
            }
            //寫進資料庫
            byte[] imgByte = null;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                imgByte = memoryStream.ToArray();
            }
            member.FileName = file.FileName;
            member.FileData = imgByte;

            _context.Members.Add(member);
            _context.SaveChanges();

            string info = $"{file.FileName} - {file.ContentType} - {file.Length}";
            return Content(info, "text/plain", System.Text.Encoding.UTF8);
        }
        public IActionResult Members()
        {
            return Json(_context.Members);
        }
        //抓照片資料
        public IActionResult GetImageBytes(int id = 1)
        {
            Member member = _context.Members.Find(id);
            byte[] img = member.FileData;
            return File(img, "image/jpeg");
        }


        //讀取城市資料
        public IActionResult City()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();
            return Json(cities);
        }
        //讀取鄉鎮區資料
        public IActionResult Districts(string city)
        {
            var districts = _context.Addresses.Where(a => a.City == city).Select(a => a.SiteId).Distinct();
            return Json(districts);
        }
        //根據鄉鎮區的資料讀出路名
        public IActionResult Roads(string district)
        {
            var roads = _context.Addresses.Where(a => a.SiteId == district).Select(a => a.Road);
            return Json(roads);
        }







    }
}
