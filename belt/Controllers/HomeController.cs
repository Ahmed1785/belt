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
    public class HomeController : Controller
    {

        private MainContext _context;

        public HomeController(MainContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            // wrap the session validation around all of the methods in all the pages!!
            // for example if(user not in session{
            // return redirectToAvtion("Index")}else{
            // ALL THE OTHER SHIT ON THE PAGEEEE
            //}
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // System.Console.WriteLine(registeredcheck);
                // System.Console.WriteLine("EMAILLL" + user.Email);
                // System.Console.WriteLine("THISSSSS****"+returnedid.id);

                System.Console.WriteLine(user.UserId);

                User registeredcheck = _context.user.SingleOrDefault(str => str.Email == user.Email);


                // System.Console.WriteLine("THE EMAILLL", registeredcheck.Email);
                // string email = registeredcheck.Email;
                if (registeredcheck == null)
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    User NewPerson = new User

                    {
                        First_Name = user.First_Name,
                        Last_Name = user.Last_Name,
                        Email = user.Email,
                        Password = user.Password,

                    };

                    User theuser = _context.Add(NewPerson).Entity;
                    _context.SaveChanges();
                    System.Console.WriteLine("NEW PERSON", NewPerson.First_Name);
                    ViewBag.Success = "You have been added to the database! Please log in now!";
                    return View("Index");

                }
                else
                {
                    System.Console.WriteLine("ALREADY IN THE DATABASE");
                    return View("Index");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser user)
        {
            User userfound = new User
            {
                First_Name = "john",
                Last_Name = "doe",
                Email = user.LogEmail,
                Password = user.LogPassword,
            };

            User loggeduser = _context.user.SingleOrDefault(str => str.Email == userfound.Email);
            if (loggeduser == null)
            {
                ViewBag.loginerror = "Login failed, email and password did not match the information in the database. If you haven't registered please register first!";
                return View("Index");
            }
            else
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();

                if (0 != Hasher.VerifyHashedPassword(loggeduser, loggeduser.Password, userfound.Password))
                {

                    HttpContext.Session.SetInt32("loggedperson", (int)loggeduser.UserId);

                    return RedirectToAction("LandingPage");
                }
                else
                {

                    ViewBag.loginerror = "Login failed, email and password did not match the information in the database. If you haven't registered please register first!";
                    return View("Index");
                }
            }
        }

        [HttpGet]
        [Route("dashboard")]

        public IActionResult LandingPage()
        {

            int? loggedperson = HttpContext.Session.GetInt32("loggedperson");
            if (loggedperson == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var oneactivity = _context.activity.Include(w => w.Participants).ThenInclude(g => g.Guest).ToList();

                ViewBag.activities = oneactivity;
                
                System.Console.WriteLine("HEYYY" + loggedperson);
                User findtheperson = _context.user.SingleOrDefault(x => x.UserId == loggedperson);

                System.Console.WriteLine("FOUND PESON " + findtheperson.UserId);

                ViewBag.User = findtheperson;
                return View("About");


            }
        }





        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {


            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }


}
