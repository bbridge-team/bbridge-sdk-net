using Newtonsoft.Json;

namespace bBridgeAPISDK.Structs
{
    [JsonObject(MemberSerialization.OptIn)]
    public class IndividualUserProfile
    {
        #region Properties

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "age_group")]
        public string AgeGroup { get; set; }

        [JsonProperty(PropertyName = "education_level")]
        public string EducationLevel { get; set; }

        [JsonProperty(PropertyName = "relationship")]
        public string RelationshipStatus { get; set; }

        [JsonProperty(PropertyName = "income")]
        public string IncomeLevel { get; set; }

        [JsonProperty(PropertyName = "occupation")]
        public bool OccupationIndustry { get; set; }

        #endregion
    }
}