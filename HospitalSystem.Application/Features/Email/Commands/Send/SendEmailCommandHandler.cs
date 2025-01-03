using HospitalSystem.Application.Bases;
using HospitalSystem.Application.Features.Email.Commands.Send;
using HospitalSystem.Application.Interfaces.AutoMapper;
using HospitalSystem.Application.Interfaces.Emails;
using HospitalSystem.Application.Interfaces.RedisCache;
using HospitalSystem.Application.Interfaces.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;

public class SendEmailCommandHandler : BaseHandler, IRequestHandler<SendEmailCommandRequest, Unit>
{
    private readonly IEmailService _emailService;
    private readonly IRedisCacheService _redisService;

    public SendEmailCommandHandler(
        IMyMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        IEmailService emailService,
        IRedisCacheService redisService)
        : base(mapper, unitOfWork, httpContextAccessor)
    {
        _emailService = emailService;
        _redisService = redisService;
    }

    public async Task<Unit> Handle(SendEmailCommandRequest request, CancellationToken cancellationToken)
    {
        var otp = GenerateOtp();

        var redisKey = $"otp:{request.To}";
        await _redisService.SetAsync(redisKey, otp, TimeSpan.FromMinutes(5));

        var emailBody = $"{request.Body}<br/><br/>Your OTP is: <b>{otp}</b>";

        var emailSent = await _emailService.SendEmailAsync(request.To, request.Subject, emailBody);

        if (!emailSent)
        {
            throw new InvalidOperationException("Failed to send the email.");
        }

        return Unit.Value;
    }

    public static string GenerateOtp(int length = 6)
    {
        var random = new Random();
        var otp = string.Empty;

        for (int i = 0; i < length; i++)
        {
            otp += random.Next(0, 10);
        }

        return otp;
    }
}
