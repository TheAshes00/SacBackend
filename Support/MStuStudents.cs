using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;
using SacBackend.Collections;
using SacBackend.Context;
using SacBackend.DTO;
using SacBackend.Models;
using SacBackend.MongoConfig;
using static SacBackend.DTO.Loan.GetmatloaGetMaterialLoanDto.Out;

namespace SacBackend.Support
{
    public class MStuStudents
    {
        //--------------------------------------------------------------------------------
        public static void servansImport(
            Caafi2Context context2_I,
            MongoDbService mongoservice_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            List<Alumno> darrstuentity = context2_I.Students.ToList();

            IMongoCollection<Collections.AcademicEntity> academicEntitiesCollection = 
                mongoservice_I.GetAcademicEntitiesCollection();

            Collections.AcademicEntity engineeringEntity = academicEntitiesCollection.Find(
                ae => ae.strName.ToLower().Contains("Ingeniería".ToLower())).FirstOrDefault();

            Collections.AcademicEntity otherEntity = academicEntitiesCollection.Find(
                ae => ae.strName.ToLower().Contains("Other".ToLower())).FirstOrDefault();

            List<string> darrEngineeringPrograms = 
                new List<string> { "ICO", "IME", "ISES", "IEL", "ICI" };

            List<Collections.Student> studentDocuments = new List<Collections.Student>();

            foreach (var student in darrstuentity)
            {
                ObjectId academicEntityId = darrEngineeringPrograms.
                    Contains(student.strBachelors)
                    ? engineeringEntity.objId
                    : otherEntity.objId;

                var academicEntityRef = new MongoDBRef("AcademicEntities", academicEntityId);

                Collections.Student stu = new Collections.Student()
                {
                    objId = ObjectId.GenerateNewId(),
                    strStudentId = student.strNmCta,
                    AcademicEntity = academicEntityRef,
                    strName = student.strName,
                    strAcademicLevel = student.strBachelors,
                    darrLoans = new List<MongoDBRef>(),
                    darrWorkshopAttendance = new List<MongoDBRef>(),
                    
                };

                studentDocuments.Add(stu);
            }

            IMongoCollection<Collections.Student> studentCollection = 
                mongoservice_I.GetStudentCollection();

            studentCollection.InsertMany(studentDocuments);

            servans_O = new ServansdtoServiceAnswerDto(200, "ok");
        }

        //--------------------------------------------------------------------------------
        public static void servansImportLoans(
            Caafi2Context context2_I,
            MongoDbService mongoservice_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            int intNumberOfNoCoincidence = 0;
            // Obtener todos los prestamos de la base de datos original
            List<Models.Prestamo> darrLoans = context2_I.Loans.ToList();

            int intIndexTest = darrLoans.IndexOf(
                darrLoans.Where(x => x.strIdRegistro == "2018-10-1516:32:021110305").FirstOrDefault());

            var test = darrLoans.Skip(intIndexTest+1);

            var test2 = test.FirstOrDefault();

            //Obtener la coleccion de docuemntos de materiales de Mongo
            IMongoCollection<Collections.Material> materialCollection =
                    mongoservice_I.GetMaterialsCollection();

            // Obtener la coleccion de administradores 
            IMongoCollection<Collections.Admin> adminCollection = 
                mongoservice_I.GetAdminsCollection();

            // Obtener la coleccion de prestamos "Loans", para agregar cada uno de los uevos prestamos
            IMongoCollection<Collections.Loan> loanCollection = 
                mongoservice_I.GetLoanCollection();


            //Iniciar ciclo de prestamos 
            foreach (var loan in test)
            {
                // Obtener el numero de cuenta del alumno en la tabla prestamos 
                string strNumCta = loan.strPkStudent;

                // Usar ese numero de cuenta para buscar el documento perteneciente al alumno con ese Numero de Cuenta
                Collections.Student studentDocument = mongoservice_I.GetStudentCollection().Find( 
                    student => student.strStudentId.CompareTo(strNumCta) == 0
                    ).FirstOrDefault();


                if (
                    studentDocument == null
                    ) {
                    intNumberOfNoCoincidence++;
                    continue;
                }
                // Obtener de la tabla de prestamo de material todos los registros que coincidan con el id del prestamo
                // para obtener los materiales
                List<PrestamoMaterial> darrmatloa = context2_I.MaterialLoans.Where(
                    ml => ml.strPkLoan.Equals(loan.strIdRegistro)
                ).ToList();


                // Declarar la una lista para el manejo de referencias a los materiales 
                var darrMaterial = new List<MongoDBRef>();

                foreach (var materialloan in darrmatloa)
                {
                    // De cada material que resitrado en la tabla se busca su par en la coleccion correspondiente de Mongo
                    var materialDocument = materialCollection.Find(m => m.strMaterialId.ToLower() == materialloan.strPkMaterial.ToLower().Trim()).FirstOrDefault();
                    
                    // Se agrega un nuevo elemento que hace referencia a la coleccion po medio de su objectId
                    darrMaterial.Add(new MongoDBRef("Materials", materialDocument.objId));
                }

                // Genera un nuevo documento de Loan y asigna la lista de materiales, junto con toda la informacion 
                //  del registro de Loan de la base de datos SQL
                Collections.Loan loanDocument = new Collections.Loan
                {
                    objId = ObjectId.GenerateNewId(),
                    dateLoanDate = loan.LoanDate,
                    timeLendTime = loan.TimeStart,
                    timeReturnTime = loan.TimeEnd.CompareTo(new TimeSpan(0,0,0)) == 0 ? null : loan.TimeEnd,
                    objAdminIssued = objGetAdmin(adminCollection, loan.intPkAdminLend),
                    objAdminReceived = objGetAdmin(adminCollection,loan.intPkAdminRecieve),
                    darrMaterials = darrMaterial
                };

                loanCollection.InsertOne(loanDocument);

                subUpdateStudentLoans(mongoservice_I,strNumCta,loanDocument);
            }

            servans_O = new ServansdtoServiceAnswerDto(200, "No coincidences: " +intNumberOfNoCoincidence);
        }

