﻿using System;
using DBZMOD.Players;
using DBZMOD.Util;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBZMOD.Items.Consumables.TestItems
{
    public class SSJ3MasteryTestItem : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 38;
            item.consumable = false;
            item.maxStack = 1;
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.value = 0;
            item.expert = true;
            item.potion = false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("SSJ3 Mastery Test Item");
            Tooltip.SetDefault("Manually Upgrades your ssj3 mastery. Each use increases it by 0.25");
        }


        public override bool UseItem(Player player)
        {
            var modPlayer = player.GetModPlayer<MyPlayer>();
            modPlayer.masteryLevels[DBZMOD.Instance.TransformationDefinitionManager.SSJ3Definition.MasteryBuffKeyName] = Math.Min(1.0f, modPlayer.masteryLevels[DBZMOD.Instance.TransformationDefinitionManager.SSJ3Definition.MasteryBuffKeyName] + 0.25f);
            return true;

        }
    }
}
