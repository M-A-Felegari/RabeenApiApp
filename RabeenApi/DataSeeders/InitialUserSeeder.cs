using DataAccess.Models;
using RabeenApi.Repositories;

namespace RabeenApi.DataSeeders;

public static class InitialUserSeeder
{
    public static async Task SeedInitialUserAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var username = configuration.GetSection("InitialUser:Username").Value!;
        var password = configuration.GetSection("InitialUser:Password").Value!;

        var isOwnerExist = await userRepository.IsAlreadyUsedUsernameAsync(username);
        if (!isOwnerExist)
        {
            var owner = new User
            {
                Username = username,
                Password = password,
                Role = UserRole.Manager
            };
            await userRepository.AddAsync(owner);
        }
    }
}