using System.Linq;
using System.Collections.Generic;
using Exiled.Events.EventArgs;
using Player = Exiled.API.Features.Player;
using MEC;
using Hints;
using UnityEngine;
using System;

namespace DoctorBuff
{
    public class EventHandler
    {
        
        public static void OnFinishingRecall(FinishingRecallEventArgs ev)
        {
            if (!RoundSummary.RoundInProgress())
            {
                return;
            }
            SCP049AbilityControl.CureCounter++;

            if (SCP049AbilityControl.CureCounter == DoctorBuff.config.MinCures)
            {
                foreach (Player D in Player.List.Where(r => r.Role == RoleType.Scp049))
                {
                    D.HintDisplay.Show(new TextHint(DoctorBuff.config.Translation_Passive_ActivationMessage, new HintParameter[] { new StringHintParameter("") }, null, 5f));
                }

                Timing.RunCoroutine(SCP049AbilityControl.EngageBuff(), "SCP049_Passive");
                Timing.RunCoroutine(SCP049AbilityControl.StartCooldownTimer(), "SCP049_Active_Cooldown");
            }

            if (DoctorBuff.config.HealType == 1 && SCP049AbilityControl.CureCounter > DoctorBuff.config.MinCures)
            {
                DoctorBuff.config.ZomHealAmountPercentage *= DoctorBuff.config.HealAmountMultiplier;
            }

            if (DoctorBuff.config.DocHeal)
            {
                SCP049AbilityControl.ApplySelfHeal(ev.Scp049, DoctorBuff.config.HealthRecover);
            }
        }

        public static void OnPlayerHit(HurtingEventArgs ev)
        {
            System.Random rnd = new System.Random();

            if (DoctorBuff.config.ZombieEnableAoe && SCP049AbilityControl.CureCounter >= DoctorBuff.config.MinCures)
            {
                SCP0492AbilityControl.DealAOEDamage(ev.Attacker, ev.Target, DoctorBuff.config.ZombieDamageAOE);
            }
            if (rnd.Next(1, 100) < DoctorBuff.config.InfectionChance)
            {
                SCP0492AbilityControl.Infect(ev);
            }
            
            
        }

        public static void OnPlayerDying(DyingEventArgs ev)
        {
            if (ev.Killer.Role == RoleType.Scp0492 && ev.Killer != ev.Target && ev.Killer != null)
            {
                Timing.CallDelayed(0.5f, () =>
                {
                    ev.Target.SetRole(RoleType.Scp0492, Exiled.API.Enums.SpawnReason.ForceClass, false);
                });

            }
            SCP0492AbilityControl.Death(ev);
        }

        public static void OnRoleChange(ChangingRoleEventArgs ev)
        {
            if (ev.NewRole == RoleType.Scp0492)
            {
                Timing.CallDelayed(4f, () =>
                {
                    ev.Player.MaxHealth = DoctorBuff.config.ZombieHealth;
                    ev.Player.Health = DoctorBuff.config.ZombieHealth;
                });
            }
        }

        public static void OnRoundStart()
        {
            try
            {
                Timing.KillCoroutines("SCP049_Passive");
                Timing.KillCoroutines("SCP049_Active_Cooldown");
                Timing.KillCoroutines("Infection");
            }
            catch { }

            SCP049AbilityControl.CureCounter = 0;
            SCP049AbilityControl.Cooldown = DoctorBuff.config.Cooldown;
            Timing.RunCoroutine(SCP0492AbilityControl.DamageInfected(), "Infection");
        }

        public static void UseItem(UsedItemEventArgs ev)
        {
            System.Random rnd = new System.Random();
            
            
            switch (ev.Item.Type)
            {
                case ItemType.Medkit:

                    if(rnd.Next(1, 100) < DoctorBuff.config.HealChance)
                    {
                        SCP0492AbilityControl.Cure(ev.Player);
                    }
                    break;

                case ItemType.SCP500:

                    SCP0492AbilityControl.Cure(ev.Player);
                    break;

            }
        }
    }
}
