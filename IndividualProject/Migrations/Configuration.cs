namespace IndividualProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IndividualProject.AppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(IndividualProject.AppContext context)
        {
            string password = "Admin";
            var salt = Password.GetSalt();
            var hash = Password.Hash(password, salt);
            password = Convert.ToBase64String(hash);
            string salt1 = Convert.ToBase64String(salt);
            User Admin = new User() { UserName = "Admin", Password = password, Role = Role.SuperAdmin ,Salt=salt1};
            context.Users.AddOrUpdate(Admin);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
