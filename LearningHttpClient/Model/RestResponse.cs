using System;
using System.Collections.Generic;
using System.Text;

namespace LearningHttpClient.Model
{
   public class RestResponse
    {
        private int statusCode;

        private string responseData;

        public int StatusCode
        {
            get
            {
                return statusCode;
            }
        }

        public string ResponseData
        {
            get
            {
                return responseData;
            }
        }

        public RestResponse(int statusCode, string responseData)
        {

            this.statusCode = statusCode;

            this.responseData = responseData;

        }
    }
}
