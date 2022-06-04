namespace CSRFInstaClone.Core.Entities; 

public class Image {
	public string Id { get; set; }
	public string? PostId { get; set; }
	public byte[] Data { get; set; }
	public string ImageType { get; set; }
	
	public Post? Post { get; set; }
}