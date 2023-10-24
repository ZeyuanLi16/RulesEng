namespace RulesEng.Model
{
    public class RuleProductNameContain : Rule, IRule
    {
        public List<string> MatchNames { get; set; }

        public bool RunRuleCondition(Person person, Product product)
        {
            foreach (string matchName in this.MatchNames)
            {
                if (product.Name.Contains(matchName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
