using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP5WebTransactionnel.Models;

namespace TP5WebTransactionnel.ViewModels
{
    public class HomeIndexViewModel
    {
        public Reservation Reservation { get; set; }

        public List<MenuChoice> ListChoixMenu { get; set; }

    }
}
