using Microsoft.AspNetCore.Mvc;
using shop.Core.Dto;
using shop.Core.ServiceInterface;
using shop.Models.Email;

namespace shop.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailServices _emailService;
        public EmailController(IEmailServices emailService)
        { 
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendEmail(EmailViewModel vm)
        {
            var files = Request.Form.Files.Any() ? Request.Form.Files.ToList() : new List<IFormFile>();

            var dto = new EmailDto
            {
                To = vm.To,
                Subject = vm.Subject,
                Body = vm.Body,
                Attachment = files
            };

            _emailService.SendEmail(dto);
            
            return RedirectToAction(nameof(Index));
        }
    }
}