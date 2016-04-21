using Microsoft.AspNet.Identity;
using MyPSBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyPSBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


     
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult>Contact([Bind(Include = "Id, SenderName, SenderEmail, SenderMessage")] Contact contact)
        {
            contact.Created = DateTime.Now;
            var newContact = contact.SenderName;
            var svc = new EmailService();
            var msg = new IdentityMessage();
            msg.Subject = "LFields Contact from Portfolio Site";
            msg.Body = "\r\n You have recieved a request to contact from " + newContact + "(" + contact.SenderEmail + ")" + "\r\n\t"
                     + "\r\n With the following message: \r\n\t"
                     + contact.SenderMessage;

            await svc.SendAsync(msg);

            return View(contact);
        }
    }
    
}