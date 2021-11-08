# DoctorBuff
A plugin that reworks SCP-049 and SCP-049-2 instances, adding new active and passive abilities.
## Features

## Releases

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
| InfectInterval | Float | 2.0f | Time between infection damage ticks |
| InfectDamage | Float | 5f | Damage done per infection tick |
| HealChance | float | 50f | Percentage chance to cure the infection using a medkit |
