using System;

namespace Crawler
{
    public class Sc
    {
        private string urlAddress = "https://www.latam.com/en_un/";

        private string departure_year_month = "2020-06";
        private string departure_day = "11";
        private string return_year_month = "2020-06";
        private string return_day = "18";

        private string city_code = "VNO";
        private string second_city_code = "MIL";
        private string city = "Milan";
        private string second_city = "Vilnius";

        private string full_departure_date = "/06/2020";
        private string full_return_date = "/06/2020";

        private string flightClass = "Y";
        private string adult_count = "1";
        private string children_count = "0";

        public int gettingDate()
        {
            string[] splitedDate = departure_year_month.Split('-');
            int days = DateTime.DaysInMonth(Int16.Parse(splitedDate[0]), Int16.Parse(splitedDate[1]));
            return days;
        }

        public String createUrl(string departure_day)
        {
            string newUrlAddress = urlAddress + "apps/personas/booking?" + "fecha1_dia=" +
                departure_day + "&fecha1_anomes=" + departure_year_month + "&fecha2_dia=" +
                return_day + "&fecha2_anomes=" + return_year_month + "&from_city2=" +
                city_code + "&to_city2=" + second_city_code +
                "&auAvailability=1&ida_vuelta=ida&vuelos_origen=" +
                city + "&from_city1=" + city_code + "&vuelos_destino=" +
                second_city + "&to_city1=" + second_city_code + "&flex=1&vuelos_fecha_salida_ddmmaaaa=" + departure_day +
                full_departure_date + "&vuelos_fecha_regreso_ddmmaaaa=" + return_day + full_return_date +
                "&cabina=" + flightClass + "&nadults=" + adult_count + "&nchildren=" +
                children_count + "&ninfants=0&cod_promo=&stopover_outbound_days=0&stopover_inbound_days=0&application=#/";

            return newUrlAddress;
        }
    }
}
