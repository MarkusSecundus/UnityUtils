// Copyright (C) 2016 Maxim Gumin, The MIT License (MIT)
// copypasted from https://github.com/mxgmn/WaveFunctionCollapse

using System.Linq;
using System.Xml.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MarkusSecundus.Utils.Procgen.Noise.WaveFunctionCollapse
{
    static class WfcHelper
    {
        public static int Random(this double[] weights, double randomTossResult)
        {
            double sum = weights.Sum();
            double threshold = randomTossResult * sum;

            double partialSum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                partialSum += weights[i];
                if (partialSum >= threshold) return i;
            }
            return 0;
        }
    }
}