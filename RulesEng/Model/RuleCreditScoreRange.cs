namespace RulesEng.Model
{
    public class RuleCreditScoreRange : Rule
    {
        public int[] ScoreRange { get; set; }

        public override bool RunRuleCondition(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
