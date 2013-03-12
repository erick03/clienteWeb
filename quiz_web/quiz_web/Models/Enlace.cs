using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Text;

namespace quiz_web.Models
{
    public class Enlace
    {
        public HttpWebRequest request;
        string usuario="admin";
        string clave="123";

        public string EjecutarAccion(string url, string metodo, object modelo = null)
        {
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 10 * 1000;
            request.Method = metodo;
            request.ContentType = "application/json; charset=utf-8";

            string credentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(usuario + ":" + clave));
            request.Headers.Add("Authorization", "Basic " + credentials);

            if (modelo != null)
            {
                var postString = new JavaScriptSerializer().Serialize(modelo);
                byte[] data = UTF8Encoding.UTF8.GetBytes(postString);
                request.ContentLength = data.Length;
                Stream postStream = request.GetRequestStream();
                postStream.Write(data, 0, data.Length);
            }
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            if (response.StatusCode != HttpStatusCode.NoContent)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                return reader.ReadToEnd();
            }

            return "";
        }
    }
}