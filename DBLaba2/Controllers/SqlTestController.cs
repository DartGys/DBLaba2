using DBLaba2.SqlModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DBLaba2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SqlTestController : ControllerBase
    {
        private readonly Dblaba2Context _context;

        public SqlTestController(Dblaba2Context context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var product = new Product()
            {
                Description = "desc",
                Price = 10,
                ProductName = "name",
                RegistrationDate = new DateOnly(2023,01,01),
            };

            var inventory = new Inventory()
            {
                QuantityInStock = 1,
                Product = product
            };

            var specialOffer = new SpecialOffer()
            {
                Discount = 1,
                ValidUntil = new DateOnly(2024, 01, 01),
                Product = product
            };

            for (int i = 0; i < 1000; i++)
            {
                _context.Products.Add(product);
                _context.Inventories.Add(inventory);
                _context.SpecialOffers.Add(specialOffer);
            }

            await _context.SaveChangesAsync();

            stopwatch.Stop();
            return Ok($"Час створення та збереження 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var product = await _context.Products.FindAsync(0);
            var inventory = await _context.Inventories.FindAsync(0);
            var specialOffer = await _context.SpecialOffers.FindAsync(0);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                _context.Inventories.Remove(inventory);
                _context.SpecialOffers.Remove(specialOffer);
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();

            stopwatch.Stop();
            return Ok($"Час видалення 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
        }

        [HttpPatch]
        public async Task<IActionResult> Update()
        {
            var product = await _context.Products.FindAsync(0);
            var inventory = await _context.Inventories.FindAsync(0);
            var specialOffer = await _context.SpecialOffers.FindAsync(0);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                _context.Inventories.Update(inventory);
                _context.SpecialOffers.Update(specialOffer);
                _context.Products.Update(product);
            }

            await _context.SaveChangesAsync();

            stopwatch.Stop();
            return Ok($"Час видалення 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                await _context.Products.FindAsync(0);
                await _context.SpecialOffers.FindAsync(0);
                await _context.Inventories.FindAsync(0);
            }

            stopwatch.Stop();
            return Ok($"Час отримання 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
        }
    }
}
