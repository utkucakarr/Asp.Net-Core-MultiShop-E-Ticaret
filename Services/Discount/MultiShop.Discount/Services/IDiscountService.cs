using MultiShop.Discount.Dtos;

namespace MultiShop.Discount.Services
{
    public interface IDiscountService
    {
        Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync();

        Task CreateDiscountCouponAsync(CreateDiscountCouponDto createCouponDto);

        Task UpdateDiscountCouponAync(UpdateDiscountCouponDto updateCouponDto);

        Task DeleteDiscountCouponAsync(int couponId);

        Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int c23z12ouponId);
    }
}
