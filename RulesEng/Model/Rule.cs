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

        public void RunRuleAction(Product product)
        {
            if (this.IfDisqualified == true)
            {
                product.Disqualified = true;
            }

            if (this.IfIncrease != null)
            {
                product.InterstRate = product.InterstRate + ((bool)this.IfIncrease ? this.RateDifferece : -this.RateDifferece);
            }
        }
    }
}
