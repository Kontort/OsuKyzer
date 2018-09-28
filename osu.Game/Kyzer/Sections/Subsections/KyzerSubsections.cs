using System;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.MathUtils;
using osu.Game.Configuration;
using osu.Game.Kyzer.Main;

namespace osu.Game.Overlays.Settings.Sections.Kyzer
{
    #region Miscellaneous

    public class KyzerMiscellaneous : SettingsSubsection
    {
        #region DrawablesValues

        private DangerousSettingsButton kyzerValues;

        #endregion DrawablesValues

        protected override string Header => "Miscellaneous";

        public KyzerMiscellaneous(DialogOverlay dialogOverlay)
        {
            Children = new Drawable[]
            {
                kyzerValues = new DangerousSettingsButton
                {
                    Text = "Operate Kyzer Values",
                    Action = () =>
                    {
                        dialogOverlay?.Push(new KyzerPopup(kyzerValues.Text, "This will selectively perform an action. What would you like to do?", "Restore Defaults", "Randomize Values", true, () =>
                        {
                            KyzerMain.KyzerBindables.ForEach(b =>
                            {
                                valueTask<BindableBool>(b, _ => { _.Value = _.Default; });
                                valueTask<BindableDouble>(b, _ => { _.Value = _.Default; });
                            });
                        }, () =>
                        {
                            KyzerMain.KyzerBindables.ForEach(b =>
                            {
                                valueTask<BindableBool>(b, _ => { _.Value = RNG.NextBool(); });
                                valueTask<BindableDouble>(b, _ => { _.Value = RNG.NextDouble(_.MinValue, _.MaxValue); });
                            });
                        }));
                    },
                },
            };
        }

        private void valueTask<T>(IBindable bindable, Action<T> value) where T : IBindable
        {
            if (bindable is T t)
                value.Invoke(t);
        }
    }

    #endregion Miscellaneous

    #region Graphics

    public class KyzerGraphics : SettingsSubsection
    {
        #region BindableValues

        public static BindableBool Graphics = new BindableBool();
        public static BindableDouble TrailFade = new BindableDouble();

        public static BindableDouble TriangleScale = new BindableDouble();
        //public static BindableBool customBackground = new BindableBool();

        #endregion BindableValues

        #region DrawablesValues

        private SettingsCheckbox enableSection;
        private SettingsSlider<double> fadeTrail;

        private SettingsSlider<double> scaleTriangle;
        //private SettingsCheckbox backgroundsCustomize;

        #endregion DrawablesValues

        protected sealed override string Header => "Graphics";

        public KyzerGraphics(KyzerConfigManager config)
        {
            #region BindWithConfiguration

            config.BindWith(KyzerSetting.Graphics, Graphics);
            config.BindWith(KyzerSetting.TrailFade, TrailFade);
            config.BindWith(KyzerSetting.TriangleScale, TriangleScale);
            //config.BindWith(KyzerSetting.CustomBackground, customBackground);

            #endregion BindWithConfiguration

            Children = new Drawable[]
            {
                enableSection = new SettingsCheckbox
                {
                    LabelText = "Enable " + Header,
                    Bindable = Graphics,
                },

                fadeTrail = new SettingsSlider<double>
                {
                    LabelText = "Trail Fade",
                    Bindable = TrailFade,
                },

                scaleTriangle = new SettingsSlider<double>
                {
                    LabelText = "Triangle Scale",
                    Bindable = TriangleScale,
                },

                //backgroundsCustomize = new SettingsCheckbox
                //{
                //LabelText = "Custom Backgrounds",
                //Bindable = customBackground,
                //},
            };
        }
    }

    #endregion Graphics

    #region SpeedOverrides

    public class KyzerSpeedOverrides : SettingsSubsection
    {
        #region BindableValues

        public static BindableBool SpeedOverride = new BindableBool();

        public static BindableDouble SpeedDoubleTime = new BindableDouble();
        public static BindableDouble SpeedHalfTime = new BindableDouble();

        #endregion BindableValues

        #region DrawablesValues

        private SettingsCheckbox enableSection;
        private SettingsSlider<double> doubleTimeOverride;

        private SettingsSlider<double> halfTimeOverride;
        //SettingsSlider<double> GTOverride;

        #endregion DrawablesValues

        protected sealed override string Header => "Speed Overrides";

        public KyzerSpeedOverrides(KyzerConfigManager config)
        {
            #region BindWithConfiguration

            config.BindWith(KyzerSetting.SpeedOverride, SpeedOverride);
            config.BindWith(KyzerSetting.SpeedDoubleTime, SpeedDoubleTime);
            config.BindWith(KyzerSetting.SpeedHalfTime, SpeedHalfTime);

            #endregion BindWithConfiguration

            Children = new Drawable[]
            {
                enableSection = new SettingsCheckbox
                {
                    LabelText = "Enable " + Header,
                    Bindable = SpeedOverride,
                },

                doubleTimeOverride = new SettingsSlider<double>
                {
                    LabelText = "Override DT",
                    Bindable = SpeedDoubleTime
                },

                halfTimeOverride = new SettingsSlider<double>
                {
                    LabelText = "Override HT",
                    Bindable = SpeedHalfTime,
                },
            };
        }
    }

    #endregion SpeedOverrides

    #region MapOverrides

    public class KyzerMapOverrides : SettingsSubsection
    {
        #region BindableValues

        public static BindableBool MapOverride = new BindableBool();

        public static BindableDouble MapCircleSize = new BindableDouble();
        public static BindableDouble MapApproachRate = new BindableDouble();
        public static BindableDouble MapHealthPoints = new BindableDouble();
        public static BindableDouble MapOverallDifficulty = new BindableDouble();

        #endregion BindableValues

        #region DrawablesValues

        private SettingsCheckbox enableSection;
        private SettingsSlider<double> circleSizeOverride;
        private SettingsSlider<double> approachRateOverride;
        private SettingsSlider<double> healthPointsOverride;
        private SettingsSlider<double> overallDifficultyOverride;

        #endregion DrawablesValues

        protected sealed override string Header => "Map Overrides";

        public KyzerMapOverrides(KyzerConfigManager config)
        {
            #region BindWithConfiguration

            config.BindWith(KyzerSetting.MapOverride, MapOverride);
            config.BindWith(KyzerSetting.MapCircleSize, MapCircleSize);
            config.BindWith(KyzerSetting.MapApproachRate, MapApproachRate);
            config.BindWith(KyzerSetting.MapHealthPoints, MapHealthPoints);
            config.BindWith(KyzerSetting.MapOverallDifficulty, MapOverallDifficulty);

            #endregion BindWithConfiguration

            Children = new Drawable[]
            {
                enableSection = new SettingsCheckbox
                {
                    LabelText = "Enable " + Header,
                    Bindable = MapOverride,
                },

                circleSizeOverride = new SettingsSlider<double>
                {
                    LabelText = "Override CS",
                    Bindable = MapCircleSize,
                    KeyboardStep = 0.01f,
                },

                approachRateOverride = new SettingsSlider<double>
                {
                    LabelText = "Override AR",
                    Bindable = MapApproachRate,
                    KeyboardStep = 0.01f,
                },

                healthPointsOverride = new SettingsSlider<double>
                {
                    LabelText = "Override HP",
                    Bindable = MapHealthPoints,
                    KeyboardStep = 0.01f,
                },

                overallDifficultyOverride = new SettingsSlider<double>
                {
                    LabelText = "Override OD",
                    Bindable = MapOverallDifficulty,
                    KeyboardStep = 0.01f,
                },
            };
        }
    }

    #endregion MapOverrides
}
