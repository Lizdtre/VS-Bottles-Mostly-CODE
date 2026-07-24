using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace BottlesMostly.Patches;

[HarmonyPatch(typeof(BlockLiquidContainerBase), "tryEatStop")]
public class TryEatStopBlockLiquidContainerBase
{
    private static bool firstCall;
    
    [HarmonyPrefix]
    static void Prefix(float secondsUsed, ItemSlot slot, EntityAgent byEntity, out bool __state, BlockLiquidContainerBase __instance)
    {
        firstCall = true;
        __state = false;
        ItemStack itemstack = slot.Itemstack; 
        if(!(itemstack == null || __instance.IsEmpty(itemstack)))
        {
            if ((double)secondsUsed >= 0.949999988079071 && byEntity.World is IServerWorldAccessor world)
            {
                __state = true;
            }
        }
    }

    [HarmonyPostfix]
    static void Postfix(float secondsUsed, ItemSlot slot, EntityAgent byEntity, bool __state)
    {
        if (!firstCall) return;
        firstCall = false;
        if (byEntity?.World?.Api.Side != EnumAppSide.Server) return;
        
        if (__state && slot.Itemstack.Collectible.GetMaxDurability(slot.Itemstack) > 0)
        {
            slot.Itemstack.Collectible.DamageItem(byEntity.World, byEntity, slot, 1);
        }
    }
}