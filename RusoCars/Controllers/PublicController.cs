using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RusoCars.Models;
using System.Net.Mail;
using RusoCars.DAL;
using System.Net;

namespace RusoCars.Controllers
{
    public class PublicController : Controller
    {
        //
        // GET: /Public/
        private UnitOfWork unitOfWork = new UnitOfWork();

        private void delay() {
            System.Threading.Thread.Sleep(1000);  
       } 

        public ActionResult Index()
        {
            ViewBag.active = "index";
            return View(ViewBag);
        }

        public ActionResult News()
        {
            ViewBag.active = "news";
            return View(unitOfWork.NewsRepository.GetAll());
        }

        public PartialViewResult ShowInstantNew(int id)
        {
            News news = unitOfWork.NewsRepository.GetByID(id);
            if (news == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showInstantNew", news);
        }

        public ActionResult Certifications()
        {
            ViewBag.active = "certifications";
            List<Certification> certifications = unitOfWork.CertificationRepository.GetAll();
            return View(certifications);
        }

        public PartialViewResult ShowInstantCertification(int id)
        {
            Certification certification = unitOfWork.CertificationRepository.GetByID(id);
            if (certification == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showInstantCertification", certification);
        }

        public ActionResult Team()
        {
            ViewBag.active = "team";
            List<Team> team = unitOfWork.TeamRepository.GetAll();
            return View(team);
        }

        public PartialViewResult ShowInstantTeam(int id)
        {
            Team team = unitOfWork.TeamRepository.GetByID(id);
            if (team == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showInstantTeam", team);
        }

        public ActionResult Pilots()
        {
            ViewBag.active = "pilots";
            List<Pilot> pilots = unitOfWork.PilotRepository.GetAll();
            return View(pilots);
        }

        public PartialViewResult ShowInstantPilot(int id)
        {
            RusoCars.Models.Pilot pilot = unitOfWork.PilotRepository.GetByID(id);
            if (pilot == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showInstantPilot", pilot);
        }

        public ActionResult Links()
        {
            ViewBag.active = "links";
            List<LinksCategory> linkscategory = unitOfWork.LinksCategoryRepository.GetAll();
            return View(linkscategory);
        }

        public PartialViewResult ShowInstantLinks(int id = 0)
        {
            List<Link> links;
            if (id != 0)
            {
                links = unitOfWork.LinkRepository.GetAllFromCategory(id);
            }
            else
            {
                List<LinksCategory> category = unitOfWork.LinksCategoryRepository.GetAll();
                links = unitOfWork.LinkRepository.GetAllFromCategory(category[0].LinksCategoryId);
            }

            if (links == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showInstantLinks", links);
        }

        public ActionResult Images()
        {
            ViewBag.active = "images";
            List<Category> categories = unitOfWork.CategoryRepository.GetAll();
            return View(categories);
        }

        public PartialViewResult ShowImages(int id = 0)
        {
            List<Image> images = null;
            if (id != 0)
            {
                Subcategory subcategory = unitOfWork.SubcategoryRepository.GetByID(id); 
                ViewBag.SubcategoryName = subcategory.Name;
                images = unitOfWork.SubcategoryRepository.GetImages(id);
            }
            if (images == null)
            {
                return PartialView();
            }
            else
            {
                this.delay();
                return PartialView("_showImages", images);            
            }
        }

        public PartialViewResult ZoomImage(int id = 0)
        {
            RusoCars.Models.Image image = unitOfWork.ImageRepository.GetByID(id);
            if (image == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_zoomImage", image);
        }

        public ActionResult Clients()
        {
            ViewBag.active = "clients";
            List<Client> clients = unitOfWork.ClientRepository.GetAll();
            return View(clients);
        }

        public PartialViewResult ShowInstantClient(int id)
        {
            RusoCars.Models.Client client = unitOfWork.ClientRepository.GetByID(id);
            if (client == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showInstantClient", client);
        }

        public ActionResult Contact()
        {
            ViewBag.active = "contact";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     var contactObj = new Contact
                     {
                         Name = contact.Name,
                         Phone = contact.Phone,
                         Email = contact.Email,
                         Consult = contact.Consult
                     };
                     MailMessage msg = new MailMessage();
                     msg.From = new System.Net.Mail.MailAddress("postmaster@gerardims.com.ar");
                     msg.To.Add("postmaster@gerardims.com.ar");
                     msg.Subject = "Gerardi MotorSport - Contacto desde Sitio Web";
                     string body = "Nombre: " + contact.Name + "\n"
                             + "Email: " + contact.Email + "\n"
                             + "Teléfono: " + contact.Phone + "\n\n"
                             + contact.Consult;
                     msg.Body = body;
                     msg.IsBodyHtml = false;
                     SmtpClient smtp = new SmtpClient("mail.gerardims.com.ar"); /*SERVIDOR SMPT DEL HOSTING, NECESARIO*/
                     NetworkCredential Credentials = new NetworkCredential("postmaster@gerardims.com.ar", "kpicua007"); 
                     smtp.Credentials = Credentials;
                     smtp.Send(msg);
                     msg.Dispose();
                     ViewBag.success = "Su consulta ha sido enviada! Pronto recibirá nuestra respuesta.";
                     return View();
                 }
                 catch (Exception ex)
                 {
                     ViewBag.error = ex.Message;
                     return View();
                 }
             }
             else
             {
                 return View(contact);                
             }
         }

        public ActionResult AboutUs()
        {
            ViewBag.active = "aboutUs";
            Text texto = unitOfWork.TextRepository.GetByID(3, 1);
            ViewBag.QuienesSomos = texto.Content;
            return View();
        }

        public ActionResult Services()
        {
            ViewBag.active = "services";
            return View();
        }

        public PartialViewResult ShowCompetition()
        {
            
            List<Service> services = unitOfWork.ServiceRepository.GetAllFromCategory(1);
            if (services == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showCompetition", services);
        }

        public PartialViewResult ShowMechanic()
        {
            List<Service> services = unitOfWork.ServiceRepository.GetAllFromCategory(0);
            if (services == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showMechanic", services);
        }

        public PartialViewResult ShowService(int id)
        {
            RusoCars.Models.Service service = unitOfWork.ServiceRepository.GetByID(id);
            if (service == null)
            {
                return PartialView();
            }
            this.delay();
            return PartialView("_showService", service);
        }

        public PartialViewResult ShowText(int id)
        {
            Text texto = unitOfWork.TextRepository.GetByID(id, null);
            ViewBag.Intro = texto.Content;
            ViewBag.Titulo = texto.Descriptor;
            return PartialView();
        }

        public PartialViewResult ShowTextQ(int id)
        {
            Text texto = unitOfWork.TextRepository.GetByID(id, null);
            ViewBag.Intro = texto.Content;
            ViewBag.Titulo = texto.Descriptor;
            return PartialView();
        }
    }
}
