using AutoMapper;
using AutoMapper.Execution;
using Microsoft.Extensions.Configuration;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderItemPicyureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemPicyureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {

            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
                return $"{_configuration["BaseUrl"]}/{source.ItemOrdered.PictureUrl}";

            return null;
        }
    }
}
