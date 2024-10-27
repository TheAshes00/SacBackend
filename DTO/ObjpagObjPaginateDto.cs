namespace SacBackend.DTO
{
    public class ObjpagObjPaginateDto
    {
        public class Out
        {
            public int intTotalCount { get; set; }
            public int intPageNumber { get; set; }
            public int intPageSize { get; set; }
            public Object objPaginatedObject {  get; set; }
        }
    
    }
}
