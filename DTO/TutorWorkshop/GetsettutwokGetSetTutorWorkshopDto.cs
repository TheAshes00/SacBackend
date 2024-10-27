namespace SacBackend.DTO.TutorWorkshop
{
    public class GetsettutwokGetSetTutorWorkshopDto
    {
        public int? intPk {get; set;}
        public int intPkTutor {get; set;}
        public string? strTutorName {get; set;}
        public int intPkWorkshop {get; set;}
        public string? strWorkshop {get; set;}
        public TimeSpan timeStartHour {get; set;}
        public TimeSpan timeFinishHour {get; set;}
        public bool boolActive {get; set;}
    }
}
