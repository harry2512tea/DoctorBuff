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

        public static void Infect(Player A, Player T)
        {
            if (A.Role == RoleType.Scp0492 && T.Team != Team.SCP && !Infected.Contains(A))
            {
                T.HintDisplay.Show(new TextHint(DoctorBuff.config.InfectedMessage, new HintParameter[] { new StringHintParameter("") }, null, 5f));
                Infected.Add(A);
            }
        }

        public static void Cure(Player P)
        {
            P.HintDisplay.Show(new TextHint(DoctorBuff.config.CuredMessage, new HintParameter[] { new StringHintParameter("") }, null, 5f));
            Infected.Remove(P);
        }
        public static IEnumerator<float> DamageInfected()
        {
            while(true)
            {
                foreach(Player p in Infected)
                {
                    
                    if (p.Health - InfectDamage > 0 && Infection && !p.IsGodModeEnabled)
                    {
                        p.Health -= InfectDamage;
                    }
                    else if (Infection && !p.IsGodModeEnabled)
                    {
                        p.SetRole(RoleType.Scp0492);
                        Infected.Remove(p);
                    }
                }
                yield return Timing.WaitForSeconds(InfectionInterval);
            }
        }

        

    }
}
