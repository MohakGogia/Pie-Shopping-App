namespace ShoppingAppTests.TagHelpers;

using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using ShoppingApp.TagHelpers;

public class EmailTagHelperTests
{
    [Fact]
    public void GeneratesEmailLink()
    {
        var emailTagHelper = new EmailTagHelper() { Address = "test@test.com", Content = "Email" };

        var tagHelperContext = new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(), string.Empty);

        var content = new Mock<TagHelperContent>();

        var tagHelperOutput = new TagHelperOutput("a",
            new TagHelperAttributeList(),
            (cache, encoder) => Task.FromResult(content.Object));

        // Act
        emailTagHelper.Process(tagHelperContext, tagHelperOutput);

        Assert.Equal("Email", tagHelperOutput.Content.GetContent());
        Assert.Equal("a", tagHelperOutput.TagName);
        Assert.Equal("mailto:test@test.com", tagHelperOutput.Attributes[0].Value);
    }

}
