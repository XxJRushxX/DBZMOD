﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using DBZMOD.Enums;
using System;
using System.Collections.Generic;
using DBZMOD.Transformations;
using DBZMOD.Utilities;

namespace DBZMOD.UI
{
    internal class TransformationMenu : EasyMenu
    {
        public static bool menuVisible = false;

        public UIImage backPanelImage;
        private UIText _titleText;

        /*
            private UIImageButton _ssjButtonTexture;
            private UIImageButton _ssj2ButtonTexture;
            private UIImageButton _ssj3ButtonTexture;
            private UIImageButton _lssjButtonTexture;
            private UIImageButton _lssj2ButtonTexture;
            private UIImageButton _ssjgButtonTexture;
            private UIImageButton _ssjSButtonTexture;
            private UIImage _lockedImage1;
            private UIImage _lockedImage2;
            private UIImage _lockedImage3;
            private UIImage _lockedImageG;
            private UIImage _lockedImageL1;
            private UIImage _lockedImageL2;
            private UIImage _unknownImage2;
            private UIImage _unknownImage3;
            private UIImage _unknownImageG;
            private UIImage _unknownImageL1;
            private UIImage _unknownImageL2;

            public static bool ssj1On;
            public static bool ssj2On;
            public static bool ssj3On;
            public static bool lssjOn;
        */

        private readonly Dictionary<TransformationDefinition, UIImagePair> _imagePairs = new Dictionary<TransformationDefinition, UIImagePair>();

        public static TransformationDefinition SelectedTransformation { get; set; }

        public const float PADDINGX = 30f;
        public const float PADDINGY = PADDINGX;

