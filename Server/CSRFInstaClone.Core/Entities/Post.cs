using System;
using System.Collections.Generic;

namespace CSRFInstaClone.Core.Entities; 

public class Post {
	public string Id { get; set; }
	public string UserId { get; set; }
	public string Description { get; set; }
	public DateTime DateTimePosted { get; set; }
	
	public List<Image> Images { get; set; }
}