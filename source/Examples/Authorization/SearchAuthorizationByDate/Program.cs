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

namespace FindAuthorizationByDate
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // TODO: Substitute the code below with a valid date interval for your authorization
            DateTime initialDate = new DateTime(2015, 09, 01, 08, 50, 0);
            DateTime finalDate = new DateTime(2015, 09, 24, 08, 50, 0);
            int maxPageResults = 10;
            int pageNumber = 1;

            try
            {

                ApplicationCredentials credentials = PagSeguroConfiguration.GetApplicationCredentials(isSandbox);

                AuthorizationSearchResult result = AuthorizationSearchService.SearchByDate(credentials, initialDate, finalDate, pageNumber, maxPageResults);

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
