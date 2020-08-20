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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        Infrastructure.Repository.ProductDao dao = new Infrastructure.Repository.ProductDao();

        IHostingEnvironment _appEnvironment;
        public ProductController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }
        [HttpGet("Index/", Name = "Index")]
        public IActionResult Index()
        {
            ViewBag.Products = dao.Convert_To_ViewModel_Readings(dao.GetProducts());
            
            return View();
        }
        [HttpGet("Market/", Name = "Market")]
        public IActionResult Market()
        {
            ViewBag.Products = dao.Convert_To_ViewModel_Readings(dao.GetProducts());
            return View();
        }
        [HttpPost("Add")]
        public ActionResult Add()
        {           
            return View();
        }
        [HttpPost("Add/{model}")]
        public async Task<JsonResult> Add(ProductViewModel model)
        {
            var errors = new List<string>();
            var result = "";
            var success = false;
            try
            {
                if (ModelState.IsValid)
                {

                    //if user insert images                
                    if (model.file != null && model.file.Count() > 0)
                    {
                        //insert imgs and define patch of principal image on the product table                 
                        model.Avatar = await Insert_Files(model.file, model.Name);
                    }
                    //DAO Mysql - run the query insert in table
                    dao.InserProduct(model);
                    success = true;
                    result = "Cadastrado com sucesso.";
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
                //return Ok("Success");
            }
            return Json(new { success = success, message = result });
            //return Ok("Success");
        }
        [NonAction]
        public ActionResult Edit(int Id)
        {            
            ProductViewModel model = (ProductViewModel)dao.Convert_To_ViewModel(dao.GetProductById(Id));

            return View(model);
        }
        [HttpPost("Edit/{model}")]
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
                        //insert imgs and define patch of principal image on the product table                 
                        model.Avatar = await Insert_Files(model.file, model.Name);
                    }
                    //run execute MYSQL query
                    dao.EditProduct(model);
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
        [HttpPut("Remove/{model}")]
        public ActionResult Remove(int Id)
        {
            //Open in modal bootstrap with insert form

            //CONVERT with a query to datatable to ViewModel
            ProductViewModel model = (ProductViewModel)dao.Convert_To_ViewModel(dao.GetProductById(Id));
            return View(model);
        }
        
        [NonAction]
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

        public async Task<string> Insert_Files(List<IFormFile> arquivos, string Name)
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

                //< obtém o caminho físico da pasta wwwroot >
                string caminho_WebRoot = _appEnvironment.WebRootPath;
                // monta o caminho onde vamos salvar o arquivo :  ~\wwwroot\Arquivos\Arquivos_Usuario\Recebidos
                string caminhoDestinoArquivo = caminho_WebRoot + "\\images\\" + pasta + "\\";
                //create a folde if dont exist
                if (!Directory.Exists(caminhoDestinoArquivo))
                    Directory.CreateDirectory(caminhoDestinoArquivo);
                // incluir a pasta Recebidos e o nome do arquiov enviado : ~\wwwroot\images\produtoid\
                caminhoDestinoArquivoOriginal = caminhoDestinoArquivo  + nomeArquivo;
                //remove anothers
                
                //copia o arquivo para o local de destino original(caso tenha já o mesmo sobreescreve)
                using (var stream = new FileStream(caminhoDestinoArquivoOriginal, FileMode.Append))
                {
                    //remove anothers                    
                    await arquivo.CopyToAsync(stream);
                }
            }

            //monta a ViewData que será exibida na view como resultado do envio 
            ViewData["Resultado"] = $"{arquivos.Count} arquivos foram enviados ao servidor, " +
             $"com tamanho total de : {tamanhoArquivos} bytes";
            int index = caminhoDestinoArquivoOriginal.IndexOf("\\images\\");
            if (index > 0)
                caminhoDestinoArquivoOriginal = caminhoDestinoArquivoOriginal.Substring(index);            
            //retorna a viewdata
            return caminhoDestinoArquivoOriginal.Replace("\\","/");
        }
    }
}