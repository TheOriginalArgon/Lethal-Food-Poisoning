using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace LethalFoodPoisoning
{
    //public class CompProperties_FoodPoisoning : CompProperties
    //{
    //    public CompProperties_FoodPoisoning()
    //    {
    //        compClass = typeof(CompFoodPoisoning);
    //    }
    //}

    //public class CompFoodPoisoning : CompFoodPoisonable
    //{
    //    private float poisonPct;

    //    public override void PostIngested(Pawn ingester)
    //    {
    //        if (Rand.Chance(poisonPct * FoodUtility.GetFoodPoisonChanceFactor(ingester)))
    //        {
    //            FoodUtility.AddFoodPoisoningHediff(ingester, parent, cause);
    //        }
    //    }
    //}

    //public static class FoodPoisoningUtility
    //{
    //    public static void AddFoodPoisoningHediff(Pawn pawn, Thing ingestible, FoodPoisonCause cause)
    //    {
    //        //Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.FoodPoisoning);
    //        Hediff firstHediffOfDef = pawn.health.hediffSet.
    //        if (firstHediffOfDef != null)
    //        {
    //            if (firstHediffOfDef.CurStageIndex != 2)
    //            {
    //                firstHediffOfDef.Severity = DefDatabase<HediffDef>.GetNamed("LFT_FoodPoisoning").stages[2].minSeverity - 0.001f;
    //            }
    //        }
    //        else
    //        {
    //            pawn.health.AddHediff(HediffMaker.MakeHediff(DefDatabase<HediffDef>.GetNamed("LFT_FoodPoisoning"), pawn));
    //        }
    //        if (PawnUtility.ShouldSendNotificationAbout(pawn) && MessagesRepeatAvoider.MessageShowAllowed("MessageFoodPoisoning-" + pawn.thingIDNumber, 0.1f))
    //        {
    //            Messages.Message("MessageFoodPoisoning".Translate(pawn.LabelShort, ingestible.LabelCapNoCount, cause.ToStringHuman().CapitalizeFirst(), pawn.Named("PAWN"), ingestible.Named("FOOD")).CapitalizeFirst(), pawn, MessageTypeDefOf.NegativeEvent);
    //        }
    //    }

    //}

    [StaticConstructorOnStartup]
    public class Patcher
    {
        static Patcher()
        {
            var harmony = new Harmony("Argon.LFP");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("AddFoodPoisoningHediff")]
    class Patch
    {
        static void Postfix(ref Pawn pawn, ref Thing ingestible, ref FoodPoisonCause cause)
        {
            if (ingestible.GetStatValue(StatDefOf.FoodPoisonChanceFixedHuman) > 0 && ingestible.def.ingestible.foodType != FoodTypeFlags.VegetableOrFruit && !ingestible.def.IsCorpse)
            {
                if (Rand.Chance(LoadedModManager.GetMod<LethalFoodPoisoning>().GetSettings<Settings>().foodPoisonChanceRawFood))
                {
                    pawn.health.AddHediff(HediffMaker.MakeHediff(DefDatabase<HediffDef>.GetNamed("LFP_FoodPoisoning_Slight"), pawn));
                    pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.FoodPoisoning));
                }
            }
            else if (ingestible.GetStatValue(StatDefOf.FoodPoisonChanceFixedHuman) > 0 && ingestible.def.ingestible.foodType != FoodTypeFlags.Meat && !ingestible.def.IsCorpse)
            {
                if (Rand.Chance(LoadedModManager.GetMod<LethalFoodPoisoning>().GetSettings<Settings>().foodPoisonChanceRawFood))
                {
                    pawn.health.AddHediff(HediffMaker.MakeHediff(DefDatabase<HediffDef>.GetNamed("LFP_FoodPoisoning_Slight"), pawn));
                    pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.FoodPoisoning));
                }
            }
            else if (ingestible.GetStatValue(StatDefOf.FoodPoisonChanceFixedHuman) <= 0 && !ingestible.def.IsCorpse)
            {
                if (Rand.Chance(LoadedModManager.GetMod<LethalFoodPoisoning>().GetSettings<Settings>().foodPoisonChanceMeal))
                {
                    pawn.health.AddHediff(HediffMaker.MakeHediff(DefDatabase<HediffDef>.GetNamed("LFP_FoodPoisoning_Slight"), pawn));
                    pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.FoodPoisoning));
                }
            }
            else if (ingestible.def.IsCorpse)
            {
                if (Rand.Chance(LoadedModManager.GetMod<LethalFoodPoisoning>().GetSettings<Settings>().foodPoisonChanceRottingCorpse))
                {
                    pawn.health.AddHediff(HediffMaker.MakeHediff(DefDatabase<HediffDef>.GetNamed("LFP_FoodPoisoning_Slight"), pawn));
                    pawn.health.RemoveHediff(pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.FoodPoisoning));
                }
            }
        }
    }

}
