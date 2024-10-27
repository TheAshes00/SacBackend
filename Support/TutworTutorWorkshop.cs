using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SacBackend.Context;
using SacBackend.DAO;
using SacBackend.DTO;
using SacBackend.DTO.Admin;
using SacBackend.DTO.Material;
using SacBackend.DTO.TutorWorkshop;
using SacBackend.Models;

namespace SacBackend.Support
{
    public class TutworTutorWorkshop
    {
        //--------------------------------------------------------------------------------
        public static void GetPaginatedTutorWorkshop(
            CaafiContext context_I,
            int intPageNumber_I,
            int intPageSize_I,
            string? strSearch_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            //                                              // Skip represents the number
            //                                              // adn positon of the registers
            //                                              // that will be 
            //                                              // shown, based on th page 
            //                                              // number, and page size
            int intSkip = (intPageNumber_I - 1) * intPageSize_I;

            //                                              //Get all tutor workshops
            List<TutorWorkshop> arrtutwokentity = TutworTutorWorkshopDao.
                tutGetAllTutorWorkshop(context_I);

            //                                              //Get total of tutor workshops
            int intTotalTutorWorkshops = arrtutwokentity.Count;

            List<GetsettutwokGetSetTutorWorkshopDto> darrPaginatedTutorWorkshops;

            if (
                //                                          //In case of no string to search
                strSearch_I.IsNullOrEmpty()
                )
            {
                darrPaginatedTutorWorkshops = context_I.TutorWorkshop
                    .Include(tw => tw.TutorEntity)
                    .Include(tw => tw.WorkshopEntity).Select(
                    tw => new GetsettutwokGetSetTutorWorkshopDto
                    {
                        intPk = tw.intPk,
                        intPkTutor = tw.intPkTutor,
                        strTutorName = tw.TutorEntity.strName + " " + tw.TutorEntity.strSurename,
                        intPkWorkshop = tw.intPkWorkshop,
                        strWorkshop = tw.WorkshopEntity.strWorkshop,
                        timeStartHour = tw.TimeStartHour,
                        timeFinishHour = tw.TimeFinishHour,
                        boolActive = tw.boolActive,
                    }
                    ).
                    OrderByDescending(w => w.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();
            }
            else
            {
                List<TutorWorkshop> darrtutwor = context_I.TutorWorkshop
                    .Include(tw => tw.TutorEntity)
                    .Include(tw => tw.WorkshopEntity)
                    .Where(
                        t => t.TutorEntity.strName.ToLower().Contains(strSearch_I.ToLower()) ||
                        t.TutorEntity.strSurename.ToLower().Contains(strSearch_I.ToLower()) ||
                        t.WorkshopEntity.strWorkshop.ToLower().Contains(strSearch_I.ToLower())
                    ).
                    ToList();

                intTotalTutorWorkshops = darrtutwor.Count;

                darrPaginatedTutorWorkshops = darrtutwor.Select(
                    tw => new GetsettutwokGetSetTutorWorkshopDto
                    {
                        intPk = tw.intPk,
                        intPkTutor = tw.intPkTutor,
                        strTutorName = tw.TutorEntity.strName + " " + tw.TutorEntity.strSurename,
                        intPkWorkshop = tw.intPkWorkshop,
                        strWorkshop = tw.WorkshopEntity.strWorkshop,
                        timeStartHour = tw.TimeStartHour,
                        timeFinishHour = tw.TimeFinishHour,
                        boolActive = tw.boolActive,
                    }
                    ).
                    OrderByDescending(w => w.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();

            }

            servans_O = new ServansdtoServiceAnswerDto(200,
                Tools.Auxiliar.Paginate.objpagoutPaginateEntity(intTotalTutorWorkshops,
                intPageNumber_I, intPageSize_I, darrPaginatedTutorWorkshops));
        }

        //--------------------------------------------------------------------------------
        public static void subAddNewTutorWorkshop(
            CaafiContext context_I,
            GetsettutwokGetSetTutorWorkshopDto getsettutwor_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if (
                getsettutwor_I.intPk != null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should be null",
                    getsettutwor_I);
            }
            else
            {
                TutorWorkshop tutworentity = new()
                {
                    intPkTutor = getsettutwor_I.intPkTutor,
                    intPkWorkshop = getsettutwor_I.intPkWorkshop,
                    TimeStartHour = getsettutwor_I.timeStartHour,
                    TimeFinishHour = getsettutwor_I.timeFinishHour,
                    boolActive = getsettutwor_I.boolActive
                };

                TutworTutorWorkshopDao.subAdd(context_I, tutworentity);

                servans_O = new(200, null);
            }

        }

        //--------------------------------------------------------------------------------
        public static void subUpdateTutorWorkshop(
            CaafiContext context_I,
            GetsettutwokGetSetTutorWorkshopDto getsettutwor_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            TutorWorkshop? tutworentity = TutworTutorWorkshopDao.tutGetTutorWorkshopByPk(
                context_I, getsettutwor_I.intPk ?? 0
                );

            if (
                tutworentity == null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should be null",
                    getsettutwor_I);
            }
            else
            {
                tutworentity.intPkTutor = getsettutwor_I.intPkTutor;
                tutworentity.intPkWorkshop = getsettutwor_I.intPkWorkshop;
                tutworentity.TimeStartHour = getsettutwor_I.timeStartHour;
                tutworentity.TimeFinishHour = getsettutwor_I.timeFinishHour;
                tutworentity.boolActive = getsettutwor_I.boolActive;

                TutworTutorWorkshopDao.subUpdate(context_I, tutworentity);

                servans_O = new(200, null);
            }

        }

