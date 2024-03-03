using Library.Application.Commands.RatingCommands;
using Library.Application.Interfaces;
using Library.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Handlers.RatingHandlers
{
    public class RateHandler : IRequestHandler<RateCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public RateHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<string> Handle(RateCommand request, CancellationToken cancellationToken)
        {
            var user = await _jwtService.GetUserByRefreshToken(request.RefreshToken);
            var bookId = request.RateDto.BookId;

            if (user == null)
                return "Invalid Token";

            var book = await _unitOfWork.Books.GetByAsync(b => b.Id == bookId);

            if (book == null)
                return "Book is not found" ;

            var rating = await _unitOfWork.Ratings.GetByAsync(r => r.ApplicationUserId == user.Id && r.BookId == bookId);
            
            if (rating == null)
            {
                await _unitOfWork.Ratings.AddAsync(new Rating { Rate = request.RateDto.Rate, ApplicationUser = user, BookId = bookId, TimeSpan = DateTime.Now });
            }
            else
            {
                rating.Rate = request.RateDto.Rate;
                rating.TimeSpan = DateTime.Now;
            }
            
            await _unitOfWork.CompleteAsync();
            return string.Empty;
        }
    }
}
