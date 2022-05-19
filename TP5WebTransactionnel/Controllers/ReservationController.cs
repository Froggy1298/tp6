using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TP5WebTransactionnel.Models;
using TP5WebTransactionnel.DataAccessLayer;

namespace TP5WebTransactionnel.Controllers
{
    public class ReservationController : Controller
    {


        [HttpGet]
        public IActionResult Detail(int Id)
        {
            DAL dal = new DAL();
            
            if (Id == 0)
                return View("Message", "L'id est zéro");
            if(dal.ReservationFactory.GetById(Id) is null)
                return View("Message", "La réservation n'existe pas");

            Reservation reservation = dal.ReservationFactory.GetById(Id);
            return View(reservation);
        }

    }
}
