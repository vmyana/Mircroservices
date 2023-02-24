using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(ILogger<DeleteOrderCommandHandler> logger, 
            IMapper mapper, IOrderRepository orderRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDelete = await _orderRepository.GetByIdAsync(request.Id);
            if (orderDelete == null)
            {
                _logger.LogError($"{request.Id} is not available");
            }

            await _orderRepository.DeleteAsync(orderDelete);
            _logger.LogInformation($"{request.Id} deleted successfully");
            return Unit.Value;
        }
    }
}
