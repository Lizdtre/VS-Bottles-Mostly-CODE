using BottlesMostly.Blocks;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace BottlesMostly;

public class BottlesMostlyModSystem : ModSystem
{
    public const string HarmonyID = "com.lizdtre.bottlesmostly";
    
    private Harmony harmony;

    public override void StartPre(ICoreAPI api)
    {
        base.StartPre(api);
        
        harmony = new Harmony(HarmonyID);
        harmony.PatchAll();
    }

    public override void Start(ICoreAPI api)
    {
        Mod.Logger.Notification("Hello from Bottles, Mostly mod: " + api.Side);
        api.RegisterBlockClass(Mod.Info.ModID + ".blockLiquidContainerAttachable", typeof(BlockLiquidContainerAttachable));
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        Mod.Logger.Notification("Hello from Bottles, Mostly mod server side: " + Lang.Get(Mod.Info.ModID + ":hello"));
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        Mod.Logger.Notification("Hello from Bottles, Mostly mod client side: " + Lang.Get(Mod.Info.ModID + ":hello"));
    }
}
