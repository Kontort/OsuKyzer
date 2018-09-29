using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using osu.Framework.Configuration;
using osu.Game.Graphics;
using osu.Game.Overlays.Dialog;
using osu.Game.Overlays.Settings;
using osu.Game.Overlays.Settings.Sections.Kyzer;

namespace osu.Game.Kyzer.Main
{
    public static class KyzerMain
    {
        public static List<IBindable> KyzerBindables = new List<IBindable>();

        public static void Initialize(SettingsSection section)
        {
            foreach (var drawable in section.Children)
            {
                KyzerFuntions.ForEachFieldWithin((SettingsSubsection)drawable,
                    BindingFlags.Public | BindingFlags.Static,
                    field => KyzerBindables.Add((IBindable)field.GetValue(null)));
            }
        }
    }

    public static class KyzerFuntions
    {
        #region KyzerFunctions

        internal static void ForEachFieldWithin<C>(C theClass, BindingFlags flags, Action<FieldInfo> task) where C : class
        {
            theClass.GetType().GetFields(flags)
                .ToList().ForEach(task);
        }

        #endregion KyzerFunctions
    }

    public static class KyzerBooleans
    {
        #region KyzerBooleans

        private static bool canOverrideValue(BindableDouble value, bool mapOverride = true)
        {
            if (mapOverride)
                return canMapOverride && value >= 0;
            return canSpeedOverride && value > 0;
        }

        #region GraphicBooleans

        private static bool canGraphicOverride => KyzerGraphics.Graphics;

        public static bool CanOverrideTrail => canGraphicOverride && KyzerGraphics.TrailFade > -1;

        //public static bool CanOverrideScale => canGraphicOverride;

        public static bool CanOverrideSpeed => canGraphicOverride;
        //public static bool canOverrideBackground => canGraphicOverride && KyzerGraphics.customBackground;

        #endregion GraphicBooleans

        #region MapOverrideBooleans

        private static bool canMapOverride => KyzerMapOverrides.MapOverride;

        public static bool CanOverrideCircleSize => canOverrideValue(KyzerMapOverrides.MapCircleSize);
        public static bool CanOverrideApproachRate => canOverrideValue(KyzerMapOverrides.MapApproachRate);
        public static bool CanOverrideHealthPoints => canOverrideValue(KyzerMapOverrides.MapHealthPoints);
        public static bool CanOverrideOverallDifficulty => canOverrideValue(KyzerMapOverrides.MapOverallDifficulty);

        #endregion MapOverrideBooleans

        #region SpeedOverrideBooleans

        private static bool canSpeedOverride => KyzerSpeedOverrides.SpeedOverride;

        public static bool CanOverrideDoubletime => canOverrideValue(KyzerSpeedOverrides.SpeedDoubleTime, false);
        public static bool CanOverrideHalftime => canOverrideValue(KyzerSpeedOverrides.SpeedHalfTime, false);
        public static bool CanOverrideNightcore => CanOverrideDoubletime;
        public static bool CanOverrideDaycore => CanOverrideHalftime;

        #endregion SpeedOverrideBooleans

        #endregion KyzerBooleans
    }

    public class KyzerPopup : PopupDialog
    {
        #region KyzerPopup

        public KyzerPopup(string header, string body, Action onKyzerPopupConfirmation, Action onKyzerPopupDeny = null, bool dualOptions = false)
            : this(header + "?", body + " " + "Are you sure you wish to proceed?", "Yes!", "No..", dualOptions, onKyzerPopupConfirmation, onKyzerPopupDeny)
        {
        }

        public KyzerPopup(string header, string body, string confirm, string deny, bool dual, Action onConfirm, Action onDeny)
        {
            HeaderText = header;
            BodyText = body;

            Icon = FontAwesome.fa_question;

            Buttons = new PopupDialogButton[]
            {
                new PopupDialogOkButton
                {
                    Text = confirm,
                    Action = onConfirm,
                },
                new PopupDialogCancelButton
                {
                    Text = deny,
                    Action = dual ? onDeny : null,
                },
            };
        }

        #endregion KyzerPopup
    }
}
