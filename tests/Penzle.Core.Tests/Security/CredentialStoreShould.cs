namespace Penzle.Core.Tests.Security;

[Trait(name: nameof(TraitDefinitions.Category), value: nameof(TraitDefinitions.Security))]
public sealed class CredentialStoreShould
{
    private readonly Mock<ICredentialStore<BearerCredentials>> _sut;

    public CredentialStoreShould()
    {
        _sut = new Mock<ICredentialStore<BearerCredentials>>();
    }

    [Fact]
    public async Task Get_Bearer_Credentials_From_A_Secured_Storage()
    {
        // Arrange
        _sut.Setup(expression: store => store.GetCredentials())
            .ReturnsAsync(valueFunction: () => new BearerCredentials(apiDeliveryKey: It.IsAny<string>(), apiManagementKey: It.IsAny<string>()));

        // Act      
        await _sut.Object.GetCredentials();

        // Arrange
        _sut.Verify(expression: store => store.GetCredentials(), times: Times.Once);
    }

    [Fact]
    public async Task Get_Bearer_Credentials_From_A_Secured_Storage_With_BearerCredentials()
    {
        // Arrange
        var bearerCredentials = new BearerCredentials(apiDeliveryKey: "1b42f8488e7f49f494694d028d1f918c", apiManagementKey: "b09b93dc0a944399a6c1c9b24308773f");
        var credentialStore = new InMemoryCredentialStore(credentials: bearerCredentials);

        // Act
        var credentials = await credentialStore.GetCredentials();

        // Arrange
        credentials.AuthenticationType.Should().Be(expected: AuthenticationType.Bearer);
        credentials.ApiDeliveryKey.Should().Be(expected: bearerCredentials.ApiDeliveryKey);
        credentials.ApiManagementKey.Should().Be(expected: bearerCredentials.ApiManagementKey);
    }
}
