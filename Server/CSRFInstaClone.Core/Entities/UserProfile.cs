namespace CSRFInstaClone.Core.Entities; 

public class UserProfile {
	public string Id { get; set; }
	public string UserId { get; set; }
	public string Biography { get; set; }
	public byte[] ProfilePicture { get; set; }
}