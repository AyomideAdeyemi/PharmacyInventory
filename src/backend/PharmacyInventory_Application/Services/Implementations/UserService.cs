using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Application.Services.Implementations
{
    public class UserService : IUserService
    {        
        
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
       

        public UserService(ILogger<User> logger, IMapper mapper, UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>> GetAllUsersAsync(UserRequestInputParameter parameter)
        {
            try
            {
                var users = await _userManager.Users
                    .Skip((parameter.PageNumber - 1) * parameter.PageSize)
                    .Take(parameter.PageSize)
                    .ToListAsync();

                var totalUsersCount = await _userManager.Users.CountAsync();

                var userDtos = _mapper.Map<IEnumerable<UserResponseDto>>(users);

                var metaData = new MetaData
                {
                    PageNumber = parameter.PageNumber,
                    PageSize = parameter.PageSize,
                    TotalCount = totalUsersCount,
                    TotalPages = (int)Math.Ceiling((double)totalUsersCount / parameter.PageSize)
                };

                return StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>.Success("Successfully retrieved all users", (userDtos, metaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users.");
                return StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>.Failed("An error occurred while getting all users.", 500);
            }
        }

        //public async Task<PagedList<User>> GetAllUsersAsync()
        //{
        //    var parameter = new UserRequestInputParameter();
        //    var users = await _userManager.Users
        //        .Skip((parameter.PageNumber - 1) * parameter.PageSize)
        //        .Take(parameter.PageSize)
        //        .ToListAsync();

        //    var totalUsersCount = await _userManager.Users.CountAsync();

        //    return new PagedList<User>(users, totalUsersCount, parameter.PageNumber, parameter.PageSize);
        //}

        public async Task<StandardResponse<string>> DeleteUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return StandardResponse<string>.Failed("User not found.", 404);
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return StandardResponse<string>.Success("User deleted successfully.","Deleted", 200);
                }

                return StandardResponse<string>.Failed("Unable to delete user.", 500);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user.");
                return StandardResponse<string>.Failed("An error occurred while deleting the user.", 500);
            }
        }

        


        public async Task<StandardResponse<UserResponseDto>> UpdateUser(string id, UserRequestDto userRequestDto)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(id);
                if (existingUser == null)
                {
                    return StandardResponse<UserResponseDto>.Failed("User not found.", 404);
                }

                // Update existing user properties with new data
                _mapper.Map(userRequestDto, existingUser);

                await _userManager.UpdateAsync(existingUser);

                var updatedUserDto = _mapper.Map<UserResponseDto>(existingUser);
                return StandardResponse<UserResponseDto>.Success("User updated successfully.", updatedUserDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return StandardResponse<UserResponseDto>.Failed("An error occurred while updating the user.", 500);
            }
        }

        public async Task<StandardResponse<UserResponseDto>> GetUserById(string id)
        {
            try
            {
               
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return StandardResponse<UserResponseDto>.Failed("User not found.", 404);
                }
                var userDto = _mapper.Map<UserResponseDto>(user);
                return StandardResponse<UserResponseDto>.Success("Successfully retrieved user by ID.", userDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting user by ID.");
                return StandardResponse<UserResponseDto>.Failed("An error occurred while getting user by ID.", 500);
            }
        }
    }
}
