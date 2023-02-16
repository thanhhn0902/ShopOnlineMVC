using Hiephashop.Ultility.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Hiephashop.Ultility.Common
{
    public static class CommonFunction
    {
        public static string CheckStatusCRUD(StatusCRUD status)
        {
            switch (status)
            {
                case StatusCRUD.Duplicate:
                    return "Dữ liệu đã tồn tại";
                case StatusCRUD.NotFound:
                    return "Không tìm thấy dữ liệu";
                case StatusCRUD.Error:
                    return "Có lỗi nào đó";
                default: return string.Empty;
            }
        }

        public static string FormatCurrency(string money)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string currency = double.Parse(money).ToString("#,###", cul.NumberFormat);
            return currency+" VNĐ";
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
