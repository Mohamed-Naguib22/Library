using Library.Application.Interfaces;
using Library.Application.Queries.UserQueries;
using Library.Domain.Const;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.UserHandlers
{
    public class GetSearchHistoryHandler : IRequestHandler<GetSearchHistoryQuery, IEnumerable<SearchQuery>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public GetSearchHistoryHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<IEnumerable<SearchQuery>> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);
            
            var searchQueries = await _unitOfWork.SearchQueries
                .GetAllAsync(q => q.ApplicationUserId == user.Id, q => q.TimSpan, OrderBy.Descending);
            
            return searchQueries;
        }

    }
}
