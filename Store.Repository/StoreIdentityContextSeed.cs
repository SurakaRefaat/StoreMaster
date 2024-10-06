using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntites;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName ="Suraka Almalla",
                    Email = "Suraka.backend@gmail.com",
                    UserName="Suraka",
                    Address = new Address
                    {
                        FirstName="Suraka",
                        LastName = "Almalla",
                        City = "Almidan",
                        State = "Damascus",
                        Street = "9",
                        PostalCode = "10"
                    }
                   
                };
                await userManager.CreateAsync(user, "Password123!");
            }
        }
    }
}
