using Moq;
using Penzle.Core.Http;

namespace Penzle.Net.Tests;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Security))]
public sealed class CredentialStoreShould
{
    private readonly Mock<ICredentialStore<BearerCredentials>> _sut;

    public CredentialStoreShould()
    {
        _sut = new Mock<ICredentialStore<BearerCredentials>>();
    }

    [Fact]
    public async Task Store_Bearer_Credentials_Secured()
    {
        // Arrange
        _sut.Setup(store => store.GetCredentials())
            .ReturnsAsync(() => new BearerCredentials(apiDeliveryKey: It.IsAny<string>(), apiManagementKey: It.IsAny<string>()));

        // Act      
        await _sut.Object.GetCredentials();

        // Arrange
        _sut.Verify(expression: store => store.GetCredentials(), times: Times.Once);
    }

    [Fact]
    public async Task Get_Bearer_Credentials_From_A_Secured_Storage()
    {
        // Arrange
        _sut.Setup(store => store.GetCredentials())
            .ReturnsAsync(() => new BearerCredentials(apiDeliveryKey: It.IsAny<string>(), apiManagementKey: It.IsAny<string>()));

        // Act      
        await _sut.Object.GetCredentials();

        // Arrange
        _sut.Verify(expression: store => store.GetCredentials(), times: Times.Once);
    }
}