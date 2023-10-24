namespace RulesEng.Model
{
    public class RuleStateMatch : Rule, IRule
    {
        public List<USState> MatchStates { get; set; }

        public bool RunRuleCondition(Person person, Product product)
        {
            foreach (USState matchState in this.MatchStates)
            {
                if (person.State == matchState)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
