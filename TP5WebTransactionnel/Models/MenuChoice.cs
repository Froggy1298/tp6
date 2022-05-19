using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TP5WebTransactionnel.Resources;

namespace TP5WebTransactionnel.Models
{
    public class MenuChoice
    {


        
        public int Id { get;  set; }

        [Display(Name = "Description", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(50, MinimumLength = 3, ErrorMessageResourceName = "ModelLengthBetween", ErrorMessageResourceType = typeof(Resource))]
        public string Description { get;  set; }



        [Display(Name = "Image", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        public string ImagePath { get; set; }

        public MenuChoice() { }
        public MenuChoice(string _description)
        {
            Description = _description;
        }
        public MenuChoice(int _id, string _description, string filepath)
        {
            Id = _id;
            Description = _description; 
            ImagePath = filepath;
        }


    }
}
