using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP5WebTransactionnel.DataAccessLayer.Factories;
using TP5WebTransactionnel.Models;

namespace TP5WebTransactionnel.DataAccessLayer
{
    public class DAL
    {
        private MenuChoiceFactory _menuChoiceFactory = null;
        private ReservationFactory _reservationFactory = null;
        private MemberFactory _memberFactory = null;

        public static string ConnectionString { get; set; }


        public DAL()
        { }

        public MemberFactory MemberFact
        {
            get
            {
                if (_memberFactory == null)
                {
                    _memberFactory = new MemberFactory();
                }
                return _memberFactory;
            }
        }

        public MenuChoiceFactory MenuChoiceFactory
        {
            get 
            { 
                if (_menuChoiceFactory == null)
                {
                    _menuChoiceFactory = new MenuChoiceFactory();
                }
                return _menuChoiceFactory; 
            }
        }

        public ReservationFactory ReservationFactory
        {
            get 
            {
                if(_reservationFactory == null)
                {
                     _reservationFactory = new ReservationFactory();
                }
                return _reservationFactory; 
            } 
        }
       

    }
}
