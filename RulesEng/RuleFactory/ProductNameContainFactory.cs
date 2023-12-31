﻿namespace RulesEng.Factory
{
    using AutoMapper;
    using RulesEng.Model;

    public class ProductNameContainFactory : RuleFactory
    {
        public ProductNameContainFactory(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public override RuleProductNameContain CreateRule(Rule rule)
        {
            RuleProductNameContain productNameContainRule = this.Mapper.Map<RuleProductNameContain>(rule);
            if (rule.RateDifferece < 0)
            {
                throw new ArgumentOutOfRangeException($"Please use valid interest rate(> 0) for rule: {rule.Name}.", new Exception());
            }

            List<string> matchNames = new List<string>();

            foreach (string name in productNameContainRule.Condition)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    matchNames.Add(name);
                }
            }

            productNameContainRule.MatchNames = matchNames;

            return productNameContainRule;
        }
    }
}
