using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuyOrBye.Controllers;
using BuyOrBye.Data;
using BuyOrBye.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BuyOrBye.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {

        private ApplicationDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);

            return context;
        }

    }
}