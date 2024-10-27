using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using SacBackend.Context;
using SacBackend.DAO;
using SacBackend.DTO;
using SacBackend.Models;
using SacBackend.Tools;

namespace SacBackend.Support
{
    public class TutTutor
    {
        //--------------------------------------------------------------------------------
        public static void GetPaginatedTutors(
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

            List<Tutor> arrtutentity = TutTutorDao.tutGetAllTutor(context_I);

            int intTotalTutors = arrtutentity.Count;

            List<Tutor> darrPaginatedTutors;
            if (
                strSearch_I.IsNullOrEmpty()
                )
            {
                darrPaginatedTutors = arrtutentity
                    .OrderByDescending(t => t.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();
            }
            else
            {
                List<Tutor> darrtutFilteredTutor = arrtutentity
                    .Where(t => t.strName.Contains(strSearch_I,
                        StringComparison.CurrentCultureIgnoreCase)
                    ).ToList();


                intTotalTutors = darrtutFilteredTutor.Count;

                darrPaginatedTutors = darrtutFilteredTutor
                    .OrderByDescending(t => t.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();
            }

            servans_O = new ServansdtoServiceAnswerDto(200,
                Tools.Auxiliar.Paginate.objpagoutPaginateEntity(intTotalTutors, intPageNumber_I,
                intPageSize_I, darrPaginatedTutors));
        }

        //--------------------------------------------------------------------------------
        public static void subAddNewTutor(
            CaafiContext context_I,
            GetsettutGetSetTutorDto.In getsettutin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if (
                getsettutin_I.intnPk != null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should be null",
                    getsettutin_I);
            }
            else
            {
                Tutor tutentity = new();

                tutentity.strSurename = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsettutin_I.strSurename);

                tutentity.strBachelors = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsettutin_I.strBachelors);

                tutentity.boolActive = true;
                tutentity.byteGender = getsettutin_I.byteGender;
                tutentity.strIdentification = getsettutin_I.strIdentification;
                tutentity.strName = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsettutin_I.strName);

                TutTutorDao.subAdd(context_I, tutentity);

                servans_O = new(200, null);
            }

        }

        //--------------------------------------------------------------------------------
        public static void subUpdateTutor(
            CaafiContext context_I,
            GetsettutGetSetTutorDto.In getsettutin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            Tutor? tutentity = TutTutorDao.tutGetTutorByPk(
                context_I, getsettutin_I.intnPk ?? 0);
            if (
                getsettutin_I.intnPk == null ||
                tutentity == null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should not be null",
                    getsettutin_I);
            }
            else
            {

                tutentity.strSurename = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsettutin_I.strSurename);

                tutentity.strBachelors = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsettutin_I.strBachelors);

                tutentity.boolActive = getsettutin_I.boolActive;
                tutentity.byteGender = getsettutin_I.byteGender;
                tutentity.strIdentification = getsettutin_I.strIdentification;

                tutentity.strName = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsettutin_I.strName);

                TutTutorDao.subUpdate(context_I, tutentity);

                TutworTutorWorkshop.subExcecuteUpdate(context_I, null, tutentity);

                servans_O = new(200, null);
            }

        }

        //--------------------------------------------------------------------------------
        public static void subGetAllActiveTutor(
            CaafiContext context_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            List<Tutor> darrtutentity = TutTutorDao.tutGetAllactiveTutor(context_I);

            servans_O = new(200, darrtutentity);
        }

        //--------------------------------------------------------------------------------
        public static void subGetAllTutor(
            CaafiContext context_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            List<Tutor> darrtutentity = TutTutorDao.tutGetAllTutor(context_I);

            servans_O = new(200, darrtutentity);
        }

        //--------------------------------------------------------------------------------

    }
}
