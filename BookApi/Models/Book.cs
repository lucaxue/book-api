using System.ComponentModel.DataAnnotations;
public class Book
{
    public long? Id { get; set; }

    //model validation
    [Required]
    [StringLength(20)]
    public string Title { get; set; }

    [Required]
    [StringLength(10)]
    //model binding - datatype
    [DataType(DataType.Text)]

    public string Author { get; set; }
}

