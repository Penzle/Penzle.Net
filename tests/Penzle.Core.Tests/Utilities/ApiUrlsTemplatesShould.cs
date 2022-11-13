// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Utilities;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.ApiUrls))]
public class ApiUrlsTemplatesShould
{
    [Fact]
    public void Construct_Api_Url_For_Get_Template()
    {
        // Arrange
        var templateId = new Guid("EA893052-CE53-4549-A4A6-075BB210518B");

        // Act
        var uri = ApiUrls.GetTemplate(templateId);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("templates/ea893052-ce53-4549-a4a6-075bb210518b/schema");
    }

    [Fact]
    public void Construct_Api_Url_For_Get_Template_By_CodeName()
    {
        // Arrange
        const string CodeName = "article";

        // Act
        var uri = ApiUrls.GetTemplateByCodeName(CodeName);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("templates/article/schema");
    }
}
