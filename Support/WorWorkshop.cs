using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using SacBackend.Context;
using SacBackend.DAO;
using SacBackend.DAO.Interfaces;
using SacBackend.DTO;
using SacBackend.DTO.Workshop;
using SacBackend.DTO.WorkshopAttendance;
using SacBackend.Models;
using SacBackend.Tools;

namespace SacBackend.Support
{
    public class WorWorkshop
    {
        //--------------------------------------------------------------------------------
        public static void subAddNewWorkshop(
            CaafiContext context_I,
            GetSetWorkshopDto.In getsetworin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if( 
                getsetworin_I.intnPk != null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should be null", 
                    getsetworin_I);
            }
            else
            {
                Workshop worentity = new();

                worentity.strWorkshop = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsetworin_I.strWorkshop);
                worentity.boolActive = true;

                WorintWorkshopInterface worint = new WordaoWorkshopDao();

                worint.subAddWorkshop(context_I, worentity);

                servans_O = new(200, null);
            }
        }

        //--------------------------------------------------------------------------------
        public static void subUpdate(
            CaafiContext context_I,
            GetSetWorkshopDto.In getsetworin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            WordaoWorkshopDao worint = new WordaoWorkshopDao();
            Workshop? worentity = worint.stuGetWorkshopByPk(context_I, getsetworin_I.intnPk ?? 0);
            if (
                getsetworin_I.intnPk == null ||
                worentity == null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should not be null or inexistent intnPk",
                    getsetworin_I);
            }
            else
            {

                worentity.strWorkshop = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsetworin_I.strWorkshop);
                worentity.boolActive = getsetworin_I.boolActive;

                worint.subUpdateWorkshop(context_I, worentity);

                TutworTutorWorkshop.subExcecuteUpdate(context_I, worentity, null);

                servans_O = new(200, null);
            }
        }

        //--------------------------------------------------------------------------------
        public static void subGetPaginatedWorkshops(
            CaafiContext context_I,
            int intPageNumber_I,
            int intPageSize_I,
            string? strSearch_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            WordaoWorkshopDao worint = new();

            //                                              // Skip represents the number
            //                                              // adn positon of the registers
            //                                              // that will be 
            //                                              // shown, based on th page 
            //                                              // number, and page size
            int intSkip = (intPageNumber_I - 1) * intPageSize_I;

            List<Workshop> arrworentity = worint.darrGetAllWorkshops(context_I);

            int intTotalWorkshops = arrworentity.Count;

            List<Workshop> darrPaginatedWorkshops;

            if (
                strSearch_I.IsNullOrEmpty()
                )
            {
                darrPaginatedWorkshops = arrworentity
                    .OrderByDescending(w => w.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();
            }
            else
            {

                List<Workshop> darrworentityFilteredWorkshops = arrworentity
                    .Where(cw => cw.strWorkshop.Contains(strSearch_I,
                        StringComparison.CurrentCultureIgnoreCase)
                    ).ToList();


                intTotalWorkshops = darrworentityFilteredWorkshops.Count;

                darrPaginatedWorkshops= darrworentityFilteredWorkshops
                    .OrderByDescending(w => w.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();

            }

            servans_O = new ServansdtoServiceAnswerDto(200,
                Tools.Auxiliar.Paginate.objpagoutPaginateEntity(intTotalWorkshops, 
                intPageNumber_I,intPageSize_I, darrPaginatedWorkshops));
        }

        //--------------------------------------------------------------------------------
        public static void subGetAllActiveWorkshops(
            CaafiContext context_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            List<Workshop> darrworkentity = context_I.Workshop.Where(
                t => t.boolActive).ToList();

            servans_O = new(200, darrworkentity);
        }

        //--------------------------------------------------------------------------------
        public static void subSetWorkshopAttendance(
            CaafiContext context_I,
            GetsetworattGetSetWorkshopAttendanceDto.In getsetworatt_I,
            DateTime dateNow_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if (
                StudaoStudentDao.boolValidatePk(context_I, getsetworatt_I.strNmCta) &&
                TutworTutorWorkshopDao.boolValidatePk(
                    context_I, getsetworatt_I.intPkTutorWorkshop)
                )
            {
                WorkshopAttendance worattentity = new();

                worattentity.intPkTutorWorkshop = getsetworatt_I.intPkTutorWorkshop;
                worattentity.strPkStudent = getsetworatt_I.strNmCta;
                worattentity.DateWorkshopDate = dateNow_I.Date;
                worattentity.TimeCheckInTime = dateNow_I.TimeOfDay;

                WorattWorkshopAttendanceDao.subAdd(context_I, worattentity);

                servans_O = new(200, null);
            }
            else
            {
                servans_O = new(400, "Invalid data", 
                    "Some of the primary keys do not exist", getsetworatt_I);
            }
        }

        //--------------------------------------------------------------------------------
    }
}
