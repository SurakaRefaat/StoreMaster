using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Repository.Specification.OrderSpecs;
using Store.Service.Services.BasketService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.OrderService.Dtos
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(
                            IBasketService basketService,
                            IUnitOfWork unitOfWork,
                            IMapper mapper)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        } 
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            //Get Basket 
            var basket = await _basketService.GetBasketAsync(input.BasketId);

            if (basket is null)
                throw new Exception("Basket Not Exist");

            // 
            #region Fill Order Item List With Items in the Basket
            var orderItems = new List<OrderItemDto>();

            foreach (var basketItem in basket.BaskeItems)
            {
                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(basketItem.ProductId);

                if (productItem is null)
                    throw new Exception($"Product With Id : {basketItem.ProductId} not Exist");

                var itemOrderd = new ProductItem
                {
                    ProductId = productItem.Id,
                    ProductName = productItem.Name,
                    PictureUrl = productItem.PictureUrl,
                };

                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = basketItem.Quantity,
                    ItemOrdered = itemOrderd
                };
                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);
                orderItems.Add(mappedOrderItem);

            }
            #endregion

            #region Get Delivery Method
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);

            if (deliveryMethod is null)
                throw new Exception("Delivery Method Not Provided");
            #endregion

            #region Calculate Subtotla
            var subtotla = orderItems.Sum(item => item.Quantity * item.Price);


            #endregion

            #region To Do => Payment

            #endregion

            #region Create Order
            var mappedShippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);

            var mappedOredrItems = _mapper.Map<List<OrderItem>>(orderItems);

            var order = new Order
            { 
                DeliveryMethodId = deliveryMethod.Id,
                SippingAddress = mappedShippingAddress,
                //BuyerEmail = input.BuyerEmail,
                BasketId = input.BasketId,
                OrderItems = mappedOredrItems,
                SubTotal = subtotla
                
            };

            await _unitOfWork.Repository<Order , Guid>().AddAsync(order);

            await _unitOfWork.CompleteAsync();

            var mappedrOrder = _mapper.Map<OrderDetailsDto>(order);

            return mappedrOrder;



            #endregion
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync()
        
            => await _unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();

        public async Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail)

        {
            var specs = new OrderWithItemSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order ,Guid>().GetAllWithSpecificationAsync(specs);

            //if (orders is { Count : <= 0 })
            if (!orders.Any())
                throw new Exception("You Dont Have any Order Yet !!");

            var mappedOderes = _mapper.Map<List<OrderDetailsDto>>(orders); 

            return mappedOderes;
        }

        public async Task<OrderDetailsDto> GetOrderByIdAsyns(Guid id)
        {
            var specs = new OrderWithItemSpecification(id);
            var order = await _unitOfWork.Repository<Order, Guid>().GetWithSpecificationByIdAsync(specs);

            //if (orders is { Count : <= 0 })
            if (order is null)
                throw new Exception($"Ther is No Order With ID {id}!!");

            var mappedOdere = _mapper.Map<OrderDetailsDto>(order);

            return mappedOdere;
        }
    }
}
