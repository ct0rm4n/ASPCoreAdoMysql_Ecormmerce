using Infrastructure.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace Infraestruture.UnitTest
{
    [TestClass]
    public class Main
    {
        ProductDao dao = new ProductDao();
        [TestInitialize]
        [TestMethod]
        public void Initialize()
        {
            try
            {                
                dao.Open();
                System.Console.WriteLine("Teste 1 - teste unitario iniciado(OK)...");
            }
            catch(System.Exception e)
            {
                System.Console.WriteLine("Teste 1 - Sistema não está pronto para uso.</br> erro:"+e);
            }
        }
        [TestMethod]
        public void SelectTable()
        {
            try
            {
                var dataTable = new DataTable();
                Assert.AreEqual(dataTable, dao.GetProducts());
                System.Console.WriteLine("teste 2 - Acesso a tablas permirtido(OK)...");
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Sistema não está pronto para uso.</br> erro:" + e);
            }
        }
        [TestMethod]
        public void After()
        {
            try
            {
                dao.Close();
                System.Console.WriteLine("Teste 3 - Conexão finalizada, sistema pronto para uso.");
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine("Sistema não está pronto para uso.</br> erro:" + e);
            }
        }
    }
}
