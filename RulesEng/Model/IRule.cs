namespace RulesEng.Model
{
    public interface IRule
    {
        public void RunRuleAction(Product product);

        public bool RunRuleCondition(Person person, Product product);
    }
}
