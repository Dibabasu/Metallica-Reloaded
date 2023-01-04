using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trades.Application.Common.Exceptions;
using Trades.Application.Common.Interfaces;

namespace Trades.Application.Trades.Queries.GetTradeById
{
    public class GetTradebyIdQuery : IRequest<TradeDTO>
    {
        public Guid TradeId { get; set; }
    }
    public class GetTradebyIdQueryHandler : IRequestHandler<GetTradebyIdQuery, TradeDTO>
    {
        private readonly ITradeApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetTradebyIdQueryHandler(ITradeApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<TradeDTO> Handle(GetTradebyIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Trades
           .Where(l => l.Id == request.TradeId)
           .ProjectTo<TradeDTO>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Trades), request.TradeId);
            }

            return entity; 

        }
    }

}
