namespace RulesEng.Factory
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
            List<string> matchNames = new List<string>();

            foreach (string name in productNameContainRule.Condition)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    matchNames.Add(name);
                }
            }

            productNameContainRule.MatchNames = matchNames;

            return productNameContainRule;
        }
    }
}
