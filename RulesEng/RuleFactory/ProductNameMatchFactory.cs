namespace RulesEng.Factory
{
    using AutoMapper;
    using RulesEng.Model;

    public class ProductNameMatchFactory : RuleFactory
    {
        public ProductNameMatchFactory(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public override RuleProductNameMatch CreateRule(Rule rule)
        {
            RuleProductNameMatch productNameMatchRule = this.Mapper.Map<RuleProductNameMatch>(rule);
            List<string> matchNames = new List<string>();

            foreach (string name in productNameMatchRule.Condition)
            {
                if (!string.IsNullOrEmpty(name))
                {
                    matchNames.Add(name);
                }
            }

            productNameMatchRule.MatchNames = matchNames;

            return productNameMatchRule;
        }
    }
}
