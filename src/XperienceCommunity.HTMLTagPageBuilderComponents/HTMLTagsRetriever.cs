namespace XperienceCommunity.HTMLTagPageBuilderComponents
{
    public interface IHTMLTagsRetriever
    {
        Task<GlobalTags> RetrieveGlobalTags(CancellationToken cancellationToken = default);
        Task<AdvancedHTMLTag> RetrieveAdvancedTag(Guid nodeGUID, CancellationToken cancellationToken = default);
    }
}
