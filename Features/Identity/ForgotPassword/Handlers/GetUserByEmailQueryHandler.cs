using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.ForgotPassword.Dtos;
using exam_system.Features.Identity.ForgotPassword.Queries;
using exam_system.Persistence.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.ForgotPassword.Handlers
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Result<UserResetPasswordDto>>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepostory;
        public GetUserByEmailQueryHandler(IGenericRepository<ApplicationUser> userRepostory)
            => _userRepostory = userRepostory;
        public async Task<Result<UserResetPasswordDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var userDto = await _userRepostory.Get(u => u.Email == request.Email && !u.IsDeleted)
                .AsNoTracking()
                .Select
                 (u => new UserResetPasswordDto(
                     u.Id,
                     u.Email,
                     u.FullName)).FirstOrDefaultAsync(cancellationToken);
            return userDto == null ? Result<UserResetPasswordDto>.Failure("User Not Found Or account is deactivated")
                                   : Result<UserResetPasswordDto>.Success(userDto);
        }
    }
}
