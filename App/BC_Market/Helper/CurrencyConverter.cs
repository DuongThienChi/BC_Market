

namespace BC_Market.Converter
{
    /// <summary>
    /// Provides methods for converting currency values.
    /// </summary>
    public static class CurrencyConverter
    {
        /// <summary>
        /// The exchange rate from USD to VND.
        /// </summary>
        private const float ExchangeRate = 23000; // Tỉ giá USD sang VNĐ, bạn có thể cập nhật tỉ giá này theo thực tế

        /// <summary>
        /// Converts a value in USD to VND.
        /// </summary>
        /// <param name="usd">The amount in USD to convert.</param>
        /// <returns>The equivalent amount in VND.</returns>
        public static int ConvertUsdToVnd(float usd)
        {
            // Chuyển đổi USD sang VNĐ và làm tròn xuống để đảm bảo VNĐ là số nguyên
            return (int)(usd * ExchangeRate);
        }
    }
}
