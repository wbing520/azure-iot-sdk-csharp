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
    internal class IotHubTokenCredential : IotHubConnectionBase
    {
#if !NET451

        private TokenCredential _credential;

        public IotHubTokenCredential(string hostName, TokenCredential credential) : base(hostName)
        {
            _credential = credential;
        }

#endif

        // (Sindhu): TODO Convert to async
        public override string GetAuthorizationHeader()
        {
#if !NET451
            return _credential.GetToken(new TokenRequestContext(), new CancellationToken()).Token;
#else
            throw new InvalidOperationException($"{nameof(GetAuthorizationHeader)} is not supported on NET451");

#endif
        }

        public async override Task<CbsToken> GetTokenAsync(Uri namespaceAddress, string appliesTo, string[] requiredClaims)
        {
#if !NET451

            AccessToken token = await _credential.GetTokenAsync(new TokenRequestContext(), new CancellationToken()).ConfigureAwait(false);
            return new CbsToken(
                token.Token,
                CbsConstants.IotHubSasTokenType,
                DateTime.UtcNow.Add(_defaultTokenTimeToLive));
#else
            throw new InvalidOperationException($"{nameof(GetTokenAsync)} is not supported on NET451");
#endif
        }
    }
}
