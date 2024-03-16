using Newtonsoft.Json;

namespace UtlimaTentativa.Controllers.Service
{
    public class FavoritesService
    {
        private readonly IWebHostEnvironment _environment;

        public FavoritesService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<List<Favorite>> GetFavorites()
        {
            var filePath = Path.Combine(_environment.ContentRootPath, "favorites.json");
            if (!System.IO.File.Exists(filePath))
            {
                return new List<Favorite>();
            }
            var json = await System.IO.File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<List<Favorite>>(json);
        }

        public async Task AddFavorite(string owner, string name)
        {
            var favorites = await GetFavorites();
            favorites.Add(new Favorite { Owner = owner, Name = name });
            var filePath = Path.Combine(_environment.ContentRootPath, "favorites.json");
            await System.IO.File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(favorites));
        }

        public async Task RemoveFavorite(string owner, string name)
        {
            var favorites = await GetFavorites();
            favorites.RemoveAll(f => f.Owner == owner && f.Name == name);
            var filePath = Path.Combine(_environment.ContentRootPath, "favorites.json");
            await System.IO.File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(favorites));
        }
        public class Favorite
        {
            public string Owner { get; set; }
            public string Name { get; set; }
        }
    }
}
