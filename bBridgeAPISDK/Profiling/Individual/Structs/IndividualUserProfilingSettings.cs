using bBridgeAPISDK.Common.Enums;

namespace bBridgeAPISDK.Profiling.Individual.Structs
{
    public class IndividualUserProfilingSettings
    {
        #region Properties

        /// <summary>
        /// Defines if the Gender needs to be predicted
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// Defines if Age Group attribute needs to be predicted
        /// </summary>
        public bool AgeGroup { get; set; }

        /// <summary>
        /// Defines if Education Level attribute needs to be predicted
        /// </summary>
        public bool EducationLevel { get; set; }

        /// <summary>
        /// Defines if Relationship Status attribute needs to be predicted
        /// </summary>
        public bool RelationshipStatus { get; set; }

        /// <summary>
        /// Defines if Income Level attribute needs to be predicted
        /// </summary>
        public bool IncomeLevel { get; set; }

        /// <summary>
        /// Defines if Occupation attribute needs to be predicted
        /// </summary>
        public bool OccupationIndustry { get; set; }

        /// <summary>
        /// Defines the language of individual user profiler.
        /// Currently, only English language is supported
        /// </summary>
        public Language Language { get; set; }

        #endregion

        #region Methods

        internal string GenerateURLAttributeString()
        {
            //Language is always English by now, but we plan to extend API by Chinese
            return "lang=en" +
                   (Gender ? "&attr=gender" : string.Empty) +
                   (AgeGroup ? "&attr=age_group" : string.Empty) +
                   (EducationLevel ? "&attr=education_level" : string.Empty) +
                   (RelationshipStatus ? "&attr=relationship" : string.Empty) +
                   (OccupationIndustry ? "&attr=occupation" : string.Empty) +
                   (IncomeLevel ? "&attr=income" : string.Empty);
        }

        #endregion
    }
}
