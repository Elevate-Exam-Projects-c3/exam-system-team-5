using exam_system.Common.Enums;
using exam_system.Common.Results;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Persistence.DataAccess;
using MediatR;

namespace exam_system.Features.Identity.Register.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;

        public RegisterCommandHandler(IGenericRepository<ApplicationUser> userRepository)
        => _userRepository = userRepository;
        public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
            var newUser = new ApplicationUser
            {
                Id = request.UserId,
                FullName = request.FullName.Trim(),
                Email = request.Email.Trim(),
                PasswordHash = passwordHash,
                Role = UserRole.Student,
                AccountStatus = AccountStatus.Pending,
                EmailConfirmed = false
            };
            
            await _userRepository.AddAsync(newUser);
            return Result.Success();
        }        
    }

}
