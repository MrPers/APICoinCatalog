using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Indentity.API
{
    public static class IdentityServerConfiguration
    {
        public const string ApiName = "Order";
        public const string SmartAppClientID = "client_angular_id";
        public const string SwaggerClientID = "swaggerui";
        //public const string Roles = "roles";
        public const string Permission = "permission";
        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            //new Client
            //{
            //    ClientId = SwaggerClientID,
            //    ClientName = SwaggerClientID,
            //    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            //    AllowAccessTokensViaBrowser = true,
            //    RequireClientSecret = false,
            //    AllowedScopes = {
            //        ApiName
            //    }
            //},
            new Client
            {
                ClientId = SmartAppClientID,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword, // Resource Owner Password Credential grant.
                AllowAccessTokensViaBrowser = true,
                RequireClientSecret = false, // This client does not need a secret to request tokens from the token endpoint.                    
                AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId, // For UserInfo endpoint.
                        IdentityServerConstants.StandardScopes.Profile,
                        ApiName
                },
                AllowOfflineAccess = true, // For refresh token.
                RefreshTokenExpiration = TokenExpiration.Sliding,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
            },
        };

        public static IEnumerable<ApiResource> GetApiResources()
        {
            //new List<ApiResource> {
            //    new ApiResource(ApiName)
            //};
            //yield return new ApiResource(SwaggerClientID);
            yield return new ApiResource(ApiName);
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
            //            return new List<IdentityResource>
            //            {
            //      new IdentityResources.OpenId(),
            //      new IdentityResources.Profile(),
            //      new IdentityResource(ApiName, new List<string> { ApiName })
            //        };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope(SwaggerClientID)
            {
                UserClaims =
                {
                    SwaggerClientID,
                }
            };
            yield return new ApiScope(ApiName)
            {
                UserClaims =
                {
                    ApiName,
                }
            };
            //return new List<ApiScope>
            //{
            //    new ApiScope(ApiName, ApiFriendlyName) {
            //        UserClaims = {
            //            JwtClaimTypes.Name,
            //            //JwtClaimTypes.Email,
            //            //JwtClaimTypes.PhoneNumber,
            //            //JwtClaimTypes.Role,
            //            //ClaimConstants.Permission
            //        }
            //    }
            //};
        }

        //new Client
        //{
        //    ClientId = "angular_id", //Идентификатор клиента, инициировавшего запрос.
        //    RequireClientSecret = false, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
        //    RequireConsent = false, //Указывает, требуется ли экран согласия
        //    RequirePkce = true, //Указывает, нужен ли этому клиенту секрет для запроса токенов из конечной точки токена
        //    AllowOfflineAccess = true,//Определяет, может ли этот клиент запрашивать токены обновления
        //    AccessTokenLifetime = 300, //Время жизни токена доступа в секундах(по умолчанию 3600 секунд / 1 час)
        //    AllowedGrantTypes =  GrantTypes.Code, //Задает типы грантов, которые разрешено использовать клиенту
        //    AllowedCorsOrigins = { "https://localhost:5001" },
        //    RedirectUris = { "https://localhost:5001/auth-callback", "https://localhost:5001/refresh" },
        //    PostLogoutRedirectUris = { "https://localhost:5001/" },
        //    AllowedScopes =
        //    {
        //        ApiName,
        //        IdentityServerConstants.StandardScopes.OpenId,
        //        IdentityServerConstants.StandardScopes.Profile
        //    },
        //    AllowAccessTokensViaBrowser = true, //Указывает, разрешено ли этому клиенту получать токены доступа через браузер
        //    IdentityTokenLifetime = 3600, //через сколько секунд токен обновлен(по умолчанию 300 секунд / 5 минут)
        //    AlwaysIncludeUserClaimsInIdToken = true, //При запросе токена идентификатора и токена доступа утверждения пользователя всегда должны добавляться к токену идентификатора вместо того, чтобы требовать от клиента использования конечной точки userinfo
        //    RefreshTokenUsage = TokenUsage.OneTimeOnly, //дескриптор токена обновления будет обновляться при обновлении токенов. Это значение по умолчанию.
        //    UpdateAccessTokenClaimsOnRefresh = true //Получает или задает значение, указывающее, следует ли обновлять маркер доступа (и его утверждения) при запросе маркера обновления.
        //}, 

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
