using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;

namespace LethalFoodPoisoning
{
    public class Settings : ModSettings
    {
        public float foodPoisonChanceRawFood = 0.95f;
        public float foodPoisonChanceRawPlant = 0.95f;
        public float foodPoisonChanceMeal = 0.3f;
        public float foodPoisonChanceRottingCorpse = 0.1f;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref foodPoisonChanceRawFood, "foodPoisonChanceRawFood", 0.95f);
            Scribe_Values.Look(ref foodPoisonChanceRawPlant, "foodPoisonChanceRawPlant", 0.95f);
            Scribe_Values.Look(ref foodPoisonChanceMeal, "foodPoisonChanceMeal", 0.3f);
            Scribe_Values.Look(ref foodPoisonChanceRottingCorpse, "foodPoisonChanceRottingCorpse", 0.3f);
            base.ExposeData();
        }
    }

    public class LethalFoodPoisoning : Mod
    {
        Settings settings;

        public LethalFoodPoisoning(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);
            //listingStandard.Label("Chance of non-lethal food poisoning for raw foods");
            //settings.foodPoisonChanceRawFood = listingStandard.Slider(settings.foodPoisonChanceRawFood, 0f, 1f);
            //listingStandard.Label("Chance of non-lethal food poisoning for meals");
            //settings.foodPoisonChanceMeal = listingStandard.Slider(settings.foodPoisonChanceMeal, 0f, 1f);
            //base.DoSettingsWindowContents(inRect);

            var area1 = listingStandard.GetRect(32);
            var area2 = listingStandard.GetRect(64);
            var area3 = listingStandard.GetRect(96);
            var area4 = listingStandard.GetRect(128);
            settings.foodPoisonChanceRawFood = Widgets.HorizontalSlider(area1, settings.foodPoisonChanceRawFood, 0f, 1f, label: "Chance for food poisoning to be non-lethal (Raw animal food).", leftAlignedLabel: "0%", rightAlignedLabel: "100%");
            settings.foodPoisonChanceRawFood = Widgets.HorizontalSlider(area2, settings.foodPoisonChanceRawPlant, 0f, 1f, label: "Chance for food poisoning to be non-lethal (Raw plant food).", leftAlignedLabel: "0%", rightAlignedLabel: "100%");
            settings.foodPoisonChanceMeal = Widgets.HorizontalSlider(area3, settings.foodPoisonChanceMeal, 0f, 1f, label: "Chance for food poisoning to be non-lethal (Meals).", leftAlignedLabel: "0%", rightAlignedLabel: "100%");
            settings.foodPoisonChanceRottingCorpse = Widgets.HorizontalSlider(area4, settings.foodPoisonChanceRottingCorpse, 0f, 1f, label: "Chance for food poisoning to be non-lethal (Corpses).", leftAlignedLabel: "0%", rightAlignedLabel: "100%");
            listingStandard.End();
            base.DoSettingsWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "LethalFoodPoisoning".Translate();
        }
    }
}
