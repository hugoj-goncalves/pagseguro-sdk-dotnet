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
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace ReceiveNotification
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            try
            {

                AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);

                // TODO: Substitute the code below with a notification code for your transaction. 
                // You receive this notification code through a post on the URL that you specify in 
                // this page: https://pagseguro.uol.com.br/integracao/notificacao-de-transacoes.jhtml
                
                // Use notificationType to check if is PreApproval (preApproval or transaction)
                Transaction transaction = NotificationService.CheckTransaction(credentials, "766B9C-AD4B044B04DA-77742F5FA653-E1AB24");

               
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
