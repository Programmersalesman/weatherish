using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

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

        private void button1_Click(object sender, EventArgs e)
        {
            //search button

        }

        private void find_btn_Click(object sender, EventArgs e)
        {
            // temp location: TODO call on find weather click
            var url = "https://api.openweathermap.org/data/2.5/onecall?lat=37.13&lon=80.57&appid=b69bb7fc2737c4c9e90c27ca9d826e02";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            Console.WriteLine(httpResponse.StatusCode);

        }

        private void clear_btn_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            maskedTextBox1.ResetText();
            maskedTextBox2.ResetText();
            maskedTextBox3.ResetText();
        }
    }
}
