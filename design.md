# RBAC API Design

> 1. Discuss Constructor vs Static call.
> 2. Discuss Enum for credential type vs derived classes as input.
> 3. Discuss naming

## Service Client

```csharp
/// <summary>
    /// Create ServiceClient using AAD or Connection String token credentials and the specified Transport Type.
    /// </summary>
    /// <param name="hostName">IoT hub host name.</param>
    /// <param name="credential">Credentials to authenticate with IoT Hub.</param>
    /// <param name="tokenType">The token type returned by the credential implementation. Possible values are <see cref="TokenType"/></param>
    /// <param name="transportType">Specifies whether Amqp or Amqp_WebSocket_Only transport is used.</param>
    /// <param name="transportSettings">Specifies the AMQP and HTTP proxy settings for Service Client.</param>
    /// <param name="options">The options that allow configuration of the service client instance during initialization.</param>
    /// <returns>An instance of <see cref="ServiceClient"/>.</returns>
public static ServiceClient Create(
    string hostName,
    TokenCredential credential,
    TokenType tokenType // Option 2 - TokenCredentialType
    TransportType transportType,
    ServiceClientTransportSettings transportSettings = default,
    ServiceClientOptions options = default)
```

```csharp
/// <summary>
/// The token type used to authenticate with IoT hub.
/// </summary>
enum TokenType
{
    SAS_TOKEN, // Option 2 - SHARED_ACCESS_SIGNATURE_TOKEN
    JWT // Option 2 - JSON_WEB_TOKEN
}
```

## Registry Manager

```csharp
/// <summary>
/// Creates an instance of <see cref="RegistryManager"/>.
/// </summary>
/// <param name="hostName">IoT hub host name.</param>
/// <param name="credential">Credentials to authenticate with IoT Hub.</param>
/// <param name="tokenType">The token type returned by the credential implementation. Possible values are <see cref="TokenType"/></param>
/// <param name="transportSettings">The HTTP transport settings.</param>
/// <returns>An instance of <see cref="RegistryManager"/>.</returns>
public static RegistryManager Create(
    string hostName,
    TokenCredential credential,
    TokenType tokenType,
    HttpTransportSettings transportSettings)
```

## Job Client

```csharp
/// <summary>
/// Creates an instance of <see cref="JobClient"/>.
/// </summary>
/// <param name="hostName">IoT hub host name.</param>
/// <param name="credential">Credentials to authenticate with IoT Hub.</param>
/// <param name="tokenType">The token type returned by the credential implementation. Possible values are <see cref="TokenType"/></param>
/// <param name="transportSettings">The HTTP transport settings.</param>
/// <returns>An instance of <see cref="JobClient"/>.</returns>
public static JobClient Create(
    string hostName,
    TokenCredential credential,
    TokenType tokenType,
    HttpTransportSettings transportSettings)
```

## Digital Twin Client

```csharp
/// <summary>
/// Initializes a new instance of the <see cref="DigitalTwinClient"/> class.</summary>
/// <param name="hostName">IoT hub host name.</param>
/// <param name="credential">Credentials to authenticate with IoT Hub.</param>
/// <param name="tokenType">The token type returned by the credential implementation. Possible values are <see cref="TokenType"/></param>
/// <param name="handlers">The delegating handlers to add to the http client pipeline. You can add handlers for tracing, implementing a retry strategy, routing requests through a proxy, etc.</param>
/// <returns>An instance of <see cref="DigitalTwinClient"/></returns>
public static DigitalTwinClient Create(
    string hostName,
    TokenCredential credential,
    TokenType tokenType,
    HttpTransportSettings transportSettings)
```
