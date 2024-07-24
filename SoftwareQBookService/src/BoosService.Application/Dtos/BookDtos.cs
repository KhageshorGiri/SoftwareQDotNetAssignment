using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Dtos;

public record CreateBookDto
{

    [Required]
    [StringLength(100)]
    public string Title { get; set; }

    [Required]
    [StringLength(50)]
    public string Author { get; set; }

    [StringLength(30)]
    public string Genre { get; set; }

    [Range(1450, 2100)]
    public int PublicationYear { get; set; }
}

public record BookListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
}

public record UpdateBookDto
{
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
}