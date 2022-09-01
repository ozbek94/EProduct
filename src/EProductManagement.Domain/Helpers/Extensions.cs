using EProductManagement.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EProductManagement.Domain.Helpers
{
    public static class Extensions
    {
        public static string TokenExtension(this HttpContext httpContext)
        {
            StringValues authorizationToken = string.Empty;
            if (httpContext.Request.Headers.TryGetValue(ExtensionParameterName.Authorization, out authorizationToken))
                return authorizationToken[0];
            return string.Empty;
        }

        public static string ExtractOcRequestId(this HttpContext httpContext)
        {
            StringValues ocRequestId = string.Empty;
            if (httpContext.Request.Headers.TryGetValue(ExtensionParameterName.OcRequestId, out ocRequestId))
                return ocRequestId[0];
            return string.Empty;
        }

        public static bool DecideIfAdmin(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.HasClaim(x => x.Type == ExtensionParameterName.AdminUserId))
                return true;
            return false;
        }

        public static bool DecideIfMerchantMainUser(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.HasClaim(x => x.Type == ExtensionParameterName.MerchantId) && claimsPrincipal.HasClaim(x => x.Type == ExtensionParameterName.IsMainUser))
            {
                if (bool.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.IsMainUser).Value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool DecideIfMerchantUser(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.HasClaim(x => x.Type == ExtensionParameterName.MerchantId))
                return true;
            return false;
        }

        public static int ExtractPartyId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.PartyId).Value);
        }

        public static int ExtractUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.UserId).Value);
        }
        public static int ExtractAdminUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.AdminUserId).Value);
        }
        public static int ExtractMerchantId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.MerchantId).Value);
        }
        public static int ExtractMerchantUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.MerchantUserId).Value);
        }
        public static int ExtractMerchantUserPartyId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.MerchantUserPartyId).Value);
        }

        public static string ExtractGsmNo(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.GsmNo).Value;
        }

        public static AdminUserDTO ExtractAdminUser(this ClaimsPrincipal claimsPrincipal)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<AdminUserDTO>(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.AdminUser).Value);
        }

        public static int GetRoleId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ExtensionParameterName.RoleId).Value);
        }

        public static string UppercaseFirstCharOnEveryWord(this string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return string.Empty;
            }
            else if (Text.Length == 1)
            {
                return Text.ToUpper();
            }
            else
            {
                string[] words = Text.Trim().Split(' ');
                string newText = string.Empty;

                foreach (var word in words)
                {
                    if (word.Trim().Length == 1)
                    {
                        newText += word.ToUpper();
                    }
                    else if (word.Trim().Length > 1)
                    {
                        newText += word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower() + " ";
                    }
                }

                return newText.Trim();
            }
        }
    }

    public static class ExtensionParameterName
    {
        internal static string Authorization = "Authorization";
        internal static string AdminUserId = "AdminUserId";
        internal static string MerchantId = "MerchantId";
        internal static string IsMainUser = "IsMainUser";
        internal static string MerchantUserId = "MerchantUserId";
        internal static string MerchantUserPartyId = "MerchantUserPartyId";
        internal static string UserId = "UserId";
        internal static string PartyId = "PartyId";
        internal static string GsmNo = "GsmNo";
        internal static string OcRequestId = "OcRequestId";

        internal static string RoleId = "RoleId";
        internal static string AdminUser = "AdminUser";
    }
}
