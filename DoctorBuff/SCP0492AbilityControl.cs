using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Hints;
using CommandSystem;
using MEC;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Player = Exiled.API.Features.Player;

namespace DoctorBuff
{
    class SCP0492AbilityControl
    {
        
        private readonly DoctorBuff doctorBuff;
        public SCP0492AbilityControl(DoctorBuff doctorBuff) => this.doctorBuff = doctorBuff;

        public static List<Player> Infected = new List<Player>();

        private static bool Infection = DoctorBuff.config.ZombieInfection;
        private static float InfectionInterval = DoctorBuff.config.InfectInterval;
        private static float InfectDamage = DoctorBuff.config.InfectDamage;
        public static void DealAOEDamage(Player A, Player T, float AOEDamage)
        {
            if (A.Role != RoleType.Scp0492 || T.Team == Team.SCP)
            {
                return;
            }

            foreach (Player p in Player.List.Where(r => r.Team != Team.SCP && r != A && !r.IsGodModeEnabled))
            {
                if (Vector3.Distance(A.Position, p.Position) > 1.65)
                {
                    return;
                }

                if (p.Health - AOEDamage > 0)
                {
                    p.Health -= AOEDamage;
                }
                else
                {
                    p.Health = 0;
                    p.Kill(DamageTypes.Scp0492);
                }
            }
        }

        public static void Infect(HurtingEventArgs ev)
        {
            if (ev.Attacker.Role == RoleType.Scp0492 && ev.Target.Team != Team.SCP)
            {
                if (Infected.Count == 0)
                {
                    try
                    {
                        Timing.KillCoroutines("Infection");
                    }
                    catch { }
                    Timing.RunCoroutine(SCP0492AbilityControl.DamageInfected(), "Infection");
                }

                if (!Infected.Contains(ev.Target))
                {
                    ev.Target.HintDisplay.Show(new TextHint(DoctorBuff.config.InfectedMessage, new HintParameter[] { new StringHintParameter("") }, null, 5f));
                    Infected.Add(ev.Target);
                }
            }
        }

        public static void Death(DyingEventArgs ev)
        {            
            try
            {
                Infected.Remove(ev.Target);
            }
            catch { }
        }

        public static void Cure(Player P)
        {
            if (Infected.Contains(P))
            {
                P.HintDisplay.Show(new TextHint(DoctorBuff.config.CuredMessage, new HintParameter[] { new StringHintParameter("") }, null, 5f));
                Infected.Remove(P);
            }
        }

        public static IEnumerator<float> DamageInfected()
        {
            Vector3 oldPos;
            while(true)
            {
                foreach(Player p in Infected)
                {
                    
                    if (p.Health - InfectDamage > 0 && Infection && !p.IsGodModeEnabled)
                    {
                        p.Hurt(InfectDamage);
                    }
                    else if (Infection && !p.IsGodModeEnabled)
                    {
                        oldPos = p.Position;


                        p.SetRole(RoleType.Scp0492);
                        Infected.Remove(p);

                        Timing.CallDelayed(0.5f, () =>
                        {
                            p.Position = new Vector3(oldPos.x, oldPos.y, oldPos.z);
                        });
                    }
                }
                yield return Timing.WaitForSeconds(InfectionInterval);
            }
        }

        

    }
}
