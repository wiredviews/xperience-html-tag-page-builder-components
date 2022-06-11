namespace XperienceCommunity.HTMLTagPageBuilderComponents.Components;

[ViewComponent(Name = "XPC-PageHTMLTagResourceHints")]
public class HTMLResourceHintTagViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(HTMLTagData tagData) =>
        View("~/Components/HTMLResourceHintTag.cshtml", tagData);
}
