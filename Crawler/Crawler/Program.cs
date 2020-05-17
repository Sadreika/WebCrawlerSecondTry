using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Configuration;

namespace SecondTryCrawler
{
    public class Program
    {
        public int triesToConnect = 0;
        public int proxy_number = 0;
        public String proxyChange()
        {
            String proxy = "";
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\mariu\Documents\GitHub\New\WebCrawlertest0.1\WebCrawlerv0.1\Proxy.txt"); // proxiu sarasas
                proxy = lines[proxy_number];
            }
            catch (Exception fileException)
            {
                Console.WriteLine("FILE ERROR");
                proxy = null;
            }
            return proxy;
        }


        public int crawling(String urlAddress)
        {
            String new_proxy = proxyChange();
            if (new_proxy == null)
            {
                System.Environment.Exit(0);
            }
            else
            {
                if (triesToConnect == 2)
                {
                    proxy_number = proxy_number + 1;
                    new_proxy = proxyChange();
                    if (new_proxy == null)
                    {
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        triesToConnect = 0;
                    }
                }
                Console.WriteLine((triesToConnect + 1) + " TRY");
            }

            WebClient client = new WebClient();

            client.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0");
            
            client.Headers.Set("Accept-Language", "en-GB,en;q=0.5");
            client.Headers.Set("Accept", "application/json, text/plain, */*");

            
            Console.WriteLine("INFO ABOUT HEADER\n" + client.Headers);

            string[] proxyInfo = new_proxy.Split(':');
            //client.Proxy = new WebProxy(proxyInfo[0], Int32.Parse(proxyInfo[1]));
            Console.WriteLine("PROXY\n" + proxyInfo[0] + " " + Int32.Parse(proxyInfo[1]));

            string marke = "Alpina";
            string modelis = "";
            string kaina_nuo = "";
            string kaina_iki = "";
            string metai_nuo = "";
            string metai_iki = "";
            string kebulas = "";
            string kuro_tipas = "";
            
            string newUrl = urlAddress;
            newUrl = newUrl + "?f_1%5B0%5D=" + marke + "&f_model_14%5B0%5D=" + modelis + "&f_215=" + kaina_nuo
                + "&f_216=" + kaina_iki + "&f_41=" + metai_nuo + "&f_42=" + metai_iki + "&f_3%5B1%5D=" + kebulas + "&f_2%5B2%5D=" +kuro_tipas + "&f_376= HTTP/1.1";
           // Console.WriteLine(newUrl);

            string pattern = @"\b[M]\W+";
            Regex createRegex = new Regex(pattern);
            
            try
            {
               // client.QueryString.Add("param1", "value1");
               // client.QueryString.Add("param2", "value2");
                string data = client.DownloadString(newUrl);

                MatchCollection matchedCars = createRegex.Matches(data);
                
                for (int count = 0; count < matchedCars.Count; count++)
                    Console.WriteLine(matchedCars[count].Value);

                WebHeaderCollection myWebHeaderCollection = client.ResponseHeaders;
               /* for (int i = 0; i < myWebHeaderCollection.Count; i++)
                    Console.WriteLine("\t" + myWebHeaderCollection.GetKey(i) + " = " + myWebHeaderCollection.Get(i));*/

                Console.WriteLine("Duomenys\n" + data);
                return 0;

            }
            catch (Exception response_exception)
            {
                triesToConnect = triesToConnect + 1;
                Console.WriteLine("ERROR\n" + response_exception);
                return 1;
            }
        }

        static void Main(string[] args)
        {
            int methodRecursion = 1;
            Program crawlerObject = new Program();
            string urlAddress = "https://en.autogidas.lt/skelbimai/automobiliai/";
            while (methodRecursion == 1)
            {
                methodRecursion = crawlerObject.crawling(urlAddress);
            }
            Console.WriteLine("END");
        }
    }
}
// client.Headers.Set("Accept-Encoding", "gzip, deflate, br");
// client.Headers.Set(HttpRequestHeader.Cookie, "MUID=");
// client.Headers.Set(HttpRequestHeader.Cookie, "cf_clearance=");
// client.Headers.Set(HttpRequestHeader.Cookie, "__cfduid=");
// client.Headers.Set(HttpRequestHeader.Cookie, "ASP.NET_SessionId=");
// client.Headers.Set("__RequestVerificationToken", "X0kk1RIYeYSnHf3iX-tS1E__veNA1-F9JUXk0D-kFjsjDM17QOyEV8zWHWWtO5svxzSAjTcKYLwMXDn46e-rAN39EEk1");
// client.Headers.Set("forterToken", "c472c17dbe8a458f8babc3299ba99403_1589618250040_15450_UDF43_9ck");