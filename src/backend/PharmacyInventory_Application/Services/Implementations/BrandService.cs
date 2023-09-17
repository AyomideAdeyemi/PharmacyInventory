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
using PharmacyInventory_Shared.RequestParameter.ModelParameter;

namespace PharmacyInventory_Application.Services.Implementations
{
    public sealed class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Brand> _logger;
        private readonly IMapper _mapper;


        public BrandService(IUnitOfWork unitOfWork, ILogger <Brand>logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<StandardResponse<BrandResponseDto>> CreateBrandAsync(BrandRequestDto brandRequestDto)
        {
            var brand = _mapper.Map<Brand>(brandRequestDto);
            await _unitOfWork.Brand.Create(brand);
             _unitOfWork.SaveAsync();
            var brandDto = _mapper.Map<BrandResponseDto>(brand);
            return StandardResponse<BrandResponseDto>.Success("Successfully created new brand", brandDto, 201);

        }

        public async Task<StandardResponse<string>> DeleteBrand(string id)
        {
            try
            {
                var brand = await _unitOfWork.Brand.GetBrandById(id);
                if (brand == null)
                {
                    return StandardResponse<string>.Failed("Brand not found.", 404);
                }

                _unitOfWork.Brand.Delete(brand);
                _unitOfWork.SaveAsync();

                return StandardResponse<string>.Success("Brand deleted successfully.", "Deleted", 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the brand.");
                return StandardResponse<string>.Failed("An error occurred while deleting the brand.", 500);
            }
        }

        public async Task<StandardResponse<BrandResponseDto>> UpdateBrand(string id, BrandRequestDto brandRequestDto)
        {
            try
            {
                var existingBrand = await _unitOfWork.Brand.GetBrandById(id);
                if (existingBrand == null)
                {
                    return StandardResponse<BrandResponseDto>.Failed("Brand not found.", 404);
                }

                // Update existing drug properties with new data
                _mapper.Map(brandRequestDto, existingBrand);

                _unitOfWork.Brand.Update(existingBrand);
                _unitOfWork.SaveAsync();

                var updatedBrandDto = _mapper.Map<BrandResponseDto>(existingBrand);
                return StandardResponse<BrandResponseDto>.Success("brand updated successfully.", updatedBrandDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the brand.");
                return StandardResponse<BrandResponseDto>.Failed("An error occurred while updating the brand.", 500);

            }
        }

        public async Task<StandardResponse<BrandResponseDto>> GetBrandById(string id)
        {
            try
            {
                var brand = await _unitOfWork.Brand.GetBrandById(id);
                if (brand == null)
                {
                    return StandardResponse<BrandResponseDto>.Failed("Brand not found.", 404);
                }
                var brandDto = _mapper.Map<BrandResponseDto>(brand);
                return StandardResponse<BrandResponseDto>.Success("Successfully retrieved brand by ID.", brandDto, 200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting brand by ID.");
                return StandardResponse<BrandResponseDto>.Failed("An error occurred while getting brand by ID.", 500);
            }
        }

        public async Task<StandardResponse<(IEnumerable<BrandResponseDto>, MetaData)>> GetAllBrands()
        {
            
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

    }
}