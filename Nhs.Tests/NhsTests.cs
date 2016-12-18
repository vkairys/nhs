using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;

namespace Nhs.Tests
{
    [TestFixture]
    public class NhsTests
    {
        private Nhs _nhs;
        private Mock<INhsProcessor> _nhsProcessor;
        private Mock<IFileStorage> _fileStorage;

        [SetUp]
        public void Init()
        {
            _fileStorage = new Mock<IFileStorage>();
            _nhsProcessor = new Mock<INhsProcessor>();
            _nhsProcessor.Setup(p => p.ProcessPractice(It.IsAny<StreamReader>())).Returns(new PracticeResult());
            _nhsProcessor.Setup(p => p.ProcessPrescriptionCost(It.IsAny<StreamReader>())).Returns(new PrescriptionCostResult());
            _nhsProcessor.Setup(
                p =>
                    p.ProcessPrescription(It.IsAny<StreamReader>(), It.IsAny<Dictionary<string, string>>(),
                        It.IsAny<Dictionary<string, byte>>())).Returns(new PrescriptionResult());

            _nhs = new Nhs(_fileStorage.Object, _nhsProcessor.Object);
        }

        [Test]
        public void Execute_ReadFromDataStorage()
        {
            _nhs.Execute("file1.csv", "file2.csv", "file3.csv");

            _fileStorage.Verify(d => d.ReadData("file1.csv"));
            _fileStorage.Verify(d => d.ReadData("file2.csv"));
            _fileStorage.Verify(d => d.ReadData("file3.csv"));
        }

        [Test]
        public void Execute_ReturnCountFromPracticeResult()
        {
            _nhsProcessor.Setup(n => n.ProcessPractice(It.IsAny<StreamReader>())).Returns(new PracticeResult { Total = 1 });

            var result = _nhs.Execute("", "", "");

            Assert.That(result.PracticeCount, Is.EqualTo(1));
        }

        [Test]
        public void Execute_ReturnAverageCostFromPrescriptionResult()
        {
            _nhsProcessor.Setup(
                n =>
                    n.ProcessPrescription(It.IsAny<StreamReader>(), It.IsAny<Dictionary<string, string>>(),
                        It.IsAny<Dictionary<string, byte>>())).Returns(new PrescriptionResult { Cost = 4 });

            var result = _nhs.Execute("", "", "");

            Assert.That(result.AverageCost, Is.EqualTo(4));
        }

        [Test]
        public void Execute_ReturnDrugTypesFromPrescriptionResult()
        {
            var drugTypes = new[] { new DrugType { Count = 4, Name = "s" } };
            _nhsProcessor.Setup(
                n =>
                    n.ProcessPrescription(It.IsAny<StreamReader>(), It.IsAny<Dictionary<string, string>>(),
                        It.IsAny<Dictionary<string, byte>>())).Returns(new PrescriptionResult { DrugTypes = drugTypes });

            var result = _nhs.Execute("", "", "");

            Assert.That(result.DrugTypes, Is.EquivalentTo(drugTypes));
        }

        [Test]
        public void Execute_ReturnRegionPrescripctionsFromprescriptionResult()
        {
            var regionPrescripctions = new[] { new RegionPrescripctions { Diff = 4, Region = "a", Spent = 1 } };
            _nhsProcessor.Setup(
                    n =>
                        n.ProcessPrescription(It.IsAny<StreamReader>(), It.IsAny<Dictionary<string, string>>(),
                            It.IsAny<Dictionary<string, byte>>()))
                .Returns(new PrescriptionResult { Regions = regionPrescripctions });

            var result = _nhs.Execute("", "", "");

            Assert.That(result.RegionPrescripctions, Is.EquivalentTo(regionPrescripctions));
        }

        [Test]
        public void Execute_ReturnPostCodeSpentsFromPrescriptionResult()
        {
            var postCodesSpend = new List<PostCodeSpent> { new PostCodeSpent { Name = "a", Spent = 4 } };
            _nhsProcessor.Setup(
                    n =>
                        n.ProcessPrescription(It.IsAny<StreamReader>(), It.IsAny<Dictionary<string, string>>(),
                            It.IsAny<Dictionary<string, byte>>()))
                .Returns(new PrescriptionResult { PostCodes = postCodesSpend });

            var result = _nhs.Execute("", "", "");

            Assert.That(result.PostCodeSpents, Is.EquivalentTo(postCodesSpend));
        }
    }
}
