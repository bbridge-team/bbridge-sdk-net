using System.Collections.Generic;
using Newtonsoft.Json;

namespace bBridgeAPISDK.NLP.Structs
{
	/// <summary>
	/// Structure to be returned as a result of Sentiment Analysis API method
	/// </summary>
	[JsonObject(MemberSerialization.OptIn)]
	public class Sentiments
	{
		/// <summary>
		/// List of detected sentiments in an order of sentences in input
		/// </summary>
		[JsonProperty(PropertyName = "results")]
		public List<double> SentimentsList { get; set; }
	}
}
