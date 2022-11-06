using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Data.DataContext;
using TicTacToe.Data.Model;
using TicTacToe.WebApi;
using Xunit;

namespace TicTacToe.Tests
{
    public class UnitTest1
    {
        private TestServer _server;

        public HttpClient Client { get; private set; }

        public UnitTest1()
        {
            SetUpClient();
        }

        //Adds Game (Positive test case)
        [Fact]
        public async Task Test1()
        {
            var game = await SeedData();
            var response = await Client.PostAsync($"/api/Game", new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json"));
            response.StatusCode.Equals((StatusCodes.Status201Created));
        }

        //Adds Game(Negative test case)
        [Fact]
        public async Task Test2()
        {
            var gameState = new GameState
            {
            };
            var game = new Game
            {
                GameState = gameState
            };
            var response = await Client.PostAsync($"/api/Game", new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json"));
            response.StatusCode.Equals((StatusCodes.Status400BadRequest));
        }

        //Get Games by Id
        [Fact]
        public async Task Test3()
        {
            var game = await SeedData();
            var response = await Client.GetAsync($"/api/Game/1");
            response.StatusCode.Equals(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task Test4()
        {
            var response = await Client.GetAsync($"/api/Game");
            response.StatusCode.Equals(StatusCodes.Status400BadRequest);
        }

        private async Task<Game> SeedData()
        {
            var createForm0 = await GenerateGameCreateForm("X");
            return createForm0;
        }

        public async Task<Game> GenerateGameCreateForm(string gameResult)
        {
            var gamestate = new GameState
            {
                TL = "X",
                TM = "0",
                TR = "X"
            };
            return new Game
            {
                GameId = 1,
                GameState = gamestate,
                GameResult = gameResult
            };
        }

        private void SetUpClient()
        {
            var builder = new WebHostBuilder().
                UseStartup<Startup>().
                ConfigureServices(service =>
                {
                    var context = new ApplicationDbContext(new
                        DbContextOptionsBuilder<ApplicationDbContext>().
                        UseSqlServer("Data Source=ITT-ABHIJEETS\\SQLEXPRESS;Database=TicTacToeDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=yes;TrustServerCertificate=True").EnableSensitiveDataLogging().Options);
                    service.RemoveAll(typeof(ApplicationDbContext));
                    service.AddSingleton(context);
                    context.Database.OpenConnection();
                    context.Database.EnsureCreated();

                    context.SaveChanges();
                    // Clear local context cache
                    foreach (var entity in context.ChangeTracker.Entries().ToList())
                    {
                        entity.State = EntityState.Detached;
                    }
                });
            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }
    }
}