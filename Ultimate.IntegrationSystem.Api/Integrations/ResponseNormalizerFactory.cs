using Ultimate.IntegrationSystem.Api.Integrations.Muqeem;

namespace Ultimate.IntegrationSystem.Api.Integrations
{
    public static class ResponseNormalizerFactory
    {
        public static IResponseNormalizer Get(string platformKey)
        {
            switch (platformKey)
            {
                case "Muqeem":
                    return new MuqeemResponseNormalizer();   // موجود عندك
                case "OnyxERP":
                    return new MuqeemResponseNormalizer();     // لو عندك ERP ثاني
                // 🔑 هنا تقدر تضيف أي منصة أخرى بسهولة
                default:
                    return new DefaultResponseNormalizer();
            }
        }
    }
}
