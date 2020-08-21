using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repository;
using ApplicationCore.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace UI.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    public class ProductController : Controller
    {
        Infrastructure.Repository.ProductDao dao = new Infrastructure.Repository.ProductDao();

        IHostingEnvironment _appEnvironment;
        public ProductController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }

        [HttpGet("Product/Index", Name = "Index")]
        public IActionResult Index()
        {
            ViewBag.Products = dao.ConvertToViewModelReadings(dao.GetProducts());

            return View();
        }

        [HttpGet("Product/Market")]
        public IActionResult Market()
        {
            ViewBag.Products = dao.ConvertToViewModelReadings(dao.GetProducts());
            return View();
        }
        [HttpGet("Product/Add")]
        public ActionResult Add()
        {
            Infrastructure.Repository.CategoryDao daoCategory = new Infrastructure.Repository.CategoryDao();
            ViewBag.Categoryid = daoCategory.ConvertToViewModelReadings(daoCategory.GetCategory());
            return View();
        }

        [HttpPost]
        [Route("Product/Add/")]
        public async Task<JsonResult> Add(ProductViewModel model)
        {
            var errors = new List<string>();
            var result = "";
            var success = false;
            try
            {
                if (ModelState.IsValid)
                {                                    
                    if (model.file != null && model.file.Count() > 0)
                    {                                         
                        model.Avatar = await InsertFiles(model.file, model.Name);
                    }                    
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

        [HttpGet("Product/Edit/{Id}")]
        public ActionResult Edit(int Id)
        {
            Infrastructure.Repository.CategoryDao daoCategory = new Infrastructure.Repository.CategoryDao();
            ProductViewModel model = (ProductViewModel)dao.ConvertToViewModel(dao.GetProductById(Id));
            ViewBag.Categoryid = daoCategory.ConvertToViewModelReadings(daoCategory.GetCategory());
            return View(model);
        }

        [HttpPost("Product/Edit/")]
        public async Task<JsonResult> Edit(ProductViewModel model)
        {
            var errors = new List<string>();
            var result = "";
            var success = false;
            try
            {
                if (ModelState.IsValid)
                {                     
                    if (model.file!=null && model.file.Count() > 0)
                    {                                        
                        model.Avatar = await InsertFiles(model.file, model.Name);
                    }                    
                    dao.EditProduct(model);
                    success = true;
                    result = "Alterado com sucesso.";
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

        [HttpGet("Product/Remove/{Id}")]
        public ActionResult Remove(int Id)
        {
            ProductViewModel model = (ProductViewModel)dao.ConvertToViewModel(dao.GetProductById(Id));
            return View(model);
        }

        [HttpPost("Product/Remove/")]
        public async Task<JsonResult> Remove(ProductViewModel model)
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

        [NonAction]
        public async Task<string> InsertFiles(List<IFormFile> arquivos, string Name)
        {
            long tamanhoArquivos = arquivos.Sum(f => f.Length);
            var caminhoArquivo = Path.GetTempFileName();
            string caminhoDestinoArquivoOriginal = "";
            int count = 0;
            foreach (var arquivo in arquivos)
            {
                count = +1; 
                if (arquivo == null || arquivo.Length == 0)
                {

                    var Erro = "Error: Arquivo(s) não selecionado(s)";
                    return Erro;
                }
                string pasta = Name.Replace(" ","_");
                string nomeArquivo = pasta+"_"+count;
                if (arquivo.FileName.Contains(".jpg"))
                    nomeArquivo += ".jpg";
                else if (arquivo.FileName.Contains(".gif"))
                    nomeArquivo += ".gif";
                else if (arquivo.FileName.Contains(".png"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".PNG"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".JPG"))
                    nomeArquivo += ".png";
                else if (arquivo.FileName.Contains(".pdf"))
                    nomeArquivo += ".pdf";
                else
                    nomeArquivo += ".tmp";
                string caminho_WebRoot = _appEnvironment.WebRootPath;                
                string caminhoDestinoArquivo = caminho_WebRoot + "\\images\\" + pasta + "\\";                
                if (!Directory.Exists(caminhoDestinoArquivo))
                    Directory.CreateDirectory(caminhoDestinoArquivo);                
                caminhoDestinoArquivoOriginal = caminhoDestinoArquivo  + nomeArquivo;                
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Append))
                {       
                    await arquivo.CopyToAsync(stream);
                }
            }
            ViewData["Resultado"] = $"{arquivos.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {tamanhoArquivos} bytes";
            int index = caminhoDestinoArquivoOriginal.IndexOf("\\images\\");
            if (index > 0)
                caminhoDestinoArquivoOriginal = caminhoDestinoArquivoOriginal.Substring(index);            
            return caminhoDestinoArquivoOriginal.Replace("\\","/");
        }

    }
}