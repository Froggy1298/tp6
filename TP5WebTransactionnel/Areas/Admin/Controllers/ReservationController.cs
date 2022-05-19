using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP5WebTransactionnel.DataAccessLayer;
using TP5WebTransactionnel.Models;
using System.Collections.Generic;
using TP5WebTransactionnel.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace TP5WebTransactionnel.Areas.Admin.Controllers
{
    [Authorize(Roles = Member.ROLE_ADMIN)]
    [Area("Admin")]
    public class ReservationController : Controller
    {
        public IActionResult List()
        {
            DAL dal = new DAL();
            List<Reservation> LesReservation = dal.ReservationFactory.GetAllReservation();
            return View(LesReservation);
        }


        public IActionResult Delete(int id)
        {
           
            DAL dal = new DAL();
            Reservation reservation = dal.ReservationFactory.GetById(id);
            if(reservation is null)
            {
                return View("AdminMessage", new AdminMessageViewModel("La réservation n'existe pas"));
            }
            return View("Delete",reservation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Reservation reserv)
        {
            DAL dal = new DAL();
            dal.ReservationFactory.DeleteFromId(reserv.Id);
            return RedirectToAction("List");
        }
    }
}
