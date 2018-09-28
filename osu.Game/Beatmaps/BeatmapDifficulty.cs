﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using osu.Game.Kyzer.Main;
using osu.Game.Overlays.Settings.Sections.Kyzer;

namespace osu.Game.Beatmaps
{
    public class BeatmapDifficulty
    {
        /// <summary>
        /// The default value used for all difficulty settings except <see cref="SliderMultiplier"/> and <see cref="SliderTickRate"/>.
        /// </summary>
        public const float DEFAULT_DIFFICULTY = 5;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int ID { get; set; }

        private float drainRate;
        private float circleSize;
        private float overallDifficulty;
        private float? approachRate;

        public float DrainRate
        {
            get => (KyzerBooleans.CanOverrideHealthPoints ? (float)KyzerMapOverrides.MapHealthPoints : drainRate);
            set => drainRate = value;
        }
        public float CircleSize
        {
            get => (KyzerBooleans.CanOverrideCircleSize ? (float)KyzerMapOverrides.MapCircleSize : circleSize);
            set => circleSize = value;
        }
        public float OverallDifficulty
        {
            get => (KyzerBooleans.CanOverrideOverallDifficulty ? (float)KyzerMapOverrides.MapOverallDifficulty : overallDifficulty);
            set => overallDifficulty = value;
        }
        public float ApproachRate
        {
            get => (KyzerBooleans.CanOverrideApproachRate ? (float)KyzerMapOverrides.MapApproachRate : (approachRate ?? OverallDifficulty));
            set => approachRate = value;
        }

        public double SliderMultiplier { get; set; } = 1;
        public double SliderTickRate { get; set; } = 1;

        /// <summary>
        /// Returns a shallow-clone of this <see cref="BeatmapDifficulty"/>.
        /// </summary>
        public BeatmapDifficulty Clone() => (BeatmapDifficulty)MemberwiseClone();

        /// <summary>
        /// Maps a difficulty value [0, 10] to a two-piece linear range of values.
        /// </summary>
        /// <param name="difficulty">The difficulty value to be mapped.</param>
        /// <param name="min">Minimum of the resulting range which will be achieved by a difficulty value of 0.</param>
        /// <param name="mid">Midpoint of the resulting range which will be achieved by a difficulty value of 5.</param>
        /// <param name="max">Maximum of the resulting range which will be achieved by a difficulty value of 10.</param>
        /// <returns>Value to which the difficulty value maps in the specified range.</returns>
        public static double DifficultyRange(double difficulty, double min, double mid, double max)
        {
            if (difficulty > 5)
                return mid + (max - mid) * (difficulty - 5) / 5;
            if (difficulty < 5)
                return mid - (mid - min) * (5 - difficulty) / 5;
            return mid;
        }

        /// <summary>
        /// Maps a difficulty value [0, 10] to a two-piece linear range of values.
        /// </summary>
        /// <param name="difficulty">The difficulty value to be mapped.</param>
        /// <param name="range">The values that define the two linear ranges.</param>
        /// <param name="range.od0">Minimum of the resulting range which will be achieved by a difficulty value of 0.</param>
        /// <param name="range.od5">Midpoint of the resulting range which will be achieved by a difficulty value of 5.</param>
        /// <param name="range.od10">Maximum of the resulting range which will be achieved by a difficulty value of 10.</param>
        /// <returns>Value to which the difficulty value maps in the specified range.</returns>
        public static double DifficultyRange(double difficulty, (double od0, double od5, double od10) range)
            => DifficultyRange(difficulty, range.od0, range.od5, range.od10);
    }
}
