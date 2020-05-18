using System;
using System.Collections.Specialized;
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
                string data = client.DownloadString(newUrlAddress);
                // Console.WriteLine(data);
                Console.WriteLine(newUrlAddress);


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
            string urlAddress = "https://www.latam.com/en_un/";

            string departure_year_month = "2020-06";
            string return_year_month = "2020-06";
            string[] splitedDate = departure_year_month.Split('-');
            int days = DateTime.DaysInMonth(Int16.Parse(splitedDate[0]), Int16.Parse(splitedDate[1]));

            string departure_day = "11";

            string return_day = "18";

            string city_code = "VNO";
            string second_city_code = "MIL";
            string city = "Milan";
            string second_city = "Vilnius";

            string full_departure_date = "/06/2020";
            string full_return_date = "/06/2020";

            string flightClass = "Y";
            string adult_count = "1";
            string children_count = "0";

            string newUrlAddress = "";

            for (int i = 1; i < days + 1; i++)
            {
                departure_day = i.ToString();

                newUrlAddress = urlAddress + "apps/personas/booking?" + "fecha1_dia=" +
                departure_day + "&fecha1_anomes=" + departure_year_month + "&fecha2_dia=" +
                return_day + "&fecha2_anomes=" + return_year_month + "&from_city2=" +
                city_code + "&to_city2=" + second_city_code +
                "&auAvailability=1&ida_vuelta=ida&vuelos_origen=" +
                city + "&from_city1=" + city_code + "&vuelos_destino=" +
                second_city + "&to_city1=" + second_city_code + "&flex=1&vuelos_fecha_salida_ddmmaaaa=" + departure_day +
                full_departure_date + "&vuelos_fecha_regreso_ddmmaaaa=" + return_day + full_return_date +
                "&cabina=" + flightClass + "&nadults=" + adult_count + "&nchildren=" +
                children_count + "&ninfants=0&cod_promo=&stopover_outbound_days=0&stopover_inbound_days=0&application=#/";

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