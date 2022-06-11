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

## Usage Examples

## Contributions

If you discover a problem, please [open an issue](https://github.com/wiredviews/xperience-html-tag-page-builder-components/issues/new).

If you would like contribute to the code or documentation, please [open a pull request](https://github.com/wiredviews/xperience-html-tag-page-builder-components/compare).

## References

### .NET

### Kentico Xperience
