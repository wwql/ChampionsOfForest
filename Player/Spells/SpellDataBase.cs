﻿using System;
using System.Collections.Generic;

using ChampionsOfForest.Effects;
using ChampionsOfForest.Localization;

using TheForest.Utils;

namespace ChampionsOfForest.Player
{
	public static class SpellDataBase
	{
		public static Dictionary<int, Spell> spellDictionary = new Dictionary<int, Spell>();
		public static int[] SortedSpellIDs;

		public static void Reset()
		{
			foreach (var spell in spellDictionary.Values)
			{
				spell.Cooldown = spell.BaseCooldown;
			}
		}

		public static void Initialize()
		{
			try
			{
				spellDictionary = new Dictionary<int, Spell>();
				FillSpells();
				List<int> SortedSpellIDsTemp = new List<int>(spellDictionary.Keys);
				SortedSpellIDsTemp.Sort((x, y) => spellDictionary[x].LevelRequirement.CompareTo(spellDictionary[y].LevelRequirement));
				SortedSpellIDs = SortedSpellIDsTemp.ToArray();
				// ModAPI.Log.Write("SETUP: SPELL DB");
			}
			catch (Exception ex)
			{
				ModAPI.Log.Write(ex.ToString());
			}
		}

		public static void FillSpells()
		{
			Spell bh = new Spell(iD: 1, TextureID: 119, levelRequirement: 20, energyCost: 100, baseCooldown: 120, name: Translations.MainMenu_HUD_11/*og:Black Hole*/, () => string.Format(Translations.SpellDataBase_1/*og:Creates a black hole that pulls enemies in and damages over {0} seconds.\nScaling: {1} spell damage*/, ModdedPlayer.Stats.spell_blackhole_duration, ModdedPlayer.Stats.spell_blackhole_damageScaling))//tr
			{
				active = SpellActions.CreatePlayerBlackHole,
				CastOnRelease = true,
				aim = SpellActions.BlackHoleAim,
				aimEnd = SpellActions.BlackHoleAimEnd
			};
			Spell healingDome = new Spell(iD: 2, TextureID: 122, levelRequirement: 6, energyCost: 80, baseCooldown: 70, name: Translations.SpellDataBase_3/*og:Healing Dome*/, () => string.Format(Translations.SpellDataBase_2/*og:Creates a sphere of vaporized aloe that heals all allies inside.*/))//tr
			{
				active = SpellActions.CreateHealingDome,
				CastOnRelease = true,
				aim = SpellActions.HealingDomeAim,
				aimEnd = SpellActions.HealingDomeAimEnd
			};
			new Spell(iD: 3, TextureID: 121, levelRequirement: 3, energyCost: 25, baseCooldown: 14, name: Translations.SpellDataBase_5/*og:Blink*/, () => string.Format(Translations.SpellDataBase_4/*og:instantaneous short distance teleportation. Upgrades allow dealing damage to enemies that you pass through.\nScaling: {0}*/, ModdedPlayer.Stats.spell_blinkDamageScaling))//tr
			{
				active = SpellActions.DoBlink,
				CastOnRelease = true,
				aim = SpellActions.DoBlinkAim,
			};
			new Spell(iD: 4, TextureID: 120, levelRequirement: 10, energyCost: 100, baseCooldown: 70, name: Translations.SpellDataBase_7/*og:Sun Flare*/, () => string.Format(Translations.SpellDataBase_6/*og:Intense light focuses onto a single spot. Heals allies, reduces all damage taken by 50% and gives movement speed. Slows down and damages enemies*/))//tr
			{
				active = SpellActions.CastFlare,
			};
			new Spell(iD: 5, TextureID: 118, levelRequirement: 8, energyCost: 50, name: Translations.SpellDataBase_9/*og:Sustain Shield*/, () => string.Format(Translations.SpellDataBase_8/*og:Channeling this spell consumes energy but grants you a protective, absorbing shield, similiar to additional health. The shield's power increases every second untill reaching max value. The shield persists for {0} seconds after channeling stops, and after that it rapidly disperses.*/, ModdedPlayer.Stats.spell_shieldPersistanceLifetime))//tr
			{
				active = SpellActions.CastSustainShieldActive,
				passive = SpellActions.CastSustainShieldPassive,
				usePassiveOnUpdate = true,
			};
			new Spell(iD: 6, TextureID: 117, levelRequirement: 2, energyCost: 10, baseCooldown: 0.4f, name: Translations.SpellDataBase_11/*og:Wide Reach*/, () => Translations.SpellDataBase_10/*og:Picks up all items, including equipment, in a small radius around you.*/)//tr
			{
				active = AutoPickupItems.DoPickup,
				CastOnRelease = false,
			};
			new Spell(iD: 7, TextureID: 115, levelRequirement: 21, energyCost: 25, baseCooldown: 2.5f, name: Translations.SpellDataBase_13/*og:Black Flame*/, () => string.Format(Translations.SpellDataBase_12/*og:Ignites your melee weapon or your ranged projectile with a dark flame that burns enemies for a large amount of damage. Scaling: {0} spell damage*/, ModdedPlayer.Stats.spell_blackFlameDamageScaling))//tr
			{
				active = BlackFlame.Toggle,
			};
			new Spell(iD: 8, TextureID: 123, levelRequirement: 14, energyCost: 85, baseCooldown: 180, name: Translations.SpellDataBase_15/*og:War Cry*/, () => Translations.SpellDataBase_14/*og:Empowers you and nearby allies for 2 minutes.*/)//tr
			{
				active = SpellActions.CastWarCry,
			};
			new Spell(iD: 9, TextureID: 114, levelRequirement: 12, energyCost: 90, baseCooldown: 60, name: Translations.SpellDataBase_17/*og:Portal*/, () => Translations.SpellDataBase_16/*og:Creates a wormhole, that links 2 locations. Allows the player and items to pass through.*/)//tr
			{
				CastOnRelease= true,
				aim= SpellActions.DoPortalAim,
				active = SpellActions.CastPortal,
			};
			new Spell(iD: 10, TextureID: 125, levelRequirement: 27, energyCost: 100, baseCooldown: 20, name: Translations.SpellDataBase_19/*og:Magic Arrow*/, () => string.Format(Translations.SpellDataBase_18/*og:A large spectral arrow is shot where you're looking at. The arrow pierces everything, slows any enemies hit and deals big damage. Scaling: {0} spell damage*/, ModdedPlayer.Stats.spell_magicArrowDamageScaling))//tr
			{
				active = SpellActions.CastMagicArrow,
				CastOnRelease = true,
				aim = SpellActions.MagicArrowAim,
				aimEnd = SpellActions.MagicArrowAimEnd
			};
			new Spell(iD: 11, TextureID: 127, levelRequirement: 40, energyCost: 30, baseCooldown: 12, name: Translations.SpellDataBase_21/*og:Multishot Enchantment*/, () => Translations.SpellDataBase_20/*og:Enchants your ranged weapons with a magic formation of copying. Every shot causes multiple projectiles to be created with a downside of heavy energy toll. Energy is consumed upon firing and depends on the amount of projectiles fired*/)//tr
			{
				active = SpellActions.ToggleMultishot,
			};
			new Spell(iD: 12, TextureID: 133, levelRequirement: 35, energyCost: 65, baseCooldown: 160, name: Translations.SpellDataBase_23/*og:Golden Skin*/, () => Translations.SpellDataBase_22/*og:For 40 seconds you turn into solid gold, turning completely immune to stuns and your attacks are 20% faster*/)//tr
			{
				active = GoldenSkin.Cast,
			};
			new Spell(iD: 13, TextureID: 132, levelRequirement: 7, energyCost: 40, baseCooldown: 16, name: Translations.SpellDataBase_25/*og:Purge*/, () => Translations.SpellDataBase_24/*og:Everyone in your surroundings gets cleansed of their negative debuffs. Negates poison.*/)//tr
			{
				active = SpellActions.CastPurge,
			};
			new Spell(iD: 14, TextureID: 128, levelRequirement: 20, energyCost: 220, baseCooldown: 40, name: Translations.SpellDataBase_27/*og:Snap Freeze*/, () => string.Format(Translations.SpellDataBase_26/*og:Enemies around you get slowed for 12 seconds by 90% you deal magic damage to them.\nScaling{0} spell damage*/, ModdedPlayer.Stats.spell_snapDamageScaling))//tr
			{
				active = SpellActions.CastSnapFreeze,
			};
			new Spell(iD: 15, TextureID: 131, levelRequirement: 25, energyCost: 10, baseCooldown: 200, name: Translations.SpellDataBase_29/*og:Berserk*/, () => Translations.SpellDataBase_28/*og:For short amount of time, gain increased damage dealt, attack speed and movement speed and have unlimited stamina, however, you take increased damage.*/)//tr
			{
				active = Berserker.Cast,
			};
			new Spell(iD: 16, TextureID: 130, levelRequirement: 44, energyCost: 350, baseCooldown: 100, name: Translations.SpellDataBase_31/*og:Ball Lightning*/, () => string.Format(Translations.SpellDataBase_30/*og:A slow moving, bouncing ball of lightning travels forward, dealing damage to hit enemies, and upon contact or when it lasts too long, it explodes. Scales with 320% spell damage.\nScaling: {0} spell damage*/, ModdedPlayer.Stats.spell_ballLightning_DamageScaling))//tr
			{
				active = SpellActions.CastBallLightning,
			};
			new Spell(iD: 17, TextureID: 134, levelRequirement: 17, energyCost: 25, baseCooldown: 25, name: "Bash",
				() => string.Format(Translations.SpellDataBase_32/*og:Passive: Every attack slows enemies for {0} seconds, and increases their damage taken by {1}\nActive: Melee damage is increased for {2} seconds by {3}*/, ModdedPlayer.Stats.spell_bashDuration, ModdedPlayer.Stats.spell_bashDamageDebuffAmount, ModdedPlayer.Stats.spell_bashDuration, (ModdedPlayer.Stats.spell_bashDamageDebuffAmount-1).ToString("P")))//tr
			{
				passive = SpellActions.BashPassiveEnabled,
				active = SpellActions.BashActive
			};
			new Spell(18, 136, 1, 0, 60, Translations.SpellDataBase_34/*og:Frenzy*/, () => string.Format(Translations.SpellDataBase_33/*og:Passive: Every attack enrages you, increasing damage all damage by {0}. Up to {1} stacks.\nActive: Consume all of your frenzy stacks and regenerate 5% of your max energy for every stack consumed.*/, ModdedPlayer.Stats.spell_frenzyDmg, ModdedPlayer.Stats.spell_frenzyMaxStacks))//tr
			{
				passive = x => ModdedPlayer.Stats.spell_frenzy.value = x,
				active = () =>
				{
					float f = ModdedPlayer.Stats.spell_frenzyStacks.valueAdditive;
					LocalPlayer.Stats.Energy += ModdedPlayer.Stats.TotalMaxEnergy * f * 0.05f;
					ModdedPlayer.Stats.attackSpeed.valueMultiplicative /= 1 + f * ModdedPlayer.Stats.spell_frenzyAtkSpeed;
					ModdedPlayer.Stats.allDamage.valueMultiplicative /= 1 + f * ModdedPlayer.Stats.spell_frenzyDmg;
					if (ModdedPlayer.Stats.spell_frenzyMS)
						ModdedPlayer.Stats.movementSpeed.valueMultiplicative /= 1 + f * 0.05f;

					ModdedPlayer.Stats.spell_frenzyStacks.valueAdditive = 0;
				}
			};
			new Spell(19, 135, 27, 40, 10, Translations.SpellDataBase_36/*og:Seeking Arrow*/, () => Translations.SpellDataBase_35/*og:Casting this spell enchants your next ranged attack, causing all of your ranged attacks to fly towards the first enemy hit by the enchanted projectile. While active, damage is decreased by 10%, projectiles deal 50% less headshot damage and bodyshots slow enemies by 10% for 4 seconds.*/)//tr
			{
				active = SpellActions.SeekingArrow_Active,
			};
			new Spell(20, 137, 4, 50, 30, Translations.SpellDataBase_38/*og:Focus*/, () => Translations.SpellDataBase_37/*og:Passive: When landing a headshot, next projectile will deal 50% more damage and slow the enemy by 20%. When landing a body shot, next projectile will deal only 15% more damage, but attack speed is increased.\nActive: Gain 15% critical hit chance for 5 seconds*/)//tr
			{
				passive = x => ModdedPlayer.Stats.spell_focus.value = x,
				active = () => BuffDB.AddBuff(28, 102, 1.15f, 5f)
			};
			new Spell(21, 140, 8, 35, 60, Translations.SpellDataBase_40/*og:Parry*/, () => Translations.SpellDataBase_39/*og:Passive: When parrying an enemy, deal magic damage to enemies around the target. Additionally, gain energy, heal yourself for a small amount and get stun immunity for 10 seconds after parrying.\nActive: Gain 50% damage reduction for 10 seconds*/)//tr
			{
				passive = x => ModdedPlayer.Stats.spell_parry.value = x,
				active = () => BuffDB.AddBuff(26, 103, 0.5f, 10f)

			};
			new Spell(22, 141, 50, 500, 300, Translations.SpellDataBase_42/*og:Cataclysm*/, () => string.Format(Translations.SpellDataBase_41/*og:Creates a fire tornado that ignites enemies, slows them and deals damage. \nScaling{0} spell damage*/, ModdedPlayer.Stats.spell_cataclysmDamageScaling))//tr
			{
				active = () => SpellActions.CastCataclysm(),
				CastOnRelease = true,
				aim = SpellActions.CataclysmAim,
				aimEnd = SpellActions.CataclysmAimEnd
			};
			new Spell(23, 165, 11, 75, 8, Translations.SpellDataBase_44/*og:Blood Infused Arrow*/, () => Translations.SpellDataBase_43/*og:Sacrifice your own vitals to empower your next arrow. Drains your health and adds lost health as damage to the next projectile.*/)//tr
			{
				active = () => SpellActions.CastBloodInfArr(),
			};
			new Spell(24, 182, 55, 400, 360, Translations.SpellDataBase_46/*og:Roaring Cheeks*/, () => Translations.SpellDataBase_45/*og:Release a powerful gust of toxic gas that poisons and knocks enemies back, or cast it mid air to perform a secondary jump, the damage and area of effect will be lower but 2/3 of the cooldown will be refunded*/)//tr
			{
				active = () => SpellActions.RipAFatOne(),
			};
			new Spell(25, 189, 9, 40, 45, Translations.SpellDataBase_48/*og:Taunt*/, () => Translations.SpellDataBase_47/*og:Makes enemies hyper aggressive and forces them to attack you. Taunted enemies move twice attack twice as fast and take 50% increased damage*/)//tr
			{
				active = Taunt.OnSpellUsed,
				CastOnRelease = true,
				aim = Taunt.Aim,
				aimEnd = Taunt.AimEnd
			};
			new Spell(26, 201, 6, 45, 20, Translations.SpellDataBase_50/*og:Firebolt*/, () => string.Format(Translations.SpellDataBase_53/*og:Puts your weapon away and instead allows you to wield fireballs. Each attack consumes energy. Once you equip another weapon, you can no longer cast fireballs until you use this spell again\nAttack speed affects fire rate\nAttack damage: {0}*/, ModdedPlayer.Stats.spell_fireboltDamageScaling.GetFormattedAmount()))//tr
			{
				active = () => Spells.ActiveSpellManager.Instance.PrepareForFiring(Spells.FireboltSpell.Instance),
			};
			new Spell(27, 200, 9, 120, 20, Translations.SpellDataBase_52/*og:Snow Storm*/, () => string.Format(Translations.SpellDataBase_51/*og:Puts your weapon away and instead allows you to summon a blizzard around you. Hold left mouse button to channel. After 10 seconds spell reaches maximum power - damage and radius is at it's peak\nAttacks twice per second, frost seeps into enemy armor reducing it by a fraction of damage dealt*/))//tr
			{
				active = () => Spells.ActiveSpellManager.Instance.PrepareForFiring(Spells.BlizzardSpell.Instance),
			};
			//new Spell(24, 165, 1, 1, 2, "Corpse Explosion", "")
			//{
			//    active = () => SpellActions.CastCorpseExplosion(),
			//};
			//new Spell(25, 165, 1, 1, "Devour", "...")
			//{
			//    active = () =>
			//};
		}
	}
}