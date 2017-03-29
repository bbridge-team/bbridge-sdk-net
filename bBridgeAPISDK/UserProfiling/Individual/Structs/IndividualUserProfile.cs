using Newtonsoft.Json;

namespace bBridgeAPISDK.UserProfiling.Individual.Structs
{

    /// <summary>
    /// Structure to be returned as a result of individual User Profiling API method
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class IndividualUserProfiling
    {
        /// <summary>
        /// Actual individual user profile
        /// </summary>
        [JsonProperty(PropertyName = "profiling")]
        public IndividualUserProfile Profile { get; set; }
    }

    /// <summary>
    /// Internal structure of Individual User Profiilng
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class IndividualUserProfile
    {
        #region Properties
        /// <summary>
        /// Detected gender
        /// </summary>
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// Detected age group
        /// </summary>
        [JsonProperty(PropertyName = "age_group")]
        public string AgeGroup { get; set; }

        /// <summary>
        /// Detected Education Level
        /// </summary>
        [JsonProperty(PropertyName = "education_level")]
        public string EducationLevel { get; set; }

        /// <summary>
        /// Detected Relationship Status
        /// </summary>
        [JsonProperty(PropertyName = "relationship")]
        public string RelationshipStatus { get; set; }

        /// <summary>
        /// Detected Income Level
        /// </summary>
        [JsonProperty(PropertyName = "income")]
        public string IncomeLevel { get; set; }

        /// <summary>
        /// Detected Occupation Industry
        /// </summary>
        [JsonProperty(PropertyName = "occupation")]
        public string OccupationIndustry { get; set; }

        #endregion
    }
}
