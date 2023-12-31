﻿namespace RulesEng.Factory
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
            if (rule.RateDifferece < 0)
            {
                throw new ArgumentOutOfRangeException($"Please use valid interest rate(> 0) for rule: {rule.Name}.", new Exception());
            }

            List<USState> matchStates = new List<USState>();

            foreach (string state in stateMatchRule.Condition)
            {
                if (string.IsNullOrWhiteSpace(state))
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
