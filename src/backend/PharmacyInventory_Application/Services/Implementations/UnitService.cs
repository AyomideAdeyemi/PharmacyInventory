using AutoMapper;
using Microsoft.Extensions.Logging;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.UnitOfWorkManager;

namespace PharmacyInventory_Application.Services.Implementations
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Unit> _logger;
        private readonly IMapper _mapper;


        public UnitService(IUnitOfWork unitOfWork, ILogger<Unit> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }
        public async Task<StandardResponse<UnitResponseDto>> CreateUnitAsync(UnitRequestDto unitRequestDto)
        {
            var unit = _mapper.Map<Unit>(unitRequestDto);
            await _unitOfWork.Unit.Create(unit);
            await _unitOfWork.SaveAsync();
            var unitDto = _mapper.Map<UnitResponseDto>(unit);
            return StandardResponse<UnitResponseDto>.Success("Successfully created new unit", unitDto, 201);

        }

        public async Task<StandardResponse<string>> DeleteUnit(int id)
        {
            try
            {
                var unit = await _unitOfWork.Unit.GetUnitById(id);
                if (unit == null)
                {
                    return StandardResponse<string>.Failed("Unit not found.", 404);
                }

                _unitOfWork.Unit.Delete(unit);
                _unitOfWork.SaveAsync();

                return StandardResponse<string>.Success("Unit deleted successfully.", "Deleted", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the unit.");
                return StandardResponse<string>.Failed("An error occurred while deleting the unit.", 500);
            }
        }

        public async Task<StandardResponse<UnitResponseDto>> UpdateUnit(int id, UnitRequestDto unitRequestDto)
        {
            try
            {
                var existingUnit = await _unitOfWork.Unit.GetUnitById(id);
                if (existingUnit == null)
                {
                    return StandardResponse<UnitResponseDto>.Failed("Unit not found.", 404);
                }

                // Update existing drug properties with new data
                _mapper.Map(unitRequestDto, existingUnit);

                _unitOfWork.Unit.Update(existingUnit);
                _unitOfWork.SaveAsync();

                var updatedUnitDto = _mapper.Map<UnitResponseDto>(existingUnit);
                return StandardResponse<UnitResponseDto>.Success("unit updated successfully.", updatedUnitDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the unit.");
                return StandardResponse<UnitResponseDto>.Failed("An error occurred while updating the unit.", 500);

            }
        }

        public async Task<StandardResponse<UnitResponseDto>> GetUnitById(int id)
        {
            try
            {
                var unit = await _unitOfWork.Unit.GetUnitById(id);
                if (unit == null)
                {
                    return StandardResponse<UnitResponseDto>.Failed("unit not found.", 404);
                }
                var unitDto = _mapper.Map<UnitResponseDto>(unit);
                return StandardResponse<UnitResponseDto>.Success("Successfully retrieved unit by ID.", unitDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting unit by ID.");
                return StandardResponse<UnitResponseDto>.Failed("An error occurred while getting unit by ID.", 500);
            }
        }
    }
}