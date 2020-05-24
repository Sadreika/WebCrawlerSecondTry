using Crawler;
using System;
using System.Collections.Specialized;
using System.Net;

namespace SecondTryCrawler
{
    public class Program
    {
        public int triesToConnect = 0;
        public int crawling(String newUrlAddress)
        {
            WebClient client = new WebClient();

            NameValueCollection myNameValueCollection = new NameValueCollection();

            String[] valueArray = null;

            myNameValueCollection.Add("Host", "www.latam.com");
            myNameValueCollection.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:76.0) Gecko/20100101 Firefox/76.0");
            myNameValueCollection.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            myNameValueCollection.Add("Accept-Language", "en-GB,en;q=0.5");

            foreach(String key in myNameValueCollection.Keys)
            {
                valueArray = myNameValueCollection.GetValues(key);
                foreach(String value in valueArray)
                {
                    client.Headers.Set(key, value); 
                }
            }

            //string pattern = @"\b[M]\W+";
            //Regex createRegex = new Regex(pattern);

            
            try
            {

              /*  System.IO.Stream stream = client.OpenRead(newUrlAddress);
                using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                {
                    String text = reader.ReadToEnd();
                }*/

                //string data = client.DownloadString(newUrlAddress);
                //client.DownloadFile(newUrlAddress, @"C:\Users\mariu\Desktop\localfile.html"); 
                Console.WriteLine(newUrlAddress);
                //Console.WriteLine(data);

                // MatchCollection matchedCars = createRegex.Matches(data);

                /*   for (int count = 0; count < matchedCars.Count; count++)
                       Console.WriteLine(matchedCars[count].Value); */

                /*WebHeaderCollection myWebHeaderCollection = client.ResponseHeaders;
                for (int i = 0; i < myWebHeaderCollection.Count; i++)
                    Console.WriteLine("\t" + myWebHeaderCollection.GetKey(i) + " = " + myWebHeaderCollection.Get(i));

                Console.WriteLine("Duomenys\n" + data);*/
                return 0;

            }
            catch (Exception response_exception)
            {
                Console.WriteLine("ERROR\n" + response_exception);
                triesToConnect = triesToConnect + 1;
                if(triesToConnect == 1)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        static void Main(string[] args)
        {
            int methodRecursion = 1;
            Program crawlerObject = new Program();
            Sc scObject = new Sc();

            int days = scObject.gettingDate();

            for (int i = 30; i < days; i++) // reikes pakeisti (int i = 1; i < days + 1; i++)
            {
                string departure_day = i.ToString();
                string newUrlAddress = scObject.createUrl(departure_day);
               
                methodRecursion = crawlerObject.crawling(newUrlAddress);

                if (methodRecursion == 1)
                {
                    methodRecursion = crawlerObject.crawling(newUrlAddress);
                }
            }
            Console.WriteLine("END");
        }
    }
}