using AutoMapper;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.UnitOfWorkManager;
using Microsoft.Extensions.Logging;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameters;

namespace PharmacyInventory_Application.Services.Implementations
{
    public sealed class GenericNameService : IGenericNameService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GenericName> _logger;
        private readonly IMapper _mapper;


        public GenericNameService(IUnitOfWork unitOfWork, ILogger<GenericName> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<StandardResponse<GenericNameResponseDto>> CreateGenericNameAsync(GenericNameRequestDto genericNameRequestDto)
        {
            var genericName = _mapper.Map<GenericName>(genericNameRequestDto);
            await _unitOfWork.GenericName.Create(genericName);
            _unitOfWork.SaveAsync();
            var genericNameDto = _mapper.Map<GenericNameResponseDto>(genericName);
            return StandardResponse<GenericNameResponseDto>.Success("Successfully created new genericName", genericNameDto, 201);

        }

        public async Task<StandardResponse<string>> DeleteGenericName(int id)
        {
            try
            {
                var genericName = await _unitOfWork.GenericName.GetGenericNameById(id);
                if (genericName == null)
                {
                    return StandardResponse<string>.Failed("GenericName not found.", 404);
                }

                _unitOfWork.GenericName.Delete(genericName);
                _unitOfWork.SaveAsync();

                return StandardResponse<string>.Success("GenericName deleted successfully.", "Deleted", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the genericName.");
                return StandardResponse<string>.Failed("An error occurred while deleting the genericName.", 500);
            }
        }

        public async Task<StandardResponse<GenericNameResponseDto>> UpdateGenericName(int id, GenericNameRequestDto genericNameRequestDto)
        {
            try
            {
                var existingGenericName = await _unitOfWork.GenericName.GetGenericNameById(id);
                if (existingGenericName == null)
                {
                    return StandardResponse<GenericNameResponseDto>.Failed("genericName not found.", 404);
                }

                // Update existing drug properties with new data
                _mapper.Map(genericNameRequestDto, existingGenericName);

                _unitOfWork.GenericName.Update(existingGenericName);
                _unitOfWork.SaveAsync();

                var updatedGenericNameDto = _mapper.Map<GenericNameResponseDto>(existingGenericName);
                return StandardResponse<GenericNameResponseDto>.Success("genericName updated successfully.", updatedGenericNameDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the genericName.");
                return StandardResponse<GenericNameResponseDto>.Failed("An error occurred while updating the genericName.", 500);

            }
        }

        public async Task<StandardResponse<GenericNameResponseDto>> GetGenericNameById(int id)
        {
            try
            {
                var genericName = await _unitOfWork.GenericName.GetGenericNameById(id);
                if (genericName == null)
                {
                    return StandardResponse<GenericNameResponseDto>.Failed("genericName not found.", 404);
                }
                var brandDto = _mapper.Map<GenericNameResponseDto>(genericName);
                return StandardResponse<GenericNameResponseDto>.Success("Successfully retrieved genericName by ID.", brandDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting genericName by ID.");
                return StandardResponse<GenericNameResponseDto>.Failed("An error occurred while getting genericName by ID.", 500);
            }
        }

        public async Task<StandardResponse<(IEnumerable<GenericNameResponseDto>, MetaData)>> GetAllUGenericName(GenericNameRequestInputParameter parameter)
        {
            try
            {
                var genericNames = await _unitOfWork.GenericName.GetAllGenericName(parameter);
                var genericNameDtos = _mapper.Map<IEnumerable<GenericNameResponseDto>>(genericNames);
                return StandardResponse<(IEnumerable<GenericNameResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved all genericName", (genericNameDtos, genericNames.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all genericNames.");
                return StandardResponse<(IEnumerable<GenericNameResponseDto>, MetaData)>.Failed("An error occurred while getting all genericNames.", 500);
            }
        }

    }
}
