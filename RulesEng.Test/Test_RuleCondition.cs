using Microsoft.VisualStudio.TestTools.UnitTesting;
using RulesEng.Model;
using System.Collections.Generic;

namespace RulesEng.Test
{
    [TestClass]
    public class Test_RuleCondition
    {
        [TestMethod]
        public void Rule_CreditScoreRange()
        {
            var rule = new RuleCreditScoreRange()
            {
                ScoreRange = new int[] { 720, 800 },
            };

            var personWithinRange = new Person
            {
                CreditScore = 750,
            };
            var personOutOfRange = new Person
            {
                CreditScore = 600,
            };

            var product = new Product();

            Assert.AreEqual(true, rule.RunRuleCondition(personWithinRange, product));
            Assert.AreEqual(false, rule.RunRuleCondition(personOutOfRange, product));
        }

        [TestMethod]
        public void Rule_ProductNameContain()
        {
            var rule = new RuleProductNameContain()
            {
                MatchNames = new List<string>() { "Cabin" },
            };
            var person = new Person();
            var productNameContain = new Product()
            {
                Name = "SmallCabin",
            };

            var productNameContainIgnoreCase = new Product()
            {
                Name = "SmallCABIN",
            };

            var productNameNotContain = new Product()
            {
                Name = "SmallCastle",
            };

            Assert.AreEqual(true, rule.RunRuleCondition(person, productNameContain));
            Assert.AreEqual(true, rule.RunRuleCondition(person, productNameContainIgnoreCase));
            Assert.AreEqual(false, rule.RunRuleCondition(person, productNameNotContain));
        }

        [TestMethod]
        public void Rule_ProductNameMatch()
        {
            var rule = new RuleProductNameMatch()
            {
                MatchNames = new List<string>() { "Cabin" },
            };
            var person = new Person();
            var productNameMatch = new Product()
            {
                Name = "Cabin",
            };

            var productNameMatchIgnoreCase = new Product()
            {
                Name = "CABIN",
            };

            var productNameNotMatch = new Product()
            {
                Name = "SmallCabin",
            };

            Assert.AreEqual(true, rule.RunRuleCondition(person, productNameMatch));
            Assert.AreEqual(true, rule.RunRuleCondition(person, productNameMatchIgnoreCase));
            Assert.AreEqual(false, rule.RunRuleCondition(person, productNameNotMatch));
        }

        [TestMethod]
        public void Rule_MatchStates()
        {
            var rule = new RuleStateMatch()
            {
                MatchStates = new List<USState> { USState.Texas },
            };

            var personMatchState = new Person
            {
                State = USState.Texas,
            };
            var personNotMatchState = new Person
            {
                State = USState.Florida,
            };

            var product = new Product();

            Assert.AreEqual(true, rule.RunRuleCondition(personMatchState, product));
            Assert.AreEqual(false, rule.RunRuleCondition(personNotMatchState, product));
        }
    }
}