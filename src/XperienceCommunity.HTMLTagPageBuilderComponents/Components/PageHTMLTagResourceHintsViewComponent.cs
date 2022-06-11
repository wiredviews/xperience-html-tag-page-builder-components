namespace XperienceCommunity.HTMLTagPageBuilderComponents.Components;

[ViewComponent(Name = "XPCPageHTMLTagResourceHintsViewComponent")]
public class PageHTMLTagResourceHintsViewComponent : ViewComponent
{
    private readonly IHTMLTagDataStore store;

    public PageHTMLTagResourceHintsViewComponent(IHTMLTagDataStore store) => this.store = store;

    public IViewComponentResult Invoke()
    {
        var tags = store.GetTagDataForResourceHints();

        return View("~/Components/PageHTMLTagResourceHints.cshtml", new PageHTMLTagResourceHintsViewModel(tags));
    }
}

public record PageHTMLTagResourceHintsViewModel(IReadOnlyList<HTMLTagData> Tags);
