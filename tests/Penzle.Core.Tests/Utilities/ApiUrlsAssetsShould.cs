// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Utilities;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.ApiUrls))]
public class ApiUrlsAssetsShould
{
    [Fact]
    public void Construct_Api_Url_For_Get_Asset()
    {
        // Arrange
        var assetId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");
        const string Language = "en-US";

        // Act
        var uri = ApiUrls.GetAsset(id: assetId, language: Language);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be($"assets/{assetId}?language={Language}");
    }

    [Fact]
    public void Construct_Api_Url_For_Get_Assets()
    {
        // Arrange
        var assetId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");
        const string Language = "en-US";

        // Act
        var uri = ApiUrls.GetAssets(parentId: assetId, language: Language, keyword: "penzle", tag: "person", mimeType: "image/png", ids: null, page: 1, pageSize: 10, orderBy: "name", direction: "asc");

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("assets?parentId=62f55cff-9cd1-4022-b8cd-751aaa1acbeb&language=en-US&keyword=penzle&tag=person&mimeType=image/png&page=1&pageSize=10&orderBy=name&direction=asc&ids=");
    }

    [Fact]
    public void Construct_Api_Url_For_Add_Asset()
    {
        // Arrange
        var assetId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");
        const string Language = "en-US";

        // Act
        var uri = ApiUrls.AddAsset(folderId: assetId, language: Language);

        // Assert
        uri.OriginalString.Should().Be("assets/folder/62f55cff-9cd1-4022-b8cd-751aaa1acbeb/file?language=en-US");
    }

    [Fact]
    public void Construct_Api_Url_For_Update_Asset()
    {
        // Arrange
        var assetId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");
        const string Language = "en-US";

        // Act
        var uri = ApiUrls.UpdateAsset(id: assetId, language: Language);

        // Assert
        uri.OriginalString.Should().Be("assets/file/62f55cff-9cd1-4022-b8cd-751aaa1acbeb?language=en-US");
    }

    [Fact]
    public void Construct_Api_Url_For_Delete_Assets()
    {
        // Arrange
        var assetIdCollection = new[]
        {
            new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB"), new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEC")
        };

        // Act
        var uri = ApiUrls.DeleteAssets(assetIdCollection);

        // Assert
        uri.OriginalString.Should().Be("assets?ids=62f55cff-9cd1-4022-b8cd-751aaa1acbeb&ids=62f55cff-9cd1-4022-b8cd-751aaa1acbec");
    }

    [Fact]
    public void Construct_Api_Url_For_Delete_Assets_Must_Be_Protected()
    {
        // Arrange
        var assetIdCollection = Array.Empty<Guid>();

        // Act
        var handler = () => ApiUrls.DeleteAssets(assetIdCollection);

        // Assert
        handler.Should().Throw<ArgumentNullException>();
    }
}
