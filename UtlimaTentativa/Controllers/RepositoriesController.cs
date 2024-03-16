using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UtlimaTentativa.Controllers.Service;
using UtlimaTentativa.Models;

namespace UtlimaTentativa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        private readonly FavoritesService _favoritesService;
        private readonly IConfiguration _configuration;

        public RepositoriesController(FavoritesService favoritesService, IConfiguration configuration)
        {
            _favoritesService = favoritesService;
            _configuration = configuration;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserRepositories()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync(_configuration["GitHub:UserReposUrl"]);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var content = await response.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<List<Repository>>(content);
            return Ok(repositories);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRepositories([FromQuery] string query)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync($"{_configuration["GitHub:SearchReposUrl"]}?q={query}");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var content = await response.Content.ReadAsStringAsync();
            var searchResult = JsonConvert.DeserializeObject<SearchResult<Repository>>(content);
            return Ok(searchResult.Items);
        }

        [HttpGet("{owner}/{name}")]
        public async Task<IActionResult> GetRepositoryDetails(string owner, string name)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync($"{_configuration["GitHub:RepositoryUrl"]}/{owner}/{name}");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            var content = await response.Content.ReadAsStringAsync();
            var repository = JsonConvert.DeserializeObject<Repository>(content);

            response = await client.GetAsync($"{_configuration["GitHub:RepositoryUrl"]}/{owner}/{name}/contributors");
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            content = await response.Content.ReadAsStringAsync();
            var contributors = JsonConvert.DeserializeObject<List<Contributor>>(content);
            repository.Contributors = contributors;

            return Ok(repository);
        }

        [HttpPost("{owner}/{name}/favorite")]
        public IActionResult FavoriteRepository(string owner, string name)
        {
            _favoritesService.AddFavorite(owner, name);
            return Ok();
        }

        [HttpGet("favorites")]
        public IActionResult GetFavorites()
        {
            var favorites = _favoritesService.GetFavorites();
            return Ok(favorites);
        }
        public class SearchResult<T>
        {
            public int TotalCount { get; set; }
            public IEnumerable<T> Items { get; set; }
        }
    }
}