namespace RulesEng.Model
{
    public class RuleProductNameMatch : Rule, IRule
    {
        public List<string> MatchNames { get; set; }

        public bool RunRuleCondition(Person person, Product product)
        {
            foreach (string matchName in this.MatchNames)
            {
                if (string.Equals(product.Name, matchName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
