namespace CSRFInstaClone.Core.Entities; 

public class Image {
	public string Id { get; set; }
	public byte[] Data { get; set; }
	public string ImageType { get; set; }
}