using System.Collections.Generic;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace Indentity.Data
{
    public static class IdentityServerConfiguration
    {
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client_id_js",
                RequireClientSecret = false,
                RequireConsent = false,
                RequirePkce = true,
                AllowedGrantTypes =  GrantTypes.Code,
                AllowedCorsOrigins = { "https://localhost:9001" },
                RedirectUris = { "https://localhost:9001/callback.html", "https://localhost:9001/refresh.html" },
                PostLogoutRedirectUris = { "https://localhost:9001/index.html" },
                AllowedScopes =
                {
                    "OrdersAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },
            new Client
            {
                ClientId = "client_angular_id", //Идентификатор клиента, инициировавшего запрос.
                RequireClientSecret = false, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
                RequireConsent = false, //Указывает, требуется ли экран согласия
                RequirePkce = true, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
                AllowedGrantTypes =  GrantTypes.Code, //Задает типы грантов, которые разрешено использовать клиенту
                AllowedCorsOrigins = { "http://localhost:4200" },
                RedirectUris = { "http://localhost:4200/security/callback", "http://localhost:4200/security/refresh" },
                PostLogoutRedirectUris = { "http://localhost:4200" },
                //AllowOfflineAccess = true,//Определяет, может ли этот клиент запрашивать токены обновления
                //AccessTokenLifetime = 300, //Время жизни токена доступа в секундах(по умолчанию 3600 секунд / 1 час)
                AllowedScopes =
                {
                    "OrdersAPI",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            }
        };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> {
                new ApiResource("OrdersAPI")
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        /// <summary>
        /// IdentityServer4 version 4.x.x changes
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> {
                new ApiScope("OrdersAPI", "Orders API")
            };

        //public static IEnumerable<ApiScope> GetApiScopes() =>
        //    new List<ApiScope> {
        //        new ApiScope(ApiName)
        //    };

        //    new Client
        //    {
        //        ClientId = "angular_id", //Идентификатор клиента, инициировавшего запрос.
        //        RequireClientSecret = false, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
        //        RequireConsent = false, //Указывает, требуется ли экран согласия
        //        RequirePkce = true, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
        //        AllowOfflineAccess = true,//Определяет, может ли этот клиент запрашивать токены обновления
        //        AccessTokenLifetime = 300, //Время жизни токена доступа в секундах(по умолчанию 3600 секунд / 1 час)
        //        AllowedGrantTypes =  GrantTypes.Code, //Задает типы грантов, которые разрешено использовать клиенту
        //        AllowedCorsOrigins = { "https://localhost:5001" },
        //        RedirectUris = { "https://localhost:5001/auth-callback", "https://localhost:5001/refresh" },
        //        PostLogoutRedirectUris = { "https://localhost:5001/" },
        //        AllowedScopes =
        //        {
        //            ApiName,
        //            IdentityServerConstants.StandardScopes.OpenId,
        //            IdentityServerConstants.StandardScopes.Profile
        //},
        //        AllowAccessTokensViaBrowser = true, //Указывает, разрешено ли этому клиенту получать токены доступа через браузер
        //        IdentityTokenLifetime = 3600, //через сколько секунд токен обновлен(по умолчанию 300 секунд / 5 минут)
        //        AlwaysIncludeUserClaimsInIdToken = true, //При запросе токена идентификатора и токена доступа утверждения пользователя всегда должны добавляться к токену идентификатора вместо того, чтобы требовать от клиента использования конечной точки userinfo
        //        RefreshTokenUsage = TokenUsage.OneTimeOnly, //дескриптор токена обновления будет обновляться при обновлении токенов. Это значение по умолчанию.
        //        UpdateAccessTokenClaimsOnRefresh = true //Получает или задает значение, указывающее, следует ли обновлять маркер доступа (и его утверждения) при запросе маркера обновления.
        //    }, 

        //public static IEnumerable<ApiScope> GetApiScopes()
        //{
        //    return new List<ApiScope>
        //    {
        //        new ApiScope(ApiName, ApiFriendlyName) {
        //            UserClaims = {
        //                JwtClaimTypes.Name,
        //                JwtClaimTypes.Email,
        //                JwtClaimTypes.PhoneNumber,
        //                JwtClaimTypes.Role,
        //                Permission
        //            }
        //        }
        //    };
        //}
    }

}
