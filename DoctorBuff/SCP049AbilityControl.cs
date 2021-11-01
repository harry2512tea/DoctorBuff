﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Hints;
using MEC;
using UnityEngine;
using CommandSystem;
using Player = Exiled.API.Features.Player;

namespace DoctorBuff
{
    public class SCP049AbilityControl
    {

        private readonly DoctorBuff doctorBuff;
        public SCP049AbilityControl(DoctorBuff doctorBuff) => this.doctorBuff = doctorBuff;

        public static uint CureCounter = 0;
        public static ushort Cooldown;
        private static float Radius = DoctorBuff.config.HealRadius;
        private static float HealAmountFlat = DoctorBuff.config.HealAmountFlat;
        public static float HealAmountPercent = DoctorBuff.config.ZomHealAmountPercentage;
        private static byte HealType = DoctorBuff.config.HealType;

        public static IEnumerator<float> EngageBuff()
        {
            while (true)
            {
                foreach (Player D in Player.List.Where(r => r.Role == RoleType.Scp049))
                {
                    foreach (Player Z in Player.List.Where(r => r.Role == RoleType.Scp0492))
                    {
                        if (Vector3.Distance(D.Position, Z.Position) <= Radius)
                        {
                            ApplyHeal(HealType, Z, HealAmountFlat, HealAmountPercent);
                        }
                    }
                }
                yield return Timing.WaitForSeconds(5f);
            }
        }

        private static void ApplyHeal(byte type, Player p, float flat, float multi)
        {
            float HpGiven;
            bool CanDisplay = true;
            float MissingHP = p.MaxHealth - p.Health;

            if (p.Health == p.MaxHealth)
            {
                CanDisplay = false;
            }

            if (type ==0)
            {
                if (p.Health + flat > p.MaxHealth)
                {
                    HpGiven = p.MaxHealth - p.Health;
                    p.Health = p.MaxHealth;
                }
                else
                {
                    HpGiven = flat;
                    p.Health += flat;
                }
            }

            else
            {
                if (p.Health + (MissingHP * multi) > p.MaxHealth)
                {
                    HpGiven = p.MaxHealth - p.Health;
                    p.Health = p.MaxHealth;
                }
                else
                {
                    HpGiven = MissingHP * multi;
                    p.Health += MissingHP * multi;
                }
            }

            if (CanDisplay)
            {
                p.HintDisplay.Show(new TextHint($"<color=red>+{HpGiven} HP</color>", new HintParameter[] { new StringHintParameter("") }, null, 2f));
            }
        }

        public static void ApplySelfHeal(Player p, float missing)
        {
            float MissingHP = p.MaxHealth - p.Health;
            if (p.Health + MissingHP * missing > p.MaxHealth)
            {
                p.Health = p.MaxHealth;
            }
            
        }

        public static IEnumerator<float> StartCooldownTimer()
        {
            while(Cooldown != 0)
            {
                Cooldown--;
                yield return Timing.WaitForSeconds(1f);
            }

            foreach(Player D in Player.List.Where(p => p.Role == RoleType.Scp049))
            {
                D.HintDisplay.Show(new TextHint(DoctorBuff.config.Translation_Active_ReadyNotification, new HintParameter[] { new StringHintParameter("") }, null, 5f));
            }

            Timing.KillCoroutines("SCP049_Active_Cooldown");
        }
    }
    [CommandHandler(typeof(ClientCommandHandler))]
    class reinforcements : ICommand
    {
        //private readonly DoctorBuff doctorBuff;
        //public reinforcements(DoctorBuff doctorBuff) => this.doctorBuff = doctorBuff;
        public string Command { get; } = "CallRefincorcements";

        public string[] Aliases { get; } = { "cr" };

        public string Description { get; } = "Summons a zombie from the spectators";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((RemoteAdmin.PlayerCommandSender)sender);
            if (player.Role == RoleType.Scp049)
            {
                List<Player> list = Player.List.Where(r => r.Role == RoleType.Spectator).ToList();

                if (SCP049AbilityControl.CureCounter < DoctorBuff.config.MinCures)
                {
                    response = $"Not enough revives";
                    return true;
                }

                if (SCP049AbilityControl.Cooldown > 0)
                {
                    response = $"Ability is still on cooldown. Time remaining: { SCP049AbilityControl.Cooldown }";
                    return true;
                }

                if (list.IsEmpty())
                {
                    response = "No Spectators to spawn as Zombies";
                    return true;
                }

                var index = 0;
                index += new System.Random().Next(list.Count);
                var selected = list[index];

                selected.SetRole(RoleType.Scp0492);
                selected.Health = selected.MaxHealth;

                Timing.CallDelayed(0.5f, () =>
                {
                    selected.Position = new Vector3(player.Position.x, player.Position.y, player.Position.z);
                });

                index = 0;

                SCP049AbilityControl.Cooldown = DoctorBuff.config.Cooldown;
                Timing.RunCoroutine(SCP049AbilityControl.StartCooldownTimer(), "SCP049_Active_Cooldown");
                response = "summoning Zombie";
                return true;
            }
            else
            {
                response = "you have to be 049 to summon zombies";
                return false;
            }
        }
    }
}