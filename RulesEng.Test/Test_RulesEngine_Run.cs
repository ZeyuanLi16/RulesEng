using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEng.Model;
using System.Collections.Generic;

namespace RulesEng.Test
{
    [TestClass]
    public class Test_RulesEngine_Run
    {
        [TestMethod]
        public void EngineRun_OneCondition()
        {
            var engine = new RulesEngine()
            {
                persons = new Person[]
                {
                    new Person
                    {
                        Name = "John Doe",
                        CreditScore = 750,
                        State = USState.Texas
                    }
                },
                rules = new IRule[]
                {
                    new RuleCreditScoreRange()
                    {
                        ScoreRange = new int[]{720, 800},
                        IfIncrease = false,
                        RateDifferece = 0.5,
                    }
                },
                products = new Product[]
                {
                    new Product()
                    {
                        InterstRate = 5,
                    }
                }
            };

            engine.Run();
            Assert.AreEqual(4.5, engine.Solutions[0].Item2[0].InterstRate);
        }

        [TestMethod]
        public void EngineRun_MultipleConditions()
        {
            var engine = new RulesEngine()
            {
                persons = new Person[]
                {
                    new Person
                    {
                        Name = "John Doe",
                        CreditScore = 750,
                        State = USState.Texas
                    }
                },
                rules = new IRule[]
                {
                    new RuleCreditScoreRange()
                    {
                        ScoreRange = new int[] { 720, 800 },
                        IfIncrease = false,
                        RateDifferece = 0.5,
                    },
                    new RuleProductNameContain()
                    {
                        MatchNames = new List<string>() { "Cabin" },
                        IfIncrease = true,
                        RateDifferece = 2.5,
                    }
                },
                products = new Product[]
                {
                    new Product()
                    {
                        Name = "SmallCabin",
                        InterstRate = 5,
                    }
                }
            };

            engine.Run();
            Assert.AreEqual(7, engine.Solutions[0].Item2[0].InterstRate);
        }

        [TestMethod]
        public void EngineRun_DropBelowZeroRate()
        {
            var engine = new RulesEngine()
            {
                persons = new Person[]
                {
                    new Person
                    {
                        Name = "John Doe",
                        CreditScore = 750,
                        State = USState.Texas
                    }
                },
                rules = new IRule[]
                {
                    new RuleCreditScoreRange()
                    {
                        ScoreRange = new int[] { 720, 800 },
                        IfIncrease = false,
                        RateDifferece = 5.5,
                    }
                },
                products = new Product[]
                {
                    new Product()
                    {
                        InterstRate = 5,
                    }
                }
            };

            engine.Run();
            Assert.AreEqual(0, engine.Solutions[0].Item2[0].InterstRate);
        }

        [TestMethod]
        public void EngineRun_DisqualifiedNotOverride()
        {
            var engine = new RulesEngine()
            {
                persons = new Person[]
                {
                    new Person
                    {
                        Name = "John Doe",
                        CreditScore = 750,
                        State = USState.Texas
                    }
                },
                rules = new IRule[]
                {
                    new RuleCreditScoreRange()
                    {
                        ScoreRange = new int[] { 720, 800 },
                        IfDisqualified = true,
                    },
                    new RuleProductNameContain()
                    {
                        MatchNames = new List<string>() { "Cabin" },
                        IfDisqualified = false,
                    }
                },
                products = new Product[]
                {
                    new Product()
                    {
                        Name = "SmallCabin",
                        InterstRate = 5,
                        Disqualified = false,
                    }
                }
            };

            engine.Run();
            Assert.IsTrue(engine.Solutions[0].Item2[0].Disqualified);
        }
    }
}