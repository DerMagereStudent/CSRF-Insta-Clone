namespace CSRFInstaClone.Core.Dtos; 

public class UserProfileDto {
	public string Id { get; set; }
	public string UserId { get; set; }
	public string DisplayName { get; set; }
	public string Biography { get; set; }
	public int Followers { get; set; }
	public int Following { get; set; }
}