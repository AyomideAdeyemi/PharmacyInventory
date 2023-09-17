using AutoMapper;
using Microsoft.Extensions.Logging;
using PharmacyInventory_Application.Services.Interfaces;
using PharmacyInventory_Domain.Dtos;
using PharmacyInventory_Domain.Dtos.Requests;
using PharmacyInventory_Domain.Dtos.Responses;
using PharmacyInventory_Domain.Entities;
using PharmacyInventory_Infrastructure.Repository.Abstractions;
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
       // private readonly IEmail _emailService;/// <summary>
        
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="email"></param>
        




        public DrugService(IUnitOfWork unitOfWork, ILogger<Drug> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            
        }
        //public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetAllDrugs()
        //{
        //    try
        //    {
        //        var parameter = new DrugRequestInputParameter();
        //        var drugs = await _unitOfWork.Drug.GetAllDrugs(parameter);

        //        if (drugs == null)
        //        {
        //            // Return an error response if the database is null
        //            return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>
        //                .Failed("Database is not available.", 500);
        //        }

        //        //var parameter = new DrugRequestInputParameter();
        //        //var drugs = await _unitOfWork.Drug.GetAllDrugs(parameter);

        //        if (drugs == null)
        //        {
        //            // Return an error response if the result is null
        //            return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>
        //                .Failed("No drugs found.", 404);
        //        }

        //        var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);

        //        var metaData = drugs.MetaData; // Assuming MetaData is a property of the drugs collection.

        //        return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>
        //            .Success("Successfully retrieved all drugs", (drugDtos, metaData), 200);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while getting all drugs.");
        //        return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>
        //            .Failed("An error occurred while getting all drugs.", 500);
        //    }
        //}
        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetAllDrugs()
        {
            var parameter = new DrugRequestInputParameter();
            try
            {
                
                var drugs = await _unitOfWork.Drug.GetAllDrugs();
                var drugsDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved all drugs", (drugsDtos, drugs.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all drugs.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting all drugs.", 500);
            }
        }

        public async Task<StandardResponse<(IEnumerable<BrandResponseDto>, MetaData)>> GetAllBrands()
        {
            var parameter = new BrandRequestInputParameter();
            try
            {
                var brands = await _unitOfWork.Brand.GetAllBrands();
                var brandDtos = _mapper.Map<IEnumerable<BrandResponseDto>>(brands);
                return StandardResponse<(IEnumerable<BrandResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved all brands", (brandDtos, brands.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all brands.");
                return StandardResponse<(IEnumerable<BrandResponseDto>, MetaData)>.Failed("An error occurred while getting all brand.", 500);
            }
        }

        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByBrand(string brandId)
        {
            try
            { 
                var drugsFromDb = await _unitOfWork.Drug.GetDrugsByBrandId(brandId);
                var drugDto = _mapper.Map<IEnumerable<DrugResponseDto>>(drugsFromDb);

                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Success("Successfully retrieved drugs by brand", (drugDto, drugsFromDb.MetaData), 200);
            }
             catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by supplier.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting drugs by supplier.", 500);
            }
        }

       
        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByGenericName(string genericNameId)
        {
            try
            {
              // var parameter = new GenericNameRequestInputParameter();
                var drugs = await _unitOfWork.Drug.GetDrugsByGenericNameId(genericNameId);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugs);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved drugs by genericName", (drugDtos, drugs.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by genericName.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting drugs by genericName.", 500);
            }
        }

        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsBySupplier(string supplierId)
        {
            try
            {
                
                var drugFromDb = await _unitOfWork.Drug.GetDrugsBySupplier(supplierId);
                var drugDtos = _mapper.Map<IEnumerable<DrugResponseDto>>(drugFromDb);
                return StandardResponse<(IEnumerable<DrugResponseDto> _contact, MetaData pagingData)>.Success("Successfully retrieved drugs by supplier", (drugDtos, drugFromDb.MetaData), 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting drugs by supplier.");
                return StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>.Failed("An error occurred while getting drugs by supplier.", 500);
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


      

      
       
        public async Task<StandardResponse<(IEnumerable<DrugResponseDto>, MetaData)>> GetDrugsByExpiryDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var drugs = await _unitOfWork.Drug.GetDrugsByExpiryDateRange(startDate, endDate);
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

        public async Task<StandardResponse<string>> DeleteDrug(string id)
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

        public async Task<StandardResponse<DrugResponseDto>> UpdateDrug(string id, DrugRequestDto drugRequestDto)
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

