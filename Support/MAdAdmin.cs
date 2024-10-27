using MongoDB.Bson;
using MongoDB.Driver;
using SacBackend.Collections;
using SacBackend.DTO;
using SacBackend.MongoConfig;

namespace SacBackend.Support
{
    public class MAdAdmin
    {
        //--------------------------------------------------------------------------------------------------------------
        public static void servansAddAdmins(
            string[] stradmins_I,
            MongoDbService mongoDbService_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            MongoDB.Driver.IMongoCollection<Admin> admincol = mongoDbService_I.GetAdminsCollection();

            List<Admin.Schedule> darrschedule;
            for (int i = 0; i < stradmins_I.Length; i++)
            {
                subAdminSchedule (i,out  darrschedule);
                Collections.Admin admindoc = new Collections.Admin
                {
                    objId = ObjectId.GenerateNewId(),
                    strName = stradmins_I[i],
                    strPassword = BCrypt.Net.BCrypt.HashPassword("1234"),
                    strUser = "admin" + i,
                    darrSchedule = darrschedule,
                    boolActive = true
                };

                admincol.InsertOne ( admindoc );
            }

            servans_O = new(200, admincol.Find(_ => true).ToList());
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subAdminSchedule(
            int intIndex_I,
            out List<Admin.Schedule> darrschedule
            )
        {
            if (
                // Lorena
                intIndex_I == 1
                )
            {
                darrschedule = new List<Admin.Schedule> {
                    new Admin.Schedule {
                        strDayOfWeek = "Monday" ,
                        StartHour = new TimeSpan(13,0,0),
                        EndHour = new TimeSpan(19,0,0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Tursday",
                        StartHour = new TimeSpan(13, 0, 0),
                        EndHour = new TimeSpan(19, 0, 0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Wednesday",
                        StartHour = new TimeSpan(13,0,0),
                        EndHour = new TimeSpan(19, 0, 0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Thursday",
                        StartHour = new TimeSpan(13, 0, 0),
                        EndHour = new TimeSpan(19, 0, 0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Friday",
                        StartHour = new TimeSpan(13, 0, 0),
                        EndHour = new TimeSpan(19, 0, 0)
                    },
                };
            }
            else if (
                //  Diana
                intIndex_I == 2
                )
            {
                darrschedule = new List<Admin.Schedule> {
                    new Admin.Schedule {
                        strDayOfWeek = "Monday" ,
                        StartHour = new TimeSpan(12,0,0),
                        EndHour = new TimeSpan(17,0,0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Tursday",
                        StartHour = new TimeSpan(9, 0, 0),
                        EndHour = new TimeSpan(17, 0, 0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Wednesday",
                        StartHour = new TimeSpan(12,0,0),
                        EndHour = new TimeSpan(17,0,0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Thursday",
                        StartHour = new TimeSpan(9, 0, 0),
                        EndHour = new TimeSpan(17, 0, 0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Friday",
                        StartHour = new TimeSpan(9, 0, 0),
                        EndHour = new TimeSpan(17, 0, 0)
                    },
                };
            }
            else
            //  vane
            {
                darrschedule = new List<Admin.Schedule> {
                    new Admin.Schedule {
                        strDayOfWeek = "Monday" ,
                        StartHour = new TimeSpan(12,0,0),
                        EndHour = new TimeSpan(17,0,0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Tursday",
                        StartHour = new TimeSpan(9, 0, 0),
                        EndHour = new TimeSpan(17, 0, 0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Wednesday",
                        StartHour = new TimeSpan(12,0,0),
                        EndHour = new TimeSpan(17,0,0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Thursday",
                        StartHour = new TimeSpan(9, 0, 0),
                        EndHour = new TimeSpan(17, 0, 0)
                    },

                    new Admin.Schedule
                    {
                        strDayOfWeek = "Friday",
                        StartHour = new TimeSpan(9, 0, 0),
                        EndHour = new TimeSpan(17, 0, 0)
                    },
                };
            }
            

        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
