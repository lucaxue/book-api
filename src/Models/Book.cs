using System.ComponentModel.DataAnnotations;
public class Book
{
    public long? Id { get; set; }

    [Required]
    [StringLength(50)]
    [DataType(DataType.Text)]
    public string Title { get; set; }

    [Required]
    [StringLength(30)]
    [DataType(DataType.Text)]

    public string Author { get; set; }
}

