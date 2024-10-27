using System.Globalization;
using SacBackend.DTO;

namespace SacBackend.Tools
{
    public class Auxiliar
    {
        //================================================================================
        public static class Paginate
        {
            //----------------------------------------------------------------------------
            public static ObjpagObjPaginateDto.Out objpagoutPaginateEntity(
                int intTotalCount_I,
                int intPageNumber_I,
                int intPageSize_I,
                Object darrEntity_I
                )
            {
                ObjpagObjPaginateDto.Out objpagObjPaginateDto = new 
                    ObjpagObjPaginateDto.Out
                {
                    intTotalCount = intTotalCount_I,
                    intPageNumber = intPageNumber_I,
                    intPageSize = intPageSize_I,
                    objPaginatedObject = darrEntity_I
                };

                return objpagObjPaginateDto;
            }
        }

        //================================================================================
        public class TextHelper
        {
            public static string strTitleCase(
                string strText_I
                )
            {
                Thread.CurrentThread.CurrentUICulture =
                    Thread.CurrentThread.CurrentCulture;

                TextInfo textinfo = new CultureInfo(
                    Thread.CurrentThread.CurrentUICulture.Name).TextInfo;

                return textinfo.ToTitleCase(strText_I);
            }
        }

        //================================================================================
    }
}
