namespace RulesEng.RulesCreator
{
    using AutoMapper;
    using RulesEng.Model;

    public class CreditScoreRangeCreator : RulesCreator
    {
        private readonly IMapper mapper;

        public CreditScoreRangeCreator(Rule rule, IMapper mapper)
        {
            this.Rule = rule;
            this.mapper = mapper;
        }

        public override Rule CreateRule()
        {
            RuleCreditScoreRange creditScoreRangeRule = this.mapper.Map<RuleCreditScoreRange>(this.Rule);
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
