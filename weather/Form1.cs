using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace howto_weather_forecast
{
    public partial class Form1 : Form
    {
        int i = 0;
        public Form1()
        {
            InitializeComponent();
        }

        // Enter your API key here.
        // Get an API key by making a free account at:
        //      http://home.openweathermap.org/users/sign_in
        private const string API_KEY = "e1053d1b85007fa1f764226b8401b627";

        // Query URLs. Replace @LOC@ with the location.
        private const string CurrentUrl =
            "http://api.openweathermap.org/data/2.5/weather?" +
            "q=@LOC@&mode=xml&units=metric&APPID=" + API_KEY;
        private const string ForecastUrl =
            "http://api.openweathermap.org/data/2.5/forecast?" +
            "q=@LOC@&mode=xml&units=imperial&APPID=" + API_KEY;

        // Get current conditions.
        private void btnConditions_Click(object sender, EventArgs e)
        {
            // Compose the query URL.
            string url = CurrentUrl.Replace("@LOC@", txtLocation.Text);
            txtXml.Text = GetFormattedXml(url);
        }

        // Get a forecast.
        private void btnForecast_Click(object sender, EventArgs e)
        {
            // Compose the query URL.
            string url = ForecastUrl.Replace("@LOC@", txtLocation.Text);
            txtXml.Text = GetFormattedXml(url);
        }

        // Return the XML result of the URL.
        private string GetFormattedXml(string url)
        {
            // Create a web client.
            using (WebClient client = new WebClient())
            {
                // Get the response string from the URL.
                string xml = client.DownloadString(url);

                // Load the response into an XML document.
                XmlDocument xml_document = new XmlDocument();
                xml_document.LoadXml(xml);
              //  XmlNodeList xnList = xml_document.SelectNodes("/Names/Name[@type='M']");
                // Format the XML.
                using (StringWriter string_writer = new StringWriter())
                {
                    XmlTextWriter xml_text_writer = new XmlTextWriter(string_writer);
                    xml_text_writer.Formatting = Formatting.Indented;
                    xml_document.WriteTo(xml_text_writer);

                    // Return the result.
                    return string_writer.ToString();
                }
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = CurrentUrl.Replace("@LOC@", txtLocation.Text);

            using (StreamWriter sw = new StreamWriter(@"weather.txt"))
            {
               sw.WriteLine(GetFormattedXml(url));
            }
            //XmlSerializer serial = new XmlSerializer(typeof(string));
            //using (FileStream fs = new FileStream(Environment.CurrentDirectory + "\\weather.xml", FileMode.Create, FileAccess.Write))
            //{ serial.Serialize(fs, GetFormattedXml(url)); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(@"weather.txt");
            label3.Text = "Temperature :" + lines[9].Remove(0, 22).Remove(3);
            label4.Text = "Weather Type :"+ lines[29].Remove(0,31).Remove(15);

            //    XmlDataDocument xmldoc = new XmlDataDocument();
            //    XmlNodeList xmlnode;
            //    XmlNodeList xmlnode1;
            //   string keimeno = null;
            //    string keimeno1 = null;


            //    FileStream fs = new FileStream("weather.xml", FileMode.Open, FileAccess.Read);

            //    xmldoc.Load(fs);
            //    xmlnode = xmldoc.GetElementsByTagName("temperature value");
            //    xmlnode1 = xmldoc.GetElementsByTagName("weather");
            //    keimeno = xmlnode[i].ChildNodes.Item(0).InnerText.Trim();
            //    keimeno1= xmlnode1[i].ChildNodes.Item(0).InnerText.Trim();

            //    label1.Text = keimeno;
            //    label2.Text = keimeno1;


            //    fs.Close();
        }
    }
}
