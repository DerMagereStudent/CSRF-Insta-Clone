using System;
using System.Collections.Generic;

using CSRFInstaClone.Core.ValueObjects;

namespace CSRFInstaClone.Core.Exceptions; 

public class InfoException : Exception {
	public List<Info> Errors { get; set; }

	public InfoException(List<Info> errors) {
		this.Errors = errors;
	}
}