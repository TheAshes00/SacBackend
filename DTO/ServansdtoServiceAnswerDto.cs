using System.Net.NetworkInformation;

namespace SacBackend.DTO
{
    public class ServansdtoServiceAnswerDto
    {
        //-------------------------------------------------------------------------------------------------------------
        //                                                  //INSTANCE VARIABLES.

        public int intStatus { get; set; }
        public String strUserMessage { get; set; }
        public String strDevMessage { get; set; }
        public Object objResponse { get; set; }
        protected bool boolSuccess
        {
            get
            {
                return this.intStatus == 200;
            }
        }

        //-------------------------------------------------------------------------------------------------------------
        public ServansdtoServiceAnswerDto(
            int intStatus_I,
            Object objResponse_I
            ) 
        {
            this.intStatus = intStatus_I;
            this.strUserMessage = "All good";
            this.strDevMessage = "Completed";
            this.objResponse = objResponse_I;
        }

        //-------------------------------------------------------------------------------------------------------------
        public ServansdtoServiceAnswerDto(

            int intStatus_I,
            String strUserMessage_I,
            String strDevMessage_I,
            Object objResponse_I
            )
        {
            this.intStatus = intStatus_I;
            this.strUserMessage = strUserMessage_I;
            this.strDevMessage = strDevMessage_I;
            this.objResponse = objResponse_I;
        }
    

        //-------------------------------------------------------------------------------------------------------------
    }

}

