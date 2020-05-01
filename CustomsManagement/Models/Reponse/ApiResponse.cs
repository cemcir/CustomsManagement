using CustomersManagement.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Models.Reponse
{
    public class ApiResponse<T> where T : class
    {

        public bool Status { get; set; }

        public T Entry { get; set; }

        public ErrorMessageCode ErrorCode { get; set; }

        public ModelStateDictionary ModelState { get; set; }

    }
}
