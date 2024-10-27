namespace SacBackend.DTO.TutorWorkshop
{
    public class GettutworhouGetTutorWorkshopsHourDto
    {
        public class Out
        {
            public bool boolLimitExceed {  get; set; }
            public string strMessage {  get; set; }
            public Tutor[] arrTutor { get; set; }
            public Workshop[] arrWorkshops { get; set; }
            
            public class Tutor
            {
                public int intPk;
                public string strName;
                public string strHour;
            }

            public class Workshop
            {
                public int intPk;
                public string strWorkshop;
            }
        }

    }
}
