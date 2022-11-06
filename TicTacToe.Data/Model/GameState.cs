using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicTacToe.Data.Model
{
    public class GameState
    {
        [Key]
        public int GameStateId { get; set; }

        [Required]
        public string TL { get; set; }

        [Required]
        public string TR { get; set; }

        [Required]
        public string TM { get; set; }

        [Required]
        public string ML { get; set; }

        [Required]
        public string MR { get; set; }

        [Required]
        public string MM { get; set; }

        [Required]
        public string BL { get; set; }

        [Required]
        public string BM { get; set; }

        [Required]
        public string BR { get; set; }
    }
}