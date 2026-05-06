using System.Runtime.InteropServices.JavaScript;
using FitnessApp.Shared.Kernel.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Entities;

public class User:IdentityUser<Guid>
{
    public bool IsDeleted { get; private set; } = false;
    public DateTime? CreatedAt { get; private set; } = DateTime.UtcNow;
    public void SoftDelete()
    {
        if(IsDeleted) throw new WrongInputException("User is already deleted");
        IsDeleted = true;
    }

    public void Restore()
    {
        if(!IsDeleted) throw new WrongInputException("User is not deleted");
        IsDeleted = false;
    }
    
}