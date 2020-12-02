using SoapHttpClient;
using SoapHttpClient.Enums;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace KY_Soap_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var soapClient = new SoapClient();
            var ns = XNamespace.Get("http://helio.spdf.gsfc.nasa.gov/");

            var result =
                 soapClient.Post(
                    new Uri("http://sscweb.gsfc.nasa.gov:80/WS/helio/1/HeliocentricTrajectoriesService"),
                    SoapVersion.Soap11,
                    body: new XElement(ns.GetName("getAllObjects")));

            if (result.IsSuccessStatusCode)
            {
                String some = result.Content.ReadAsStringAsync().Result;

                textBox1.Text = FormatXml(some);
            }
        }

        string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }
    }
}
