using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Web.Controllers
{
    public class HangFireController : Controller
    {
        Infrastructure.Repository.ProductDao dao = new Infrastructure.Repository.ProductDao();
        public string String()
        {            
            return dao.connection_On_HangFire;
        }
        public void VerifyStockAsync()
        {
            Console.WriteLine("Verificando..");
        }
    }
}
