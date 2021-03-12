namespace DatingApp.API.handlers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemPerPage, int totalItem, int totalPage)
        {
            this.CurrentPage = currentPage;
            this.ItemPerPage = itemPerPage;
            this.TotalItem = totalItem;
            this.TotalPage = totalPage;

        }
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
        public int TotalItem { get; set; }
        public int TotalPage { get; set; }/**/

    }
}