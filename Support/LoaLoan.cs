using System;
using SacBackend.Context;
using SacBackend.DAO;
using SacBackend.DTO;
using SacBackend.DTO.Loan;
using SacBackend.Models;

namespace SacBackend.Support
{
    public class LoaLoan
    {
        //--------------------------------------------------------------------------------
        public static void subRegisterLoan(
            CaafiContext context_M,
            SetloaregSetLoanRegisterDto.In setloaregin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if (
                boolValidateInfo(context_M, setloaregin_I)
                )
            {
                DateTime dateTime = DateTime.Now;

                int intLoanPk = intSetLoan(context_M, setloaregin_I, dateTime);

                subSetMaterialLoan(context_M, setloaregin_I.arrstrNumContInt, intLoanPk);

                servans_O = new(200, "Loan succesfully registered", "Ok", setloaregin_I);
            }
            else
            {
                servans_O = new(400, "Provided info is wrong",
                    "One or more Pk are not valid", setloaregin_I);
            }

        }

        //--------------------------------------------------------------------------------
        private static void subSetMaterialLoan(
            CaafiContext context_M,
            string[] arrstrNumContInt_I,
            int intPkLoan_I
            )
        {
            for (int intIndex = 0; intIndex < arrstrNumContInt_I.Length; intIndex++)
            {
                MaterialLoan matloaentity = new();
                matloaentity.intPkLoan = intPkLoan_I;
                matloaentity.strPkMaterial = arrstrNumContInt_I[intIndex];

                MatLoaMaterialLoanDao.subAdd(context_M, matloaentity);
            }
        }

        //--------------------------------------------------------------------------------
        private static int intSetLoan(
            CaafiContext context_M,
            SetloaregSetLoanRegisterDto.In setloaregin_I,
            DateTime date_I
            )
        {
            Loan loanentity = new Loan();

            loanentity.strStudyArea = "Estudio Libre";
            loanentity.strPkStudent = setloaregin_I.strNmCta;
            loanentity.intPkAdminLend = setloaregin_I.intAdminPk;
            loanentity.intPkAdminRecieve = 3;
            loanentity.LoanDate = date_I.Date;
            loanentity.TimeStart = date_I.TimeOfDay;

            LoaLoanDao.subAdd(context_M, loanentity);

            return loanentity.intPk;
        }

        //--------------------------------------------------------------------------------
        private static bool boolValidateInfo(
            CaafiContext context_I,
            SetloaregSetLoanRegisterDto.In setloaregin_I
            )
        {
            bool boolIsValid = false;

            if (
                AdmAdminDao.boolValidatePk(context_I, setloaregin_I.intAdminPk) &&
                StudaoStudentDao.boolValidatePk(context_I, setloaregin_I.strNmCta) &&
                MatdaoMaterialDao.boolValidateMaterialPk(
                    context_I, setloaregin_I.arrstrNumContInt)
                )
            {
                boolIsValid = true;
            }

            return boolIsValid;

        }

        //--------------------------------------------------------------------------------
        public static void subReturnMaterial(
            CaafiContext context_M,
            string strNmCta_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            List<GetmatloaGetMaterialLoanDto.Out> arrloaentity = subGetMaterialLoan(
                context_M, strNmCta_I);

            servans_O = new(200, arrloaentity);
        }

        //--------------------------------------------------------------------------------

        private static List<GetmatloaGetMaterialLoanDto.Out> subGetMaterialLoan(
            CaafiContext context_I,
            string strNmCta_I
            )
        {
            return context_I.Loan.
                Where(loan => loan.strPkStudent == strNmCta_I &&
                    loan.intPkAdminRecieve == 3)
                .Select(matloan => new GetmatloaGetMaterialLoanDto.Out
                {
                    intPkLoan = matloan.intPk,
                    strLoanDate = matloan.LoanDate.Date.ToString("dd-MM-yyyy"),
                    arrobjMaterial = matloan.IcMaterialLoanEntity.Select(
                        ml => new GetmatloaGetMaterialLoanDto.Out.Material
                        {
                            strPkMaterial = ml.strPkMaterial,
                            strMaterialName = ml.MaterialEntity.strName
                        }
                    ).ToArray()
                }).ToList();
        }

        //--------------------------------------------------------------------------------
        public static void subReturnmaterial(
            CaafiContext context_M,
            SetloaregSetLoanRegisterDto.In setloareg_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            DateTime dateTime = DateTime.Now;

            foreach (int intPk in setloareg_I.arrintPKLoan)
            {
                Loan loaentity = LoaLoanDao.subGetByPk(context_M, intPk);

                loaentity.intPkAdminRecieve = setloareg_I.intAdminPk;
                loaentity.TimeEnd = dateTime.TimeOfDay;

                LoaLoanDao.subUpdate(context_M, loaentity);
            }

            servans_O = new ServansdtoServiceAnswerDto(200, null);

        }

        //--------------------------------------------------------------------------------
    }
}
