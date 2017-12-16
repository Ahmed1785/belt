using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using belt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;




namespace belt.Controllers
{
    public class ActivityController : Controller
    {

        private MainContext _context;
       
        public ActivityController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("activityform")]
        public IActionResult Activityform()
        {

            return View("Activity");
        }

        [HttpPost]
        [Route("addactivity")]
        public IActionResult AddActivity(Activity activitydetails)
        {

            // if(ActiveUser == null)
            //     return RedirectToAction("Index", "Home");

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");

            // System.Console.WriteLine(int? loggedperson);
            if(ModelState.IsValid)
            
            {

            Activity activityplan = new Activity
            {
                Title = activitydetails.Title,
                Time = activitydetails.Time,
                Date = activitydetails.Date,
                Duration = activitydetails.Duration,
                Description = activitydetails.Description,
                UserId = (int)loggedperson, 
            };
            
                _context.activity.Add(activityplan);
                _context.SaveChanges();
                System.Console.WriteLine("USER____ID FOR WEDDING PERSON" + activityplan.UserId);

                return RedirectToAction("LandingPage", "Home");
            }

            return View("Activity");

            
        }

        [HttpGet]
        [Route("participant/{ParticipantId}")]

        public IActionResult Participant(int ParticipantId)
        
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");

            System.Console.WriteLine("LOGGESPERSON " + loggedperson);
            if (loggedperson == null){
                return RedirectToAction("Index", "Home");
            }else{
        
            
            
            Participant participant = new Participant 
            {                
            UserId = (int) loggedperson,
            ActivityId = ParticipantId
            };

            Participant reservation = _context.Add(participant).Entity;
            _context.SaveChanges();
            
            return RedirectToAction("LandingPage", "Home");
            }


        }

        [HttpGet]
        [Route("deleteactivity/{ParticipantId}")]

        public IActionResult deleteactivity(int ParticipantId)
        
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            System.Console.WriteLine("LOGGESPERSON " + loggedperson);
            if (loggedperson == null){
                return RedirectToAction("Index", "Home");
            }else{
        
            
            
            var deleteaactivity = _context.activity.Include(w => w.Participants).ThenInclude(g => g.Guest).Where(w => w.ActivityId == ParticipantId).SingleOrDefault();
            _context.Remove(deleteaactivity);
            _context.SaveChanges();
            return RedirectToAction("LandingPage", "Home");
            }

        }

        [HttpGet]
        [Route("UNJOIN/{itemId}")]

        public IActionResult UNJOIN(int itemId)
        
        {
            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            System.Console.WriteLine("LOGGESPERSON " + loggedperson);
            if (loggedperson == null){
                return RedirectToAction("Index", "Home");
            }else{
        
            
            
            var unjoin = _context.participant.Where(w => w.ParticipantId == itemId)
                                        .Where(g => g.UserId == loggedperson)
                                        .SingleOrDefault();
            _context.participant.Remove(unjoin);
            _context.SaveChanges();
            return RedirectToAction("LandingPage", "Home");
            }

        }

        [HttpGet]
        [Route("showaactivity/{ParticipantId}")]

         public IActionResult Show(int ParticipantId)
        {
            if(HttpContext.Session.GetInt32("loggedperson") == null)
               return RedirectToAction("Index");
            
            
            var oneactivity = _context.activity.Include(w => w.Participants).Include(w => w.Participants).ThenInclude(w => w.Guest).Where(w => w.ActivityId == ParticipantId).SingleOrDefault();
            ViewBag.oneactivity = oneactivity;

            return View("ShowActivity");
        }

        

        [HttpGet]

        [Route("showaactivity/logout")]
        public IActionResult Logout()
        {

            return RedirectToAction("Logout", "Home");
        }
        
    }
    }