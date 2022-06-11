using Kentico.Components.Web.Mvc.FormComponents;
using Kentico.Forms.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Html;
using XperienceCommunity.HTMLTagPageBuilderComponents.Components;

[assembly: RegisterWidget(
    AdvancedHTMLTagWidget.IDENTIFIER, typeof(AdvancedHTMLTagWidget),
    name: "Advanced HTML Tag",
    propertiesType: typeof(AdvancedHTMLTagWidgetProperties),
    Description = "Inserts the selected HTML Tag Content as a tag into a specified area of the Page",
    IconClass = "icon-xml-tag")]

namespace XperienceCommunity.HTMLTagPageBuilderComponents.Components;

[ViewComponent(Name = "XPCAdvancedHtmlTagWidget")]
public class AdvancedHTMLTagWidget : ViewComponent
{
    public const string IDENTIFIER = "xperience-community.widget.advanced-html-tag";

    private readonly IHTMLTagsRetriever retriever;
    private readonly IHTMLTagDataStore store;

    public AdvancedHTMLTagWidget(IHTMLTagsRetriever retriever, IHTMLTagDataStore store)
    {
        this.retriever = retriever;
        this.store = store;
    }

    public async Task<IViewComponentResult> InvokeAsync(ComponentViewModel<AdvancedHTMLTagWidgetProperties> vm)
    {
        var nodeGUID = vm.Properties.AdvancedHTMLTagContents
            .Select(p => p.NodeGuid)
            .FirstOrDefault();

        if (nodeGUID == default)
        {
            return View(
                "~/Components/AdvancedHTMLTagError.cshtml",
                new AdvancedHTMLTagWidgetErrorViewModel($"No Advanced HTML Tag Content Page has been selected. Please open the Widget properties and select a valid Page"));
        }

        try
        {
            var advancedTag = await retriever.RetrieveAdvancedTag(nodeGUID);

            var (location, locationName) = vm.Properties.TagLocation switch
            {
                "Widget" => (PageRenderLocation.Current, "to this Widget's location in the Page"),
                "Head" => (PageRenderLocation.HeadEnd, "at the end of the <head>"),
                "AfterBodyStart" => (PageRenderLocation.AfterBodyStart, "after the <body> start"),
                "BeforeBodyEnd" => (PageRenderLocation.BeforeBodyEnd, "before the </body> end"),
                _ => (PageRenderLocation.None, "[unknown]")
            };

            var (block, path, type) = advancedTag switch
            {
                AdvancedHTMLTagFile fileResp => (null, fileResp.FilePath, fileResp.Type),
                AdvancedHTMLTagBlock blockResp => (blockResp.TagHTML, null, blockResp.Type),
                _ => ((string?)null, (string?)null, HTMLTagType.None)
            };

            var tagData = new HTMLTagData(
                location,
                type,
                path,
                string.IsNullOrWhiteSpace(block) ? null : new HtmlString(block));

            store.StoreTagData(tagData);

            return View(
                "~/Components/AdvancedHTMLTag.cshtml",
                new AdvancedHTMLTagWidgetViewModel(tagData, locationName));
        }
        catch (Exception ex)
        {
            return View(
                "~/Components/AdvancedHTMLTagError.cshtml",
                new AdvancedHTMLTagWidgetErrorViewModel($"An error occurred while retrieving the Advanced HTML Tag Content Page.{Environment.NewLine}Please open the Widget properties and select a valid Page.{Environment.NewLine}Error:{Environment.NewLine}{Environment.NewLine}{ex.Message}"));
        }
    }
}

public class AdvancedHTMLTagWidgetProperties : IWidgetProperties
{
    [EditingComponent(
        PageSelector.IDENTIFIER,
        Order = 0,
        Label = "HTML Tag",
        ExplanationText = "The HTML Tag Page that is source of content for this Widget")]
    public IList<PageSelectorItem> AdvancedHTMLTagContents { get; set; } = Array.Empty<PageSelectorItem>();

    [EditingComponent(
        DropDownComponent.IDENTIFIER,
        Order = 1,
        Label = "Tag Location",
        ExplanationText = "The location on the current Page where the HTML Tag should be added")]
    [EditingComponentProperty(
        nameof(DropDownProperties.DataSource),
        "Widget;This Widget's Location\r\nHead\r\nAfterBodyStart;After Body Start\r\nBeforeBodyEnd;Before Body End")]
    public string TagLocation { get; set; } = "";
}

public record AdvancedHTMLTagWidgetViewModel(HTMLTagData TagData, string Description);
public record AdvancedHTMLTagWidgetErrorViewModel(string ErrorMessage);
