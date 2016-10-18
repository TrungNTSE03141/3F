using _3F.Models;
using _3F.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;

namespace _3F
{
    /// <summary>
    /// Summary description for _3FWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class _3FWebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        /// <summary>
        /// Using Geocoding webservice by google to determine geocoding by address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [WebMethod]
        public Location GetGeocoding(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));
            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var result = xdoc.Element("GeocodeResponse").Element("result");
            var locationElement = result.Element("geometry").Element("location");
            var lat = locationElement.Element("lat");
            var lng = locationElement.Element("lng");

            Location location = new Location(double.Parse(lat.Value),double.Parse(lng.Value));
            return location;
        }


        //Reverse Decoding
        [WebMethod]
        [ScriptMethod]
        public string ReverseDecoding(string longitude, string latitude)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false");
                XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                if (element.InnerText == "ZERO_RESULTS")
                {
                    return ("No data available for the specified location");
                }
                else
                {
                    element = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");
                    return element.LastChild.InnerText;
                }
            }
            catch (Exception ex)
            {
                return ("(Address lookup failed: ) " + ex.Message);
            }
        }

        //Send mail
        [WebMethod]
        public bool SendMail(string email, string firstname, string lastname)
        {
            bool flgResult = false;
            MailHelpers mailHelpers = new MailHelpers();
            mailHelpers.SendMailWelcome("TrungNTSE03141@fpt.edu.vn", "Trung", "Tran");
            return true;
        }

    }
}
