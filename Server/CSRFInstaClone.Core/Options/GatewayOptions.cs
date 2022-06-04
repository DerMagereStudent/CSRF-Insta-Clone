namespace CSRFInstaClone.Core.Options;

public class GatewayOptions {
	public const string AppSettingsKey = nameof(GatewayOptions);
	public RoutesModel Routes { get; set; }

	public class RoutesModel {
		public string IdentityServiceSignUp { get; set; }
		
		public string IdentityServiceInformationGetUserById { get; set; }
		public string IdentityServiceInformationGetUsersByNameOrEmail { get; set; }
		
		public string IdentityServiceAuthorizeUser { get; set; }
	}
}