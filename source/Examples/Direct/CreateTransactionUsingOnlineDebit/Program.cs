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
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Domain.Direct;
using Uol.PagSeguro.Exception;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Service;

namespace CreateTransactionUsingOnlineDebit
{

    class Program
    {
        static void Main(string[] args)
        {

            PagSeguroConfiguration.XmlConfigFile = ".../.../.../.../.../Configuration/PagSeguroConfig.xml";

            bool isSandbox = false;
            EnvironmentConfiguration.ChangeEnvironment(isSandbox);

            // Instantiate a new checkout
            OnlineDebitCheckout checkout = new OnlineDebitCheckout();

            // Sets the payment mode
            checkout.PaymentMode = PaymentMode.DEFAULT;

            // Sets the receiver e-mail should will get paid
            checkout.ReceiverEmail = "backoffice@lojamodelo.com.br";

            // Sets the currency
            checkout.Currency = Currency.Brl;

            // Add items
            checkout.Items.Add(new Item("0001", "Notebook Prata", 1, 1300.00m));
            checkout.Items.Add(new Item("0002", "Notebook Rosa", 1, 150.99m));

            // Sets a reference code for this checkout, it is useful to identify this payment in future notifications.
            checkout.Reference = "REF1234";

            // Sets shipping information for this payment request
            checkout.Shipping = new Shipping();
            checkout.Shipping.ShippingType = ShippingType.Sedex;
            checkout.Shipping.Cost = 0.00m;
            checkout.Shipping.Address = new Address(
                "BRA",
                "SP",
                "Sao Paulo",
                "Jardim Paulistano",
                "01452002",
                "Av. Brig. Faria Lima",
                "1384",
                "5o andar"
            );

            // Sets your customer information.
            // If you using SANDBOX you must use an email @sandbox.pagseguro.com.br
            checkout.Sender = new Sender(
                "Joao Comprador",
                "comprador@uol.com.br",
                new Phone("11", "56273440")
            );
            checkout.Sender.Hash = "b2806d600653cbb2b195f317ca9a1a58738187a02c05bf7f2280e2076262e73b";
            SenderDocument senderCPF = new SenderDocument(Documents.GetDocumentByType("CPF"), "12345678909");
            checkout.Sender.Documents.Add(senderCPF);

            // Sets the notification url
            checkout.NotificationUrl = "http://www.lojamodelo.com.br";

            // Sets the bank information
            checkout.BankName = BankName.Bradesco;

            try
            {
                AccountCredentials credentials = PagSeguroConfiguration.GetAccountCredentials(isSandbox);
                Transaction result = TransactionService.CreateCheckout(credentials, checkout);

     
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
