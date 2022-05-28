namespace CSRFInstaClone.Core.Entities; 

public class Image {
	public string Id { get; set; }
	public byte[] Data { get; set; }
	private string ImageType { get; set; }
}