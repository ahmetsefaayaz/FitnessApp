using FitnessApp.Modules.Identity.Entities;
using FitnessApp.Shared.Kernel.Contracts;
using FitnessApp.Shared.Kernel.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Features.Users.Register;

internal sealed class RegisterCommandHandler: IRequestHandler<RegisterCommand, Guid>
{
    private readonly UserManager<User> _userManager;
    private readonly IPublisher _publisher;
    
    public RegisterCommandHandler(
        UserManager<User> userManager,
        IPublisher publisher)
    {
        _userManager = userManager;
        _publisher = publisher;
    }
    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.model.Email);
        if (existingUser is not null) throw new WrongInputException("Mail adresi sistemde kayıtlı");
        if (!request.model.Password.Equals(request.model.ConfirmPassword)) throw new WrongInputException("Şifreler uyuşmadı");
        var user = new User
        {
            UserName = request.model.UserName,
            Email = request.model.Email
        };
        var result = await _userManager.CreateAsync(user, request.model.Password);
        if (!result.Succeeded) throw new WrongInputException($"Kullanıcı oluşturulamadı: {result.Errors.First().Description}");
        await _userManager.AddToRoleAsync(user, "User");
        await _publisher.Publish(new UserCreatedEvent(user.Id));
        return user.Id;
    }
}