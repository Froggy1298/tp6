using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP5WebTransactionnel.Areas.Admin.ViewModels
{
    public class AdminMessageViewModel
    {
        public string Message { get; set; }

        public AdminMessageViewModel(string _message)
        {
            Message = _message;
        }
    }
}
