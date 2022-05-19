using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP5WebTransactionnel.DataAccessLayer;
using TP5WebTransactionnel.Models;
using System.Collections.Generic;
using TP5WebTransactionnel.Areas.Admin.ViewModels;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using TP5WebTransactionnel.Resources;
using System.IO;
using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TP5WebTransactionnel.Areas.Admin.Controllers
{
    [Authorize(Roles = Member.ROLE_ADMIN)]
    [Area("Admin")]
    public class MenuChoiceController : Controller
    {
        // GET: MenuChoiceController
        public ActionResult List()
        {
            DAL dal = new DAL();
            List<MenuChoice> list = dal.MenuChoiceFactory.GetAllMenuChoice();

            return View(list);
        }

       
        public ActionResult Create()
        {
            return View("CreateEdit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MenuChoice mn, IFormFile uploadfile)
        {
            DAL dal = new DAL();

            if (dal.MenuChoiceFactory.GetAllMenuChoice().Where(x => x.Description == mn.Description).Any())
                return View("AdminMessage", new AdminMessageViewModel("Le menu existe déja"));
            if (mn is null)
                return View("AdminMessage", new AdminMessageViewModel("Le menu est null"));
            if (string.IsNullOrEmpty(mn.Description))
                return View("AdminMessage", new AdminMessageViewModel("La description ne doit pas être vide"));

            MenuChoice mn2 = dal.MenuChoiceFactory.GetByName(mn.Description);

            if (mn2 != null)
            {
                // Il est possible d'ajouter une erreur personnalisée.
                // Le premier paramètre est la propriété touchée
                return View("AdminMessage", Resource.NameAlreadyExists);
            }
            else if (uploadfile != null && uploadfile.Length > 0)
            {
                string extension = Path.GetExtension(uploadfile.FileName).ToLower();
                string filename = String.Format("{0}{1}", Guid.NewGuid().ToString(), extension);

                string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\MenuChoice", filename); //TODO Pour le chemin du fichier

                using FileStream stream = System.IO.File.Create(pathToSave);
                uploadfile.CopyTo(stream);

                mn.ImagePath = filename;
                ModelState["ImagePath"].ValidationState = ModelValidationState.Valid;
            }

            // Si le modèle n'est pas valide, on retourne à la vue Create où les messages seront affichés.
            if (!ModelState.IsValid)
            {
                return View("AdminMessage", new AdminMessageViewModel("Le modèle n'est pas valide"));
            }

            dal.MenuChoiceFactory.AjouterMenuChoice(mn);
        

            return RedirectToAction("List");

            /*if(dal.MenuChoiceFactory.GetAllMenuChoice().Where(x => x.Description == mn.Description).Any())
                return View("AdminMessage", new AdminMessageViewModel("Le menu existe déja"));
            if (mn is null)
                return View("AdminMessage", new AdminMessageViewModel("Le menu est null"));
            if (string.IsNullOrEmpty(mn.Description))
                return View("AdminMessage", new AdminMessageViewModel("La description ne doit pas être vide"));

            dal.MenuChoiceFactory.AjouterMenuChoice(mn);
            return RedirectToAction("List");*/
        }


        public ActionResult Edit(int id)
        {
            DAL dal = new DAL();

            
            if (id == 0)
                return View("AdminMessage", new AdminMessageViewModel("L'id est vide"));
            if (dal.MenuChoiceFactory.GetById(id) is null)
                return View("AdminMessage", new AdminMessageViewModel("Le menu n'existe pas"));


            MenuChoice mn = dal.MenuChoiceFactory.GetById(id);
            return View("CreateEdit", mn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MenuChoice mn)
        {
            DAL dal = new DAL();
            if (dal.MenuChoiceFactory.GetAllMenuChoice().Where(x => x.Description == mn.Description).Any())
                return View("AdminMessage", new AdminMessageViewModel("Le menu existe déja"));
            if (mn is null)
                return View("AdminMessage", new AdminMessageViewModel("Le menu est null"));
            if (string.IsNullOrEmpty(mn.Description))
                return View("AdminMessage", new AdminMessageViewModel("La description est null"));

            


            dal.MenuChoiceFactory.UpdateMenuChoice(mn);
            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            DAL dal = new DAL();
            if (id == 0)
                return View("AdminMessage", new AdminMessageViewModel("L'id est vide"));
            if (dal.MenuChoiceFactory.GetById(id) is null)
                return View("AdminMessage", new AdminMessageViewModel("Le Menu n'existe pas"));

            MenuChoice mn = dal.MenuChoiceFactory.GetById(id);
            return View("Delete", mn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(MenuChoice mn)
        {
            if (mn.Id == 0)
                return View("AdminMessage", new AdminMessageViewModel("L'id est vide"));
            if (mn is null)
                return View("AdminMessage", new AdminMessageViewModel("Le menu est null"));
            DAL dal = new DAL();
            dal.MenuChoiceFactory.DeleteMenuChoice(mn.Id);
            return RedirectToAction("List");
        }
    }
}
