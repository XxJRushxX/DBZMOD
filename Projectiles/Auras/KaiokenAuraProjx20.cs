﻿﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Projectiles.Auras;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Util;

namespace DBZMOD.Projectiles.Auras
{
    public class KaiokenAuraProjx20 : AuraProjectile
    {
        public float KaioAuraTimer;
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 113;
            projectile.height = 115;
            projectile.aiStyle = 0;
            projectile.timeLeft = 10;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.damage = 0;
            KaioAuraTimer = 240;
            IsKaioAura = true;
            OriginalScale = 1.6f;
            OriginalAuraOffset.Y = -20;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            if (Transformations.IsAnythingOtherThanKaioken(player))
            {
                projectile.scale = OriginalScale * 1.5f;
            }
            else
            {
                projectile.scale = OriginalScale;
            }

            // correct scaling
            if (ScaledAuraOffset != OriginalAuraOffset)
                ScaledAuraOffset = OriginalAuraOffset * projectile.scale;
            projectile.netUpdate = true;

            if (!player.HasBuff(Transformations.Kaioken20.GetBuffId()))
            {
                projectile.Kill();
            }
            if (KaioAuraTimer > 0)
            {
                //projectile.scale = 4f + 4f * (KaioAuraTimer / 240f);
                KaioAuraTimer--;
            }
            base.AI();
        }
    }
}