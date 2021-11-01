using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Interfaces;
using Exiled.API.Enums;
using System.ComponentModel;

namespace DoctorBuff
{
    public class Config : IConfig
    {
        [Description("Enable or disable DocRework's mechanics")]
        public bool IsEnabled { get; set; } = true;

        [Description("Set the minimum cure amount for the buff area to kick in")]
        public uint MinCures { get; set; } = 3;

        [Description("Size of 049's healing radius")]
        public float HealRadius { get; set; } = 2.6f;

        [Description("The amount of HP the Doc heals their Zombies")]
        public float HealAmountFlat { get; set; } = 15.0f;

        [Description("The base amount of missing % HP the Doc heals their Zombies at the start of their buff")]
        public float ZomHealAmountPercentage { get; set; } = 10.0f;

        [Description("Change between 049's arua's heal type: 0 is for flat HP, 1 is for missing % HP")]
        public byte HealType { get; set; } = 0;

        [Description("Multiplier for the ZomHealAmountPercentage value every time a Doctor revives someone")]
        public float HealAmountMultiplier { get; set; } = 1.3f;

        [Description("Cooldown for SCP049 active ability")]
        public ushort Cooldown { get; set; } = 60;

        [Description("Allow SCP-049 to be healed for a percentage of it's missing health every player revival")]
        public bool DocHeal { get; set; } = false;

        [Description("Percentage of SCP-049's missing health to be healed")]
        public float HealthRecover { get; set; } = 15.0f;

        [Description("Allow SCP-049-2 to damage everyone around upon hitting an enemy target")]
        public bool ZombieEnableAoe { get; set; } = false;

        [Description("Amount of health each person in 049-2's range loses by 049-2's AOE attack")]
        public float ZombieDamageAOE { get; set; } = 15.0f;

        [Description("The amount of health zombies are given")]
        public int ZombieHealth { get; set; } = 300;

        [Description("Whether or not getting hit by a zombie infects the player")]
        public bool ZombieInfection { get; set; } = true;

        [Description("Delay between infection damage")]
        public float InfectInterval { get; set; } = 2.0f;

        [Description("Amount of damage done per infection interval")]
        public float InfectDamage { get; set; } = 5f;

        [Description("Chance to cure infection with a medkit")]
        public float HealChance { get; set; } = 50f;

        [Description("Message displayed to players who are infected")]
        public string InfectedMessage { get; set; } = "You have been infected by a Zombie. \nUse SCP-500 or a medkit to cure yourself";

        [Description("Messaged displayed to players who cure themselves of the zombie infection")]
        public string CuredMessage { get; set; } = "You have cured yourself of the zombie infection";

        [Description("Hint displayed when the .cr ability's cooldown has expired")]
        public string Translation_Active_ReadyNotification { get; set; } = "<color=green>You can now use your active ability.\nUse .cr in your console to spawn a zombie from the spectators.</color>";

        [Description("Message sent to SCP-049 upon reaching the minimum cures amount required")]
        public string Translation_Passive_ActivationMessage { get; set; } = "<color=red>Your passive ability is now activated.\nYou now heal zombies around you every 5 seconds.</color>";
        
    }
}
