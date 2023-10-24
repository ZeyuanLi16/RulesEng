namespace RulesEng
{
    using System;
    using AutoMapper;
    using Newtonsoft.Json;
    using RulesEng.Factory;
    using RulesEng.Model;

    public class RulesEngine
    {
        private IMapper mapper;

        private string configPath;

        public Person[] persons { get; set; }

        public IRule[] rules { get; set; }

        public Product[] products { get; set; }

        public List<Tuple<Person, List<Product>>> Solutions { get; private set; }

        public RulesEngine(Setting setting, IMapper mapper, string configPath)
        {
            if (setting == null || setting.DefaultInterestRate < 0)
            {
                throw new ArgumentNullException($"Please use valid interest rate(> 0) in the setting config.", new Exception());
            }

            if (mapper == null)
            {
                throw new ArgumentNullException($"Mapper is null.", new Exception());
            }

            if (string.IsNullOrWhiteSpace(configPath))
            {
                throw new ArgumentNullException($"Please use valid interest rate(> 0) in the setting config.", new Exception());
            }

            this.mapper = mapper;
            this.configPath = configPath;
            this.persons = this.InitializePersons();
            this.products = this.InitializeProducts(setting.DefaultInterestRate);
            this.rules = this.InitializeRules();
        }

        public void Run()
        {
            this.Solutions = new List<Tuple<Person, List<Product>>>();
            foreach (Person person in this.persons)
            {
                List<Product> products = new List<Product>();
                foreach (Product originalProduct in this.products)
                {
                    // Create a deep copy from the original product.
                    Product product = new Product(originalProduct);

                    // Run rule condition and action if condition is met.
                    foreach (IRule rule in this.rules)
                    {
                        if (rule.RunRuleCondition(person, product))
                        {
                            rule.RunRuleAction(product);
                        }
                    }

                    if (product.InterstRate < 0)
                    {
                        product.InterstRate = 0;
                    }

                    products.Add(product);
                }

                this.Solutions.Add(new Tuple<Person, List<Product>>(person, products));
            }
        }

        public void PrintSolution()
        {
            if (this.Solutions == null || this.Solutions.Count == 0)
            {
                Console.WriteLine("Solition is empty. Please make sure the RuleEngine has run and the configration is correct.");
            }
            else
            {
                Console.WriteLine("{0,-12} | {1,-12} | {2,-12} | {3,-13} | {4,-13} | {5,-13}", "Person Name", "Credit Score", "State", "Product Name", "Interest Rate", "Disqualified");
                Console.WriteLine(new string('-', 90));

                foreach (var solution in this.Solutions)
                {
                    Person person = solution.Item1;
                    List<Product> products = solution.Item2;

                    foreach (var product in products)
                    {
                        Console.WriteLine("{0,-12} | {1,-12} | {2,-12} | {3,-13} | {4,-13} | {5,-13}", person.Name, person.CreditScore, person.State, product.Name, product.InterstRate, product.Disqualified);
                    }
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public Person[] InitializePersons()
        {
            string personContent = File.ReadAllText($@"{this.configPath}\Persons.json");
            Person[] persons = JsonConvert.DeserializeObject<Person[]>(personContent) !;
            foreach (Person person in persons)
            {
                if (person.CreditScore < 300 || person.CreditScore > 850)
                {
                    throw new ArgumentOutOfRangeException($"Please use valid credit score range(300 - 850) for person named {person.Name}.", new Exception());
                }
            }

            return persons;
        }

        public Product[] InitializeProducts(double defaultRate)
        {
            string productContent = File.ReadAllText($@"{this.configPath}\Products.json");
            Product[] products = JsonConvert.DeserializeObject<Product[]>(productContent) !;
            foreach (Product product in products)
            {
                if (product.InterstRate == null)
                {
                    product.InterstRate = defaultRate;
                }
                else if (product.InterstRate < 0)
                {
                    throw new ArgumentOutOfRangeException($"Please use valid interest rate(> 0) for product: {product.Name}.", new Exception());
                }
            }

            return products;
        }

        public IRule[] InitializeRules()
        {
            string ruleContent = File.ReadAllText($@"{this.configPath}\Rules.json");
            Rule[] rules = JsonConvert.DeserializeObject<Rule[]>(ruleContent) !;

            Dictionary<ConditionCategory, RuleFactory> factoryDict = new ();
            factoryDict.Add(ConditionCategory.CreditScoreRange, new CreditScoreRangeFactory(this.mapper));
            factoryDict.Add(ConditionCategory.ProductNameContain, new ProductNameContainFactory(this.mapper));
            factoryDict.Add(ConditionCategory.ProductNameMatch, new ProductNameMatchFactory(this.mapper));
            factoryDict.Add(ConditionCategory.StateMatch, new StateMatchFactory(this.mapper));

            List<IRule> initializedRules = new ();

            foreach (Rule rule in rules)
            {
                initializedRules.Add(factoryDict[rule.Category].CreateRule(rule));
            }

            return initializedRules.ToArray();
        }
    }
}