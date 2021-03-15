using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace news.Infrastructure.Enums
{
    public enum ResultCode
    {
        // success
        [Description("Thành công")]
        Ok = 200,

        // system error
        [Description("Lỗi")]
        ErrorAuthenticationFail = -120,    // lỗi chứng thực  

        [Description("Lỗi")]
        ErrorInputInvalid = -121,          // lỗi input

        [Description("Lỗi")]
        ErrorChecksumFail = -122,          // checksum fail

        [Description("Lỗi")]
        ErrorTokenExpired = -123,          // access-token hết hạn

        [Description("Thông báo bảo trì!")]
        ErrorMaintenance = -199,           // Bảo trì

        // error
        [Description("Lỗi")]
        ErrorFail = -110,                  // lỗi chung business

        [Description("Lỗi")]
        ErrorBusiness_1 = -111,            // lỗi chung business 1

        [Description("Lỗi")]
        ErrorException = -130,             // Lỗi Exception

        [Description("Lỗi")]
        ErrorNotFound = -140,              // Lỗi không tìm thấy dữ liệu

        [Description("Thông báo")]
        ErrorNoContent = -141,              // Không có data

        // warning
        [Description("Thông báo")]
        Warning = -311
    }
}
