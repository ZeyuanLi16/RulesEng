// <copyright file="Program.cs" company="RulesEng">
// Copyright (c) RulesEng. All rights reserved.
// </copyright>

namespace RulesEng
{
    using AutoMapper;
    using Newtonsoft.Json;
    using RulesEng.Model;
    using RulesEng.RulesCreator;

    class Program
    {
        private const string ConfigPath = $@"Configration\";

        public static void Main(string[] args)
        {
            string settingContent = File.ReadAllText($@"{ConfigPath}\Settings.json");
            Setting setting = JsonConvert.DeserializeObject<Setting>(settingContent) !;            
            InitializeRulesEngine(setting);


        }

        private static RulesEngine InitializeRulesEngine(Setting setting)
        {


            string personContent = File.ReadAllText($@"Configration\Persons.json");

            Person[] persons = JsonConvert.DeserializeObject<Person[]>(personContent) !;


            return new RulesEngine()
            {
                Products = InitializeProducts(setting.DefaultInterestRate),
                Persons = persons,
                Rules = InitializeRules(),
            };
        }

        private static Product[] InitializeProducts(double defaultRate)
        {
            string productContent = File.ReadAllText($@"{ConfigPath}\Products.json");
            Product[] products = JsonConvert.DeserializeObject<Product[]>(productContent) !;
            foreach (Product product in products)
            {
                product.InterstRate = defaultRate;
            }

            return products;
        }

        private static Rule[] InitializeRules()
        {
            // Initialize and configure AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            IMapper mapper = configuration.CreateMapper();

            string ruleContent = File.ReadAllText($@"{ConfigPath}\Rules.json");
            Rule[] rules = JsonConvert.DeserializeObject<Rule[]>(ruleContent) !;
            List<Rule> initializedrules = new ();

            foreach (Rule rule in rules)
            {
                if (rule.Category == ConditionCategory.CreditScoreRange)
                {
                    initializedrules.Add(new CreditScoreRangeCreator(rule, mapper).CreateRule());
                }
            }

            return initializedrules.ToArray();
        }
    }
}
