namespace keepr.Models;

public class Vault
{
    public int Id { get; set; }
    public string CreatorId { get; set; }
    public string Name { get; set; }
    public string Img { get; set; }
    public string Description { get; set; }
    public bool? IsPrivate { get; set; }
    public Profile Creator { get; set; }
    // extra
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}

//which way works?
// public class Creator : Account
// {
//     public int CreatorId { get; set; } // this is a virtual
// }