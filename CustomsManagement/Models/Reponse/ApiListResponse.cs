using CustomersManagement.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Models.Reponse
{
    public class ApiListResponse<T> where T : class
    {
        public bool Status { get; set; }

        public List<T> Entry { get; set; }

        public ErrorMessageCode ErrorMessage { get; set; }

        public ModelStateDictionary ModelState { get; set; }
    }
}
