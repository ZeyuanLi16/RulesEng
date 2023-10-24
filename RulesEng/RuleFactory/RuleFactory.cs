namespace RulesEng.Factory
{
    using AutoMapper;
    using RulesEng.Model;

    public abstract class RuleFactory
    {
        protected IMapper Mapper { get; set; }

        public abstract IRule CreateRule(Rule rule);
    }
}
