using System;
using System.Collections.Generic;

namespace CSRFInstaClone.Core.Dtos; 

public class PostDto {
	public string Id { get; set; }
	public string UserId { get; set; }
	public string Description { get; set; }
	public DateTime DateTimePosted { get; set; }
	public int Likes { get; set; }
	
	public List<ImageDto> Images { get; set; }
}