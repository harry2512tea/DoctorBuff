using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using Doc = Exiled.Events.Handlers.Scp049;
using Server = Exiled.Events.Handlers.Server;
using Player = Exiled.Events.Handlers.Player;

namespace DoctorBuff
{
    public class DoctorBuff : Plugin<Config>
    {
        public static Config config;
        public EventHandler EventHandler;
        public override string Name { get; } = "Doctor Buff";
        public override string Author { get; } = "Thire";
        public override Version Version { get; } = new Version(0, 3, 0);
        public override Version RequiredExiledVersion { get; } = new Version(3, 3, 1);
        public override string Prefix { get; } = "DB";
        public override PluginPriority Priority { get; } = PluginPriority.Medium;

        internal static DoctorBuff Instance;
        public override void OnEnabled()
        {
            Instance = this;

            Log.Info("DocBuff is currently enabled, thank you!");
            config = Config;
            EventHandler = new EventHandler();
            Player.ChangingRole += EventHandler.OnRoleChange;
            Doc.FinishingRecall += EventHandler.OnFinishingRecall;
            Server.RoundStarted += EventHandler.OnRoundStart;
            Player.Hurting += EventHandler.OnPlayerHit;
            Player.ItemUsed += EventHandler.UseItem;
        }
        public override void OnDisabled()
        {

            try
            {
                Timing.KillCoroutines("SCP049_Active");
                Timing.KillCoroutines("SCP049_Active_Cooldown");
                Timing.KillCoroutines("Infection");
            }
            catch { }
            Player.ChangingRole -= EventHandler.OnRoleChange;
            Doc.FinishingRecall -= EventHandler.OnFinishingRecall;
            Server.RoundStarted -= EventHandler.OnRoundStart;
            Player.Hurting -= EventHandler.OnPlayerHit;
            Player.ItemUsed -= EventHandler.UseItem;

            SCP049AbilityControl.CureCounter = 0;
            SCP049AbilityControl.Cooldown = Config.Cooldown;
        }
    }
}
