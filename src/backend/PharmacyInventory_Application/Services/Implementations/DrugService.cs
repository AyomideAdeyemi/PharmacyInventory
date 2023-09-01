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
    public sealed class DrugService : IDrugService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Drug> _logger;
        private readonly IMapper _mapper;
       

        public DrugService(IUnitOfWork unitOfWork, ILogger<Drug> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            
        }

        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetAllDrugs(DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetAllDrugs(parameter);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved all drugs", (drugDtos, drugs.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all drugs.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting all drugs.", 500);
            }
        }

        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByBrand(string brand, DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetDrugsByBrand(brand, parameter);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved drugs by brand", (drugDtos, drugs.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by brand.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting drugs by brand.", 500);
            }
        }
        public async Task<StandardResponse<DrugResponseDto>> CreateDrugAsync(DrugRequestDto drugRequestDto)
        {
            if (drugRequestDto == null)
            {
                return StandardResponse<DrugResponseDto>.Failed("Invalid drug request data.", 400);
            }

            var drug = _mapper.Map<Drug>(drugRequestDto);

            if (drug == null)
            {
                return StandardResponse<DrugResponseDto>.Failed("Error mapping drug request data.", 500);
            }

            try
            {
                await _unitOfWork.Drug.Create(drug);
                 _unitOfWork.SaveAsync();

                var drugDto = _mapper.Map<DrugResponseDto>(drug);
                return StandardResponse<DrugResponseDto>.Success("Successfully created new drug", drugDto, 201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a drug.");
                return StandardResponse<DrugResponseDto>.Failed("Error occurred while creating the drug.", 500);
            }
        }

        //public async Task<StandardResponse<DrugResponseDto>> CreateDrugAsync(DrugRequestDto drugRequestDto)
        //{
        //    var drug = _mapper.Map<Drug>(drugRequestDto);
        //    await _unitOfWork.Drug.Create(drug);
        //    _unitOfWork.SaveAsync();
        //    var drugDto = _mapper.Map<DrugResponseDto>(drug);
        //    return StandardResponse<DrugResponseDto>.Success("Successfully created new contact", drugDto, 201);

        //}
        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByGenericName(string genericName, DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetDrugsByGenericName(genericName, parameter);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved drugs by generic name", (drugDtos, drugs.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by generic name.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting drugs by generic name.", 500);
            }
        }
        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsBySupplier(string supplier, DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetDrugsBySupplier(supplier, parameter);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved drugs by supplier", (drugDtos, drugs.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by supplier.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting drugs by supplier.", 500);
            }
        }

        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate, DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetDrugsByExpiryDateRange(startDate, endDate, parameter);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved drugs by expiry date range", (drugDtos, drugs.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by expiry date range.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting drugs by expiry date range.", 500);
            }
        }

        public async Task<StandardResponse<DrugResponseDto>> GetDrugById(int id)
        {
            try
            {
                var drug = await _unitOfWork.Drug.GetdrugById(id);
                if (drug == null)
                {
                    return StandardResponse<DrugResponseDto>.Failed("Drug not found.", 404);
                }
                var drugDto = _mapper.Map<DrugResponseDto>(drug);
                return StandardResponse<DrugResponseDto>.Success("Successfully retrieved drug by ID.", drugDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drug by ID.");
                return StandardResponse<DrugResponseDto>.Failed("An error occurred while getting drug by ID.", 500);
            }
        }

        public async Task<StandardResponse<string>> DeleteDrug(int id)
        {
            try
            {
                var drug = await _unitOfWork.Drug.GetdrugById(id);
                if (drug == null)
                {
                    return StandardResponse<string>.Failed("Drug not found.", 404);
                }

                _unitOfWork.Drug.Delete(drug);
                _unitOfWork.SaveAsync();

                return StandardResponse<string>.Success("Drug deleted successfully.", "Deleted", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the drug.");
                return StandardResponse<string>.Failed("An error occurred while deleting the drug.", 500);
            }
        }

        public async Task<StandardResponse<DrugResponseDto>> UpdateDrug(int id, DrugRequestDto drugRequestDto)
        {
            try
            {
                var existingDrug = await _unitOfWork.Drug.GetdrugById(id);
                if (existingDrug == null)
                {
                    return StandardResponse<DrugResponseDto>.Failed("Drug not found.", 404);
                }

                // Update existing drug properties with new data
                _mapper.Map(drugRequestDto, existingDrug);

                _unitOfWork.Drug.Update(existingDrug);
                _unitOfWork.SaveAsync();

                var updatedDrugDto = _mapper.Map<DrugResponseDto>(existingDrug);
                return StandardResponse<DrugResponseDto>.Success("Drug updated successfully.", updatedDrugDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the drug.");
                return StandardResponse<DrugResponseDto>.Failed("An error occurred while updating the drug.", 500);

            }
        }

       
        


    }
}

