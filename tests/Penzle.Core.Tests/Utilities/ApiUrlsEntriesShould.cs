// Copyright (c) 2022 Penzle LLC. All Rights Reserved. Licensed under the MIT license. See License.txt in the project root for license information.

namespace Penzle.Core.Tests.Utilities;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.ApiUrls))]
public class ApiUrlsEntriesShould
{
    [Fact]
    public void Construct_Api_Url_For_Get_Entry()
    {
        // Arrange
        var entryId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");
        const string Language = "en-US";

        // Act
        var uri = ApiUrls.GetEntry(entryId: entryId, language: Language);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("entries/62f55cff-9cd1-4022-b8cd-751aaa1acbeb?language=en-US");
    }

    [Fact]
    public void Construct_Api_Url_For_Get_Entries()
    {
        // Arrange
        var entryId = new Guid("62F55CFF-9CD1-4022-B8CD-751AAA1ACBEB");
        const string Language = "en-US";

        // Act
        
        var queryEntryBuilder = QueryEntryBuilder<ArticleWithSystem>
            .New
            .Where(x => x.System.ParentId == entryId && x.System.Language == Language);
     
        var uri = ApiUrls.GetEntries(template: "article", queryEntryBuilder);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("entries/article/v2?filter[where][and][system.ParentId]=62f55cff-9cd1-4022-b8cd-751aaa1acbeb&filter[where][and][system.Language]=en-US");
    }

    [Fact]
    public void Construct_Api_Url_For_Get_Entry_By_Alias_Url()
    {
        // Arrange
        const string Url = "articles/2021/01/01/first-article";
        const string Language = "en-US";

        // Act
        var uri = ApiUrls.GetEntryByAliasUrl(uri: Url, language: Language);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("entries/url?language=en-US&aliasUrl=articles/2021/01/01/first-article");
    }

    [Fact]
    public void Construct_Api_Url_For_Create_Entry()
    {
        // Act
        var uri = ApiUrls.CreateEntry();

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("entries");
    }

    [Fact]
    public void Construct_Api_Url_For_Update_Entry()
    {
        // Arrange
        var entryId = new Guid("03FA65F2-690E-4DBB-A486-57A28ED980A5");

        // Act
        var uri = ApiUrls.UpdateEntry(entryId);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("entries/03fa65f2-690e-4dbb-a486-57a28ed980a5");
    }

    [Fact]
    public void Construct_Api_Url_For_Delete_Entry()
    {
        // Arrange
        var entryId = new Guid("03FA65F2-690E-4DBB-A486-57A28ED980A5");

        // Act
        var uri = ApiUrls.DeleteEntry(entryId);

        // Assert
        uri.Should().NotBeNull();
        uri.OriginalString.Should().Be("entries/03fa65f2-690e-4dbb-a486-57a28ed980a5");
    }
}
