using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UI.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    public class CategoryController : Controller
    {
        Infrastructure.Repository.CategoryDao dao = new Infrastructure.Repository.CategoryDao();
        [HttpGet("Category/", Name = "Category/Index")]
        public IActionResult Index()
        {
            ViewBag.Categorys = dao.Convert_To_ViewModel_Readings(dao.GetCategory());

            return View();
        }
        [HttpGet("Category/Add")]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Route("Category/Add_/")]
        public async Task<JsonResult> Add_(CategoryViewModel model)
        {
            var errors = new List<string>();
            var result = "";
            var success = false;
            try
            {
                if (ModelState.IsValid)
                {                  

                    dao.InserProduct(model);
                    success = true;
                    result = "Cadastrado com sucesso.";
                }
                else
                {

                    foreach (var modelStateVal in ViewData.ModelState.Values)
                    {

                        errors.AddRange(modelStateVal.Errors.Select(error => "</br>" + error.ErrorMessage));

                    }
                    success = false;
                }
            }
            catch (Exception ex)
            {
                errors.Add("Ocorreu o erro:" + ex);
                success = false;
                result = errors.ToString();
            }
            if (success == false)
            {
                return Json(new { success = false, message = errors });

            }
            return Json(new { success = success, message = result });
        }
        [HttpGet("Category/Edit/{Id}")]
        public ActionResult Edit(int Id)
        {

            CategoryViewModel model = (CategoryViewModel)dao.Convert_To_ViewModel(dao.GetCategoryById(Id));

            return View(model);
        }
        [HttpPost]
        [Route("Category/Edit/")]
        public async Task<JsonResult> Edit(CategoryViewModel model)
        {
            var errors = new List<string>();
            var result = "";
            var success = false;
            try
            {
                if (ModelState.IsValid)
                {

                    
                    //run execute MYSQL query
                    dao.EditCategory(model);
                    success = true;
                    result = "Alterado com sucesso.";
                }
                else
                {
                    //caso de validação falhar
                    foreach (var modelStateVal in ViewData.ModelState.Values)
                    {

                        errors.AddRange(modelStateVal.Errors.Select(error => "</br>" + error.ErrorMessage));

                    }
                    success = false;
                }
            }
            catch (Exception ex)
            {
                errors.Add("Ocorreu o erro:" + ex);
                success = false;
                result = errors.ToString();
            }
            if (success == false)
            {
                return Json(new { success = false, message = errors });
            }

            return Json(new { success = success, message = result });
        }
        [HttpGet("Category/Remove")]
        public ActionResult Remove(int Id)
        {
            //Open in modal bootstrap with insert form

            //CONVERT with a query to datatable to ViewModel
            CategoryViewModel model = (CategoryViewModel)dao.Convert_To_ViewModel(dao.GetCategoryById(Id));
            return View(model);
        }

        [HttpPut("Category/Remove")]
        public async Task<JsonResult> Remove(CategoryViewModel model)
        {
            var errors = new List<string>();
            var result = "";
            var success = false;
            try
            {

                dao.RemoveProduct(model);
                success = true;
                result = "Removido com sucesso.";

            }
            catch (Exception ex)
            {
                errors.Add("Ocorreu o erro:" + ex);
                success = false;
                result = errors.ToString();
            }
            if (success == false)
            {
                return Json(new { success = false, message = errors });
            }

            return Json(new { success = success, message = result });
        }
    }
}
