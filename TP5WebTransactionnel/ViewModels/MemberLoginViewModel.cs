using System.ComponentModel.DataAnnotations;
using TP5WebTransactionnel.Models;
using TP5WebTransactionnel.Resources;

namespace TP5WebTransactionnel.ViewModels
{
    public class MemberLoginViewModel
    {
        [Display(Name = "Username", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(20, MinimumLength = 3, ErrorMessageResourceName = "ModelLengthBetween", ErrorMessageResourceType = typeof(Resource))]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ModelRequired", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(20, MinimumLength = 5, ErrorMessageResourceName = "ModelLengthBetween", ErrorMessageResourceType = typeof(Resource))]
        public string Password { get; set; }

        // Constructeur vide requis pour la désérialisation
        public MemberLoginViewModel()
        {
        }

        public MemberLoginViewModel(Member member)
        {
            Username = member.Username;
            Password = member.Password;
        }

    }
}
