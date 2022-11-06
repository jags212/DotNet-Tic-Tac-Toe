using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Data.Model;

namespace TicTacToe.DAL.Interface
{
    public interface IGameRepository
    {
        Task<Game> Add(Game game);

        Task<List<Game>> GetAll();

        Task<Game> GetById(int gameId);
    }
}