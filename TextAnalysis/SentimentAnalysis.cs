using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class for analysing the sentiment of some text using Microsoft cognitive services' text analytics API
/// </summary>
namespace TextAnalysis {

    public class SentimentAnalysis {

        /// <summary>
        /// Gets the sentiment of given text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// A JSON Object containing an array of documents each with an ID number and score between 0 and 1
        /// </returns>
        public async Task<string> getSentiment (string text) {
            /*
             * get the API key from the config file.
             * keeps the key private when using GitHub to manage code
             */
            string APIKey = ConfigurationManager.AppSettings["MicrosoftTextAnalyticsKey"];

            //based on https://docs.microsoft.com/en-us/azure/cognitive-services/cognitive-services-text-analytics-quick-start
            //declare constant base URL string
            const string URL = "https://westus.api.cognitive.microsoft.com";
            //instantiate the HTTP client object
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            //add headers for request
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", APIKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            /*
             * json string
             * escaped double quotes
             * given text converted to JSON format and dynamically escaped using Newtonsoft.Json library
             */
            string jsonRequestData = ("{\"documents\":[{\"id\": \"1\",\"text\": " + JsonConvert.SerializeObject(text) + "}]}");

            //convert the json data to a byte array
            byte[] byteData = Encoding.UTF8.GetBytes(jsonRequestData);

            //add the byte array to a ByteArrayContent object so it can be sent through POST
            ByteArrayContent content = new ByteArrayContent(byteData);
            //add content type header to data
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //send the POST request and wait for a response
            HttpResponseMessage response = await client.PostAsync("/text/analytics/v2.0/sentiment", content);

            //read the response content and return to caller
            return await response.Content.ReadAsStringAsync();
        }
    }
}