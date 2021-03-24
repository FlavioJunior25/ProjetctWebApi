using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class CoinModel
    {
        [Required]
        [StringLength(3)]
        public string Moeda { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data_inicio { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data_fim { get; set; }

    }
}