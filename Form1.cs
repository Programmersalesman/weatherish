using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace weatherish
{
    public partial class Form1 : Form
    {
        const string apiKey = "b69bb7fc2737c4c9e90c27ca9d826e02";

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


        }

        private void button1_Click(object sender, EventArgs e)
        {
            //search button

        }

        private void find_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
