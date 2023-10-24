namespace RulesEng.Model
{
    public class RuleCreditScoreRange : Rule, IRule
    {
        public int[] ScoreRange { get; set; }

        public bool RunRuleCondition(Person person, Product product)
        {
            return person.CreditScore >= this.ScoreRange[0] && person.CreditScore <= this.ScoreRange[1];
        }
    }
}
