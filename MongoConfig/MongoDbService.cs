using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SacBackend.Collections;

namespace SacBackend.MongoConfig
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
        {
            
            var client = new MongoClient(mongoDbSettings.Value.strConnectionString);
            _database = client.GetDatabase(mongoDbSettings.Value.strDatabaseName);
        }

        //                                                  // Collection : Students
        public IMongoCollection<Student> GetStudentCollection() =>
            _database.GetCollection<Student>("Students");

        //                                                  // Collection : Loans
        public IMongoCollection<Loan> GetLoanCollection() =>
            _database.GetCollection<Loan>("Loans");

        //                                                  // Collection : WorkshopsAttendances
        public IMongoCollection<WorkshopAttendance> GetWorkshopAttendanceCollection() =>
            _database.GetCollection<WorkshopAttendance>("WorkshopsAttendances");

        //                                                  // Collection : Workshops
        public IMongoCollection<Workshop> GetWorkshopCollection() =>
            _database.GetCollection<Workshop>("Workshops");

        //                                                  // Collection : Tutors
        public IMongoCollection<Tutor> GetTutorsCollection() =>
            _database.GetCollection<Tutor>("Tutors");

        //                                                  // Collection : AcademicEntities
        public IMongoCollection<AcademicEntity> GetAcademicEntitiesCollection() =>
            _database.GetCollection<AcademicEntity>("AcademicEntities");

        //                                                  // Collection : Admins
        public IMongoCollection<Admin> GetAdminsCollection() =>
            _database.GetCollection<Admin>("Admins");

        //                                                  // Collection : Materials
        public IMongoCollection<Material> GetMaterialsCollection() =>
            _database.GetCollection<Material>("Materials");
    }
}