        //--------------------------------------------------------------------------------
        public static void subGetWorkshopTutor(
            CaafiContext context_I,
            int intPkWorkshop_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            TimeSpan time = DateTime.Now.TimeOfDay;
            int intTimeNowHour = time.Hours;
            int intTimeNowMinute = time.Minutes;

            //                                          // In the 15 minutes
            //                                          // alloweance
            //                                          // 
            string strTimeStart = (intTimeNowHour < 10 ? "0" : "") +
                intTimeNowHour + ":00:00";

            string strTimeLimit = (intTimeNowHour < 10 ? "0" : "") +
                intTimeNowHour + ":15:00";

            TimeSpan timeStart = TimeSpan.Parse(strTimeStart);
            TimeSpan timeLimit = TimeSpan.Parse(strTimeLimit);

            List<GetwortutbyhoupkGetWorkshopTutorsByHourPkDto.Out> darout = context_I.TutorWorkshop
                    .Include(tw => tw.TutorEntity)
                    .Include(tw => tw.WorkshopEntity)
                    .Where(tw =>
                            tw.intPkWorkshop == intPkWorkshop_I &&
                            tw.boolActive &&
                            tw.TimeStartHour >= timeStart &&
                            tw.TimeStartHour <= timeLimit
                    )
                    .Select(tw => new GetwortutbyhoupkGetWorkshopTutorsByHourPkDto.Out
                    {
                        intPkTutorWorkshop = tw.intPk,
                        intPkTutor = tw.intPkTutor,
                        strFullName = tw.TutorEntity.strName + " " + 
                            tw.TutorEntity.strSurename,
                        strFormatHour = strFormatHour(tw.TimeStartHour,tw.TimeFinishHour)
                    }).ToList();
            if(
                darout.Count == 0
                )
            {
                servans_O = new(200, "", "not-found", darout);
            }
            else if (
                darout.Count > 0 &&
                intTimeNowMinute >15
                )
            {
                servans_O = new(200, "", "over-limit-time", darout);
            }
            else
            {
                servans_O = new(200, darout);
            }
        }
        
