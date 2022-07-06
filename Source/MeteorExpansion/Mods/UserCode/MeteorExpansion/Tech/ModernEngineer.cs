namespace Eco.Mods.TechTree
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
    using Eco.Core.Plugins.Interfaces;

    [Serialized]
    [LocDisplayName("Modern Engineer")]
    [Tag("Profession")]
    public partial class ModernEngineerSkill : Skill
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern Engineer's use processed advanced materials to craft a variety of useful products. Advanced materials are of great use when dealing with a massive techonogical disadvantage versus global disaster."); } }


        public override string Title { get { return Localizer.DoStr("Modern Engineer"); } }

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
    public class ApplyNewRootSkills : IModKitPlugin, IInitializablePlugin
    {
        public string GetStatus()
        {
            return String.Empty;
        }

        public void Initialize(TimedTask timer)
        {
            UserManager.OnUserLoggedIn.Add(u => {
                if (!u.Skillset.HasSkill(typeof(ModernEngineerSkill)))
                    u.Skillset.LearnSkill(typeof(ModernEngineerSkill));
                if (!u.Skillset.HasSkill(typeof(MaterialExpertSkill)))
                    u.Skillset.LearnSkill(typeof(MaterialExpertSkill));
            });
        }
    }


}
