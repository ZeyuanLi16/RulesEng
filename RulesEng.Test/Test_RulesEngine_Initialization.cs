using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEng.Model;
using System;

namespace RulesEng.Test
{
    [TestClass]
    public class Test_RulesEngine_Initialization
    {
        private IMapper mapper;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize and configure AutoMapper.
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            this.mapper = configuration.CreateMapper();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_SettingIsNull()
        {
            var engine = new RulesEngine(null, mapper, @"SomeConfiguration\");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_InvalidInterestRate()
        {
            var setting = new Setting() { DefaultInterestRate = -1.0 };
            var engine = new RulesEngine(setting, mapper, @"SomeConfiguration\");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_MapperIsNull()
        {
            var setting = new Setting { DefaultInterestRate = 5 };
            var engine = new RulesEngine(setting, null, @"SomeConfiguration\");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ConfigPathIsInvalid()
        {
            var setting = new Setting { DefaultInterestRate = 5 };
            var engine = new RulesEngine(setting, this.mapper, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializePersons_CreditScoreOutOfRange()
        {
            var engine = new RulesEngine();
            engine.configPath = @"Configuration\CreditScoreOutOfRange\";
            engine.InitializePersons();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeRules_CreditScoreOutOfRange()
        {
            var engine = new RulesEngine();
            engine.configPath = @"Configuration\CreditScoreOutOfRange\";
            engine.mapper = mapper;
            engine.InitializeRules();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeProducts_InterestRateOutOfRange()
        {
            var engine = new RulesEngine();
            engine.configPath = @"Configuration\InterestRateOutOfRange\";
            engine.InitializeProducts(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InitializeRules_InterestRateOutOfRange()
        {
            var engine = new RulesEngine();
            engine.configPath = @"Configuration\InterestRateOutOfRange\";
            engine.mapper = mapper;
            engine.InitializeRules();
        }
    }
}