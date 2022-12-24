using BookStoreAPIClientCMS.DataAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace BookStoreAPIClientCMS.Models
{
    public class RestClient
    {
        public string BaseUrl { get; set; }
        public string EndPoint { get; set; }

        public ResponseMessage RestRequestAll()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            HttpResponseMessage response = client.GetAsync(EndPoint).Result;
            ResponseMessage responseData;

            if (response.IsSuccessStatusCode)
            {
                responseData = JsonConvert.DeserializeObject<ResponseMessage>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                responseData =  new ResponseMessage(0, "Err: " + (int)response.StatusCode + " (" + response.ReasonPhrase + ")");
            }

            return responseData;
        }

        public ResponseMessage InsertData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            HttpContent c = new StringContent("", Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(EndPoint, c).Result;
            ResponseMessage responseData;

            if (response.IsSuccessStatusCode)
            {
                responseData = JsonConvert.DeserializeObject<ResponseMessage>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                responseData = new ResponseMessage(0, "Insert Failed");
            }

            return responseData;
        }

        public ResponseMessage InsertData(Object obj)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            string postBody = JsonConvert.SerializeObject(obj);
            HttpContent c = new StringContent(postBody, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(EndPoint, c).Result;
            ResponseMessage responseData;

            if (response.IsSuccessStatusCode)
            {
                responseData = JsonConvert.DeserializeObject<ResponseMessage>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                responseData = new ResponseMessage(0, "Insert Failed");
            }

            return responseData;
        }

        public ResponseMessage UpdateData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            HttpContent c = new StringContent("", Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync(EndPoint, c).Result;
            ResponseMessage responseData;

            if (response.IsSuccessStatusCode)
            {
                responseData = JsonConvert.DeserializeObject<ResponseMessage>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                responseData = new ResponseMessage(0, "Update Failed");
            }

            return responseData;
        }
        public ResponseMessage UpdateData(Object obj)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            string postBody = JsonConvert.SerializeObject(obj);
            HttpContent c = new StringContent(postBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(EndPoint, c).Result;
            ResponseMessage responseData;

            if (response.IsSuccessStatusCode)
            {
                responseData = JsonConvert.DeserializeObject<ResponseMessage>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                responseData = new ResponseMessage(0, "Update Failed");
            }

            return responseData;
        }

        public ResponseMessage DeleteData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);

            HttpResponseMessage response = client.DeleteAsync(EndPoint).Result;
            ResponseMessage responseData;

            if (response.IsSuccessStatusCode)
            {
                responseData = JsonConvert.DeserializeObject<ResponseMessage>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                responseData = new ResponseMessage(0, "Delete Failed");
            }

            return responseData;
        }
    }
}