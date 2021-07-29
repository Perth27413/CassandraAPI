using System;
using System.Collections.Generic;

namespace CassandraAPI.Models
{
    public class PageObjectResponse<T>
    {
        public bool isFirst { get; set; }
        public bool isLast { get; set; }
        public int totalPage { get; set; }
        public int totalElement { get; set; }
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public List<T> entities { get; private set; }
        public DateTime? minDate { get; set; }
        public DateTime? maxDate { get; set; }

        public PageObjectResponse(List<T> entities, int size = 0, int num = 0, int totalelement = 0, DateTime? minDate = null, DateTime? maxDate = null)
        {
            this.pageSize = 8;
            if (size != 0)
            {
                this.pageSize = size;
            }

            this.pageNumber = 1;
            if (num != 0)
            {
                this.pageNumber = num;
            }

            this.totalElement = totalelement;
            this.totalPage = (int)Math.Ceiling(this.totalElement / Convert.ToDouble(this.pageSize));
            this.isFirst = this.pageNumber == 1;
            this.isLast = this.totalPage == this.pageNumber || this.totalPage == 0;
            this.entities = entities;
            this.minDate = minDate;
            this.maxDate = maxDate;
        }
    }
    public class PageQuery
    {
        public int totalcount { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public PageQuery(int number, int size)
        {
            this.pageNumber = number <= 0 ? 1 : number;
            this.pageSize = size <= 0 ? 8 : size;
            this.totalcount = 0;
        }
        public void setTotalCount(int total)
        {
            this.totalcount = total;
        }
    }
}