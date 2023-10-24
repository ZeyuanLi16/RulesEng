// <copyright file="Program.cs" company="RulesEng">
// Copyright (c) RulesEng. All rights reserved.
// </copyright>

namespace RulesEng
{
    using AutoMapper;
    using Newtonsoft.Json;
    using RulesEng.Model;

    class Program
    {
        private const string ConfigPath = $@"Configration\";

        public static void Main(string[] args)
        {
            // Load setting file.
            string settingContent = File.ReadAllText($@"{ConfigPath}\Settings.json");
            Setting setting = JsonConvert.DeserializeObject<Setting>(settingContent) !;

            // Initialize and configure AutoMapper.
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingConfig>();
            });

            IMapper mapper = configuration.CreateMapper();

            // Initialize a RulesEngine instance.
            RulesEngine rulesEng = new RulesEngine(setting, mapper, ConfigPath);

            // Run engine.
            rulesEng.Run();
            rulesEng.PrintSolution();
        }
    }
}
