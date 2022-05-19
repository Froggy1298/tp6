using System.Collections.Generic;
using TP5WebTransactionnel.Models;
namespace TP5WebTransactionnel.Areas.Admin.ViewModels
{
    public class CreateMemberViewModel
    {
        public Member Membre { get; set; }
        public List<string> ListeRole { get; set; }
    }
}
