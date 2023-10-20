// <copyright file="PersonalizedProductPricing.cs" company="RulesEng">
// Copyright (c) RulesEng. All rights reserved.
// </copyright>

namespace RulesEng.Model
{
    public class PersonalizedProductPricing
    {
        public Person Person { get; set; }

        public Product[] Products { get; set; }
    }
}