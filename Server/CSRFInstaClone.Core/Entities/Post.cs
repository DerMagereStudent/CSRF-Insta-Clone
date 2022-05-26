namespace CSRFInstaClone.Core.Entities; 

public class Post {
	public string Id { get; set; }
	public string UserId { get; set; }
	
	public byte[] Image { get; set; }
	public string Description { get; set; }
}