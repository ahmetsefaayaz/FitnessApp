using FitnessApp.Modules.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessApp.Modules.Identity.Infrastructure;

public class FitnessIdentityDbContext: IdentityDbContext<User, Role, Guid>
{
    public FitnessIdentityDbContext(DbContextOptions<FitnessIdentityDbContext> options):  base(options){}
    
}