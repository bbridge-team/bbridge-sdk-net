using Newtonsoft.Json;

namespace bBridgeAPISDK.Common.Structs
{
	/// <summary>
	/// Error message value structure
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class ErrorMessage
	{
		#region Properties

		/// <summary>
		/// Error message value
		/// </summary>
		[JsonProperty(PropertyName = "error")]
		public string Message { get; set; }

		#endregion
    }
}
