using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace BottlesMostly.Blocks;

/*
 * Cheeky fix for not being able to attach containers holding consumable liquids to entities (vanilla block behavior for eating/drinking always takes precedent)
 * TODO: implementing this as harmony patch??
 */
public class BlockLiquidContainerAttachable : BlockLiquidContainerTopOpened{
    
    
    public override void OnHeldInteractStart(ItemSlot itemslot, EntityAgent byEntity, BlockSelection blockSel, EntitySelection entitySel,
        bool firstEvent, ref EnumHandHandling handHandling){
        if (entitySel != null){
            foreach (var b in entitySel.Entity.SidedProperties.Behaviors){
                if (b is EntityBehaviorAttachable){
                    var handling = EnumHandling.PassThrough;
                    b.OnInteract(byEntity, itemslot, entitySel.HitPosition, EnumInteractMode.Interact, ref handling);
                    if (handling == EnumHandling.PreventSubsequent)
                        return;
                }
                
            }
        }
        base.OnHeldInteractStart(itemslot, byEntity, blockSel, entitySel, firstEvent, ref handHandling);
    }

    // protected override void tryEatStop(float secondsUsed, ItemSlot slot, EntityAgent byEntity){
    //     var doDamage = false;
    //     ItemStack itemstack = slot.Itemstack;
    //     if (!(itemstack == null || this.IsEmpty(itemstack))){
    //         if (!((double)secondsUsed < 0.949999988079071 || !(byEntity.World is IServerWorldAccessor world))){
    //             WaterTightContainableProps contentProps = this.GetContentProps(itemstack);
    //             FoodNutritionProperties nutritionProperties =
    //                 this.GetNutritionPropertiesPerLitre((IWorldAccessor)world, itemstack, (Entity)byEntity)?.Clone();
    //             if (!(contentProps == null || nutritionProperties == null)){
    //                 doDamage = true;
    //             }
    //         }
    //     }
    //
    //     base.tryEatStop(secondsUsed, slot, byEntity);
    //
    //     if (itemstack != null && doDamage && slot.Itemstack.Collectible.Durability != 0){
    //         slot.Itemstack.Collectible.DamageItem(byEntity.World, (Entity)byEntity, slot);
    //     }
    // }
}