// Copyright (C) 2016 Maxim Gumin, The MIT License (MIT)
// copypasted from https://github.com/mxgmn/WaveFunctionCollapse

using System;
using System.Xml.Linq;
using System.Diagnostics;
using MarkusSecundus.Utils.Assets._Scripts.Utils.Datastructs;
using UnityEngine;

namespace MarkusSecundus.Utils.Procgen.Noise.WaveFunctionCollapse
{
    static class Program
    {

        static Array2D<int> DoGenerate(bool isOverlapping, int width, int height, bool periodic, Model.Heuristic heuristic, 
            int N=3, bool periodicInput=true, int symmetry=8, bool ground=false,
            string subset=null, bool blackBackground=false,
            int limit=-1, int generationAttempts=10
            )
        {
            System.Random random = new();
            Model model;

            if (isOverlapping)
            {
                model = new OverlappingModel(new Array2D<int>(), N, width, height, periodicInput, periodic, symmetry, ground, heuristic);
            }
            else
            {
                model = new SimpleTiledModel(new(true, new SimpleTiledModel.TileConfig.Subset[0], new SimpleTiledModel.TileConfig.Tile[0], new(), new SimpleTiledModel.TileConfig.Neighbor[0]), subset, width, height, periodic, blackBackground, heuristic);
            }

            for (int k = 0; k < generationAttempts; k++)
            {
                bool success = model.Run(random.Next(), limit);
                if (success)
                    return model.Save();
            }
            throw new System.InvalidOperationException("Too many contradictions!");
        }
        static Array2D<int> DoGenerateOverlapping(int width, int height, bool periodic, Model.Heuristic heuristic, 
            int N=3, bool periodicInput=true, int symmetry=8, bool ground=false,
            int limit=-1, int generationAttempts=10
            )
        {
            System.Random random = new();
            Model model = new OverlappingModel(new Array2D<int>(), N, width, height, periodicInput, periodic, symmetry, ground, heuristic);

            for (int k = 0; k < generationAttempts; k++)
            {
                bool success = model.Run(random.Next(), limit);
                if (success)
                    return model.Save();
            }
            throw new System.InvalidOperationException("Too many contradictions!");
        }
        static Array2D<int> DoGenerateTiled(int width, int height, bool periodic, Model.Heuristic heuristic,
            string subset = null, bool blackBackground = false,
            int limit = -1, int generationAttempts = 10
            )
        {
            System.Random random = new();
            Model model;

            model = new SimpleTiledModel(new(true, new SimpleTiledModel.TileConfig.Subset[0], new SimpleTiledModel.TileConfig.Tile[0], new(), new SimpleTiledModel.TileConfig.Neighbor[0]), subset, width, height, periodic, blackBackground, heuristic);
            

            for (int k = 0; k < generationAttempts; k++)
            {
                bool success = model.Run(random.Next(), limit);
                if (success)
                    return model.Save();
            }
            throw new System.InvalidOperationException("Too many contradictions!");
        }




        //static void _main()
        //{
        //    var folder = System.IO.Directory.CreateDirectory("output");
        //    foreach (var file in folder.GetFiles()) file.Delete();
        //
        //    Random random = new();
        //    XDocument xdoc = XDocument.Load("samples.xml");
        //
        //    foreach (XElement xelem in xdoc.Root.Elements("overlapping", "simpletiled"))
        //    {
        //        Model model;
        //        string name = xelem.Get<string>("name");
        //        Console.WriteLine($"< {name}");
        //
        //        bool isOverlapping = xelem.Name == "overlapping";
        //        int size = xelem.Get("size", isOverlapping ? 48 : 24);
        //        int width = xelem.Get("width", size);
        //        int height = xelem.Get("height", size);
        //        bool periodic = xelem.Get("periodic", false);
        //        string heuristicString = xelem.Get<string>("heuristic");
        //        var heuristic = heuristicString == "Scanline" ? Model.Heuristic.Scanline : (heuristicString == "MRV" ? Model.Heuristic.MRV : Model.Heuristic.Entropy);
        //
        //        if (isOverlapping)
        //        {
        //            int N = xelem.Get("N", 3);
        //            bool periodicInput = xelem.Get("periodicInput", true);
        //            int symmetry = xelem.Get("symmetry", 8);
        //            bool ground = xelem.Get("ground", false);
        //
        //            model = new OverlappingModel(new Array2D<int>(), N, width, height, periodicInput, periodic, symmetry, ground, heuristic);
        //        }
        //        else
        //        {
        //            string subset = xelem.Get<string>("subset");
        //            bool blackBackground = xelem.Get("blackBackground", false);
        //
        //            model = new SimpleTiledModel(new(true, new SimpleTiledModel.TileConfig.Subset[0], new SimpleTiledModel.TileConfig.Tile[0], new(), new SimpleTiledModel.TileConfig.Neighbor[0]), subset, width, height, periodic, blackBackground, heuristic);
        //        }
        //
        //        for (int i = 0; i < xelem.Get("screenshots", 2); i++)
        //        {
        //            for (int k = 0; k < 10; k++)
        //            {
        //                Console.Write("> ");
        //                int seed = random.Next();
        //                bool success = model.Run(seed, xelem.Get("limit", -1));
        //                if (success)
        //                {
        //                    Console.WriteLine("DONE");
        //                    var ret = model.Save();
        //                    if (model is SimpleTiledModel stmodel && xelem.Get("textOutput", false))
        //                        System.IO.File.WriteAllText($"output/{name} {seed}.txt", stmodel.TextOutput());
        //                    break;
        //                }
        //                else Console.WriteLine("CONTRADICTION");
        //            }
        //        }
        //    }
        //}

    }
}