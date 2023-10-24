namespace RulesEng.Factory
{
    using AutoMapper;
    using RulesEng.Model;

    public class CreditScoreRangeFactory : RuleFactory
    {
        public CreditScoreRangeFactory(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public override RuleCreditScoreRange CreateRule(Rule rule)
        {
            RuleCreditScoreRange creditScoreRangeRule = this.Mapper.Map<RuleCreditScoreRange>(rule);
            if (rule.RateDifferece < 0)
            {
                throw new ArgumentOutOfRangeException($"Please use valid interest rate(> 0) for rule: {rule.Name}.", new Exception());
            }

            int scoreLowerRange = 0;
            int scoreUpperRange = 0;
            try
            {
                scoreLowerRange = int.Parse(creditScoreRangeRule.Condition[0]);
                scoreUpperRange = int.Parse(creditScoreRangeRule.Condition[1]);
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Please use number format for rules {creditScoreRangeRule.Name}. " + ex.Message);
                throw;
            }

            if (scoreLowerRange < 300 || scoreLowerRange > 850 || scoreUpperRange < 300 || scoreUpperRange > 850)
            {
                Console.WriteLine($"Please use valid credit score range(300 - 850) for rules {creditScoreRangeRule.Name}.");
                throw new ArgumentOutOfRangeException();
            }

            creditScoreRangeRule.ScoreRange = new int[] { scoreLowerRange, scoreUpperRange };

            return creditScoreRangeRule;
        }
    }
}
