using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Models
{

    //T is the type of data that we want to return 
    //This is a service response for errors in your code 
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true; 

        public string Message { get; set; } = string.Empty; 
    }
}