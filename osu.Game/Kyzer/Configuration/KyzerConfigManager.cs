using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace osu.Game.Configuration
{
    public class KyzerConfigManager : IniConfigManager<KyzerSetting>
    {
        protected override string Filename => "kyzer.ini";

        protected override void InitialiseDefaults()
        {
            Set(KyzerSetting.Graphics, false);
            Set(KyzerSetting.TrailFade, 300d, -1d, 1000d, 1d);
            //Set(KyzerSetting.TriangleScale, 1, 0.1, 5, 0.1);
            Set(KyzerSetting.TriangleSpeed, 1, 0, 10, 0.1);
            //Set(KyzerSetting.CustomBackground, false);

            Set(KyzerSetting.SpeedOverride, false);
            Set(KyzerSetting.SpeedDoubleTime, 1.5, 0, 2, 0.01);
            Set(KyzerSetting.SpeedHalfTime, 0.75, 0, 2, 0.01);

            Set(KyzerSetting.MapOverride, false);
            Set(KyzerSetting.MapCircleSize, -1, -1, 10, 0.1);
            Set(KyzerSetting.MapApproachRate, -1, -1, 10, 0.1);
            Set(KyzerSetting.MapHealthPoints, -1, -1, 10, 0.1);
            Set(KyzerSetting.MapOverallDifficulty, -1, -1, 10, 0.1);
        }

        public KyzerConfigManager(Storage storage) : base(storage)
        {
        }
    }

    public enum KyzerSetting
    {
        Graphics,
        TrailFade,
        TriangleScale,
        TriangleSpeed,
        //CustomBackground,

        SpeedOverride,
        SpeedDoubleTime,
        SpeedHalfTime,

        MapOverride,
        MapCircleSize,
        MapApproachRate,
        MapHealthPoints,
        MapOverallDifficulty,
    }
}
