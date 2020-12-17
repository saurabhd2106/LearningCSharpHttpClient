using LearningHttpClient.Model.JsonModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LearningHttpClient
{
    [TestClass]
    public class UnitTest1
    {
        HttpClient httpClient;

        string endpointUrl = "http://localhost:3030/products";

        Uri uri;

        [TestInitialize]
        public void Initialize()
        {
            httpClient = new HttpClient();

        }

        [TestMethod]
        public void VerifyGetMethod()
        {

            Task<HttpResponseMessage> responseMessage =    httpClient.GetAsync(endpointUrl);

            HttpResponseMessage httpResponseMessage = responseMessage.Result;

            string responseAsString = httpResponseMessage.ToString();

            Console.WriteLine(responseAsString);

            HttpContent httpContent = httpResponseMessage.Content;

            Task<string> responseData = httpContent.ReadAsStringAsync();

            string responseDataAsString = responseData.Result;

            Console.WriteLine(responseDataAsString);
            
            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        }

        [TestMethod]
        public void VerifyGetMethodWithRequestHeaderAsJson()
        {

            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;

            requestHeaders.Add("Accept", "application/json");

            Task<HttpResponseMessage> responseMessage = httpClient.GetAsync(endpointUrl);

            HttpResponseMessage httpResponseMessage = responseMessage.Result;

            HttpContent httpContent = httpResponseMessage.Content;

            Task<string> responseData = httpContent.ReadAsStringAsync();

            string responseDataAsString = responseData.Result;

            Console.WriteLine(responseDataAsString);


            Assert.AreEqual(HttpStatusCode.OK, httpResponseMessage.StatusCode);

        }

       

        [TestMethod]
        public void VerifyGetProductWithEndpointURI()
        {

            uri = new Uri(endpointUrl);

            Task<HttpResponseMessage> responseMessage = httpClient.GetAsync(uri);

            HttpResponseMessage httpResponseMessage = responseMessage.Result;

            Assert.AreEqual(200, (int) httpResponseMessage.StatusCode);
        }

        [TestMethod]
        public void VerifyGetProductWithSendAsync()
        {

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            httpRequestMessage.RequestUri = new Uri(endpointUrl);

            httpRequestMessage.Method = HttpMethod.Get;

            httpRequestMessage.Headers.Add("Accept", "application/json");


            Task<HttpResponseMessage> responseMessage = httpClient.SendAsync(httpRequestMessage);

            HttpResponseMessage httpResponseMessage = responseMessage.Result;

            Assert.AreEqual(200, (int)httpResponseMessage.StatusCode);
        }


        [TestMethod]
        public void VerifyGetMethodWithDeserializeResponse()
        {

            HttpRequestHeaders requestHeaders = httpClient.DefaultRequestHeaders;

            requestHeaders.Add("Accept", "application/json");

            Task<HttpResponseMessage> responseMessage = httpClient.GetAsync(endpointUrl);

            HttpResponseMessage httpResponseMessage = responseMessage.Result;

            HttpContent httpContent = httpResponseMessage.Content;

            Task<string> responseData = httpContent.ReadAsStringAsync();

            ProductRootObject productRootObject =   JsonConvert.DeserializeObject<ProductRootObject>(responseData.Result);

            Console.WriteLine(productRootObject.limit);

            Console.WriteLine(productRootObject.data.Count);

            Console.WriteLine(productRootObject.data[0].id);

        }

        [TestMethod]
        public void VerifyPostProductRequest()
        {
            String mediaType = "application/json";

            String requestPayLoad = "{\r\n  \"name\": \"Samsung Mobile\",\r\n  \"type\": \"Mobile\",\r\n  \"price\": 500,\r\n  \"shipping\": 10,\r\n  \"upc\": \"Mobile\",\r\n  \"description\": \"Best Mobile in the town\",\r\n  \"manufacturer\": \"Samsung\",\r\n  \"model\": \"M31\",\r\n  \"url\": \"string\",\r\n  \"image\": \"string\"\r\n}";

            HttpContent httpContent = new StringContent(requestPayLoad, Encoding.UTF8, mediaType);

            Task<HttpResponseMessage> responseMessage = httpClient.PostAsync(endpointUrl, httpContent);

            HttpResponseMessage httpResponseMessage = responseMessage.Result;

            Assert.AreEqual(201, (int)httpResponseMessage.StatusCode);

        }


        [TestCleanup]
        public void CleanUp()
        {
            httpClient.Dispose();
        }
    }
}
