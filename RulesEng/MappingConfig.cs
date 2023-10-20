namespace RulesEng
{
    using AutoMapper;
    using RulesEng.Model;

    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            this.CreateMap<Rule, RuleCreditScoreRange>();
            this.CreateMap<Rule, RuleProductNameMatch>();
            this.CreateMap<Rule, RuleStateMatch>();
            this.CreateMap<Rule, RuleProductNameContain>();
        }
    }
}
