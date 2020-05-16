using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace SecondTryCrawler
{
    public class Program
    {
        public String proxyChange()
        {
            String proxy = "";
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\mariu\Documents\GitHub\New\WebCrawlertest0.1\WebCrawlerv0.1\Proxy.txt"); // proxiu sarasas
                proxy = lines[3];
            }
            catch (Exception fileException)
            {
                Console.WriteLine("Cant find the file");
                proxy = null;
            }
            return proxy;
            //String new_proxy = proxyChange();
            //string[] proxyInfo = new_proxy.Split(':');
            //client.Proxy = new WebProxy(proxyInfo[0], Int32.Parse(proxyInfo[1]));
        }

        public int crawling(String urlAddress)
        {
            
            RestClient client = new RestClient(urlAddress);
            client.AddDefaultHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0;)");
            client.AddDefaultHeader("Accept-Language", "en-GB,en;q=0.5");
            //client.AddDefaultHeader("Accept-Encoding", "gzip, deflate, br"); */
            client.AddDefaultHeader("Accept", "text / html,application / xhtml + xml,application / xml; q = 0.9,image / webp,*/*;q=0.8");
            var cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie("__cfduid", "d817e3346238b447472c70cf62558b2fd1589259908", "/", "www.norwegian.com"));
            cookieContainer.Add(new Cookie("cf_clearance", "8d93bdf19e18771df2f57d6b92ce47b8167ba87c-1589563029-0-250", "/", "www.norwegian.com"));
            client.CookieContainer = cookieContainer;

            var request = new RestRequest("", Method.GET);
            
            try
            {
                var response = client.Execute(request).Content;
                Console.WriteLine(response);
            }
            catch (Exception response_exception)
            {
                Console.WriteLine("Error");
            }
            return 0;
        }

        static void Main(string[] args)
        {
            int methodRecursion = 1;
            Program crawlerObject = new Program();
            string urlAddress = "https://www.norwegian.com/uk/";
            methodRecursion = crawlerObject.crawling(urlAddress);
        }
    }
}
