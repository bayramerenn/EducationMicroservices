using AutoMapper;
using Education.Services.Catalog.Dtos;
using Education.Services.Catalog.Models;
using Education.Services.Catalog.Settings;
using Education.Shared.Dtos;
using MongoDB.Driver;

namespace Education.Services.Catalog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
           var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Success(categoryDto,200);
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories),200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category = await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            return category == null
                ? Response<CategoryDto>.Fail("Category not found", 404)
                : Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
