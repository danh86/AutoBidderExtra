namespace FUTAutoBuyer.Params
{
    public abstract class SearchParamsBase<TValue>
    {
        public string Description { get; set; }

        public TValue Value { get; set; }

        public TValue Parent { get; set; }
    }
}
