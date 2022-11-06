using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.DAL.Interface;
using TicTacToe.Data.DataContext;
using TicTacToe.Data.Model;

namespace TicTacToe.DAL.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _context;

        public GameRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Game> Add(Game game)
        {
            var newGame = await _context.AddAsync(game);
            _context.SaveChanges();
            return newGame.Entity;
        }

        public async Task<List<Game>> GetAll()
        {
            var games = await _context.Game.Include(g => g.GameState).ToListAsync();
            return games;
        }

        public async Task<Game> GetById(int gameId)
        {
            var game = await _context.Game.Where(g => g.GameId == gameId).Include(g => g.GameState).FirstOrDefaultAsync();
            return game;
        }
    }
}