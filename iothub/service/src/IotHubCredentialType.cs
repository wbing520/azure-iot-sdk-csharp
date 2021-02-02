// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Devices
{
    /// <summary>
    /// The credential type used to authenticate with IoT hub.
    /// </summary>
    public enum TokenType
    {
        SAS_TOKEN,
        JWT
    }
}
