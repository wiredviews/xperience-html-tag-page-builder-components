# Xperience HTML Tag Page Builder Components

[![GitHub Actions CI: Build](https://github.com/wiredviews/xperience-html-tag-page-builder-components/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/wiredviews/xperience-html-tag-page-builder-components/actions/workflows/ci.yml)

[![Publish Packages to NuGet](https://github.com/wiredviews/xperience-html-tag-page-builder-components/actions/workflows/publish.yml/badge.svg?branch=main)](https://github.com/wiredviews/xperience-html-tag-page-builder-components/actions/workflows/publish.yml)

[![NuGet Package](https://img.shields.io/nuget/v/XperienceCommunity.HTMLTagPageBuilderComponents.svg)](https://www.nuget.org/packages/XperienceCommunity.HTMLTagPageBuilderComponents)

A collection of ASP.NET Core components for optimizing the rendering and loading of script, style, and image assets on Kentico Xperience 13.0 sites

## Dependencies

This package is compatible with ASP.NET Core 6.0+ applications or libraries integrated with Kentico Xperience 13.0.

## How to Integrate?

1. Install the NuGet package in your ASP.NET Core project (or class library)

   ```bash
   dotnet add package XperienceCommunity.HTMLTagPageBuilderComponents
   ```

2. Create an implementation of `IHTMLTagsRetriever` which should use `IPageRetriever` to get either global HTML tag page content or a specific HTML tag page's content to display on the site:

   ```csharp
   public class : MyHTMLTagsRetreiver : IHTMLTagsRetriever
   {
	   private readonly IPageRetriever pageRetriever;

	   public MyHTMLTagsRetriever(IPageRetriever pageRetriever) => this.pageRetriever = pageRetriever;

       public async Task<GlobalTags> RetrieveGlobalTags(CancellationToken cancellationToken = default)
	   {
		   var pages = await pageRetriever.RetrieveAsync<GlobalTagsContent>();

		   if (!pages.Any())
		   {
			   throw new Exception("...");
		   }

		   return pages.Select(p => new GlobalTags(...)).Single();
	   }

       public async Task<AdvancedHTMLTag> RetrieveAdvancedTag(Guid nodeGUID, CancellationToken cancellationToken = default)
	   {
		   var pages = await pageRetriever.RetrieveAsync<HTMLTagContent>(q => q.WhereEquals(nameof(TreeNode.NodeGUID), nodeGUID));

		   if (!pages.Any())
		   {
			   throw new Exception("...");
		   }

		   return pages.Select(p => new AdvancedHTMLTag(...)).Single();
	   }
   }
   ```

3. Call the `IServiceCollection` extension to register all the library's services:

	```csharp
	services.AddHTMLTagPageBuilderComponents<MyHTMLTagsRetreiver>();
	```

4. Add the HTML Tag View Components to the main areas of your Layout:

	```html
	<!DOCTYPE html>
	<html lang="en">
	<head id="head">
		<vc:page-html-tag-resource-hints />

		<vc:page-html-tags render-location="HeadStart" />

		<!-- ... -->
	
		<vc:page-html-tags render-location="HeadEnd" />	
	</head>

	<body>
		<vc:page-html-tags render-location="AfterBodyStart" />	

		<!-- ... ->

		@RenderBody()

		<!-- ... ->

		<vc:page-html-tags render-location="BeforeBodyEnd" />
	</body>
	</html>
	```

## Usage Examples

### Above-the-fold Image Preload

If you have an image added to a page programmatically, or through a Widget, you can use the `IHTMLTagDataStore` to store an Image file path which will have a resource hint added to the `<head>`:

```csharp
void AddImageLinkPreload(IHTMLTagDataStore store, string imagePath)
{
	string tag = new HTMLTagData(
		PageRenderLocation.None,
		HTMLTagType.ImageFile,
		imagePath);

	store.StoreTagData(tag);
}
```

In the `<head>` where you add `<vc:page-html-tag-resource-hints />`, the following will be rendered:

```
<link rel="preload" as="image" href="/path/to/your/image.jpg" />
```

When using a Widget, you could provide a Widget Property that toggles whether or not this image has a preload hint.

### Global HTML Tags

With a single Page in the content tree that stores global HTML tags (ex: Marketing Tags, Analytics scripts) you can
map the fields of that content to various locations on the page automatically with the `<vc:page-html-tags />` View Component.

If your Page Type has a "Before Body End" field, that would be rendered at the bottom of the Layout using the following:

```html
<vc:page-html-tags render-location="BeforeBodyEnd" />
```

### Advanced HTML Tag Widget

TODO

## Contributions

If you discover a problem, please [open an issue](https://github.com/wiredviews/xperience-html-tag-page-builder-components/issues/new).

If you would like contribute to the code or documentation, please [open a pull request](https://github.com/wiredviews/xperience-html-tag-page-builder-components/compare).

## References

### .NET

### Kentico Xperience
