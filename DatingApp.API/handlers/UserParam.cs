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

        public int Id { get; set; }
        public string Gender { get; set; }
        public int minAge { get; set; } = 18;
        public int maxAge { get; set; } = 99;
        public string orderBy { get; set; }/***/

    }
}