using AutoMapper;
using CustomersManagement.Domain;
using CustomersManagement.Models.Resource;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Mapping
{
    public class UserMapping : Profile { 
        public UserMapping(){
            CreateMap<UserResource, Users>();
            CreateMap<Users, UserResource>();
        } 
    }   
}
