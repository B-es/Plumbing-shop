using System;

namespace Plumbing_shop.Models
{
    public class PageViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }
        public int StartLoop = 0;
        public int EndLoop = 6;

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            
            if (PageNumber == TotalPages)
            {
                StartLoop += (PageNumber - 1) * 6;
                EndLoop = StartLoop + count - StartLoop;
            }
            else
            {
                StartLoop += (PageNumber - 1) * 6;
                EndLoop = StartLoop + 6;
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }


    }
}