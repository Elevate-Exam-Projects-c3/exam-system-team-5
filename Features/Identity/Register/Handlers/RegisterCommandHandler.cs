using exam_system.Common.Enums;
using exam_system.Common.Interfaces;
using exam_system.Domain.Entities.Identity;
using exam_system.Features.Identity.Register.Commands;
using exam_system.Persistence.DataAccess;
using MailKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace exam_system.Features.Identity.Register.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IGenericRepository<ApplicationUser> _userRepository;
        private readonly IGenericRepository<EmailVerificationOtp> _otpRepository;
        private readonly IEmailService _mailService;

        public RegisterCommandHandler(
            IGenericRepository<ApplicationUser> userRepository,
            IGenericRepository<EmailVerificationOtp> otpRepository,
            IEmailService mailService)
        {
            
            _userRepository = userRepository;
            _otpRepository = otpRepository;
            _mailService = mailService;
        }
        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //Get the email from the request and convert it to lowercase for consistency
            string email = request.Email.ToLower();
            var emailExist = await _userRepository.Get(u => u.Email.ToLower() == request.Email.ToLower())
            .AnyAsync(cancellationToken);
            if (emailExist)
            {
                throw new InvalidOperationException("Email already exists.");
            }
            // Hash the password using BCrypt with a work factor of 12
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, workFactor: 12);
            var newUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = email,
                PasswordHash = passwordHash,
                Role = UserRole.Student,
                AccountStatus = AccountStatus.Pending,
                EmailConfirmed = false
            };
            // Generate a 6-digit OTP and hash it using BCrypt
            string rawOtp = new Random().Next(100000, 999999).ToString();
            string otpHash = BCrypt.Net.BCrypt.HashPassword(rawOtp, workFactor: 12);
            var verificationOtp = new EmailVerificationOtp
            {
                UserId = newUser.Id,
                Email = newUser.Email,
                OtpHash = otpHash,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                AttemptCount = 0,
                IsUsed = false
            };
            await _userRepository.AddAsync(newUser);
            await _otpRepository.AddAsync(verificationOtp);
            
            string emailBody = $@"
                <h2>Welcome to Exam System</h2>
                <p>Hello {newUser.FullName},</p>
                <p>Your verification code is: <strong>{rawOtp}</strong></p>
                <p>This code will expire in 10 minutes.</p>";
            await _mailService.SendEmailAsync(newUser.Email, "Verify Your Email", emailBody, cancellationToken);
            return newUser.Id;

        }
    }
}
