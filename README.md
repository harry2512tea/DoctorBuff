# DoctorBuff
A plugin that reworks SCP-049 and SCP-049-2 instances, adding new active and passive abilities. This plugin allows easy buffs of zombies, and makes them more fun to play. The doctor recieves two new abilties that make them more fun to play, and make it easier to build zombie hoards!

## Features
* The doctor can heal zombies within a specified area around them.
* The doctor can summon zombies from the spectator chat after a specified cooldown.
* Option for zombies to deal AOE damage, making them more dangerous in groups.
* Zombies can infect other players.
* On being killed by a zombie, the player turns into a zombie.
* Ability to set zombie's base health.
* Ability to change the chance to become infected
* Option to make any infected become zombies regardless of the circumstances of their death

## Releases
[Releases found here](https://github.com/harry2512tea/DoctorBuff/releases)

## Config:
| Option | Value Type | Default | Description |
| ------ | :--------: | :-----: | ----------: |
| Is-Enabled | Bool | True | Enable or disable the plugin |
| MinCures | Int | 3 | Sets the minimum number of cures for abilities to activate |
| HealRadius | Float | 2.6f | Sets the radius aroundthe doctor in which zombies heal |
| HealAmountFlat | Float | 15.0f | The amount of HP zombies regain when healed by the doctor |
| ZomHealAmountPercentage | Float | 10.0f | The base percentage of health zombies regain when healed by the doctor |
| HealType | Int | 0 | The healing type (percentage or base rate) used when healing zombies |
| HealAmountMultiplier | Float | 1.3f | Multiplier for the ZomHealAmountPercentage value every time a Doctor revives someone |
| Cooldown | ushort | 60 | Cooldown for the doctor's active abilities in seconds |
| DocHeal | Bool | false | Whether or not the doctor regains health after every revive |
| HealthRecover | Float | 15.0f | Percentage of health to be healed after revive |
| ZombieEnableAOE | Bool | false | Whether or not zombies should deal AOE damage |
| ZombieHealth | int | 300 | Base health for zombies |
| ZombieInfection | Bool | true | Whether zombie infection should be enabled or not |
| InfectionChance | Float | 75 | The percentage chance of being infected after a zombie hits you |
| InfectInterval | Float | 2.0f | Time between infection damage ticks |
| InfectDamage | Float | 5f | Damage done per infection tick |
| HealChance | float | 50f | Percentage chance to cure the infection using a medkit |
| InfectedAlwaysTurn | Bool | false | whether or not an infected individual should turn into a zombie, regardless of how they die |

## Disclaimer
This plugin is heavily influenced by the initial creator of [this plugin](https://github.com/rby-blackruby/DocRework/tree/master) who is no longer supporting their version.
