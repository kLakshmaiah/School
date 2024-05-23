namespace School.Models
{
    public class Pagging
    {
        public int TotalPages { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? ActionName { get; set; }

        public int? StartPage { get; set; } 
        //public int? Total { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int? CurrentPage = 1;
        public bool? IsFirstPage = false;

        public bool? IsLastPage = false;

        public bool? Disable = false;

        public string? ActionName1 { get;set; }
        public Pagging() 
        {
            
        }

        public Pagging(int totalPages, string? actionName, int? currentPage)
        {
            TotalPages = totalPages;
            StartPage = currentPage;
            ActionName = actionName;
            CurrentPage = currentPage;
            FirstPage = 1;
            LastPage = totalPages;

            if (CurrentPage<=FirstPage)
            {
                IsFirstPage = true;
            }
            else
            {
                IsFirstPage = false;
            }

            if (CurrentPage >=totalPages)
            {
                IsLastPage = true;
            }
            else
            {
                IsLastPage = false;
            }
        }



        //public async Task PreviousPage(int? currentPage)
        //{
        //    if (currentPage < TotalPages)
        //    {
        //        if (currentPage == FirstPage)
        //        {
        //            currentPage = FirstPage;
        //            IsFirstPage = true;
        //        }
        //        else if (currentPage == LastPage)
        //        {
        //            currentPage = LastPage;
        //            IsLastPage = true;
        //        }
        //        else
        //        {
        //            currentPage = currentPage - 1;
        //        }
        //    }
        //}

        //public async Task NextPage(int? currentPage)
        //{
        //    if (currentPage <= TotalPages)
        //    {
        //        if (currentPage == FirstPage)
        //        {
        //            currentPage = FirstPage;
        //            IsFirstPage = true;
        //        }
        //        else if (currentPage == LastPage)
        //        {
        //            currentPage = LastPage;
        //            IsLastPage = true;
        //        }
        //        else
        //        {
        //            currentPage = currentPage + 1;
        //        }
        //    }
        //}

        //public void CurrentPage()
        //{

        //}

        //public async Task Page(int? currentPage)
        //{
        //    if (currentPage == FirstPage)
        //    {
        //        currentPage = FirstPage;
        //        IsFirstPage = true;
        //    }
        //    else if (currentPage == LastPage)
        //    {
        //        currentPage = LastPage;
        //        IsLastPage = true;
        //    }
        //    else
        //    {
        //        CurrentPage = currentPage;
        //    }
        //}
    }
}
