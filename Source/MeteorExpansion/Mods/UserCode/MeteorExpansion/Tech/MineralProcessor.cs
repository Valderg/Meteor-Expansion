

namespace Vald.ME.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Core.Items;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Mods.TechTree;
    using Vald.ME.TechTree;

    [Serialized]
    [LocDisplayName("Material Processor")]
    [Ecopedia("Professions", "Mineral Expert", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]
    [RequiresSkill(typeof(MineralExpertSkill), 0), Tag("Mineral Expert Specialty"), Tier(5)]
    [Tag("Specialty")]
    [Tag("Teachable")]
    public partial class MaterialProcessorSkill : Skill
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Learn how to process modern material into new alloys and new types of resources! Level by refining advanced minerals and crafting related recipes."); } }

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20, Localizer.DoStr("for leveling up another specialization."));
        }


        public static MultiplicativeStrategy MultiplicativeStrategy =
            new MultiplicativeStrategy(new float[] {
                1,
                1 - 0.5f,
                1 - 0.55f,
                1 - 0.6f,
                1 - 0.65f,
                1 - 0.7f,
                1 - 0.75f,
                1 - 0.8f,
            });
        public override MultiplicativeStrategy MultiStrategy => MultiplicativeStrategy;

        public static AdditiveStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] {
                0,
                0.5f,
                0.55f,
                0.6f,
                0.65f,
                0.7f,
                0.75f,
                0.8f,
            });
        public override AdditiveStrategy AddStrategy => AdditiveStrategy;
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 5; } }
    }

    [Serialized]
    [LocDisplayName("Material Processor Skill Book")]
    [Ecopedia("Items", "Skill Books", createAsSubPage: true, display: InPageTooltip.DynamicTooltip)]
    public partial class MaterialProcessorSkillBook : SkillBook<MaterialProcessorSkill, MaterialProcessorSkillScroll> { }

    [Serialized]
    [LocDisplayName("Material Processor Skill Scroll")]
    public partial class MaterialProcessorSkillScroll : SkillScroll<MaterialProcessorSkill, MaterialProcessorSkillBook> { }


    [RequiresSkill(typeof(IndustrySkill), 1)]
    public partial class MaterialProcessorSkillBookRecipe : RecipeFamily
    {
        public MaterialProcessorSkillBookRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                "MaterialProcessor",  //noloc
                Localizer.DoStr("MaterialProcessor Skill Book"),
                new List<IngredientElement>
                {
                    new IngredientElement(typeof(DendrologyResearchPaperBasicItem), 3, typeof(AdvancedSmeltingSkill)),
                },
                new List<CraftingElement>
                {
                    new CraftingElement<MaterialProcessorSkillBook>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.LaborInCalories = CreateLaborInCaloriesValue(500, typeof(AdvancedSmeltingSkill));
            this.CraftMinutes = CreateCraftTimeValue(typeof(MaterialProcessorSkillBookRecipe), 5, typeof(AdvancedSmeltingSkill));
            this.ModsPreInitialize();
            this.Initialize(Localizer.DoStr("Material Processor Skill Book"), typeof(MaterialProcessorSkillBookRecipe));
            this.ModsPostInitialize();
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();
        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
