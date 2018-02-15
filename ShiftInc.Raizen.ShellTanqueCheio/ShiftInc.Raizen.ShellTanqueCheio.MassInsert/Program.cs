using Newtonsoft.Json;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Controllers;
using ShiftInc.Raizen.ShellTanqueCheio.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShiftInc.Raizen.ShellTanqueCheio.MassInsert
{
    class Program
    {
    //    Dictionary<string, int> count = new Dictionary<string, int>()
    //    {
    //        { "productType", 0 },
    //        { "cpf", 0 },
    //        { "email", 0 },
    //        { "name", 0 },
    //        { "phone", 0 },
    //        { "idProduct", 0 },
    //        { "cnpj", 0 }
    //    };

    //    static void Main(string[] args)
    //    {
    //        List<string> productTypes = new List<string>() { "v-power", "lubrificantes" };

    //        string email = "ana.banach@shiftinc.com.br ";
    //        string name = "Cadastro Automático";
    //        string phone = "1122334455";
    //        string cnpj = "10306689000130";
    //        DateTime birth = new DateTime(1981, 2, 10);

    //        foreach (var productType in productTypes)
    //        {
    //            for (int i = 0; i < 500; i++)
    //            {
    //                Console.WriteLine("{0} - Inserindo i: {1}", DateTime.Now.ToString("HH:mm:ss"), i);

    //                int idProduct = 0;
    //                if (productType == "v-power")
    //                {
    //                    idProduct = new Random().Next(1, 3);
    //                }
    //                else
    //                {
    //                    idProduct = new Random().Next(6, 7);
    //                }

    //                birth.AddDays(2);
    //                Save(productType, GenerateNew(false), email, name + i.ToString(), phone, idProduct, cnpj, birth);
    //            }
    //        }
    //    }

    //    static void Save(string productType, string cpf, string email, string name, string phone, int idProduct, string cnpj, DateTime birth)
    //    {
            
    //        string fileName = @"Imagens\A.jpg";

    //        ReceiptController receiptController = new ReceiptController();
    //        receiptController.ControllerContext = new ControllerContext()
    //        {
    //            Controller = receiptController,
    //            RequestContext = new RequestContext(new MockHttpContext(), new RouteData()),
    //        };

    //        var getByCPFResult = receiptController.GetPersonByCPF(cpf, productType) as ViewResult;

    //        if (getByCPFResult.ViewName == "PersonalDataForm")
    //        {
    //            Entity.Person person = new Entity.Person()
    //            {
    //                cpf = cpf,
    //                dtCreation = DateTime.Now,
    //                email = email,
    //                name = name,
    //                phone = phone
    //            };

    //            //var savePersonResult = receiptController.SavePerson(person) as ViewResult;
    //            //if (savePersonResult.ViewName == "ReceiptDataForm")
    //            //{
    //            //    ReceiptViewModel receiptModel = new ReceiptViewModel();

    //            //    FileStream fs = new FileStream(fileName, FileMode.Open);

    //            //    receiptModel.ReceiptFile = new MemoryFile(fs, "image/jpeg", fileName);

    //            //    receiptModel.Receipt = new Entity.Receipt()
    //            //    {
    //            //        dtCreation = DateTime.Now,
    //            //        idPerson = person.idPerson,
    //            //        idProduct = idProduct,
    //            //        isValidated = false,
                        
    //            //        Person = person,
                        
    //            //    };

    //            //    receiptModel.Receipt.Person = person;
                    

    //            //    receiptController.Session["Entity.Person"] = person;

    //            //    var saveReceiptResult = receiptController.SaveReceipt(receiptModel) as ViewResult;

    //                fs.Close();
    //            }
    //            else
    //            {
    //                Console.WriteLine("{0} - Falha ao salvar a pessoa {1}", DateTime.Now.ToString("HH:mm:ss"), cpf);
    //            }
    //        }
    //        //else
    //        //{
    //        //    Console.WriteLine("{0} - CPF {1} não pode ser utilizado", DateTime.Now.ToString("HH:mm:ss"), cpf);
    //        //}
    //    }

    //    public static string GenerateNew(bool useDots)
    //    {
    //        int soma = 0, resto = 0;

    //        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    //        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

    //        Random rnd = new Random();
    //        string semente = rnd.Next(100000000, 999999999).ToString();

    //        for (int i = 0; i < 9; i++)
    //            soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

    //        resto = soma % 11;

    //        if (resto < 2)
    //            resto = 0;
    //        else
    //            resto = 11 - resto;

    //        semente = semente + resto;

    //        soma = 0;

    //        for (int i = 0; i < 10; i++)
    //            soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

    //        resto = soma % 11;

    //        if (resto < 2)
    //            resto = 0;
    //        else
    //            resto = 11 - resto;

    //        semente = semente + resto;

    //        if (useDots)
    //        {
    //            semente = string.Format("{0}.{1}.{2}-{3}", semente.Substring(0, 3), semente.Substring(3, 3), semente.Substring(6, 3), semente.Substring(9, 2));
    //        }

    //        return semente;
    //    }
    //}

    //public class MockHttpContext : HttpContextBase
    //{

    //    private readonly HttpRequestBase _request = new MockHttpRequest();
    //    private readonly HttpServerUtilityBase _server = new MockHttpServerUtilityBase();
    //    private HttpSessionStateBase _session = new MockHttpSession();

    //    public override HttpRequestBase Request
    //    {
    //        get { return _request; }
    //    }
    //    public override HttpServerUtilityBase Server
    //    {
    //        get { return _server; }
    //    }
    //    public override HttpSessionStateBase Session
    //    {
    //        get { return _session; }
    //    }
    //}

    //public class MockHttpRequest : HttpRequestBase
    //{
    //    private Uri _url = new Uri("http://www.mockrequest.moc/Controller/Action");

    //    public override Uri Url
    //    {
    //        get { return _url; }
    //    }
    //}

    //public class MockHttpServerUtilityBase : HttpServerUtilityBase
    //{
    //    public override string UrlEncode(string s)
    //    {
    //        //return base.UrlEncode(s);     
    //        return s;       // Not doing anything (this is just a Mock)
    //    }

    //    public override string MapPath(string path)
    //    {
    //        return path;
    //    }
    //}

    //public class MockHttpSession : HttpSessionStateBase
    //{
    //    // Started with sample http://stackoverflow.com/questions/524457/how-do-you-mock-the-session-object-collection-using-moq
    //    // from http://stackoverflow.com/users/81730/ronnblack

    //    System.Collections.Generic.Dictionary<string, object> _sessionStorage = new System.Collections.Generic.Dictionary<string, object>();
    //    public override object this[string name]
    //    {
    //        get { return _sessionStorage[name]; }
    //        set { _sessionStorage[name] = value; }
    //    }

    //    public override void Add(string name, object value)
    //    {
    //        _sessionStorage[name] = value;
    //    }
    }
}
