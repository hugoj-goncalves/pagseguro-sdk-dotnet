﻿using System.Configuration;
// ReSharper disable UnusedMember.Global

namespace Uol.PagSeguro.Configuration
{
    /// <inheritdoc cref="ConfigurationElement" />
    public class PreApprovalElement : ConfigurationElement, ITypedConfigValueProvider
    {
        private const string PreApprovalRequestKey = "PreApprovalRequest";
        private const string PreApprovalRedirectKey = "PreApprovalRedirect";
        private const string PreApprovalNotificationKey = "PreApprovalNotification";
        private const string PreApprovalSearchKey = "PreApprovalSearch";
        private const string PreApprovalCancelKey = "PreApprovalCancel";
        private const string PreApprovalPaymentKey = "PreApprovalPayment";

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PreApprovalRequestKey, IsRequired = true)]
        public UrlElement PreApprovalRequest
        {
            get => (UrlElement)this[PreApprovalRequestKey];
            set => this[PreApprovalRequestKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PreApprovalRedirectKey, IsRequired = true)]
        public UrlElement PreApprovalRedirect
        {
            get => (UrlElement)this[PreApprovalRedirectKey];
            set => this[PreApprovalRedirectKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PreApprovalNotificationKey, IsRequired = true)]
        public UrlElement PreApprovalNotification
        {
            get => (UrlElement)this[PreApprovalNotificationKey];
            set => this[PreApprovalNotificationKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PreApprovalSearchKey, IsRequired = true)]
        public UrlElement PreApprovalSearch
        {
            get => (UrlElement)this[PreApprovalSearchKey];
            set => this[PreApprovalSearchKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PreApprovalCancelKey, IsRequired = true)]
        public UrlElement PreApprovalCancel
        {
            get => (UrlElement)this[PreApprovalCancelKey];
            set => this[PreApprovalCancelKey] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(PreApprovalPaymentKey, IsRequired = true)]
        public UrlElement PreApprovalPayment
        {
            get => (UrlElement)this[PreApprovalPaymentKey];
            set => this[PreApprovalPaymentKey] = value;
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="sandbox"></param>
        /// <returns></returns>
        public T GetValue<T>(string elementKey = null, bool sandbox = false)
        {
            if (typeof(T) != typeof(string) || string.IsNullOrWhiteSpace(elementKey))
                return default(T);

            return ((UrlElement) this[elementKey]).GetValue<T>();
        }
    }
}
