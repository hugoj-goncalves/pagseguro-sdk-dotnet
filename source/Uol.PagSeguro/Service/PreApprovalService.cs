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
using System.Globalization;
using System.Net;
using System.Xml;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Log;
using Uol.PagSeguro.Parse;
using Uol.PagSeguro.Resources;
using Uol.PagSeguro.Util;
using Uol.PagSeguro.XmlParse;
using System.Web;

namespace Uol.PagSeguro.Service
{
    /// <summary>
    /// Encapsulates web service calls regarding PagSeguro pre-approval requests
    /// </summary>
    public static class PreApprovalService
    {
        /// <summary>
        /// CreatePreApproval is the actual implementation of the Register method
        /// This separation serves as test hook to validate the Uri
        /// against the code returned by the service
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="preApproval">PreApproval request information</param>
        /// <returns>The Uri to where the user needs to be redirected to in order to complete the payment process</returns>
        public static Uri CreatePreApproval(Credentials credentials, PreApprovalRequest preApproval)
        {

            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - begin", preApproval));

            try
            {
                using (var response = HttpURLConnectionUtil.GetHttpPostConnection(
                    PagSeguroConfiguration.PreApprovalUri.AbsoluteUri, BuildPreApprovalUrl(credentials, preApproval), credentials.IsSandbox()))
                {

                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        using (var reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            var preApprovalResponse = new PreApprovalRequestResponse(PagSeguroUris.GetPreApprovalRedirectUri(credentials));
                            PreApprovalSerializer.Read(reader, preApprovalResponse);
                            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - end {1}", preApproval, preApprovalResponse.PreApprovalRedirectUri));
                            return preApprovalResponse.PreApprovalRedirectUri;
                        }
                    }

                    var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException(response);
                    PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - error {1}", preApproval, pse));
                    throw pse;
                }
            }
            catch (WebException exception)
            {
                var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.Register({0}) - error {1}", preApproval, pse));
                throw pse;
            }
        }

        /// <summary>
        /// CancelPreApproval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="preApprovalCode">PreApproval code</param>
        /// <returns>The PreApprovalRequestResponse wich contains the response</returns>
        public static bool CancelPreApproval(Credentials credentials, string preApprovalCode)
        {

            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - begin", preApprovalCode));

            try
            {
                using (var response = HttpURLConnectionUtil.GetHttpGetConnection(BuildCancelUrl(credentials, preApprovalCode), credentials.IsSandbox()))
                {

                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        using (var reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            var paymentResponse = new PreApprovalRequestResponse(PagSeguroUris.GetPreApprovalCancelUri(credentials));
                            PreApprovalSerializer.Read(reader, paymentResponse);
                            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - end {1}", preApprovalCode, paymentResponse.Status));
                            return paymentResponse.Status.Equals("OK", StringComparison.CurrentCultureIgnoreCase);
                        }
                    }

                    var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException(response);
                    PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - error {1}", preApprovalCode, pse));
                    throw pse;
                }
            }
            catch (WebException exception)
            {
                var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.CancelPreApproval({0}) - error {1}", preApprovalCode, pse));
                throw pse;
            }
        }

        /// <summary>
        /// ChargePreApproval
        /// </summary>
        /// <param name="credentials">PagSeguro credentials</param>
        /// <param name="payment">PreApproval payment request information</param>
        /// <returns>The Uri to where the user needs to be redirected to in order to complete the payment process</returns>
        public static string ChargePreApproval(Credentials credentials, PaymentRequest payment)
        {

            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - begin", payment));

            try
            {
                using (var response = HttpUrlConnectionUtil.GetHttpPostConnection(
                    PagSeguroUris.GetPreApprovalPaymentUri(credentials).AbsoluteUri, BuildChargeUrl(credentials, payment)))
                {
                    
                    if (HttpStatusCode.OK.Equals(response.StatusCode))
                    {
                        using (var reader = XmlReader.Create(response.GetResponseStream()))
                        {
                            
                            var chargeResponse = new PaymentRequestResponse(PagSeguroUris.GetPreApprovalPaymentUri(credentials));
                            PaymentSerializer.Read(reader, chargeResponse);
                            PagSeguroTrace.Info(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - end {1}", payment, chargeResponse.PaymentRedirectUri));
                            return chargeResponse.TransactionCode;
                        }
                    }

                    var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException(response);
                    PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - error {1}", payment, pse));
                    throw pse;
                }
            }
            catch (WebException exception)
            {
                var pse = HttpUrlConnectionUtil.CreatePagSeguroServiceException((HttpWebResponse)exception.Response);
                PagSeguroTrace.Error(string.Format(CultureInfo.InvariantCulture, "PreApprovalService.ChargePreApproval({0}) - error {1}", payment, pse));
                throw pse;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="preApproval"></param>
        /// <returns></returns>
        internal static string BuildPreApprovalUrl(Credentials credentials, PreApprovalRequest preApproval)
        {
            var builder = new QueryStringBuilder();
            var data = PreApprovalParse.GetData(preApproval);

            builder.EncodeCredentialsAsQueryString(credentials);
            
            foreach (var pair in data)
                builder.Append(pair.Key, pair.Value);

            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="preApprovalCode"></param>
        /// <returns></returns>
        private static string BuildCancelUrl(Credentials credentials, string preApprovalCode)
        {
            var searchUrlByCode = new QueryStringBuilder("{url}/{preApprovalCode}?{credential}");
            searchUrlByCode.ReplaceValue("{url}", PagSeguroUris.GetPreApprovalCancelUri(credentials).AbsoluteUri);
            searchUrlByCode.ReplaceValue("{preApprovalCode}", HttpUtility.UrlEncode(preApprovalCode));
            searchUrlByCode.ReplaceValue("{credential}", new QueryStringBuilder().EncodeCredentialsAsQueryString(credentials).ToString());
            return searchUrlByCode.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        internal static string BuildChargeUrl(Credentials credentials, PaymentRequest payment)
        {
            var builder = new QueryStringBuilder();
            var data = PaymentParse.GetData(payment);

            builder.EncodeCredentialsAsQueryString(credentials);

            foreach (var pair in data)
                builder.Append(pair.Key, pair.Value);

            return builder.ToString();
        }
    }
}
