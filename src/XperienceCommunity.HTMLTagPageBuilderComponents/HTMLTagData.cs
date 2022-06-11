using Microsoft.AspNetCore.Html;

namespace XperienceCommunity.HTMLTagPageBuilderComponents
{
    public enum HTMLTagType
    {
        CSSBlock,
        CSSFile,
        JavaScriptTraditionalBlock,
        JavaScriptESModuleBlock,
        JavaScriptTraditionalFile,
        JavaScriptESModuleFile,
        HTMLBlock,
        Image,
        Unknown
    }


    public record HTMLTagData(PageRenderLocation Location, HTMLTagType Type, string? FilePath, HtmlString? Block);

    public record GlobalTags(string? HeadHTML, string? AfterBodyStartHTML, string? BeforeBodyEndHTML);

    public record AdvancedHTMLTag(HTMLTagType Type);
    public record AdvancedHTMLTagBlock : AdvancedHTMLTag
    {
        public AdvancedHTMLTagBlock(HTMLTagType type, string? tagHTML) : base(type) => TagHTML = string.IsNullOrWhiteSpace(tagHTML) ? null : tagHTML;

        public string? TagHTML { get; }
    }

    public record AdvancedHTMLTagFile : AdvancedHTMLTag
    {
        public AdvancedHTMLTagFile(HTMLTagType type, string? filePath) : base(type) => FilePath = string.IsNullOrWhiteSpace(filePath) ? null : filePath;

        public string? FilePath { get; }
    }
}
