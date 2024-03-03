using Library.Application.Dtos.RatingDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Commands.RatingCommands
{
    public class RateCommand : IRequest<string>
    {
        public RateDto RateDto { get; }
        public string RefreshToken { get; }
        public RateCommand(RateDto rateDto, string refreshToken)
        {
            RateDto = rateDto;
            RefreshToken = refreshToken;
        }
    }
}
