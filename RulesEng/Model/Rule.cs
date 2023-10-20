namespace RulesEng.Model
{
    public class Rule
    {
        public string Name { get; set; }

        public ConditionCategory Category { get; set; }

        public string[] Condition { get; set; }

        public bool IfDisqualified { get; set; } = false;

        public bool? IfIncrease { get; set; } = null;

        public double RateDifferece { get; set; }

        public Product RunRuleAction(Product product)
        {
            throw new NotImplementedException();
        }

        public virtual bool RunRuleCondition(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
