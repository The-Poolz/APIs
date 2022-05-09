using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.QueryCreator
{
    public abstract class BaseQueryCreator
    {
        public abstract Providers Provider { get; }
        public string ProviderName => Provider.ToString();

        public string CreateCommandQuery(Request request)
        {
            if (!RequestValidator.IsValidRequest(request))
                return null;

            return OnCreateCommandQuery(request);
        }

        protected abstract string OnCreateCommandQuery(Request request);
    }
}
