using XperienceCommunity.HTMLTagPageBuilderComponents;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HTMLTagPageBuilderComponentsServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services for HTML Tag Page Builder Components to the application.
        /// <typeparamref name="THTMLTagsRetriever"/> is the implementation type of <see cref="IHTMLTagsRetriever"/>
        /// </summary>
        /// <typeparam name="THTMLTagsRetriever"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHTMLTagPageBuilderComponents<THTMLTagsRetriever>(IServiceCollection services)
            where THTMLTagsRetriever : IHTMLTagsRetriever
        {
            services.AddPageBuilderContext();
            services.AddTransient(typeof(IHTMLTagsRetriever), typeof(THTMLTagsRetriever));
            services.AddScoped<IHTMLTagDataStore, HTMLTagDataStore>();

            return services;
        }
    }
}
