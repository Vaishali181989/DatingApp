using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;
[Table("Photos")]
public class  Photos
{
    public int Id { get; set; }
    public required string URL { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    //Navigation Properties
//Required one to many relationship
    public int AppUserId { get; set; } 
    public AppUsers AppUser { get; set; } =null!;
}