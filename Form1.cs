using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using GoogleMaps.LocationServices;
using Newtonsoft.Json;

namespace weatherish
{
    public partial class Form1 : Form
    {
        const string apiKey = "b69bb7fc2737c4c9e90c27ca9d826e02";
        // use in the api call

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // API CALL 
            // api.openweathermap.org/data/2.5/onecall?
            // lat={lat}
            // &lon={lon}
            // &exclude={part}
            // &appid={API key}
            //

        }

        // Search location button  
        private void button1_Click(object sender, EventArgs e)
        {
            //System.Console.WriteLine(apiKey);
            try
            {
                var gls = new GoogleLocationService(useHttps: true);
                var address = textBox1.Text;
                var latlong = gls.GetLatLongFromAddress(address);
                var Latitude = latlong.Latitude;
                var Longitude = latlong.Longitude;
                System.Console.WriteLine("Address ({0}) is at {1},{2}", address, Latitude, Longitude);
                maskedTextBox1.Text = Latitude.ToString();
                maskedTextBox2.Text = Longitude.ToString();
            }
            catch (System.Net.WebException ex)
            {
                System.Console.WriteLine("Google Maps API Error {0}", ex.Message);
            }
        }

        public void find_btn_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

            } else if (radioButton2.Checked)
            {

            } else if (radioButton3.Checked)
            {

            } else if (radioButton4.Checked)
            {

            } else
            {
                throw new Exception();
            }
            String url = "https://api.openweathermap.org/data/2.5/onecall?lat=37.13&lon=80.57&appid=b69bb7fc2737c4c9e90c27ca9d826e02&units=metric";
            init(url);
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            maskedTextBox1.ResetText();
            maskedTextBox2.ResetText();
            maskedTextBox3.ResetText();
        }

        public void init(string url)
        {
            // temp location: TODO call on find weather click
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
                maskedTextBox3.Text = myDeserializedClass.timezone.ToString();
                maskedTextBox2.Text = myDeserializedClass.lon.ToString();
                maskedTextBox1.Text = myDeserializedClass.lat.ToString();

                textBox2.AppendText(myDeserializedClass.current.temp.ToString());
                textBox2.AppendText(myDeserializedClass.current.sunset.ToString());
            }
            Console.WriteLine(httpResponse.StatusCode);
        }
        public class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        public class Current
        {
            public int dt { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double dew_point { get; set; }
            public double uvi { get; set; }
            public int clouds { get; set; }
            public int visibility { get; set; }
            public double wind_speed { get; set; }
            public int wind_deg { get; set; }
            public double wind_gust { get; set; }
            public List<Weather> weather { get; set; }
        }

        public class Minutely
        {
            public int dt { get; set; }
            public int precipitation { get; set; }
        }

        public class Hourly
        {
            public int dt { get; set; }
            public double temp { get; set; }
            public double feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double dew_point { get; set; }
            public double uvi { get; set; }
            public int clouds { get; set; }
            public int visibility { get; set; }
            public double wind_speed { get; set; }
            public int wind_deg { get; set; }
            public double wind_gust { get; set; }
            public List<Weather> weather { get; set; }
            public int pop { get; set; }
        }

        public class Temp
        {
            public double day { get; set; }
            public double min { get; set; }
            public double max { get; set; }
            public double night { get; set; }
            public double eve { get; set; }
            public double morn { get; set; }
        }

        public class FeelsLike
        {
            public double day { get; set; }
            public double night { get; set; }
            public double eve { get; set; }
            public double morn { get; set; }
        }

        public class Daily
        {
            public int dt { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
            public int moonrise { get; set; }
            public int moonset { get; set; }
            public double moon_phase { get; set; }
            public Temp temp { get; set; }
            public FeelsLike feels_like { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double dew_point { get; set; }
            public double wind_speed { get; set; }
            public int wind_deg { get; set; }
            public double wind_gust { get; set; }
            public List<Weather> weather { get; set; }
            public int clouds { get; set; }
            public int pop { get; set; }
            public double uvi { get; set; }
        }

        public class Root
        {
            public double lat { get; set; }
            public double lon { get; set; }
            public string timezone { get; set; }
            public int timezone_offset { get; set; }
            public Current current { get; set; }
            public List<Minutely> minutely { get; set; }
            public List<Hourly> hourly { get; set; }
            public List<Daily> daily { get; set; }
        }
    }
}
