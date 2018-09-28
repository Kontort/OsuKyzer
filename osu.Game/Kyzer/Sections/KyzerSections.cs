using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Kyzer.Main;
using osu.Game.Overlays.Settings.Sections.Kyzer;

namespace osu.Game.Overlays.Settings.Sections
{
    internal class KyzerSection : SettingsSection
    {
        public override string Header => "Kyzer";
        public override FontAwesome Icon => FontAwesome.fa_code_fork;

        [BackgroundDependencyLoader]
        private void load(DialogOverlay dialog, KyzerConfigManager config)
        {
            Children = new Drawable[]
            {
                new KyzerMiscellaneous(dialog),
                new KyzerGraphics(config),
                new KyzerSpeedOverrides(config),
                new KyzerMapOverrides(config),
            };

            KyzerMain.Initialize(this);
        }
    }
}
