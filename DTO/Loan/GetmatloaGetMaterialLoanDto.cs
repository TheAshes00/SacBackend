namespace SacBackend.DTO.Loan
{
    public class GetmatloaGetMaterialLoanDto
    {
        public class Out
        {
            public string strLoanDate { get; set; }
            public int intPkLoan { get; set; }
            public Material[] arrobjMaterial { get; set; }


            public class Material
            {
                public string strPkMaterial { get; set; }
                public string strMaterialName { get; set; }
            }

        }
    }
}
