using Microsoft.AspNetCore.Html;

namespace XperienceCommunity.HTMLTagPageBuilderComponents.Components;

[ViewComponent(Name = "XPCPageHTMLTagsViewComponent")]
public class PageHTMLTagsViewComponent : ViewComponent
{
    private readonly IHTMLTagsRetriever retriever;
    private readonly IHTMLTagDataStore store;

    public PageHTMLTagsViewComponent(IHTMLTagsRetriever retriever, IHTMLTagDataStore store)
    {
        this.retriever = retriever;
        this.store = store;
    }

    public async Task<IViewComponentResult> InvokeAsync(PageRenderLocation renderLocation)
    {
        var globalTags = await retriever.RetrieveGlobalTags();

        string? content = renderLocation switch
        {
            PageRenderLocation.HeadEnd => globalTags.HeadHTML,
            PageRenderLocation.AfterBodyStart => globalTags.AfterBodyStartHTML,
            PageRenderLocation.BeforeBodyEnd => globalTags.BeforeBodyEndHTML,
            PageRenderLocation.HeadStart => null,
            PageRenderLocation.Current => null,
            PageRenderLocation.None => null,
            _ => null
        };

        var tagData = store.GetTagData(renderLocation).ToList();

        if (content is not null)
        {
            tagData.Add(new HTMLTagData(renderLocation, HTMLTagType.HTMLBlock, new HtmlString(content)));
        }

        return View("~/Components/PageHTMLTags.cshtml", new PageHTMLTagsViewModel(tagData));
    }
}

public class PageHTMLTagsViewModel
{
    public PageHTMLTagsViewModel(IReadOnlyList<HTMLTagData> tags) => Tags = tags;

    public IReadOnlyList<HTMLTagData> Tags { get; }
}