        //--------------------------------------------------------------------------------
        private static void subUpdateStudentLoans(
            MongoDbService mongoservice_I,
            string strNumCta_I,
            Collections.Loan loanDocument_I
            )
        {
            var studentCollection = mongoservice_I.GetStudentCollection();
            
            var filter = Builders<Collections.Student>.Filter.Eq(
                s => s.strStudentId, strNumCta_I);
            
            var update = Builders<Collections.Student>.Update.Push(
                s => s.darrLoans, new MongoDBRef("Loans", loanDocument_I.objId));

            studentCollection.UpdateOne(filter, update);
        }

        //--------------------------------------------------------------------------------
        private static MongoDBRef? objGetAdmin(
            IMongoCollection<Collections.Admin> adminCollection_I,
            int intAdmin
            )
        {
            MongoDBRef? objAdminReference;
            if (
                intAdmin == 1
                )
            {
                Collections.Admin adminLorena = adminCollection_I.Find(
                    admin => admin.strName.Contains("Lorena"))
                    .FirstOrDefault();

                objAdminReference = new MongoDBRef("Admins", adminLorena.objId);
            }
            else if (
                intAdmin == 2
                )
            {
                Collections.Admin adminDiana = adminCollection_I.Find(
                    admin => admin.strName.Contains("Diana"))
                    .FirstOrDefault();

                objAdminReference = new MongoDBRef("Admins",adminDiana.objId);
            }
            else if (
                intAdmin == 3
                )
            {
                Collections.Admin adminVanessa = adminCollection_I.Find(
                    admin => admin.strName.Contains("Vanessa"))
                    .FirstOrDefault();

                objAdminReference = new MongoDBRef("Admins", adminVanessa.objId);
            }
            else
            {
                objAdminReference = null;
            }

            return objAdminReference;
        }

        //--------------------------------------------------------------------------------
        public static void servansImportMaterial(
            MongoDbService mongoService_I,
            Caafi2Context context2_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            var darrmatentity = context2_I.Materiales.ToList();

            var darrmaterialDocuments = new List<Collections.Material>();
            foreach (var material in darrmatentity)
            {
                Collections.Material materialDocument = new Collections.Material()
                {
                    objId = ObjectId.GenerateNewId(),
                    strMaterialId = material.strNumCtrlInt,
                    strName = material.strName,
                    strCodeType = material.strCodeType,
                    strMaterialType = material.strMarerialType,
                    boolActive = true
                };
                darrmaterialDocuments.Add(materialDocument);
            }

            mongoService_I.GetMaterialsCollection().InsertMany( darrmaterialDocuments );

            servans_O = new(200,"ok");
        }

        //--------------------------------------------------------------------------------
        public static void servansExportLoans(
            MongoDbService mongoService_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            //
            IMongoCollection<Collections.Material> materialCollection = 
                mongoService_I.GetMaterialsCollection();

            //
            IMongoCollection<Collections.Student> studentCollection = 
                mongoService_I.GetStudentCollection();

            List<Collections.Student> studentCollectionFiltered = studentCollection.Find(
                FilterDefinition<Collections.Student>.Empty).ToList();

            // Crear un StringBuilder para construir el archivo CSV
            StringBuilder csv = new StringBuilder();

            csv.AppendLine("Date,Lend Time,Return Time, Academic Entity, Academic Level, Material Types");

            // Iterar a través de los préstamos y agregar las filas al CSV
            foreach (var student in studentCollectionFiltered)
            {
                string strStudentAcademy = mongoService_I.GetAcademicEntitiesCollection()
                    .Find(ae => ae.objId == student.AcademicEntity.Id)
                    .FirstOrDefault()?.strName;

                foreach (var loanRef in student.darrLoans)
                {
                    IMongoCollection<Collections.Loan> loanCollection =
                        mongoService_I.GetLoanCollection();

                    Collections.Loan loan = loanCollection.Find(
                        l => l.objId == loanRef.Id).FirstOrDefault();

                    if (
                        loan != null
                        )
                    {
                        string strTypes = null;
                        // Preparar arreglo de materiales
                        if (
                            !loan.darrMaterials.IsNullOrEmpty()
                            )
                        {
                            IEnumerable<ObjectId> arrMongoRefMaterials =
                                loan.darrMaterials.Select(mr => mr.Id.AsObjectId);

                            List<string> darrmaterials = materialCollection.
                                Find(m => arrMongoRefMaterials.Contains(m.objId)).ToList().
                                Select(mt => mt.strMaterialType).ToList();

                            strTypes = string.Join(",", darrmaterials);
                        }
                        

                        //csv.AppendLine("Date,Lend Time,Return Time, Academic Entity, Academic Level");
                        csv.AppendLine(
                            $"{loan.dateLoanDate},{loan.timeLendTime},{loan.timeReturnTime},{strStudentAcademy},{student.strAcademicLevel},{strTypes}");
                    }

                }

            }

            // Guardar el archivo CSV
            File.WriteAllText("loans_info.csv", csv.ToString());

            servans_O = new(200, "Datos exportados a loans_info_2_0.csv");
        }

        //--------------------------------------------------------------------------------
    }
}
