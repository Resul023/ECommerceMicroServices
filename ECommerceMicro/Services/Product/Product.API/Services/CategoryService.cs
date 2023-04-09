using AutoMapper;
using MongoDB.Driver;
using Product.API.Dtos.Product;
using Product.API.Settings;
using Shared.Dtos;
using Shared.Messages;

namespace Product.API.Services;
public class CategoryService : ICategoryService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Category> _categoriesCollection;

    public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
    {
        this._mapper = mapper;

        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        this._categoriesCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
    }


    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoriesCollection.Find(category => true).ToListAsync();

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories),200);
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await _categoriesCollection.InsertOneAsync(category);
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoriesCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
        if (category == null)
        {
            return Response<CategoryDto>.Fail("Category not found",404);
        }

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200); 
    }

    public async Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
    {
        var updateCategory = _mapper.Map<Category>(categoryUpdateDto);

        var result = await _categoriesCollection.FindOneAndReplaceAsync(x => x.Id == categoryUpdateDto.Id, updateCategory);

        if (result == null)
        {
            return Response<NoContent>.Fail("Category not found", 404);
        }

        return Response<NoContent>.Success(204);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var result = await _categoriesCollection.DeleteOneAsync(x => x.Id == id);

        if (result.DeletedCount > 0)
        {
            return Response<NoContent>.Success(204);
        }
        else
        {
            return Response<NoContent>.Fail("Category not found", 404);
        }
    }

}