        //--------------------------------------------------------------------------------
        public static void subGetTutorReport(
            CaafiContext context_I,
            int intPkTutor_I,
            DateTime dateStart_I,
            DateTime? dateEnd_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            DateTime date = (DateTime)(dateEnd_I == null ? DateTime.Now : dateEnd_I);
            
            //
            var reportePorTaller = context_I.Tutor
                .Include(r => r.IcTutorWorkshopEntity)
                    .ThenInclude(r => r.IcWorkshopAttendanceEntity)
                .Include(r => r.IcTutorWorkshopEntity)
                    .ThenInclude(r => r.WorkshopEntity)
                .Where(t => t.intPk == intPkTutor_I &&
                    t.IcTutorWorkshopEntity.Any(
                        tw => tw.IcWorkshopAttendanceEntity.Any(
                            wa => (wa.DateWorkshopDate <= date) && 
                                  (wa.DateWorkshopDate >= dateStart_I)
                        )
                    )
                ).ToList();

            List<GettutrepGetTutorReportDto.Out> darrgettutrepout = new();
            if (
                reportePorTaller.Count != 0
                )
            {
                darrgettutrepout = reportePorTaller.
                    SelectMany(t => t.IcTutorWorkshopEntity)
                .GroupBy(tw => tw.WorkshopEntity.strWorkshop)
                .Select(g => new GettutrepGetTutorReportDto.Out
                {
                    strWorkshop = g.Key,
                    intTotalSessions = g.SelectMany(tw => tw.IcWorkshopAttendanceEntity
                                            .GroupBy(wa => new 
                                            { 
                                                wa.DateWorkshopDate, 
                                                tw.TimeStartHour 
                                            })
                    ).Count(), // Contar las sesiones únicas
                    //                                      // Sumar total de asistentes
                    intTotalAttendance = g.Sum(tw => tw.IcWorkshopAttendanceEntity.Count) 
                })
                .ToList();
                
            }

            servans_O = new ServansdtoServiceAnswerDto(200, darrgettutrepout);


        } 

        //--------------------------------------------------------------------------------
        private static string strFormatHour(
            TimeSpan timeStartHour_I,
            TimeSpan timeFinishHour_I
            )
        {
             return String.Format("[{0} - {1}]", 
                 timeStartHour_I.ToString(@"hh\:mm"),
                 timeFinishHour_I.ToString(@"hh\:mm"));
        }

        //--------------------------------------------------------------------------------
        public static void subExcecuteUpdate(
            CaafiContext context_M,
            Workshop? worentity_I,
            Tutor? tutentity_I
            )
        {
            //                                              // SubExcetuteUpdate is supposed
            //                                              // to receive one of the two entities
            //                                              // Based on which one is received 
            //                                              // and which action is supposed to do
            //                                              // activate o de-activate, will do the
            //                                              // following:
            //                                              // In case activate, will verify 
            //                                              // if the workshop TUTOR is active
            //                                              // before activating all entries where
            //                                              // the workshop is assign, in case 
            //                                              // de-activate, will automatically 
            //                                              // deactivate all TutorWorkshop entries
            //                                              // where the workshop is asign
            //                                              // Will follow the same steps in case 
            //                                              // in case tutor entity is received.

            if (
                worentity_I != null
                )
            {
                if (
                    worentity_I.boolActive
                    )
                {
                    List<int> darrintPkTutorWorkshop = 
                        TutworTutorWorkshopDao.subGetTutorWorkshopPk(
                            context_M, null, worentity_I.intPk);
                    if (
                       darrintPkTutorWorkshop.Count() > 0
                        )
                    {
                        TutworTutorWorkshopDao.subExcecuteUpdateByPk(
                            context_M, darrintPkTutorWorkshop, worentity_I.boolActive);
                    }
                }
                else
                {
                    TutworTutorWorkshopDao.subExcecuteUpdateByWorkshop(
                        context_M, worentity_I.intPk , worentity_I.boolActive);
                }
            }


            if ( 
                tutentity_I != null 
                )
            {
                if (
                    tutentity_I.boolActive
                    )
                {
                    List<int> darrintPkTutorWorkshop = 
                        TutworTutorWorkshopDao.subGetTutorWorkshopPk(
                            context_M, tutentity_I.intPk, null);
                    if (
                        darrintPkTutorWorkshop.Count > 0
                        )
                    {
                        TutworTutorWorkshopDao.subExcecuteUpdateByPk(
                            context_M, darrintPkTutorWorkshop, tutentity_I.boolActive);
                    }
                }
                else
                {
                    TutworTutorWorkshopDao.subExcecuteUpdateByTutor(
                        context_M, tutentity_I.intPk, tutentity_I.boolActive);
                }
            }
            
        }

        //--------------------------------------------------------------------------------
    }
}
