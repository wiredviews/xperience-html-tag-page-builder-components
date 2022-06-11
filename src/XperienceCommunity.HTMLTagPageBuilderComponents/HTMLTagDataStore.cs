using Microsoft.AspNetCore.Html;

namespace XperienceCommunity.HTMLTagPageBuilderComponents;

/// <summary>
/// A location within the Page where HTML or content can be rendered
/// </summary>
public enum PageRenderLocation
{
    /// <summary>
    /// Near the top of the &lt;head&gt; tag
    /// </summary>
    HeadStart,

    /// <summary>
    /// Near the bottom of the &lt;head&gt; tag
    /// </summary>
    HeadEnd,

    /// <summary>
    /// Directly after the opening &lt;body&gt; tag
    /// </summary>
    AfterBodyStart,

    /// <summary>
    /// Directly before the closing of the &lt;body&gt; tag
    /// </summary>
    BeforeBodyEnd,

    /// <summary>
    /// Wherever the current code is executing (eg: Widget/Section/View Component)
    /// </summary>
    Current,

    /// <summary>
    /// Fallback for removed / unparsable values
    /// </summary>
    Unknown
}

public interface IHTMLTagDataStore
{
    IReadOnlyList<HTMLTagData> GetTagDataForResourceHints();
    IReadOnlyList<HTMLTagData> GetTagData(PageRenderLocation location);
    void StoreTagData(PageRenderLocation location, string tagBlock);
    void StoreTagData(HTMLTagData tag);
}

public class HTMLTagDataStore : IHTMLTagDataStore
{
    private readonly List<HTMLTagData> store = new();
    private readonly HTMLTagType[] resourceHintTagTypes =
    {
            HTMLTagType.CSSFile,
            HTMLTagType.JavaScriptESModuleFile,
            HTMLTagType.JavaScriptTraditionalFile
        };

    public IReadOnlyList<HTMLTagData> GetTagDataForResourceHints() =>
        store.Where(t => resourceHintTagTypes.Contains(t.Type)).ToList().AsReadOnly();

    public IReadOnlyList<HTMLTagData> GetTagData(PageRenderLocation location) =>
        store.Where(t => t.Location == location).ToList().AsReadOnly();

    public void StoreTagData(PageRenderLocation location, string tagBlock)
    {
        var tag = new HTMLTagData(
            location,
            HTMLTagType.HTMLBlock,
            null,
            string.IsNullOrWhiteSpace(tagBlock) ? null : new HtmlString(tagBlock));

        store.Add(tag);
    }

    public void StoreTagData(HTMLTagData tag) => store.Add(tag);
}
