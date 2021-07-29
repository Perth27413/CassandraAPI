using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CassandraAPI.Models;

namespace CassandraAPI.BussinessLogic
{
    public class ValidationLogic
    {
        public static void ValidateNotNull(string value)
        {
            if (value == null || value == "")
            {
                throw new ValidationException("กรุณาใส่ข้อมูล");
            }
        }

        public static void ValidateListNotNull(List<string> value)
        {
            if (value.Count == 0)
            {
                throw new ValidationException("กรุณาใส่ข้อมูล");
            }
        }
        public static void ValidatePage(PageRequest page)
        {
            if (page == null || page.pageNumber <= 0 || page.pageSize <= 0)
            {
                throw new ValidationException("กรุณาใส่ pageNumber และ pageSize");
            }
            if (page.pageSize > 1000)
            {
                throw new ValidationException("PageSize can't more than 1000");
            }
        }
    }
}
