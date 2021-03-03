using System.ComponentModel.DataAnnotations;
public class Book
{
    public long? Id { get; set; }

    [Required]
    [StringLength(2)]
    public string Title { get; set; }

    [Required]
    [StringLength(2)]
    public string Author { get; set; }
}

