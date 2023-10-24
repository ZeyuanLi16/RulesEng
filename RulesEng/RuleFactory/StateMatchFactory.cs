namespace RulesEng.Factory
{
    using AutoMapper;
    using RulesEng.Model;

    public class StateMatchFactory : RuleFactory
    {
        public StateMatchFactory(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public override RuleStateMatch CreateRule(Rule rule)
        {
            RuleStateMatch stateMatchRule = this.Mapper.Map<RuleStateMatch>(rule);

            List<USState> matchStates = new List<USState>();

            foreach (string state in stateMatchRule.Condition)
            {
                if (string.IsNullOrEmpty(state))
                {
                    continue;
                }

                try
                {
                    matchStates.Add((USState)Enum.Parse(typeof(USState), state));
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine($"USState Enum.Parse: '{state}' is not spelled correctly or a US state." + e.Message);
                    throw;
                }
            }

            stateMatchRule.MatchStates = matchStates;

            return stateMatchRule;
        }
    }
}
