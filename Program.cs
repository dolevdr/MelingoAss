using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WrdList
{
    class Program
    {

        static void Main(string[] args)
        {
            Objects ob = new Objects();

            while (true)
            {
                HttpListener listener = new HttpListener();
                listener.Prefixes.Add("http://localhost:8080/");
                Console.WriteLine("listening to requests....");
                listener.Start();
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                if (request.HttpMethod == "OPTIONS")
                {
                    // Set the response status code
                    context.Response.StatusCode = (int)HttpStatusCode.OK;

                    // Set the response headers
                    //context.Response.AddHeader("Access-Control-Allow-Origin", "http://localhost:3000");
                    context.Response.AddHeader("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT");
                    context.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type");
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                }
                if (request.HttpMethod == "POST")
                {
                    //get response
                    HttpListenerResponse resp = context.Response;

                    //configure response
                    resp.ContentType = "application/json";
                    resp.StatusCode = (int)HttpStatusCode.OK;
                    resp.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                    
                    
                    var body = JObject.Parse(new StreamReader(context.Request.InputStream, request.ContentEncoding).ReadToEnd());

                    var list = ob.filter((string)body["word"]);
                    
                    

                    string responseString = JsonConvert.SerializeObject(list);
                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    resp.ContentLength64 = buffer.Length;

                    //write response body
                    Stream output = resp.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    // Close the output stream
                    output.Close();
                }

                context.Response.Close();
                listener.Close();
            }
        }
    }
}
