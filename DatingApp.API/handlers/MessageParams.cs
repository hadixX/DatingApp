using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.handlers
{
    public class MessageParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pSize = 10;
        public int PageSize
        {
            get { return pSize; }
            set { pSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int Id { get; set; }
        public string MessageContainer { get; set; } = "Unread";
    }
}
