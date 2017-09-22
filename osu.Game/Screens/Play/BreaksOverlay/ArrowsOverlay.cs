﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using OpenTK;
using osu.Game.Graphics.Containers;

namespace osu.Game.Screens.Play.BreaksOverlay
{
    public class ArrowsOverlay : Container
    {
        private const int glow_icon_size = 65;
        private const int glow_icon_blur_sigma = 8;
        private const float glow_icon_final_offset = 0.2f;
        private const float glow_icon_offscreen_offset = 0.6f;

        private const int blurred_icon_blur_sigma = 20;
        private const int blurred_icon_size = 130;
        private const float blurred_icon_final_offset = 0.35f;
        private const float blurred_icon_offscreen_offset = 0.7f;

        private readonly GlowIcon leftGlowIcon;
        private readonly GlowIcon rightGlowIcon;

        private readonly BlurredIcon leftBlurredIcon;
        private readonly BlurredIcon rightBlurredIcon;

        public ArrowsOverlay()
        {
            RelativeSizeAxes = Axes.Both;
            Children = new Drawable[]
            {
                leftGlowIcon = new GlowIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.CentreRight,
                    X = - glow_icon_offscreen_offset,
                    Icon = Graphics.FontAwesome.fa_chevron_right,
                    BlurSigma = new Vector2(glow_icon_blur_sigma),
                    Size = new Vector2(glow_icon_size),
                },
                rightGlowIcon = new GlowIcon
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.CentreLeft,
                    X = glow_icon_offscreen_offset,
                    Icon = Graphics.FontAwesome.fa_chevron_left,
                    BlurSigma = new Vector2(glow_icon_blur_sigma),
                    Size = new Vector2(glow_icon_size),
                },
                new ParallaxContainer
                {
                    ParallaxAmount = -0.02f,
                    Children = new Drawable[]
                    {
                        leftBlurredIcon = new BlurredIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.CentreRight,
                            X = - blurred_icon_offscreen_offset,
                            Icon = Graphics.FontAwesome.fa_chevron_right,
                            BlurSigma = new Vector2(blurred_icon_blur_sigma),
                            Size = new Vector2(blurred_icon_size),
                        },
                        rightBlurredIcon = new BlurredIcon
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.CentreLeft,
                            X = blurred_icon_offscreen_offset,
                            Icon = Graphics.FontAwesome.fa_chevron_left,
                            BlurSigma = new Vector2(blurred_icon_blur_sigma),
                            Size = new Vector2(blurred_icon_size),
                        },
                    }
                }
            };
        }

        public void Show(double fadeDuration)
        {
            leftGlowIcon.MoveToX(-glow_icon_final_offset, fadeDuration, Easing.OutQuint);
            rightGlowIcon.MoveToX(glow_icon_final_offset, fadeDuration, Easing.OutQuint);

            leftBlurredIcon.MoveToX(-blurred_icon_final_offset, fadeDuration, Easing.OutQuint);
            rightBlurredIcon.MoveToX(blurred_icon_final_offset, fadeDuration, Easing.OutQuint);
        }

        public void Hide(double fadeDuration)
        {
            leftGlowIcon.MoveToX(-glow_icon_offscreen_offset, fadeDuration, Easing.OutQuint);
            rightGlowIcon.MoveToX(glow_icon_offscreen_offset, fadeDuration, Easing.OutQuint);

            leftBlurredIcon.MoveToX(-blurred_icon_offscreen_offset, fadeDuration, Easing.OutQuint);
            rightBlurredIcon.MoveToX(blurred_icon_offscreen_offset, fadeDuration, Easing.OutQuint);
        }
    }
}
