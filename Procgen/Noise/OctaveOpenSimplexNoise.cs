using MarkusSecundus.Utils.Randomness;
using System.Numerics;

namespace MarkusSecundus.Utils.Procgen.Noise
{
    public class OctaveOpenSimplexNoise
    {
        [System.Serializable]public struct Config
        {
            public int Octaves;
            public double Scale;
            public double Amplitude;
            public double Frequency;

            public static Config Default => new Config { Octaves = 3, Scale = 1, Amplitude = 2.0, Frequency = 0.5 };
        }

        Octave[] _octaves;
        double _scale;
        double _amplitude;
        double _frequency;
        public OctaveOpenSimplexNoise(System.Random rand, Config cfg):this(rand, cfg.Octaves, cfg.Scale, cfg.Amplitude, cfg.Frequency) { }
        public OctaveOpenSimplexNoise(System.Random rand, int octaves, double scale, double amplitude, double frequency)
        {
            (_scale, _amplitude, _frequency) = (scale, amplitude, frequency);
            _octaves = new Octave[octaves];
            for(int t = 0; t < octaves; ++t)
            {
                _octaves[t].Seed = rand.NextLong();
            }
        }

        public float Sample1F(double x, bool normalized=true) => (float)Sample1D(x,normalized);
        public double Sample1D(double x, bool normalized=true) => Sample2D(x, 0.0, normalized);
        public float Sample2F(double x, double y, bool normalized = true) => (float)Sample2D(x, y, normalized);
        public float Sample2F(UnityEngine.Vector2 v, bool normalized = true) => Sample2F(v.x, v.y, normalized);
        public double Sample2D(UnityEngine.Vector2 v, bool normalized = true) => Sample2D(v.x, v.y, normalized);
        public double Sample2D(double x, double y, bool normalized=true)
        {
            double result = 0.0;
            double amplitude = 1.0;
            double frequency = 1.0;
            double max = 0.0;

            x *= _scale;
            y *= _scale;

            for(int t=0;t< _octaves.Length; ++t)
            {
                var octave = _octaves[t];
                result += OpenSimplexNoise.noise2(octave.Seed, x*frequency, y*frequency) * amplitude;
                max += amplitude;
                frequency *= _frequency;
                amplitude *= _amplitude;
            }
            if (normalized)
                result /= max;

            return result;
        }
        public float Sample3F(double x, double y, double z, bool normalized = true) => (float)Sample3D(x, y, z, normalized);
        public double Sample3D(double x, double y, double z, bool normalized = true)
        {
            double result = 0.0;
            double amplitude = 1.0;
            double frequency = 1.0;
            double max = 0.0;

            x *= _scale;
            y *= _scale;
            z *= _scale;

            for(int t=0;t< _octaves.Length; ++t)
            {
                var octave = _octaves[t];
                result += OpenSimplexNoise.noise3_Fallback(octave.Seed, x * frequency, y * frequency, z * frequency) * amplitude;
                max += amplitude;
                frequency *= _frequency;
                amplitude *= _amplitude;
            }
            if (normalized)
                result /= max;

            return result;
        }
        public float Sample4F(double x, double y, double z, double w, bool normalized = true) => (float)Sample4D(x, y, z, w, normalized);
        public double Sample4D(double x, double y, double z, double w, bool normalized = true)
        {
            double result = 0.0;
            double amplitude = 1.0;
            double frequency = 1.0;
            double max = 0.0;

            x *= _scale;
            y *= _scale;
            z *= _scale;
            w *= _scale;

            for(int t=0;t< _octaves.Length; ++t)
            {
                var octave = _octaves[t];
                result += OpenSimplexNoise.noise4_Fallback(octave.Seed, x * frequency, y * frequency, z * frequency, w * frequency) * amplitude;
                max += amplitude;
                frequency *= _frequency;
                amplitude *= _amplitude;
            }
            if (normalized)
                result /= max;

            return result;
        }

        struct Octave
        {
            public long Seed;
        }
    }
}