        public override void OnInitialize()
        {
            base.OnInitialize();

            // TODO : Fix panel not being drageable all over its surface.

            backPanel = new UIPanel();
            backPanel.Width.Set(Gfx.backPanel.Width, 0f);
            backPanel.Height.Set(Gfx.backPanel.Height, 0f);
            backPanel.Left.Set(Main.screenWidth / 2f - backPanel.Width.Pixels / 2f, 0f);
            backPanel.Top.Set(Main.screenHeight / 2f - backPanel.Height.Pixels / 2f, 0f);
            backPanel.BackgroundColor = new Color(0, 0, 0, 0);
            backPanel.OnMouseDown += new MouseEvent(DragStart);
            backPanel.OnMouseUp += new MouseEvent(DragEnd);
            Append(backPanel);

            backPanelImage = new UIImage(Gfx.backPanel);
            backPanelImage.Width.Set(Gfx.backPanel.Width, 0f);
            backPanelImage.Height.Set(Gfx.backPanel.Height, 0f);
            backPanelImage.Left.Set(-12, 0f);
            backPanelImage.Top.Set(-12, 0f);
            backPanel.Append(backPanelImage);
            float row1OffsetX = 0.0f;

            // 125 is the width of the text ?
            InitText(ref _titleText, "Transformation Tree", 1, Gfx.backPanel.Bounds.X, -32, Color.White);

            TransformationDefinitionManager tDefMan = DBZMOD.Instance.TransformationDefinitionManager;

            row1OffsetX = PADDINGX;

            int j = 0;
            for (int i = 0; i < tDefMan.Count; i++)
            {
                TransformationDefinition def = tDefMan[i];
                if (def.BuffIcon == null) continue;

                UIImageButton transformationButton = null;
                UIImage lockedImage = null, unknownImage = null;

                InitButton(ref transformationButton, def.BuffIcon, new MouseEvent((evt, element) => TrySelectingTransformation(def, evt, element)), 
                    row1OffsetX, PADDINGY, backPanelImage);
                
                InitImage(ref lockedImage, Gfx.lockedImage, 0, 0, transformationButton);
                lockedImage.ImageScale = 0f;

                InitImage(ref unknownImage, Gfx.unknownImage, 0, 0, transformationButton);
                unknownImage.ImageScale = 0f;

                _imagePairs.Add(def, new UIImagePair(transformationButton, lockedImage, unknownImage));
                row1OffsetX += def.BuffIcon.Width + 15;
            }

            /*InitButton(ref _ssjButtonTexture, Gfx.ssj1ButtonImage, new MouseEvent(TrySelectingSSJ1),
                row1OffsetX - 2,
                PADDINGY - 20,
                backPanelImage);

            InitImage(ref _lockedImage1, Gfx.lockedImage,
                0,
                0,
                _ssjButtonTexture);

            row1OffsetX = PADDINGX + Gfx.ssj1ButtonImage.Width;
            InitButton(ref _ssj2ButtonTexture, Gfx.ssj2ButtonImage, new MouseEvent(TrySelectingSSJ2),
                row1OffsetX + 14,
                PADDINGY - 20,
                backPanelImage);

            InitImage(ref _lockedImage2, Gfx.lockedImage,
                0,
                0,
                _ssj2ButtonTexture);

            InitImage(ref _unknownImage2, Gfx.unknownImage,
                0,
                0,
                _ssj2ButtonTexture);

            row1OffsetX = PADDINGX + Gfx.ssj2ButtonImage.Width * 2;
            InitButton(ref _ssj3ButtonTexture, Gfx.ssj3ButtonImage, new MouseEvent(TrySelectingSSJ3),
                row1OffsetX + 22,
                PADDINGY - 20,
                backPanelImage);

            InitImage(ref _lockedImage3, Gfx.lockedImage,
                0,
                0,
                _ssj3ButtonTexture);

            InitImage(ref _unknownImage3, Gfx.unknownImage,
                0,
                0,
                _ssj3ButtonTexture);

            InitButton(ref _lssjButtonTexture, Gfx.lssjButtonImage, new MouseEvent(TrySelectingLSSJ),
                PADDINGX + 14 + Gfx.ssj1ButtonImage.Width,
                Gfx.ssj1ButtonImage.Height + PADDINGY - 10,
                backPanelImage);

            InitImage(ref _lockedImageL1, Gfx.lockedImage,
                0,
                0,
                _lssjButtonTexture);

            InitImage(ref _unknownImageL1, Gfx.unknownImage,
                0,
                0,
                _lssjButtonTexture);

            row1OffsetX = PADDINGX + Gfx.ssj3ButtonImage.Width * 3;
            InitButton(ref _ssjgButtonTexture, Gfx.ssjgButtonImage, new MouseEvent(TrySelectingSSJG),
                row1OffsetX + 30,
                PADDINGY - 20,
                backPanelImage);

            InitImage(ref _lockedImageG, Gfx.lockedImage,
                0,
                0,
                _ssjgButtonTexture);

            InitImage(ref _unknownImageG, Gfx.unknownImage,
                0,
                0,
                _ssjgButtonTexture);

            InitButton(ref _lssj2ButtonTexture, Gfx.lssj2ButtonImage, new MouseEvent(TrySelectingLSSJ2),
                PADDINGX + 22 + Gfx.ssj1ButtonImage.Width * 2,
                Gfx.ssj1ButtonImage.Height + PADDINGY - 10,
                backPanelImage);

            InitImage(ref _lockedImageL2, Gfx.lockedImage,
                0,
                0,
                _lssj2ButtonTexture);

            InitImage(ref _unknownImageL2, Gfx.unknownImage,
                0,
                0,
                _lssj2ButtonTexture);

            InitButton(ref _ssjSButtonTexture, Gfx.ssjsButtonImage, new MouseEvent(TrySelectingSSJS),
                PADDINGX + 14 + Gfx.ssj1ButtonImage.Width,
                PADDINGY + 55,
                backPanelImage);*/
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            // TODO Make this use Dynamicity

            TransformationDefinitionManager tDefMan = DBZMOD.Instance.TransformationDefinitionManager;
            for (int i = 0; i < tDefMan.Count; i++)
            {
                TransformationDefinition def = tDefMan[i];

                if (def.BuffIcon == null) continue;
                _imagePairs[def].lockedImage.ImageScale = player.PlayerTransformations.ContainsKey(def) ? 0f : 1f;
            }

            //_lockedImage1.ImageScale = !modplayer.IsSSJ1Achieved ? 1.0f : 0.0f;

            /*if (player.name == "Nuova")
            {
                _ssjSButtonTexture.SetVisibility(1.0f, 0.5f);
            }
            else
            {
                _ssjSButtonTexture.SetVisibility(0.0f, 0.0f);
            }

            if (modplayer.IsPlayerLegendary())
            {
                _lockedImageL1.ImageScale = !modplayer.LSSJAchieved ? 1.0f : 0.0f;

                _lockedImageL2.ImageScale = !modplayer.LSSJ2Achieved ? 1.0f : 0.0f;

                _lockedImage2.ImageScale = 1.0f;

                _unknownImage2.ImageScale = 0.0f;

                _lockedImage3.ImageScale = 0.0f;

                _unknownImage3.ImageScale = 1.0f;

                _lockedImageG.ImageScale = 0.0f;

                _unknownImageG.ImageScale = 1.0f;

                _unknownImageL1.ImageScale = 0.0f;

                _unknownImageL2.ImageScale = !modplayer.LSSJAchieved ? 1.0f : 0.0f;
            }
            else
            {
                _unknownImageL1.ImageScale = 0.0f;
                _unknownImageL2.ImageScale = 1.0f;
                _unknownImage2.ImageScale = 0.0f;
                _unknownImage3.ImageScale = 0.0f;
                _unknownImageG.ImageScale = 0.0f;
                _lockedImage2.ImageScale = !modplayer.SSJ2Achieved ? 1.0f : 0.0f;

                _lockedImage3.ImageScale = !modplayer.SSJ3Achieved ? 1.0f : 0.0f;

                _lockedImageG.ImageScale = !modplayer.SSJGAchieved ? 1.0f : 0.0f;

                _lockedImageL1.ImageScale = 1.0f;
                _lockedImageL2.ImageScale = 0.0f;

            }*/
        }

