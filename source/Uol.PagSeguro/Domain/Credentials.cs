﻿// Copyright [2011] [PagSeguro Internet Ltda.]
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Uol.PagSeguro.Domain
{
    /// <summary>
    /// Abstract class that represents a PagSeguro credential
    /// </summary>
    public abstract class Credentials
    {
        /// <summary>
        /// </summary>
        public bool IsSandbox { get; set; }

        /// <summary>
        /// Derived classes should add all of the credential "parts" as name value pairs
        /// in this dictionary. 
        /// </summary>
        protected Dictionary<string, string> AttributeDictionary { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Returns a collection of name value pairs that compose this set of credentials
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<CredentialsNameValuePair> Attributes
        {
            get
            {
                var list = new List<CredentialsNameValuePair>(AttributeDictionary.Count);
                foreach(var kv in AttributeDictionary)
                    list.Add(new CredentialsNameValuePair(kv.Key, kv.Value));

                return list.AsReadOnly();
            }
        }
    }
}
