namespace DatingApp.API.handlers
{
    public class UserParam
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pSize = 10;
        public int PageSize
        {
            get { return pSize; }
            set { pSize = (value > MaxPageSize) ? MaxPageSize: value; }
        }
        
    }
}