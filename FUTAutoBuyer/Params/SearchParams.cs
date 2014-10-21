namespace FUTAutoBuyer.Params
{
    public abstract class SearchParams
    {
        public string Type { get; set; }
        
        public uint Page { get; set; }

        public uint League { get; set; }

        public string Position { get; set; }

        public uint Nation { get; set; }

        public ChemistryStyle ChemistryStyle { get; set; }

        public uint Team { get; set; }

        public uint MinBuy { get; set; }

        public uint MaxBuy { get; set; }

        public uint MinBid { get; set; }

        public uint MaxBid { get; set; }

        public Level Level { get; set; }

        public string DevelopmentType { get; set; }

        public string TrainingType { get; set; }

        public string ClubInfoType { get; set; }

        public string StaffType { get; set; }

        public uint PlayerDataID { get; set; }

        protected SearchParams(ResourceType resourceType)
        {
            Type = resourceType.ToString().ToLower();
            Page = 1;
        }

        internal abstract string BuildUriString(ref string uriString);
    }
}
