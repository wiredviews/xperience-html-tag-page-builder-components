namespace XperienceCommunity.HTMLTagPageBuilderComponents.Components;

[ViewComponent(Name = "XPC-HTMLTagPreviewViewComponent")]
public class HTMLTagPreviewViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(HTMLTagData tagData) =>
        View("~/Components/HTMLTags/HTMLTagPreview.cshtml", tagData);
}
