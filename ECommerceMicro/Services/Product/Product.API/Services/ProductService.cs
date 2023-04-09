using AutoMapper;
using Mass=MassTransit;
using MongoDB.Driver;
using Product.API.Dtos.Product;
using Product.API.Settings;
using Shared.Dtos;
using Shared.Messages;

namespace Product.API.Services;
public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMongoCollection<Models.Product> _productCollection;
    private readonly Mass.IPublishEndpoint _publishEndpoint;

    public ProductService(
        IMapper mapper, 
        IDatabaseSettings databaseSettings, 
        Mass.IPublishEndpoint publishEndpoint)
    {
        this._mapper = mapper;
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);

        this._categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        this._productCollection = database.GetCollection<Models.Product>(databaseSettings.ProductCollectionName);
        this._publishEndpoint = publishEndpoint;
    }

    public async Task<Response<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productCollection.Find(product => true).ToListAsync();
        if (products.Any())
        {
            foreach (var product in products)
            {
                product.Category = await _categoryCollection.Find<Category>(x=>x.Id == product.CategoryId).FirstAsync();
            }
        }
        else
        {
            products = new List<Models.Product>();
        }
        return Response<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), 200);
    }

    public async Task<Response<ProductDto>> GetByIdAsync(string id)
    {
        var product = await _productCollection.Find<Models.Product>(x => x.Id == id).FirstOrDefaultAsync();

        if (product == null)
        {
            return Response<ProductDto>.Fail("Product not found", 404);
        }
        product.Category = await _categoryCollection.Find<Category>(x => x.Id == product.CategoryId).FirstAsync();

        return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), 200);
    }

    public async Task<Response<ProductDto>> CreateAsync(ProductCreateDto courseCreateDto)
    {
        var newProduct = _mapper.Map<Models.Product>(courseCreateDto);

        newProduct.CreatedAt = DateTime.Now;
        await _productCollection.InsertOneAsync(newProduct);

        return Response<ProductDto>.Success(_mapper.Map<ProductDto>(newProduct), 200);
    }
    public async Task<Response<NoContent>> UpdateAsync(ProductUpdateDto productUpdateDto)
    {
        var updateProduct = _mapper.Map<Models.Product>(productUpdateDto);

        var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == productUpdateDto.Id, updateProduct);

        if (result == null)
        {
            return Response<NoContent>.Fail("Product not found", 404);
        }

        await _publishEndpoint.Publish<ProductNameChangedEvent>(new ProductNameChangedEvent 
        { 
            ProductId = productUpdateDto.Id,
            UpdatedName=productUpdateDto.Name 
        });

        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _productCollection.DeleteOneAsync(x => x.Id == id);

        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(204);
        }
        else
        {
            return Response<NoContent>.Fail("Course not found", 404);
        }
    }

}
