namespace SacBackend.DTO.Admin
{
    public class GetrepbystuGetReportByStudent
    {
        public class Out
        {
            public string strStudentName { get; set; }
            public string strCourse { get; set; }
            public LoanReport[] arrLoanReport { get; set; }
            public WorkshopReport[] arrWorkshopReport { get; set; }
            public class LoanReport
            {
                public string strDate { get; set; }
                public string strHour { get; set; }
                public string[] arrstrMaterial { get; set; }
            }

            public class WorkshopReport 
            {
                public string strDate { get; set; }
                public string strHour { get; set; }
                public string strWorkshopName {  get; set; }
                public string strTutorName { get; set; }

            }
        }
    }
}
