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

using System;
using System.Net;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Authorization;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace FindAuthorizationByCode
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // TODO: Substitute the code below with a valid preApproval code for your transaction
            String authorizationCode = "(ADD-TOKEN-HERE)";

            try
            {
                ApplicationCredentials credentials = PagSeguroConfiguration.GetApplicationCredentials(isSandbox);

                AuthorizationSummary result = AuthorizationSearchService.SearchByCode(credentials, authorizationCode);


            } 
            catch (WebException exception) 
            {

            }
            catch (PagSeguroServiceException exception)
            {


                foreach (ServiceError element in exception.Errors)
                {

                }
      
            }
        }
    }
}
