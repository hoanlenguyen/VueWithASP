namespace webapi.Paging
{
    public abstract class BaseFilterDto
    {
        public string? Keyword { get; set; }
        public bool? Status { get; set; }
        public int? Page { get; set; }
        public int? RowsPerPage { get; set; }
        public int SkipCount => ((Page ?? 1) - 1) * (RowsPerPage ?? 0);
        public string? SortBy { get; set; } = "Id";
        public string? SortDirection { get; set; } = "desc";
    }
}
