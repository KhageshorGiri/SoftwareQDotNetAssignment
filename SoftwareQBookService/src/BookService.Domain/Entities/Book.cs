using System.ComponentModel.DataAnnotations;

namespace BookService.Domain.Entities;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [StringLength(50)]
    public string Author { get; set; }

    [StringLength(30)]
    public string Genre { get; set; }

    [Range(1450, 2100)]
    public int? PublicationYear { get; set; }

    [Required]
    public int CreatedBy { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime CreatedOn { get; set; }

    public int LastUpdatedBy { get; set; }

    [DataType(DataType.Date)]
    public DateTime? LastUpdatedOn { get; set; }
}

