

namespace BC_Market.Converter
{
    public static class CurrencyConverter
    {
        private const float ExchangeRate = 23000; // Tỉ giá USD sang VNĐ, bạn có thể cập nhật tỉ giá này theo thực tế

        public static int ConvertUsdToVnd(float usd)
        {
            // Chuyển đổi USD sang VNĐ và làm tròn xuống để đảm bảo VNĐ là số nguyên
            return (int)(usd * ExchangeRate);
        }
    }
}
