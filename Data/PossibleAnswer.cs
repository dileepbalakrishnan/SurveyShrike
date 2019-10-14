using System;
using System.ComponentModel.DataAnnotations;
namespace NewBlazor.Data
{
    public class PossibleAnswer
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(200)]
        public string Answer { get; set; }
    }
}
