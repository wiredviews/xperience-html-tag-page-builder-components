namespace XperienceCommunity.HTMLTagPageBuilderComponents.Components
{
    [ViewComponent(Name = "XPC-HTMLTagViewComponent")]
    public class HTMLTagViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(HTMLTagData tagData) =>
            View("~/Components/HTMLTag.cshtml", tagData);
    }
}
