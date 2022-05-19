using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TP5WebTransactionnel.Models;
using TP5WebTransactionnel.DataAccessLayer;
using TP5WebTransactionnel.ViewModels;

namespace TP5WebTransactionnel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            DAL dal = new DAL();

            HomeIndexViewModel viewModel = new HomeIndexViewModel
            {
                ListChoixMenu = dal.MenuChoiceFactory.GetAllMenuChoice(),
                Reservation = dal.ReservationFactory.CreateEmpty()
            };

            return View(viewModel);
        }


        // POST: Home/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(HomeIndexViewModel homeIndexViewModel)
        {
            DAL dal = new DAL();

            if(homeIndexViewModel.Reservation is null)
                return View("Message", "La réservation est null");
            if(string.IsNullOrEmpty(homeIndexViewModel.Reservation.Nom))
                return View("Message", "Le nom ne doit pas etre vide");
            if (homeIndexViewModel.Reservation.NbPersonne > 6 || homeIndexViewModel.Reservation.NbPersonne ==0)
                return View("Message", "Le nomber de personne doit etre entre 1 et 6");
            if (dal.MenuChoiceFactory.GetById(homeIndexViewModel.Reservation.ChoixMenu.Id) is null)
                return View("Message", "Le menu n'existe pas");

            homeIndexViewModel.Reservation.ChoixMenu = dal.MenuChoiceFactory.GetById(homeIndexViewModel.Reservation.ChoixMenu.Id);
            dal.ReservationFactory.AjouterReservation(homeIndexViewModel.Reservation);

            
            return RedirectToAction("Detail", "Reservation", new { Id = homeIndexViewModel.Reservation.Id });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
