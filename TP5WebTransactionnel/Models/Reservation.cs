using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using TP5WebTransactionnel.Resources;

namespace TP5WebTransactionnel.Models
{
    public class Reservation
    {

        //@html.DiplayFor = valeur de la variable
        //@html.DiplayNameFor = nom du display, si pas de display, met le nom de la propriété
        //Data Annotation ASP net core
        public int Id { get;  set; }

        [Display(Name = "Name", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(30, ErrorMessageResourceName = "ModelLengthLessThan", ErrorMessageResourceType = typeof(Resource))]
        public string Nom { get;  set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailFormat", ErrorMessageResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(50, ErrorMessageResourceName = "ModelLengthLessThan", ErrorMessageResourceType = typeof(Resource))]
        public string Courriel { get;  set; }

        [Display(Name = "NbClient", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [Range(1, 6, ErrorMessage = "Le nombre de client doit etre entre 1 et 6")]
        public int NbPersonne { get;  set; }

        [Display(Name = "DateReserv", ResourceType = typeof(Resource))]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString = "{0:d}")]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        public DateTime DateReservation { get; set; }


        [Display(Name = "ChoixMenu", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        public MenuChoice ChoixMenu { get;  set; }


        public Reservation() { }

        public Reservation(string nom, string courriel)
        {
            Nom = nom;
            Courriel = courriel;
        }
        public Reservation(string nom, string courriel, int nbPersonne, DateTime dateRéservation, MenuChoice menuChoiceId)
        {
            Nom = nom;
            Courriel = courriel;
            NbPersonne = nbPersonne;
            DateReservation = dateRéservation;
            ChoixMenu = menuChoiceId;
        }
        public Reservation(int id, string nom, string courriel, int nbPersonne, DateTime dateRéservation, MenuChoice menuChoiceId)
        {
            Id = id;
            Nom = nom;
            Courriel = courriel;
            NbPersonne = nbPersonne;
            DateReservation = dateRéservation;
            ChoixMenu = menuChoiceId;
        }
    }
}
