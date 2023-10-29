using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.UnitOfWorkManager;
using PharmacyInventory_Shared.RequestParameter.Common;
using PharmacyInventory_Shared.RequestParameter.ModelParameter;
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
       
        public async Task<StandardResponse<PagedList<DrugResponseDto>>> GetAllDrugs(DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetAllDrugs(parameter);
                var drugsDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
               var pageList = new PagedList<DrugResponseDto>(drugsDtos.ToList(), drugs.MetaData.TotalCount, parameter.PageNumber, parameter.PageSize);
                return StandardResponse<PagedList<DrugResponseDto>>.Success("Successfully retrieved all drugs", pageList, 200);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Processing completed successfully." + " " + ex.ToString());
                _logger.LogError(ex, "An error occurred while getting all drugs.");
                return StandardResponse<PagedList<DrugResponseDto>>.Failed("An error occurred while getting all drugs.", 500);
            }
        }

        public async Task<StandardResponse<IEnumerable<DrugResponseDto>>> GetDrugsByQuantityRange(double minQuantity, double maxQuantity, DrugRequestInputParameter parameter )
        {
            try
            {
                var drugsFromDb = await _unitOfWork.Drug.GetDrugsByQuantityRange(minQuantity, maxQuantity, parameter);
                var drugDto = _mapper.Map<IEnumerable<DrugResponseDto>>(drugsFromDb);
                _logger.LogInformation("Successfully retrieve all data");
                return StandardResponse<IEnumerable<DrugResponseDto>>.Success("Successfully retrieved drugs by quantity range", drugDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by quantity range.");
                return StandardResponse<IEnumerable<DrugResponseDto>>.Failed("An error occurred while getting drugs by quantity range.", 500);
            }
        }
       
        public async Task<StandardResponse<PagedList<DrugResponseDto>>> GetDrugsByBrandId(string brandId, DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetDrugsByBrandId(brandId, parameter);
                var drugDto = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                var pagedList = new PagedList<DrugResponseDto>(drugDto.ToList(), drugs.MetaData.TotalCount, parameter.PageNumber, parameter.PageSize);

                return  StandardResponse<PagedList<DrugResponseDto>>.Success("Successfully retrieved drugs by brand.", pagedList, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching drugs by brand ID");
                return  StandardResponse<PagedList<DrugResponseDto>>.Failed("An error occurred while processing your request. Please try again later.", 500);
            }
        }
    
   


    public async Task<StandardResponse<PagedList<DrugResponseDto>>> GetDrugsByGenericName(string genericNameId, DrugRequestInputParameter parameter)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetDrugsByGenericNameId(genericNameId, parameter);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                var pagedList = new PagedList<DrugResponseDto>(drugDtos.ToList(),drugs.MetaData.TotalCount,parameter.PageNumber,parameter.PageSize);
                return  StandardResponse<PagedList<DrugResponseDto>>.Success("Successfully retrieved drugs by genericName", pagedList, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by genericName.");
                return StandardResponse<PagedList<DrugResponseDto>>.Failed("An error occurred while getting drugs by genericName.", 500);
            }
        }

        public async Task<StandardResponse<PagedList<DrugResponseDto>>> GetDrugsBySupplier(string supplierId, DrugRequestInputParameter parameter)
        {
            try
            {
                
                var drugFromDb = await _unitOfWork.Drug.GetDrugsBySupplier(supplierId, parameter);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugFromDb);
                var pageList = new PagedList<DrugResponseDto>(drugDtos.ToList(),drugFromDb.MetaData.TotalCount,parameter.PageNumber, parameter.PageSize);
                return StandardResponse<PagedList<DrugResponseDto>>.Success("Successfully retrieved drugs by supplier",pageList, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by supplier.");
                return StandardResponse<PagedList<DrugResponseDto>>.Failed("An error occurred while getting drugs by supplier.", 500);
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
               await  _unitOfWork.SaveAsync();

                var drugDto = _mapper.Map<DrugResponseDto>(drug);
                return StandardResponse<DrugResponseDto>.Success("Successfully created new drug", drugDto, 201);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a drug.");
                return StandardResponse<DrugResponseDto>.Failed("Error occurred while creating the drug.", 500);
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

        public async Task<StandardResponse<DrugResponseDto>> GetDrugById(string id)
        {
            try
            {
                var drug = await _unitOfWork.Drug.GetDrugById(id);
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

        public async Task<StandardResponse<string>> DeleteDrug(string id)
        {
            try
            {
                var drug = await _unitOfWork.Drug.GetDrugById(id);
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

        public async Task<StandardResponse<DrugResponseDto>> UpdateDrug(string id, DrugRequestDto drugRequestDto)
        {
            try
            {
                var existingDrug = await _unitOfWork.Drug.GetDrugById(id);
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
        //public async Task CheckAndSendLowQuantityNotificationsAsync()
        //{

        //    var lowQuantityDrugs = await _unitOfWork.Drug.GetLowQuantityDrugsAsync(5);

        //    foreach (var drug in lowQuantityDrugs)
        //    {
        //        var message = $"Low stock alert for drug '{drug.Id}'. Current quantity: {drug.Quantity}";
        //        await _emailService.SendEmailAsync("kemifolami222444@gmail.com", "Low Stock Alert", message);
        //    }
        //}

        //public async Task CheckAndSendExpiringDrugNotificationsAsync()
        //{
        //    // Calculate the date one month from now
        //    var oneMonthFromNow = DateTime.Now.AddMonths(1);

        //    var expiringDrugs = await _unitOfWork.Drug.GetExpiringDrugsAsync(oneMonthFromNow);

        //    foreach (var drug in expiringDrugs)
        //    {
        //        var message = $"Expiring drug alert for drug '{drug.Id}'. Expiry date: {drug.ExpireDate}";
        //        await _emailService.SendEmailAsync("kemifolami222444@gmail.com", "Expiring Drug Alert", message);
        //    }
        //}





    }
}

