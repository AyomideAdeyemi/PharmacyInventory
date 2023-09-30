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
    public sealed class SupplierService : ISupplierService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Supplier> _logger;
        private readonly IMapper _mapper;



        public SupplierService(IUnitOfWork unitOfWork, ILogger<Supplier> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }
       

        public async Task<StandardResponse<SupplierResponseDto>> CreateSupplierAsync(SupplierRequestDto supplierRequestDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierRequestDto);
            await _unitOfWork.Supplier.Create(supplier);
            await _unitOfWork.SaveAsync();
            var supplierDto = _mapper.Map<SupplierResponseDto>(supplier);
            return StandardResponse<SupplierResponseDto>.Success("Successfully created new supplier", supplierDto, 201);

        }

        public async Task<StandardResponse<PagedList<SupplierResponseDto>>> GetAllSuppliers(SupplierRequestInputParameter parameter)
        {
            try
            {
                var suppliers = await _unitOfWork.Supplier.GetAllSupplier(parameter);
                var suppliersDtos = _mapper.Map<IEnumerable<SupplierResponseDto>>(suppliers);
                var pageList = new PagedList<SupplierResponseDto>(suppliersDtos.ToList(), suppliers.MetaData.TotalCount, parameter.PageNumber, parameter.PageSize);
                return StandardResponse<PagedList<SupplierResponseDto>>.Success("Successfully retrieved all suppliers", pageList, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all suppliers.");
                return StandardResponse<PagedList<SupplierResponseDto>>.Failed("An error occurred while getting all suppliers.", 500);
            }
        }


        public async Task<StandardResponse<string>> DeleteSupplier(string id)
        {
            try
            {
                var supplier = await _unitOfWork.Supplier.GetSupplierById(id);
                if (supplier == null)
                {
                    return StandardResponse<string>.Failed("Supplier not found.", 404);
                }

                _unitOfWork.Supplier.Delete(supplier);
                _unitOfWork.SaveAsync();

                return StandardResponse<string>.Success("Supplier deleted successfully.", "Deleted", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the supplier.");
                return StandardResponse<string>.Failed("An error occurred while deleting the supplier.", 500);
            }
        }

        public async Task<StandardResponse<SupplierResponseDto>> UpdateSupplier(string id, SupplierRequestDto supplierRequestDto)
        {
            try
            {
                var existingSupplier = await _unitOfWork.Supplier.GetSupplierById(id);
                if (existingSupplier == null)
                {
                    return StandardResponse<SupplierResponseDto>.Failed("Supplier not found.", 404);
                }

                // Update existing drug properties with new data
                _mapper.Map(supplierRequestDto, existingSupplier);

                _unitOfWork.Supplier.Update(existingSupplier);
                _unitOfWork.SaveAsync();

                var updatedSupplierDto = _mapper.Map<SupplierResponseDto>(existingSupplier);
                return StandardResponse<SupplierResponseDto>.Success("supplier updated successfully.", updatedSupplierDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the supplier.");
                return StandardResponse<SupplierResponseDto>.Failed("An error occurred while updating the supplier.", 500);

            }
        }

        public async Task<StandardResponse<SupplierResponseDto>> GetSupplierById(string id)
        {
            try
            {
                var supplier = await _unitOfWork.Supplier.GetSupplierById(id);
                if (supplier == null)
                {
                    return StandardResponse<SupplierResponseDto>.Failed("Supplier not found.", 404);
                }
                var supplierDto = _mapper.Map<SupplierResponseDto>(supplier);
                return StandardResponse<SupplierResponseDto>.Success("Successfully retrieved supplier by ID.", supplierDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting supplier by ID.");
                return StandardResponse<SupplierResponseDto>.Failed("An error occurred while getting supplier by ID.", 500);
            }
        }
    }
}
