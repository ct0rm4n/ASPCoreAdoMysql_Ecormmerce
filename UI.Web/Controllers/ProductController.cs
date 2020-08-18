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
    public class ProductController : Controller
    {
        Infrastructure.Repository.ProductDao dao = new Infrastructure.Repository.ProductDao();

        //Define uma instância de IHostingEnvironment
        IHostingEnvironment _appEnvironment;
        //Injeta a instância no construtor para poder usar os recursos
        public ProductController(IHostingEnvironment env)
        {
            _appEnvironment = env;
        }
        public IActionResult Index()
        {
            ViewBag.Products = dao.Convert_To_ViewModel_Readings(dao.GetProducts());
            return View();
        }
        public ActionResult Add()
        {
            //Open modal bootstrap of insert form

            return View();
        }
        [HttpPost]
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

        public ActionResult Edit(int Id)
        {
            //Open in modal bootstrap with insert form
          
            //CONVERT with a query to datatable to ViewModel
            ProductViewModel model = (ProductViewModel)dao.Convert_To_ViewModel(dao.GetProductById(Id));
            return View(model);
        }
        [HttpPost]
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
            }

            return Json(new { success = success, message = result });
        }
        

        

        //método para enviar os arquivos usando a interface IFormFile
        public async Task<string> Insert_Files(List<IFormFile> arquivos, string Name)
        {
            long tamanhoArquivos = arquivos.Sum(f => f.Length);
            // caminho completo do arquivo na localização temporária
            var caminhoArquivo = Path.GetTempFileName();

            string caminhoDestinoArquivoOriginal = "";
            // processa o arquivo enviado
            //percorre a lista de arquivos selecionados
            int count = 0;
            foreach (var arquivo in arquivos)
            {
                count = +1;
                //verifica se existem arquivos 
                if (arquivo == null || arquivo.Length == 0)
                {
                    //retorna a viewdata com erro
                    var Erro = "Error: Arquivo(s) não selecionado(s)";
                    return Erro;
                }

                // < define a pasta onde vamos salvar os arquivos >
                string pasta = Name.Replace(" ","_");
                // Define um nome para o arquivo enviado incluindo o sufixo obtido de milesegundos
                string nomeArquivo = pasta+"_"+count;

                //verifica qual o tipo de arquivo : jpg, gif, png, pdf ou tmp
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