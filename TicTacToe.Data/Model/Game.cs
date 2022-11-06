using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TicTacToe.Data.Model
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [StringLength(100)]
        public string PlayerX { get; set; }

        [StringLength(100)]
        public string Player0 { get; set; }

        [StringLength(100)]
        public string GameResult { get; set; }

        [Required]
        public string Date { get; set; }

        //[ForeignKey("GameStateId")]
        public int GameStateId { get; set; }

        //[Display(Name = "GameState")]
        public GameState GameState { get; set; }
    }
}