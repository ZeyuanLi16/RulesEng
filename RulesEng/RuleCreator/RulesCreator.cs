namespace RulesEng.RulesCreator
{
    using AutoMapper;
    using RulesEng.Model;

    public abstract class RulesCreator
    {
        private Rule rule;

        public Rule Rule 
        {
            get { return this.rule; }
            set { this.rule = value; }
        }

        public abstract Rule CreateRule();
    }
}
