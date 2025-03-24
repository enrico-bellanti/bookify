using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Bookify.Dtos;

namespace Bookify.Services
{
    public class KeycloakUserService: IKeycloakUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _keycloakBaseUrl;
        private readonly string _realm;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public KeycloakUserService(
            IConfiguration configuration,
            HttpClient httpClient
        )
        {
            _httpClient = httpClient;
            _keycloakBaseUrl = configuration["Keycloak:BaseUrl"];
            _realm = configuration["Keycloak:Realm"];
            _clientId = configuration["Keycloak:ClientId"];
            _clientSecret = configuration["Keycloak:Secret"];
        }
        public async Task<Guid?> AddUserAsync(AddKeycloakUserDto keycloakUserDto)
        {
            try
            {
                // 1. Get admin token
                var adminToken = await GetAdminToken();

                // 2. Create user in Keycloak
                var keycloakUserId = await CreateKeycloakUser(keycloakUserDto, adminToken);
                if (string.IsNullOrEmpty(keycloakUserId))
                    return null;

                // 3. Parse string ID to Guid
                if (Guid.TryParse(keycloakUserId, out Guid userId))
                    return userId;
                else
                {
                    Console.WriteLine($"Error: Could not parse keycloakUserId '{keycloakUserId}' to Guid");
                    return null;
                }
            }
            catch (Exception ex) 
            {
                throw new ApplicationException(ex.Message);
                //Console.WriteLine($"Error adding user: {ex.Message}");
                //return null;
            }
        }
        public async Task<bool> UpdateUserAsync(string userUuid, UpdateKeycloakUserDto updateUserDto)
        {
            try
            {
                // 1. Get admin token
                var adminToken = await GetAdminToken();

                // 2. Update user in Keycloak
                var keycloakUpdated = await UpdateKeycloakUser(userUuid, updateUserDto, adminToken);
                if (!keycloakUpdated)
                    return false;

                // 3. Update password if provided
                if (!string.IsNullOrEmpty(updateUserDto.Password))
                {
                    var passwordUpdated = await UpdateKeycloakUserPasswordAsync(userUuid, updateUserDto.Password, adminToken);
                    if (!passwordUpdated)
                        return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error updating user: {ex.Message}");
                return false;
            }
        }

        private async Task<string> GetAdminToken()
        {
            var tokenEndpoint = $"{_keycloakBaseUrl}/realms/{_realm}/protocol/openid-connect/token";

            var formData = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", _clientId },
                { "client_secret", _clientSecret }
            };

            var content = new FormUrlEncodedContent(formData);
            var response = await _httpClient.PostAsync(tokenEndpoint, content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to get admin token: {response.StatusCode}");

            var responseData = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<JsonElement>(responseData);

            return tokenResponse.GetProperty("access_token").GetString();
        }

        private async Task<string> CreateKeycloakUser(AddKeycloakUserDto addUserDto, string adminToken)
        {
            // Check if username or email already exists
            await ValidateUserDoesNotExist(addUserDto.Username, addUserDto.Email, adminToken);

            var usersEndpoint = $"{_keycloakBaseUrl}/admin/realms/{_realm}/users";

            var userData = new
            {
                username = addUserDto.Username,
                email = addUserDto.Email,
                firstName = addUserDto.FirstName,
                lastName = addUserDto.LastName,
                enabled = true,
                emailVerified = true,
                credentials = new[]
                {
                    new {
                        type = "password",
                        value = addUserDto.Password,
                        temporary = false
                    }
                }
            };

            var userJson = JsonSerializer.Serialize(userData);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await _httpClient.PostAsync(usersEndpoint, content);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to create user in Keycloak: {response.StatusCode}");

            // Keycloak returns the location of the created user in the headers
            var locationHeader = response.Headers.Location?.ToString();
            if (string.IsNullOrEmpty(locationHeader))
                throw new Exception("User created but location header missing");

            // Extract user ID from location URL
            var userId = locationHeader.Split('/').Last();
            return userId;
        }

        private async Task<bool> UpdateKeycloakUser(string userUuid, UpdateKeycloakUserDto updateUserDto, string adminToken)
        {
            var userEndpoint = $"{_keycloakBaseUrl}/admin/realms/{_realm}/users/{userUuid}";

            // Build update data object with only the properties that are provided
            var updateData = new Dictionary<string, object>();


            if (!string.IsNullOrEmpty(updateUserDto.Email))
                updateData["email"] = updateUserDto.Email;

            if (!string.IsNullOrEmpty(updateUserDto.FirstName))
                updateData["firstName"] = updateUserDto.FirstName;

            if (!string.IsNullOrEmpty(updateUserDto.LastName))
                updateData["lastName"] = updateUserDto.LastName;

            if (updateUserDto.IsActive.HasValue)
                updateData["enabled"] = updateUserDto.IsActive.Value;

            var userJson = JsonSerializer.Serialize(updateData);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            // Keycloak user update is a PUT operation
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(userEndpoint),
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserPasswordAsync(string userUuid, string password)
        {
            try
            {
                // 1. Get admin token
                var adminToken = await GetAdminToken();

                // 2. Update password in Keycloak
                var passwordUpdated = await UpdateKeycloakUserPasswordAsync(userUuid, password, adminToken);
                if (!passwordUpdated)
                    return false;

                return true;

            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error updating user password: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> UpdateKeycloakUserPasswordAsync(string userUuid, string newPassword, string adminToken)
        {
            var passwordEndpoint = $"{_keycloakBaseUrl}/admin/realms/{_realm}/users/{userUuid}/reset-password";

            var passwordData = new
            {
                type = "password",
                value = newPassword,
                temporary = false
            };

            var passwordJson = JsonSerializer.Serialize(passwordData);
            var content = new StringContent(passwordJson, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
            var response = await _httpClient.PutAsync(passwordEndpoint, content);

            return response.IsSuccessStatusCode;
        }

        private async Task ValidateUserDoesNotExist(string username, string email, string adminToken)
        {
            // Check for existing username
            var usernameExists = await UserExistsByAttribute("username", username, adminToken);
            if (usernameExists)
            {
                throw new Exception($"User with username '{username}' already exists");
            }

            // Check for existing email
            var emailExists = await UserExistsByAttribute("email", email, adminToken);
            if (emailExists)
            {
                throw new Exception($"User with email '{email}' already exists");
            }
        }

        private async Task<bool> UserExistsByAttribute(string attribute, string value, string adminToken)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var searchEndpoint = $"{_keycloakBaseUrl}/admin/realms/{_realm}/users?{attribute}={Uri.EscapeDataString(value)}&exact=true";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            var response = await _httpClient.GetAsync(searchEndpoint);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to search users in Keycloak: {response.StatusCode}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<JsonElement[]>(responseContent);

            return users != null && users.Length > 0;
        }

        public bool CompareUserRoles(string[] requiredRoles, string[] userRoles)
        {
            bool hasRequiredRole = requiredRoles.Any(requiredRole =>
                userRoles.Any(userRole =>
                    string.Equals(userRole, requiredRole, StringComparison.OrdinalIgnoreCase)));

            return hasRequiredRole;
        }
    }
}
