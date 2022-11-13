// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Utilities;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.ApiUrls))]
public class ApiUrlsFormsShould
{
    [Fact]
    public void Construct_Api_Url_For_Get_Form()
    {
        // Arrange
        var formId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");
        const string Language = "en-US";

        // Act
        var uri = ApiUrls.GetForm(formId: formId, language: Language);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("forms/62f55cff-9cd1-4022-b8cd-751aaa1acbeb?language=en-US");
    }

    [Fact]
    public void Construct_Api_Url_For_Create_Form()
    {
        // Act
        var uri = ApiUrls.CreateForm();

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("forms");
    }

    [Fact]
    public void Construct_Api_Url_For_Update_Form()
    {
        // Arrange
        var formId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");

        // Act
        var uri = ApiUrls.UpdateForm(formId: formId);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("forms/62f55cff-9cd1-4022-b8cd-751aaa1acbeb");
    }

    [Fact]
    public void Construct_Api_Url_For_Delete_Form()
    {
        // Arrange
        var formId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");

        // Act
        var uri = ApiUrls.DeleteForm(formId: formId);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("forms/62f55cff-9cd1-4022-b8cd-751aaa1acbeb");
    }
}
