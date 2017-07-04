using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HelloNordeaConsole
{
    /*
     * The most simple command line util to showcase how to connect to Nordea OB Accounts API with C# code.
     * 4.7.2017 j-sa
     * 
     */
    class Program
    {
        // 1st = web service URL
        // 2nd = client-id
        // 3rd = client-secret
        static void Main(string[] args)
        {
            string webServiceUrl = "";
            string cliendId = "";
            string clientSecret = "";
            string authorization = "Bearer authenticated-user-full-access";
            try
            {
                webServiceUrl = args[0]; // https://api.nordeaopenbanking.com/v1/accounts
                cliendId = args[1]; 
                clientSecret = args[2]; 
            }
            catch (Exception e)
            {
                Console.WriteLine("Correct command line parameters are [0] web service url [1] cliendId [2] clientSecret");
                Console.WriteLine("press enter to quit");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Connecting to:" + webServiceUrl);
            string payload = "";
            try
            {
                var result = WebRequest(ref payload, ref webServiceUrl, ref cliendId, ref clientSecret, ref authorization);
            } catch (Exception e)
            {
                Console.WriteLine("Something went wrong:");
                Console.Write(e.Message);
            }

            Console.WriteLine("Response from the bank:" + webServiceUrl);
            Console.WriteLine(payload);

           
            Console.WriteLine("press enter to quit");
            Console.ReadLine();

        }

        private static int WebRequest(ref string payload, ref string webServiceUrl, ref string cliendId, ref string clientSecret, ref string authorization)
        {

            var webRequest = new HttpRequestMessage(HttpMethod.Get, webServiceUrl);
            webRequest.Headers.Add("X-IBM-Client-ID", cliendId);
            webRequest.Headers.Add("X-IBM-Client-Secret", clientSecret);
            webRequest.Headers.Add("Authorization", authorization);

            // This is the most simple example and the code below should not be used as a reference to make async call properly
            using (HttpClient client = new HttpClient())
            {
                Task<HttpResponseMessage> response = client.SendAsync(webRequest, HttpCompletionOption.ResponseContentRead);
                HttpResponseMessage responsehttp = response.Result;

                payload = responsehttp.Content.ReadAsStringAsync().Result;
            }

            return 1;
        }
    }

}
