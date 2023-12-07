using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;

namespace SikkerhedsLogRepositoryLib.Tests
{
    [TestClass()]
    public class SikkerhedsLogRepositoryDBTests
    {
        private const bool useDB = true;
        private static SikkerhedsLogDBContext _dbContext = null;
        private static ISikkerhedsLogRepository _repo = null;

        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SikkerhedsLogDBContext>();
            string connectionString = "Data Source = mssql5.unoeuro.com; Initial Catalog = bbksolutions_dk_db_databasen; User ID = bbksolutions_dk; Password=cmfbeAtrkR5zBaF426x3;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent = ReadWrite; MultiSubnetFailover=False";

            optionsBuilder.UseSqlServer(connectionString);
            _dbContext = new SikkerhedsLogDBContext(optionsBuilder.Options);
            
            InitRepository();
        }

        public static void InitRepository()
        {
            if (_repo == null)
            {
                _repo = new SikkerhedsLogRepositoryDB(_dbContext);
            }
            _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.SikkerhedsLog");

            _repo.Add(new SikkerhedsLog() { Id = 1, Tidspunkt = "30-11-2023 14:34" });
            _repo.Add(new SikkerhedsLog() { Id = 2, Tidspunkt = "14-06-2024 12:45" });
            _repo.Add(new SikkerhedsLog() { Id = 3, Tidspunkt = "14-06-2001 19:54" });
        }

        [TestMethod()]
        public void SikkerhedsLogRepositoryDBTest()
        {
            IEnumerable<SikkerhedsLog> sl = _repo.Get();

            Assert.IsNotNull(sl);
        }

        [TestMethod()]
        public void GetTest()
        {
            IEnumerable<SikkerhedsLog> sl = _repo.Get();

            Assert.IsNotNull(sl);

            SikkerhedsLog sl1 = new SikkerhedsLog()
            { Id = 10, Tidspunkt = "01-12-2023 00:00" };

            SikkerhedsLog? slReturned = _repo.Add(sl1);
            SikkerhedsLog? slFound = null;

            slFound = _repo.Get().FirstOrDefault(s => s.Id == slReturned.Id);

            Assert.IsNotNull(slReturned);
            Assert.AreEqual(5, slReturned.Id);
            Assert.AreEqual("01-12-2023 00:00", slReturned.Tidspunkt);

            Assert.IsNotNull(slFound);
            Assert.AreEqual(5, slFound.Id);
            Assert.AreEqual("01-12-2023 00:00", slFound.Tidspunkt);
        }

        [TestMethod()]
        public void AddTest()
        {
            IEnumerable<SikkerhedsLog> sl = _repo.Get();

            Assert.IsNotNull(sl);

            SikkerhedsLog sl1 = new SikkerhedsLog()
            { Id = 10, Tidspunkt = "01-12-2023 00:00" };

            SikkerhedsLog? slReturned = _repo.Add(sl1);
            SikkerhedsLog? slFound = null;

            slFound = _repo.Get().FirstOrDefault(s => s.Id == slReturned.Id);

            Assert.IsNotNull(slReturned);
            Assert.AreEqual(4, slReturned.Id);
            Assert.AreEqual("01-12-2023 00:00", slReturned.Tidspunkt);

            Assert.IsNotNull(slFound);
            Assert.AreEqual(4, slFound.Id);
            Assert.AreEqual("01-12-2023 00:00", slFound.Tidspunkt);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            IEnumerable<SikkerhedsLog> sl = _repo.Get();

            Assert.IsNotNull(sl);
            Assert.AreEqual<bool>(true, sl.Count() > 0);

            SikkerhedsLog someSl = sl.First<SikkerhedsLog>();
            int numOfSl = sl.Count();

            SikkerhedsLog? deletedSl = _repo.Delete(someSl.Id);

            Assert.AreEqual(numOfSl - 1, _repo.Get().Count());
            SikkerhedsLog? slFound = null;

            slFound = _repo.Get().FirstOrDefault(a => a.Id == deletedSl.Id);

            Assert.IsNull(slFound);
        }

        [TestMethod()]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(20)]
        public void GetIdFørstTest(int idFørst)
        {
            IEnumerable<SikkerhedsLog> sl = _repo.Get(idFørst);

            Assert.IsNotNull(sl);
            foreach(SikkerhedsLog s in sl)
            {
                if (s.Id >= idFørst)
                {
                    Assert.Fail($"Id {s.Id} is NOT before {idFørst}");
                }
            }
        }

        [TestMethod()]
        [DataRow(51, 22)]
        [DataRow(32, 30)]
        [DataRow(12, 11)]
        public void GetIdFørstAndSidstTest(int idFørst, int idSidst)
        {
            IEnumerable<SikkerhedsLog> sl = _repo.Get(idFørst, idSidst);

            Assert.IsNotNull(sl);

            foreach (SikkerhedsLog s in sl)
            {
                if (s.Id >= idFørst)
                {
                    Assert.Fail($"Id {s.Id} is NOT before {idFørst}");
                }

                if (s.Id <= idSidst)
                {
                    Assert.Fail($"Id {s.Id} is NOT after {idSidst}");
                }
            }
        }

        [TestMethod()]
        [DataRow(1, 5, "id")]
        [DataRow(10, 12, "id")]
        [DataRow(1, 30, "id")]
        public void GetIdFørstAndSidstOrderByTest(int idFørst,
                                                  int idSidst,
                                                  string orderBy)
        {
            IEnumerable<SikkerhedsLog>? sl = _repo.Get(idFørst, idSidst, orderBy);

            Assert.IsNotNull(sl);

            foreach(SikkerhedsLog s in sl)
            {
                if (s.Id >= idFørst)
                {
                    Assert.Fail($"Id {s.Id} is NOT before {idFørst}");
                }

                if (s.Id <= idSidst)
                {
                    Assert.Fail($"Id {s.Id} is NOT after {idSidst}");
                }
            }

            IEnumerable<SikkerhedsLog>? orderedSl = null;

            switch (orderBy.ToLower())
            {
                case "id":
                    orderedSl = sl.OrderBy(s => s.Id);
                    break;
            }

            CollectionAssert.AreEqual(orderedSl.ToList(), sl.ToList(),"SikkerhedsLogs NOT ordered correctly");
        }
        
        [TestMethod()]
        [DataRow(SikkerhedsLog.InvalidId, SikkerhedsLog.ValidTidspunkt)]
        [DataRow(SikkerhedsLog.ValidId, SikkerhedsLog.ShortTidspunkt)]
        [DataRow(SikkerhedsLog.ValidId, SikkerhedsLog.LongTidspunkt)]
        public void ValidateTestArgumentOutOfRange(int id, string tidspunkt)
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            { Id = id, Tidspunkt = tidspunkt };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(sl));
        }

        [TestMethod()]
        public void ValidateTestArgumentNull()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            { Id = SikkerhedsLog.ValidId, Tidspunkt = SikkerhedsLog.NullTidspunkt };

            Assert.ThrowsException<ArgumentNullException>(() => _repo.Add(sl));
        }
    }
}