using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP5WebTransactionnel.DataAccessLayer;
using TP5WebTransactionnel.Helpers;
using TP5WebTransactionnel.Models;
using TP5WebTransactionnel.Areas.Admin.ViewModels;
using System.Collections.Generic;

namespace TP5WebTransactionnel.Areas.Admin.Controllers
{
    [Authorize(Roles = Member.ROLE_ADMIN)]
    [Area("Admin")]
    public class MemberController : Controller
    {
        public IActionResult List()
        { 
            DAL dal = new DAL();

            List<Member> members = dal.MemberFact.GetAllMember();

            return View(members);
        }



        public IActionResult Create()
        {
            DAL dal = new DAL();
            CreateMemberViewModel viewModel = new CreateMemberViewModel
            {
                Membre = dal.MemberFact.CreateEmpty(),
                ListeRole = new List<string> { Member.ROLE_STANDARD, Member.ROLE_ADMIN }
            };
            return View("CreateEdit", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMemberViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                DAL dal = new DAL();

                if (string.IsNullOrEmpty(memberViewModel.Membre.Role))
                    memberViewModel.Membre.Role = Models.Member.ROLE_STANDARD;

                if (memberViewModel.Membre.Role != Member.ROLE_ADMIN && memberViewModel.Membre.Role != Member.ROLE_STANDARD)
                    memberViewModel.Membre.Role = Models.Member.ROLE_STANDARD;

                memberViewModel.Membre.Password = CryptographyHelper.HashPassword(memberViewModel.Membre.Password);

                dal.MemberFact.AjouterMember(memberViewModel.Membre);

                return RedirectToAction("List");
            }
            CreateMemberViewModel viewModel = new CreateMemberViewModel
            {
               Membre = memberViewModel.Membre,
                ListeRole = new List<string> { Member.ROLE_STANDARD, Member.ROLE_ADMIN }
            };
            return View("CreateEdit", viewModel);
        }

        public IActionResult Edit(int id)
        {
            DAL dal = new DAL();


            Member member = dal.MemberFact.GetById(id);
            if (member != null)
            {
                CreateMemberViewModel viewModel = new CreateMemberViewModel
                {
                    Membre = member,
                    ListeRole = new List<string> { Member.ROLE_STANDARD, Member.ROLE_ADMIN }
                };
                return View("CreateEdit", viewModel);
            }
            else
                return View("AdminMessage", new AdminMessageViewModel("Le client n'existe pas"));
        }

        [HttpPost]
        public IActionResult Edit(CreateMemberViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                DAL dal = new DAL();

                if (string.IsNullOrEmpty(memberViewModel.Membre.Role))
                    memberViewModel.Membre.Role = Models.Member.ROLE_STANDARD;

                if (memberViewModel.Membre.Role != Member.ROLE_ADMIN && memberViewModel.Membre.Role != Member.ROLE_STANDARD)
                    memberViewModel.Membre.Role = Models.Member.ROLE_STANDARD;

                memberViewModel.Membre.Password = CryptographyHelper.HashPassword(memberViewModel.Membre.Password);

                dal.MemberFact.UpdateMember(memberViewModel.Membre);

                return RedirectToAction("List");
            }

            CreateMemberViewModel viewModel = new CreateMemberViewModel
            {
                Membre = memberViewModel.Membre,
                ListeRole = new List<string> { Member.ROLE_STANDARD, Member.ROLE_ADMIN }
            };
            return View("CreateEdit", viewModel);
        }

        public IActionResult Delete(int id)
        {
            DAL dal = new DAL();


            Member member = dal.MemberFact.GetById(id);
            if (member != null)
                return View(member);
            else
                return View("AdminMessage", new AdminMessageViewModel("Le membre n'existe pas"));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Member member)
        {
            if (member != null)
            {
                DAL dal = new DAL();

                dal.MemberFact.DeleteFromId(member.Id);
                return RedirectToAction("List");
            }
            return View("AdminMessage", new AdminMessageViewModel("Le membre n'existe pas"));
        }
    }
}
