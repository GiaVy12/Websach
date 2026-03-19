using System;

namespace storesach.Helpers
{
    public static class ValidationHelper
    {
        // Kiểm tra chuỗi có đủ độ dài tối thiểu
        public static bool MinLength(string input, int length = 5)
        {
            return !string.IsNullOrEmpty(input) && input.Length >= length;
        }
    }
}