        private void TrySelectingTransformation(TransformationDefinition def, UIMouseEvent evt, UIElement listeningElement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            if (player.PlayerTransformations.ContainsKey(def))
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);

                if (SelectedTransformation != def)
                {
                    SelectedTransformation = def;
                    Main.NewText($"Selected {def.TransformationText}, Mastery: {Math.Round(100f * def.GetPlayerMastery(player), 2)}%");
                }
                else
                    Main.NewText($"{def.TransformationText} Mastery: {Math.Round(100f * def.GetPlayerMastery(player), 2)}%");
            }
            else if (def.SelectionRequirementsFailed.Invoke(player, def))
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);

                if (def.TransformationFailureText == null) return;
                Main.NewText(def.TransformationFailureText);
            }
        }

        /*private void TrySelectingSSJ1(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.IsSSJ1Achieved)
            {
                menuSelection = MenuSelectionID.SSJ1;
                TransformationDefinition buff = FormBuffHelper.GetBuffFromMenuSelection(menuSelection);
                string displayName = buff.TransformationText;
                string keyName = buff.MasteryBuffKeyName;
                float masteryLevel = player.masteryLevels.ContainsKey(keyName) ? player.masteryLevels[keyName] : 0f;
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);
                Main.NewText($"{displayName} Mastery: {Math.Round(100f * masteryLevel, 2)}%");
            }
            else
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);
                Main.NewText("Only through failure with a powerful foe will true power awaken.");
            }
        }

        private void TrySelectingSSJ2(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.SSJ2Achieved && !player.IsPlayerLegendary())
            {
                menuSelection = MenuSelectionID.SSJ2;
                TransformationDefinition buff = FormBuffHelper.GetBuffFromMenuSelection(menuSelection);
                string displayName = buff.TransformationText;
                string keyName = buff.MasteryBuffKeyName;
                float masteryLevel = player.masteryLevels.ContainsKey(keyName) ? player.masteryLevels[keyName] : 0f;
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);
                Main.NewText($"{displayName} Mastery: {Math.Round(100f * masteryLevel, 2)}%");
            }
            else if (!player.LSSJAchieved)
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);
                Main.NewText("One may awaken their true power through extreme pressure while ascended.");
            }
        }
        private void TrySelectingSSJ3(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.SSJ3Achieved && !player.IsPlayerLegendary())
            {
                menuSelection = MenuSelectionID.SSJ3;
                TransformationDefinition buff = FormBuffHelper.GetBuffFromMenuSelection(menuSelection);
                string displayName = buff.TransformationText;
                string keyName = buff.MasteryBuffKeyName;
                float masteryLevel = player.masteryLevels.ContainsKey(keyName) ? player.masteryLevels[keyName] : 0f;
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);
                Main.NewText($"{displayName} Mastery: {Math.Round(100f * masteryLevel, 2)}%");
            }
            else if (!player.LSSJAchieved)
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);
                Main.NewText("The power of an ancient foe may be the key to unlocking greater power.");
            }
        }
        private void TrySelectingLSSJ(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.LSSJAchieved)
            {
                menuSelection = MenuSelectionID.LSSJ1;
                TransformationDefinition buff = FormBuffHelper.GetBuffFromMenuSelection(menuSelection);
                string displayName = buff.TransformationText;
                string keyName = buff.MasteryBuffKeyName;
                float masteryLevel = player.masteryLevels.ContainsKey(keyName) ? player.masteryLevels[keyName] : 0f;
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);
                Main.NewText($"{displayName} Mastery: {Math.Round(100f * masteryLevel, 2)}%");
            }
            else if (!player.SSJ2Achieved)
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);
                Main.NewText("The rarest saiyans may be able to achieve a form beyond anything a normal saiyan could obtain.");
            }
        }

        private void TrySelectingLSSJ2(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.LSSJ2Achieved)
            {
                menuSelection = MenuSelectionID.LSSJ2;
                TransformationDefinition buff = FormBuffHelper.GetBuffFromMenuSelection(menuSelection);
                string displayName = buff.TransformationText;
                string keyName = buff.MasteryBuffKeyName;
                float masteryLevel = player.masteryLevels.ContainsKey(keyName) ? player.masteryLevels[keyName] : 0f;
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);
                Main.NewText($"{displayName} Mastery: {Math.Round(100f * masteryLevel, 2)}%");
            }
            else if (!player.LSSJ2Achieved)
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);
                Main.NewText("A legendary saiyan sometimes may lose complete control upon being pushed into a critical state.");
            }
        }
        private void TrySelectingSSJG(UIMouseEvent evt, UIElement listeningelement)
        {
            MyPlayer player = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            if (player.SSJGAchieved && !player.IsPlayerLegendary())
            {
                menuSelection = MenuSelectionID.SSJG;
                TransformationDefinition buff = FormBuffHelper.GetBuffFromMenuSelection(menuSelection);
                string displayName = buff.TransformationText;
                string keyName = buff.MasteryBuffKeyName;
                float masteryLevel = player.masteryLevels.ContainsKey(keyName) ? player.masteryLevels[keyName] : 0f;
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);
                Main.NewText($"{displayName} Mastery: {Math.Round(100f * masteryLevel, 2)}%");
            }
            else if (!player.LSSJAchieved)
            {
                SoundHelper.PlayVanillaSound(SoundID.MenuClose);
                Main.NewText("The godlike power of the lunar star could awaken something beyond mortal comprehension.");
            }
        }
        private void TrySelectingSSJS(UIMouseEvent evt, UIElement listeningelement)
        {
            Player player = Main.LocalPlayer;
            if (player.name == "Nuova")
            {
                menuSelection = MenuSelectionID.Spectrum;
                SoundHelper.PlayVanillaSound(SoundID.MenuTick);
            }
        }*/
    }

    struct UIImagePair
    {
        public UIImageButton button;
        public UIImage lockedImage, unknownImage;

        public UIImagePair(UIImageButton button, UIImage lockedImage, UIImage unknownImage)
        {
            this.button = button;
            this.lockedImage = lockedImage;
            this.unknownImage = unknownImage;
        }
    }
}