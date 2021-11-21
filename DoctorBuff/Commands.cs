using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace DoctorBuff
{
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
