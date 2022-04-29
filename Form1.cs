﻿using System;
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
            // API CALL 
            // api.openweathermap.org/data/2.5/onecall?
            // lat={lat}
            // &lon={lon}
            // &exclude={part}
            // &appid={API key}
            //
            var unit_flag = "standard";
            if (radioButton6.Checked)
            {
                unit_flag = "standard";
            } else if (radioButton7.Checked)
            {
                unit_flag = "metric";
            } else if (radioButton8.Checked)
            {
                unit_flag = "imperial";
            }
            if (maskedTextBox1.Text == "string>.Empty" || maskedTextBox2.Text == "string>.Empty")
            {
                maskedTextBox1.Text = "0";
                maskedTextBox2.Text = "0";
            }
            String url = "https://api.openweathermap.org/data/2.5/onecall?lat=" + maskedTextBox1.Text + "&lon=" + maskedTextBox2.Text + "&appid=b69bb7fc2737c4c9e90c27ca9d826e02&units=" + unit_flag;
            init(url);
        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            maskedTextBox1.ResetText();
            maskedTextBox2.ResetText();
            maskedTextBox3.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;
            radioButton8.Checked = false;
        }

        public void init(string url)
        {
            textBox2.ResetText();
            // temp location: TODO call on find weather click
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(result);
                    //maskedTextBox3.Text = myDeserializedClass.timezone.ToString();
                    //maskedTextBox2.Text = myDeserializedClass.lon.ToString();
                    //maskedTextBox1.Text = myDeserializedClass.lat.ToString();
                    maskedTextBox3.Text = myDeserializedClass.timezone.ToString();
                    if (radioButton1.Checked) // current
                    {
                        textBox2.AppendText("Current time: " + myDeserializedClass.current.dt.ToString() + Environment.NewLine);
                        textBox2.AppendText("Sunrise: " + myDeserializedClass.current.sunrise.ToString() + Environment.NewLine);
                        textBox2.AppendText("Sunset: " + myDeserializedClass.current.sunset.ToString() + Environment.NewLine);
                        textBox2.AppendText("Temperature: " + myDeserializedClass.current.temp.ToString() + Environment.NewLine);
                        textBox2.AppendText("Feels Like: " + myDeserializedClass.current.feels_like.ToString() + Environment.NewLine);
                        textBox2.AppendText("Pressure: " + myDeserializedClass.current.pressure.ToString() + " hPa" + Environment.NewLine);
                        textBox2.AppendText("Humidity: " + myDeserializedClass.current.humidity.ToString() + "%" + Environment.NewLine);
                        textBox2.AppendText("Dew Point: " + myDeserializedClass.current.dew_point.ToString() + Environment.NewLine);
                        textBox2.AppendText("UV Index: " + myDeserializedClass.current.uvi.ToString() + Environment.NewLine);
                        textBox2.AppendText("Clouds: " + myDeserializedClass.current.clouds.ToString() + "%" + Environment.NewLine);
                        textBox2.AppendText("Visibility: " + myDeserializedClass.current.visibility.ToString() + Environment.NewLine);
                        textBox2.AppendText("Wind Speed: " + myDeserializedClass.current.wind_speed.ToString() + "mph" + Environment.NewLine);
                        textBox2.AppendText("Wind Degree: " + myDeserializedClass.current.wind_deg.ToString() + Environment.NewLine);
                        textBox2.AppendText("Wind Gust: " + myDeserializedClass.current.wind_gust.ToString() + "mph" + Environment.NewLine);
                    }
                    else if (radioButton2.Checked)
                    {
                        textBox2.AppendText("Current time: " + myDeserializedClass.minutely[0].dt.ToString() + Environment.NewLine);
                        textBox2.AppendText("Precipitation: " + myDeserializedClass.minutely[0].precipitation.ToString() + Environment.NewLine);
                    }
                    else if (radioButton3.Checked)
                    {
                        textBox2.AppendText("Current time: " + myDeserializedClass.hourly[0].dt.ToString() + Environment.NewLine);
                        //textBox2.AppendText("Sunrise: " + myDeserializedClass.hourly[0].sunrise.ToString() + Environment.NewLine);
                        //textBox2.AppendText("Sunset: " + myDeserializedClass.hourly[0].sunset.ToString() + Environment.NewLine);
                        textBox2.AppendText("Temperature: " + myDeserializedClass.hourly[0].temp.ToString() + Environment.NewLine);
                        textBox2.AppendText("Feels Like: " + myDeserializedClass.hourly[0].feels_like.ToString() + Environment.NewLine);
                        textBox2.AppendText("Pressure: " + myDeserializedClass.hourly[0].pressure.ToString() + " hPa" + Environment.NewLine);
                        textBox2.AppendText("Humidity: " + myDeserializedClass.hourly[0].humidity.ToString() + "%" + Environment.NewLine);
                        textBox2.AppendText("Dew Point: " + myDeserializedClass.hourly[0].dew_point.ToString() + Environment.NewLine);
                        textBox2.AppendText("UV Index: " + myDeserializedClass.hourly[0].uvi.ToString() + Environment.NewLine);
                        textBox2.AppendText("Clouds: " + myDeserializedClass.hourly[0].clouds.ToString() + "%" + Environment.NewLine);
                        textBox2.AppendText("Visibility: " + myDeserializedClass.hourly[0].visibility.ToString() + Environment.NewLine);
                        textBox2.AppendText("Wind Speed: " + myDeserializedClass.hourly[0].wind_speed.ToString() + "mph" + Environment.NewLine);
                        textBox2.AppendText("Wind Degree: " + myDeserializedClass.hourly[0].wind_deg.ToString() + Environment.NewLine);
                        textBox2.AppendText("Wind Gust: " + myDeserializedClass.hourly[0].wind_gust.ToString() + "mph" + Environment.NewLine);
                    }
                    else if (radioButton4.Checked)
                    {
                        textBox2.AppendText("Current time: " + myDeserializedClass.daily[0].dt.ToString() + Environment.NewLine);
                        textBox2.AppendText("Sunrise: " + myDeserializedClass.daily[0].sunrise.ToString() + Environment.NewLine);
                        textBox2.AppendText("Sunset: " + myDeserializedClass.daily[0].sunset.ToString() + Environment.NewLine);
                        textBox2.AppendText("Moonrise: " + myDeserializedClass.daily[0].moonrise.ToString() + Environment.NewLine);
                        textBox2.AppendText("Moonset: " + myDeserializedClass.daily[0].moonset.ToString() + Environment.NewLine);
                        textBox2.AppendText("Moon Phase: " + myDeserializedClass.daily[0].moon_phase.ToString() + Environment.NewLine);
                        textBox2.AppendText("Temperature: " + myDeserializedClass.daily[0].temp.day.ToString() + Environment.NewLine);
                        textBox2.AppendText("Feels Like: " + myDeserializedClass.daily[0].feels_like.day.ToString() + Environment.NewLine);
                        textBox2.AppendText("Pressure: " + myDeserializedClass.daily[0].pressure.ToString() + " hPa" + Environment.NewLine);
                        textBox2.AppendText("Humidity: " + myDeserializedClass.daily[0].humidity.ToString() + "%" + Environment.NewLine);
                        textBox2.AppendText("Precip %: " + myDeserializedClass.daily[0].pop.ToString() + "%" + Environment.NewLine);
                        textBox2.AppendText("Dew Point: " + myDeserializedClass.daily[0].dew_point.ToString() + Environment.NewLine);
                        textBox2.AppendText("UV Index: " + myDeserializedClass.daily[0].uvi.ToString() + Environment.NewLine);
                        textBox2.AppendText("Clouds: " + myDeserializedClass.daily[0].clouds.ToString() + "%" + Environment.NewLine);
                        //textBox2.AppendText("Visibility: " + myDeserializedClass.daily[0].visibility.ToString() + Environment.NewLine);
                        textBox2.AppendText("Wind Speed: " + myDeserializedClass.daily[0].wind_speed.ToString() + "mph" + Environment.NewLine);
                        textBox2.AppendText("Wind Degree: " + myDeserializedClass.daily[0].wind_deg.ToString() + Environment.NewLine);
                        textBox2.AppendText("Wind Gust: " + myDeserializedClass.daily[0].wind_gust.ToString() + "mph" + Environment.NewLine);
                    }
                    else if (radioButton5.Checked)
                    {
                        //textBox2.AppendText("ALERT!" + Environment.NewLine);
                        //textBox2.AppendText("Alert Sender Name: " + myDeserializedClass.);
                    }
                    else
                    {
                        textBox2.Text = "No timeframe given.";
                    }

                }
                Console.WriteLine(httpResponse.StatusCode);
            }
            catch (Exception e)
            {
                textBox2.Text = "Check your input, exception thrown: " + e;
            }
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
            public double pop { get; set; }
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
            public double pop { get; set; }
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
