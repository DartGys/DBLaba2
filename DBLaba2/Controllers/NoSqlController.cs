using DBLaba2.NoSqlModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Diagnostics;

[Route("[controller]")]
[ApiController]
public class NoSqlController : ControllerBase
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Inventory> _inventoryCollection;
    private readonly IMongoCollection<SpecialOffer> _specialOfferCollection;


    public NoSqlController(IConfiguration config)
    {
        string connectionString = config.GetConnectionString("NoSqlConnection");
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Bohdan");

        _productCollection = database.GetCollection<Product>("Products");
        _inventoryCollection = database.GetCollection<Inventory>("Inventories");
        _specialOfferCollection = database.GetCollection<SpecialOffer>("SpecialOffers");
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var product = new Product()
        {
            Description = "desc",
            Price = 10,
            ProductName = "name",
            RegistrationDate = new DateOnly(2023, 01, 01),
        };

        var inventory = new Inventory()
        {
            QuantityInStock = 1,
            Product = product
        };

        var specialOffer = new SpecialOffer()
        {
            Discount = 1,
            ValidUntil = new DateOnly(2023, 01, 01),
            Product = product
        };

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < 1000; i++)
        {
            try
            {
                await _productCollection.InsertOneAsync(product);
                await _inventoryCollection.InsertOneAsync(inventory);
                await _specialOfferCollection.InsertOneAsync(specialOffer);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        stopwatch.Stop();
        return Ok($"Час створення та збереження 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < 1000; i++)
        {
            await _productCollection.DeleteOneAsync(Builders<Product>.Filter.Empty);
            await _inventoryCollection.DeleteOneAsync(Builders<Inventory>.Filter.Empty);
            await _specialOfferCollection.DeleteOneAsync(Builders<SpecialOffer>.Filter.Empty);
        }

        stopwatch.Stop();
        return Ok($"Час видалення 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
    }

    [HttpPatch]
    public async Task<IActionResult> Update()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < 1000; i++)
        {
            await _productCollection.UpdateOneAsync(Builders<Product>.Filter.Empty, Builders<Product>.Update.Set("ProductName", "name"));
            await _inventoryCollection.UpdateOneAsync(Builders<Inventory>.Filter.Empty, Builders<Inventory>.Update.Set("QuantityInStock", 1));
            await _specialOfferCollection.UpdateOneAsync(Builders<SpecialOffer>.Filter.Empty, Builders<SpecialOffer>.Update.Set("Discount", 1));
        }

        stopwatch.Stop();
        return Ok($"Час оновлення 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < 1000; i++)
        {
            try
            {
                await _productCollection.FindAsync(Builders<Product>.Filter.Empty);
                await _inventoryCollection.FindAsync(Builders<Inventory>.Filter.Empty);
                await _specialOfferCollection.FindAsync(Builders<SpecialOffer>.Filter.Empty);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        stopwatch.Stop();
        return Ok($"Час отримання 1000 об'єктів: {stopwatch.ElapsedMilliseconds} мілісекунд");
    }
}
