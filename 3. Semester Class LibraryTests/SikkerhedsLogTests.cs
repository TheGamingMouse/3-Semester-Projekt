using Microsoft.VisualStudio.TestTools.UnitTesting;
using SikkerhedsLogRepositoryLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SikkerhedsLogRepositoryLib.Tests
{
    [TestClass()]
    public class SikkerhedsLogTests
    {
        [TestMethod()]
        public void SikkerhedsLogTest()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = SikkerhedsLog.ValidTidspunkt
            };
            
            Assert.AreEqual(SikkerhedsLog.ValidId, sl.Id);
            Assert.AreEqual(SikkerhedsLog.ValidTidspunkt, sl.Tidspunkt);
        }

        [TestMethod()]
        public void ValidateTestSuccess()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = SikkerhedsLog.ValidTidspunkt
            };

            sl.Validate();
        }

        [TestMethod()]
        [DataRow(SikkerhedsLog.InvalidId, SikkerhedsLog.ValidTidspunkt)]
        [DataRow(SikkerhedsLog.ValidId, SikkerhedsLog.ShortTidspunkt)]
        [DataRow(SikkerhedsLog.ValidId, SikkerhedsLog.LongTidspunkt)]
        public void ValidateTestOutOfRange(int id, string tidspunkt)
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = id,
                Tidspunkt = tidspunkt
            };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sl.Validate());
        }

        [TestMethod()]
        public void ValidateTestNull()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = SikkerhedsLog.NullTidspunkt
            };

            Assert.ThrowsException<ArgumentNullException>(() => sl.Validate());
        }

        [TestMethod()]
        public void ValidateTestArgument()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = SikkerhedsLog.ArgumentTidspunkt
            };

            Assert.ThrowsException<ArgumentException>(() => sl.Validate());
        }

        [TestMethod()]
        public void ValidateIdTestSuccess()
        {
            {
                SikkerhedsLog sl = new SikkerhedsLog()
                {
                    Id = SikkerhedsLog.ValidId,
                    Tidspunkt = SikkerhedsLog.ValidTidspunkt
                };

                sl.ValidateId();
            }
        }

        [TestMethod()]
        public void ValidateIdTestOutOfRange()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.InvalidId,
                Tidspunkt = SikkerhedsLog.ValidTidspunkt
            };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sl.ValidateId());
        }

        [TestMethod()]
        public void ValidateTidspunktTestSuccess()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = SikkerhedsLog.ValidTidspunkt
            };

            sl.ValidateTidspunkt();
        }

        [TestMethod()]
        [DataRow(SikkerhedsLog.ShortTidspunkt)]
        [DataRow(SikkerhedsLog.LongTidspunkt)]
        public void ValidateTidspunktTestOutOfRange(string tidspunkt)
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = tidspunkt
            };

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => sl.ValidateTidspunkt());
        }

        [TestMethod()]
        public void ValidateTidspunktTestNull()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = SikkerhedsLog.NullTidspunkt
            };

            Assert.ThrowsException<ArgumentNullException>(() => sl.ValidateTidspunkt());
        }

        [TestMethod()]
        public void ValidateTidspunktTestArgument()
        {
            SikkerhedsLog sl = new SikkerhedsLog()
            {
                Id = SikkerhedsLog.ValidId,
                Tidspunkt = SikkerhedsLog.ArgumentTidspunkt
            };

            Assert.ThrowsException<ArgumentException>(() => sl.ValidateTidspunkt());
        }
    }
}