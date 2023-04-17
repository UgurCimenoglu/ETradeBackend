using System.Text;
using ETradeBackend.Application.Abstracts.Services;
using ETradeBackend.Application.Abstracts.Token;
using ETradeBackend.Application.DTOs;
using ETradeBackend.Application.DTOs.Facebook;
using ETradeBackend.Application.Exceptions;
using ETradeBackend.Domain.Entities.Identity;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using ETradeBackend.Application.Helpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace ETradeBackend.Persistance.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
            UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService, IMailService mailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
            _mailService = mailService;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password)
        {
            var appUser = await _userManager.FindByNameAsync(usernameOrEmail);
            if (appUser == null)
                appUser = await _userManager.FindByEmailAsync(usernameOrEmail);

            if (appUser == null)
                throw new NotFoundUserException();
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(appUser, tokenExpirationMinute: 30);
                await _userService.UpdateRefreshTokenAsync(appUser, token.RefreshToken, token.Expiration, 30);
                //Todo yetkiler
                return token;
            }
            //return new LoginUserErrorCommandResponse()
            //{
            //    Message = "Kullanici adi veya sifre hatali!"
            //};
            throw new AuthenticationErrorException();
        }

        public async Task<Token> FacebookLoginAsync(string authToken)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync(
                $"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");
            FacebookAccessTokenResponse? facebookAccessTokenResponse =
                JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            string userAccessTokenResponse = await _httpClient.GetStringAsync(
                $"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");
            FacebookUserAceesTokenValidation? validation =
                JsonSerializer.Deserialize<FacebookUserAceesTokenValidation>(userAccessTokenResponse);
            if (validation?.Data.IsValid != null)
            {
                string userInfoResponse =
                    await _httpClient.GetStringAsync(
                        $"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                FacebookUserInfoResponse? userInfo =
                    JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");

                var appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                return await CreateExternalUserAsync(appUser, userInfo?.Email, userInfo?.Name, info);
            }

            throw new Exception("Invalid External Authentication");
        }

        public async Task<Token> GoogleLoginAsync(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>
                {
                    _configuration["ExternalLoginSettings:Google:Client_ID"]
                } //google'dan aldıgımız client ID'yi veriyoruz bunu googleLoginCommandRequest objesinden gelen token ile validate edeceğiz.
            };

            var payload =
                await GoogleJsonWebSignature.ValidateAsync(idToken,
                    settings); //Burada googleLoginCommandRequest objesindeki tokeni ve üst satırdaki settings objesini parametre geçerek bir validasyon yapıyoruz.

            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            var appUser = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            return await CreateExternalUserAsync(appUser, payload.Email, payload.Name, info);

        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
                //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
                resetToken = resetToken.UrlEncode();

                await _mailService.SendResetPasswordMailAsync(user.Email, user.Id, resetToken);
            }
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
                //resetToken = Encoding.UTF8.GetString(tokenBytes);
                resetToken = resetToken.UrlDecode();
                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
                    "ResetPassword", resetToken);
            }
            return false;
        }

        private async Task<Token> CreateExternalUserAsync(AppUser appUser, string email, string name, UserLoginInfo info)
        {
            bool result = appUser != null;
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(email);
                if (appUser == null)
                {
                    appUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        FullName = name,
                        Email = email,
                        UserName = name
                    };
                    var identityResult = await _userManager.CreateAsync(appUser);
                    result = identityResult.Succeeded;
                }
                else
                {
                    await _userManager.AddLoginAsync(appUser, info); //AspNetUserLogins
                    result = true;
                }
            }
            if (result)
            {
                Token token = _tokenHandler.CreateAccessToken(appUser, tokenExpirationMinute: 30);
                await _userService.UpdateRefreshTokenAsync(appUser, token.RefreshToken, token.Expiration, 30);
                return token;
            }
            throw new Exception("Invalid External Authentication");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(user, 30);
                await _userService.UpdateRefreshTokenAsync(user, token.RefreshToken, token.Expiration, 30);
                return token;
            }
            else
                throw new NotFoundUserException();

        }


    }
}