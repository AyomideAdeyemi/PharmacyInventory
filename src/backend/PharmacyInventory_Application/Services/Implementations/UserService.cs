using AutoMapper;
using Microsoft.Extensions.Logging;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.UnitOfWorkManager;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Application.Services.Implementations
{
    public class UserService : IUserService
    {        
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<User> _logger;
        private readonly IMapper _mapper;


        public UserService(IUnitOfWork unitOfWork, ILogger<User> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }
        public async Task<StandardResponse<UserResponseDto>> CreateUserAsync(UserRequestDto userRequestDto)
        {
            var user = _mapper.Map<User>(userRequestDto);
            await _unitOfWork.User.Create(user);
            await _unitOfWork.SaveAsync();
            var userDto = _mapper.Map<UserResponseDto>(user);
            return StandardResponse<UserResponseDto>.Success("Successfully created new user", userDto, 201);

        }

        public async Task<StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>> GetAllUsers(UserRequestInputParameter parameter)
        {
            try
            {
                var users = await _unitOfWork.User.GetAllUsers(parameter);
                var userDtos = _mapper.Map<IEnumerable<UserResponseDto>>(users);
                return StandardResponse<(IEnumerable<UserResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved all users", (userDtos, users.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all users.");
                return StandardResponse<(IEnumerable<UserResponseDto>, MetaData)>.Failed("An error occurred while getting all users.", 500);
            }
        }

        public async Task<StandardResponse<string>> DeleteUser(int id)
        {
            try
            {
                var user = await _unitOfWork.User.GetUserById(id);
                if (user == null)
                {
                    return StandardResponse<string>.Failed("User not found.", 404);
                }

                _unitOfWork.User.Delete(user);
                _unitOfWork.SaveAsync();

                return StandardResponse<string>.Success("User deleted successfully.", "Deleted", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user.");
                return StandardResponse<string>.Failed("An error occurred while deleting the user.", 500);
            }
        }

        public async Task<StandardResponse<UserResponseDto>> UpdateUser(int id, UserRequestDto userRequestDto)
        {
            try
            {
                var existingUser= await _unitOfWork.User.GetUserById(id);
                if (existingUser == null)
                {
                    return StandardResponse<UserResponseDto>.Failed("User not found.", 404);
                }

                // Update existing user properties with new data
                _mapper.Map(userRequestDto, existingUser);

                _unitOfWork.User.Update(existingUser);
                _unitOfWork.SaveAsync();

                var updatedUserDto = _mapper.Map<UserResponseDto>(existingUser);
                return StandardResponse<UserResponseDto>.Success("User updated successfully.", updatedUserDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return StandardResponse<UserResponseDto>.Failed("An error occurred while updating the user.", 500);

            }
        }

        public async Task<StandardResponse<UserResponseDto>> GetUserById(int id)
        {
            try
            {
                var user = await _unitOfWork.User.GetUserById(id);
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
