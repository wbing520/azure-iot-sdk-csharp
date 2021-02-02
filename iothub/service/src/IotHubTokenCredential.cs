// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Amqp;
using System.Threading;

#if !NET451

using Azure.Core;

#endif

namespace Microsoft.Azure.Devices
{
    /// <summary>
    /// AAD token credentials to authenticate with the IoT hub.
    /// </summary>
    internal class IotHubTokenCredential : IotHubConnectionBase
    {
#if !NET451

        private TokenCredential _credential;
        public TokenType _tokenType { get; }

        /// <summary>
        /// Creates and instance of <see cref="IotHubTokenCredential"/>.
        /// </summary>
        /// <param name="hostName">The IoT hub host name.</param>
        /// <param name="credential">An implementation of AAD token credentials. For more information,
        /// <see href="https://docs.microsoft.com/en-us/dotnet/api/overview/azure/identity-readme"/>.</param>
        /// <param name="tokenType"></param>
        public IotHubTokenCredential(string hostName, TokenCredential credential, TokenType tokenType) : base(hostName)
        {
            if (string.IsNullOrEmpty(hostName))
            {
                throw new ArgumentNullException($"{nameof(hostName)} is null or empty.");
            }

            if (credential == null)
            {
                throw new ArgumentNullException($"{nameof(credential)} is null.");
            }

            _credential = credential;
            _tokenType = tokenType;
        }

#endif

        // (Sindhu): TODO Convert to async
        public override string GetAuthorizationHeader()
        {
#if !NET451
            AccessToken token = _credential.GetToken(new TokenRequestContext(), new CancellationToken());
            if (_tokenType == TokenType.JWT)
            {
                // TODO: Find out correct value to send
                return $"bearer{token.Token}{token.ExpiresOn}";
            }
            else
            {
                return token.Token;
            }
#else
            throw new InvalidOperationException($"{nameof(GetAuthorizationHeader)} is not supported on NET451");

#endif
        }

        public async override Task<CbsToken> GetTokenAsync(Uri namespaceAddress, string appliesTo, string[] requiredClaims)
        {
#if !NET451

            AccessToken token = await _credential.GetTokenAsync(new TokenRequestContext(), new CancellationToken()).ConfigureAwait(false);
            var tokenType = _tokenType == TokenType.SAS_TOKEN ? CbsConstants.IotHubSasTokenType : "jwt";
            return new CbsToken(
                token.Token,
                tokenType,
                DateTime.UtcNow.Add(_defaultTokenTimeToLive));
#else
            throw new InvalidOperationException($"{nameof(GetTokenAsync)} is not supported on NET451");
#endif
        }
    }
}
