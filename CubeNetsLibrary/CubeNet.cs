﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CubeNetsLibrary
{
    public class CubeNet
    {
        public int Dimension;
        public int NetDimension { get { return Dimension - 1; } }

        List<int[]> Coords = new List<int[]>();

        List<int> Pairs = new List<int>();

        public static List<CubeNet> GetTesseractNets()
        {
            string firschingData = @"
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-2),(1,0,0),(1,0,1),(1,1,1) {0 1 3 0 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(-1,0,1),(-1,1,1),(1,0,0) {0 1 3 0 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(1,0,0),(1,1,0),(1,1,1) {0 1 3 2 0 2 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,0,-2),(0,1,0),(-1,1,0),(1,0,0) {0 1 3 1 0 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(0,1,1),(-1,0,1),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(1,0,0),(1,1,0),(2,1,0) {0 1 3 1 2 2 3 0}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,1,0),(-1,1,1),(1,0,0) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,-1,-2),(0,0,1),(0,1,1),(1,1,1) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,0),(-2,0,0),(1,0,0),(1,0,1) {0 1 3 3 2 0 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,0,1),(-1,0,1),(1,0,1),(0,1,0) {0 1 0 3 1 2 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,0),(2,0,0),(1,0,1),(1,1,0) {0 1 3 2 2 0 1 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,0,1),(-1,1,0),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,-1),(0,0,1),(0,0,2),(0,1,2) {0 1 3 2 2 1 0 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-1,-1),(1,0,0),(2,0,0),(2,1,0) {0 1 3 1 2 2 0 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,1,0),(-1,1,1),(1,0,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,1,0),(1,1,0),(1,1,1) {0 1 3 0 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(1,0,0),(1,1,0),(1,1,1) {0 1 3 0 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,1,0),(0,2,0),(-1,1,0),(1,1,0) {0 1 3 1 3 0 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,1),(0,0,2),(0,1,1),(1,0,0) {0 1 3 2 1 0 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-2),(0,1,0),(0,1,1),(1,1,1) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(0,1,0),(1,1,0),(1,2,0) {0 1 3 1 2 3 2 0}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,-1,-2),(1,0,0),(1,0,1),(1,1,1) {0 1 0 3 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,-1),(1,0,0),(2,0,0),(1,0,1) {0 1 3 2 3 2 0 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(0,0,1),(0,1,1),(1,1,1) {0 1 3 0 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(0,0,1),(1,0,1),(1,1,1) {0 1 3 2 0 1 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,-1),(0,0,1),(-1,0,1),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,-2),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,0),(0,1,1),(-1,1,0),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,0,-1),(0,0,1),(0,0,2),(0,1,0),(1,0,0) {0 1 3 2 1 0 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,0,1),(0,1,0),(1,1,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,1,-1),(-1,0,0),(-2,0,0),(1,0,0) {0 1 3 1 3 2 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,1,0),(0,0,1),(1,0,0) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,0,-1),(0,0,1),(0,0,2),(0,1,0),(1,1,0) {0 1 3 2 1 0 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,0),(0,2,0),(0,0,1),(1,0,1) {0 1 3 2 3 0 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,1,0),(0,1,1),(1,1,1) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,-1,-3),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 0 3 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(-1,0,1),(1,0,1),(0,1,0) {0 1 3 0 1 2 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,1,0),(0,2,0),(-1,1,0),(1,0,0) {0 1 3 1 3 0 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(0,0,1),(0,0,2),(-1,0,0),(1,0,0) {0 1 3 3 1 0 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,0,1),(1,0,0),(1,1,0) {0 1 3 0 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,0,1),(0,1,0),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(1,0,0),(1,1,0),(0,0,1) {0 1 0 3 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-1,-1),(0,1,0),(0,2,0),(1,2,0) {0 1 3 1 2 3 0 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,-1),(0,1,0),(0,1,1),(1,1,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,0),(0,1,1),(-1,1,0),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,0),(0,-1,1),(0,1,0),(-1,1,0),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,-1),(1,0,0),(2,0,0),(0,0,1) {0 1 3 3 2 2 0 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(1,0,0),(1,1,0),(0,0,1) {0 1 3 0 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,0,1),(0,1,1),(1,0,1) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,0),(0,2,0),(0,1,1),(1,0,0) {0 1 3 2 3 0 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(1,0,0),(1,0,1),(1,1,1) {0 1 3 2 0 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,-1),(1,0,0),(2,0,0),(1,0,1) {0 1 3 3 2 2 0 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,1,0),(1,1,0),(0,0,1) {0 1 3 0 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(1,0,0),(1,1,0),(0,0,1) {0 1 3 2 0 2 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,0),(0,1,1),(-1,1,1),(1,0,0) {0 1 3 0 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,-2),(0,1,0),(-1,1,0),(1,1,0) {0 1 0 1 3 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,-1),(-1,0,-1),(1,0,0),(1,0,1) {0 1 0 3 3 2 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,0),(0,1,1),(-1,0,0),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,0,-2),(-1,0,0),(-1,1,0),(1,0,0) {0 1 3 1 0 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(0,1,0),(0,1,1),(1,1,1) {0 1 3 2 0 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(1,0,0),(1,0,1),(1,1,0) {0 1 3 0 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,0),(-1,-1,0),(1,-1,0),(0,1,0) {0 1 0 1 3 2 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,-1),(0,1,0),(0,2,0),(0,2,1) {0 1 3 2 2 3 0 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(1,0,0),(1,0,1),(1,1,1) {0 1 0 3 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,1),(0,-1,1),(0,1,0),(-1,0,0),(1,0,0) {0 1 0 1 3 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,-1),(0,0,1),(0,0,2),(1,0,0) {0 1 3 3 2 1 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,1),(0,0,2),(0,1,0),(1,1,0) {0 1 3 2 1 0 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,1,0),(0,2,0),(-1,0,0),(1,0,0) {0 1 3 1 3 0 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,0,1),(0,1,0),(-1,0,0),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(1,0,0),(1,0,1),(1,1,0) {0 1 0 3 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(0,0,1),(0,1,1),(1,1,1) {0 1 3 2 0 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,1,-1),(-1,0,0),(-2,0,0),(-3,0,0) {0 1 3 1 3 2 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-2,-1),(0,1,0),(0,2,0),(1,2,0) {0 1 3 1 2 3 0 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,-1),(0,0,1),(-1,0,0),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(0,1,1),(-1,0,1),(1,0,0) {0 1 3 0 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,0),(-1,1,0),(0,0,1),(1,0,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,0),(2,0,0),(0,0,1),(0,1,0) {0 1 3 2 2 0 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(1,0,0),(1,0,1),(1,1,1) {0 1 3 0 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,0),(0,1,1),(-1,1,0),(1,0,0) {0 1 3 0 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,-1),(0,0,1),(0,0,2),(1,0,2) {0 1 3 2 3 1 0 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,-1),(-1,0,-1),(0,0,1),(1,0,1) {0 1 0 3 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,0,1),(-1,0,2),(0,1,0) {0 1 3 0 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,0),(-1,-1,0),(0,1,0),(1,1,0),(0,0,1) {0 1 0 3 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,-1),(0,0,1),(0,1,1),(1,0,1) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,0),(0,2,0),(0,1,1),(1,1,0) {0 1 3 2 3 0 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,0),(0,1,1),(-1,1,0),(1,1,0) {0 1 3 0 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(0,0,1),(0,1,1),(1,0,1) {0 1 3 2 0 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,1,0),(0,1,1),(1,1,1) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,0,1),(0,1,0),(1,0,0) {0 1 3 0 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,0),(0,1,1),(-1,1,1),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(-1,0,1),(-1,1,1),(1,0,0) {0 1 0 3 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-1,-1),(0,1,0),(0,2,0),(1,1,0) {0 1 3 1 2 3 0 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,-1,-2),(1,0,0),(1,1,0),(1,1,1) {0 1 0 3 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,-2,-2),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 3 1 0 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(1,0,0),(2,0,0),(1,1,0) {0 1 3 1 2 2 0 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,-1),(0,1,0),(0,1,1),(1,0,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,-2),(0,0,1),(-1,0,1),(1,0,1) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,-2,-2),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 3 0 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,0),(-1,1,0),(1,1,0),(0,0,1) {0 1 0 3 3 2 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,0,-1),(0,1,0),(0,2,0),(1,0,0),(1,0,1) {0 1 3 2 3 0 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,-1),(0,0,1),(1,0,1),(1,0,2) {0 1 3 2 3 1 2 0}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,-1),(1,0,-1),(0,0,1),(0,0,2) {0 1 3 3 2 2 1 0}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(0,1,1),(-1,0,0),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,-1,-1),(0,1,0),(0,2,0),(0,2,1) {0 1 3 2 2 3 0 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-2),(0,0,1),(0,1,1),(1,0,1) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,1),(0,-1,1),(0,1,0),(-1,1,0),(1,0,0) {0 1 0 1 3 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(-1,0,1),(0,1,0),(1,1,0) {0 1 0 3 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,0,1),(0,1,1),(1,1,1) {0 1 3 0 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,0),(-1,1,0),(-1,1,1),(1,0,0) {0 1 3 0 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,-2,-1),(0,1,0),(-1,1,0),(1,1,0) {0 1 3 0 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-2),(0,1,0),(0,1,1),(1,1,1) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(-1,0,1),(0,1,0),(1,0,0) {0 1 3 0 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,-1),(0,1,0),(0,1,1),(0,2,1) {0 1 3 2 2 3 1 0}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,0),(-2,0,0),(0,0,1),(1,0,1) {0 1 3 3 2 0 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,0),(0,-1,1),(-1,0,0),(-1,1,0),(1,0,0) {0 1 0 3 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,-3),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(0,1,0),(1,1,0),(0,0,1) {0 1 3 2 0 3 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,-1),(0,0,1),(0,0,2),(0,1,1) {0 1 3 2 2 1 0 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,0),(0,1,1),(-1,0,0),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,-3,-1),(-1,0,0),(-2,0,0),(-3,0,0) {0 1 3 1 3 2 0 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,0),(-1,-1,0),(1,0,0),(1,1,0) {0 1 0 1 3 2 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-2),(1,0,0),(1,1,0),(1,1,1) {0 1 3 0 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(0,1,1),(-1,1,1),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,-1),(0,0,1),(-1,0,1),(1,0,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(-1,0,1),(0,1,0),(1,1,0) {0 1 3 0 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,1,0),(1,1,0),(0,0,1) {0 1 0 3 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,0),(2,0,0),(1,1,0),(0,0,1) {0 1 3 2 2 0 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,-2,-1),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 3 0 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,-1),(-1,0,0),(-1,0,1),(1,0,0) {0 1 0 3 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,-1),(1,0,0),(1,0,1),(2,0,1) {0 1 3 2 3 2 1 0}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,1,0),(0,1,1),(1,0,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,-1),(-1,0,0),(-1,0,1),(1,0,0) {0 1 0 3 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,0,1),(0,1,1),(1,0,1) {0 1 3 0 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,0),(-1,1,0),(1,1,0),(0,0,1) {0 1 3 0 3 2 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,-1),(0,0,1),(-1,0,1),(1,0,1) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(0,1,0),(0,1,1),(1,1,1) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(0,1,1),(-1,1,1),(1,0,0) {0 1 3 0 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,-1,-2),(0,1,0),(0,1,1),(1,1,1) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,-1,-3),(-1,0,0),(-1,0,1),(-1,0,2) {0 1 3 0 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(-1,-2,-1),(0,1,0),(0,2,0),(1,2,0) {0 1 3 2 1 3 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-1,-1),(1,0,0),(1,1,0),(2,1,0) {0 1 3 1 2 2 3 0}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(1,0,0),(2,0,0),(2,1,0) {0 1 3 1 2 2 0 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,-1,-1),(0,0,1),(0,1,1),(0,1,2) {0 1 3 2 2 1 3 0}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,-1),(0,1,0),(-1,1,0),(1,1,0) {0 1 0 1 3 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(0,1,0),(0,2,0),(1,0,0) {0 1 3 1 2 3 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,1),(0,0,2),(0,1,0),(1,0,0) {0 1 3 2 1 0 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,0,1),(1,0,1),(0,1,0) {0 1 0 3 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,0),(-1,0,1),(0,1,0),(1,1,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(1,0,0),(1,0,1),(0,1,0) {0 1 3 0 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(-1,0,1),(1,0,0),(1,1,0) {0 1 3 0 1 2 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,-1),(0,0,1),(0,1,1),(0,1,2) {0 1 3 2 2 1 3 0}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,0,1),(0,1,1),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,-1),(0,0,1),(0,0,2),(1,0,1) {0 1 3 2 3 1 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,-1),(1,0,0),(2,0,0),(2,0,1) {0 1 3 2 3 2 0 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,0),(0,-1,1),(0,1,0),(-1,0,0),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,0),(-1,1,0),(0,0,1),(1,0,0) {0 1 0 3 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,0,1),(0,1,1),(-1,0,1),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,-1),(0,0,1),(-1,0,1),(1,0,1) {0 1 3 0 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,0),(-1,-1,0),(-2,-1,0),(1,0,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(0,1,0),(0,1,1),(1,1,0) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,0),(0,1,1),(-1,1,0),(1,1,0) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(0,1,0),(0,2,0),(1,1,0) {0 1 3 1 2 3 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,0,-1),(0,1,0),(0,2,0),(0,0,1),(1,0,0) {0 1 3 2 3 0 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,0),(-1,1,0),(-1,1,1),(1,0,0) {0 1 0 3 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(1,0,0),(1,0,1),(0,1,0) {0 1 0 3 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,0),(-1,0,1),(-1,1,0),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,0,-1),(0,1,0),(0,2,0),(0,0,1),(1,0,1) {0 1 3 2 3 0 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(-1,0,1),(1,0,1),(0,1,0) {0 1 0 3 1 2 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,-2),(-1,0,0),(-1,0,1),(-1,0,2) {0 1 0 3 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(0,1,1),(-1,0,0),(1,0,0) {0 1 3 0 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-2),(0,1,0),(0,1,1),(1,1,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,0),(-1,0,1),(1,0,0),(1,1,0) {0 1 0 3 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,0),(0,2,0),(1,0,0),(1,0,1) {0 1 3 2 3 0 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(1,0,0),(1,0,1),(1,1,1) {0 1 3 0 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,0),(0,2,0),(0,0,1),(1,0,0) {0 1 3 2 3 0 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-1,-1),(0,1,0),(1,1,0),(1,2,0) {0 1 3 1 2 3 2 0}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,0),(-2,0,0),(0,0,1),(1,0,0) {0 1 3 3 2 0 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(-1,-1,-2),(0,1,0),(1,1,0),(1,1,1) {0 1 3 2 0 3 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(1,0,0),(2,0,0),(0,1,0) {0 1 3 1 2 2 0 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,0,-2),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 3 1 0 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(0,0,1),(1,0,1),(1,1,1) {0 1 3 0 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,0),(-2,0,0),(-1,1,0),(1,0,0) {0 1 3 1 2 0 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,0,1),(0,1,1),(1,1,1) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,-1,-2),(0,1,0),(1,1,0),(1,1,1) {0 1 0 3 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,1,0),(0,2,0),(1,1,0),(0,0,1) {0 1 3 2 3 0 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,1,0),(-1,2,0),(0,0,1) {0 1 3 0 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(-1,0,1),(1,0,0),(1,1,0) {0 1 0 3 1 2 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,0,1),(-1,0,1),(1,0,0),(1,1,0) {0 1 0 3 1 2 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-2),(0,1,0),(1,1,0),(1,1,1) {0 1 3 0 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,0,1),(1,0,1),(1,1,1) {0 1 3 0 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,0,1),(0,1,1),(-1,0,1),(1,0,1) {0 1 3 0 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,0),(-1,-1,0),(1,0,0),(1,1,0),(0,0,1) {0 1 0 3 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,0),(-1,-1,0),(0,1,0),(1,1,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(-1,-2,-1),(0,1,0),(1,1,0),(1,2,0) {0 1 3 2 1 3 2 0}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,-1),(-1,0,0),(-1,1,0),(1,0,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,1,0),(0,1,1),(1,1,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,-1),(0,1,0),(0,2,0),(1,2,0) {0 1 3 1 2 3 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,0),(0,1,1),(-1,0,0),(1,0,0) {0 1 3 0 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,0,1),(-1,0,1),(0,1,0),(1,1,0) {0 1 0 3 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,1),(0,0,2),(1,0,1),(0,1,0) {0 1 3 2 1 0 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,-1),(-1,0,0),(-1,0,1),(-1,0,2) {0 1 0 3 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,0),(-1,-1,0),(0,1,0),(1,0,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,0),(-1,1,0),(0,0,1),(1,0,0) {0 1 3 0 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,-1),(0,0,1),(-1,0,1),(1,0,1) {0 1 0 3 3 1 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,0,1),(1,0,1),(1,1,1) {0 1 0 3 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(0,1,0),(1,1,0),(1,1,1) {0 1 0 3 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(-1,0,1),(0,1,0),(1,0,0) {0 1 0 3 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,0,1),(-1,1,1),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,0,1),(-1,1,0),(1,0,0) {0 1 3 0 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,0,1),(0,1,1),(-1,0,0),(1,0,0) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,-1),(0,1,0),(-1,1,0),(1,0,0) {0 1 0 1 3 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(1,0,0),(1,0,1),(1,1,0) {0 1 3 2 0 2 1 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,1),(0,-1,0),(0,1,0),(-1,0,0),(1,0,0) {0 1 0 1 3 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(0,1,0),(1,1,0),(1,1,1) {0 1 3 2 0 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,0),(-1,-1,0),(0,0,1),(0,1,0),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,1),(0,0,2),(0,1,1),(1,0,1) {0 1 3 2 1 0 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,1),(0,-1,1),(-1,0,0),(-1,1,0),(1,0,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,1),(0,0,2),(1,0,0),(1,1,0) {0 1 3 2 1 0 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(-1,-2,-1),(1,0,0),(1,1,0),(2,1,0) {0 1 3 2 1 2 3 0}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,-1,-1),(0,0,1),(0,0,2),(0,1,2) {0 1 3 2 2 1 0 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,0),(-2,0,0),(1,0,0),(1,1,0) {0 1 3 1 2 0 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,1,-1),(-1,0,-1),(0,0,1),(0,0,2),(1,0,1) {0 1 3 3 2 1 0 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,0),(-2,0,0),(0,1,0),(1,1,0) {0 1 3 1 2 0 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,1,0),(0,1,1),(1,0,0) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,1,0),(-1,1,0),(1,1,0),(0,0,1) {0 1 0 3 3 2 2 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,0,1),(0,1,0),(1,1,0) {0 1 3 0 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,-1,-1),(0,1,0),(0,1,1),(0,2,1) {0 1 3 2 2 3 1 0}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,-1),(0,1,0),(0,1,1),(1,1,0) {0 1 3 0 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,0),(2,0,0),(0,1,0),(0,1,1) {0 1 3 2 2 0 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-1,-1),(1,0,0),(2,0,0),(1,1,0) {0 1 3 1 2 2 0 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,0,0),(-2,0,0),(0,1,0),(1,0,0) {0 1 3 1 2 0 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,1,0),(-1,2,0),(0,0,1) {0 1 0 3 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,0,-3),(0,-1,-1),(-1,0,0),(-1,1,0),(-1,2,0) {0 1 0 1 3 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,0,1),(-1,0,1),(0,1,0),(1,0,0) {0 1 0 3 1 2 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,1,0),(0,0,1),(1,0,0) {0 1 0 3 2 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-2),(0,0,1),(0,1,1),(1,1,1) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,-1),(0,0,1),(0,1,1),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,-1),(-1,0,0),(-1,0,1),(-1,0,2) {0 1 3 0 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,0,1),(-1,0,2),(0,1,0) {0 1 0 3 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(0,0,-2),(0,1,0),(0,1,1),(1,1,0) {0 1 3 2 0 3 1 2}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,-1),(0,0,1),(0,1,0),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(-1,-2,-1),(1,0,0),(2,0,0),(2,1,0) {0 1 3 1 2 2 0 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(-1,-2,-1),(1,0,0),(2,0,0),(2,1,0) {0 1 3 2 1 2 0 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(1,0,0),(1,0,1),(1,1,0) {0 1 3 0 2 2 1 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,0),(2,0,0),(1,0,1),(0,1,0) {0 1 3 2 2 0 1 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-2),(0,1,0),(1,1,0),(1,1,1) {0 1 0 3 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,-1,-2),(0,0,1),(1,0,1),(1,1,1) {0 1 0 3 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,0,0),(-1,0,1),(-1,1,1),(1,0,0) {0 1 3 0 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(-1,-1,-2),(1,0,0),(1,1,0),(1,1,1) {0 1 3 2 0 2 3 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,0),(-1,0,1),(1,0,0),(1,1,0) {0 1 0 3 2 1 2 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(0,0,1),(0,1,1),(-1,0,1),(1,0,1) {0 1 0 3 1 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-2,-1),(0,0,-2),(0,1,0),(-1,1,0),(1,1,0) {0 1 3 1 0 3 2 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(0,1,-1),(-1,0,0),(-1,0,1),(1,0,0) {0 1 3 0 3 2 1 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(0,1,0),(1,1,0),(1,1,1) {0 1 3 0 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,-1),(0,1,0),(1,1,0),(0,0,1) {0 1 0 3 2 3 2 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-2),(-1,0,-1),(1,0,0),(1,1,0),(1,1,1) {0 1 0 3 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(0,-1,-2),(-1,-1,-1),(1,0,0),(1,1,0),(1,1,1) {0 1 3 0 2 2 3 1}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,-1),(0,1,0),(0,2,0),(0,1,1) {0 1 3 2 2 3 0 1}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(-1,0,0),(-1,0,1),(0,1,0),(1,0,0) {0 1 0 3 2 1 3 2}]
[(0,0,0),(0,0,-1),(0,-1,-1),(-1,-1,-1),(1,0,0),(2,0,0),(0,0,1),(0,1,1) {0 1 3 2 2 0 1 3}]
[(0,0,0),(0,0,-1),(0,0,-2),(0,-1,-1),(0,1,0),(-1,1,0),(0,0,1),(1,0,0) {0 1 0 3 3 2 1 2}]
";

            List<CubeNet> nets = new List<CubeNet>();

            int netEnd, netStart = firschingData.IndexOf('[');
            do
            {
                netEnd = firschingData.IndexOf(']', netStart);
                nets.Add(new CubeNet(firschingData.Substring(netStart + 1, netEnd - netStart - 1)));
                netStart = firschingData.IndexOf('[', netEnd);
            }while (netStart > -1);

            return nets;
        }

        public CubeNet(string netData)
        {
            int numFacets = netData.ToCharArray().Where(c => c == '(').Count();
            Dimension = numFacets / 2;
            int netDimension = NetDimension;

            int coordsEnd, coordsStart = netData.IndexOf('(');
            do
            {
                int[] coords = new int[netDimension];
                coordsEnd = netData.IndexOf(')', coordsStart);
                string coordsString = netData.Substring(coordsStart + 1, coordsEnd - coordsStart - 1);
                string[] coordsStrings = coordsString.Split(',');
                for (int c = 0; c < netDimension; c++) int.TryParse(coordsStrings[c].Trim(), out coords[c]);
                Coords.Add(coords);

                coordsStart = netData.IndexOf('(', coordsEnd);
            } while (coordsStart > -1);

            // Get pairs data
            coordsStart = netData.IndexOf('{', coordsEnd);
            coordsEnd = netData.IndexOf('}', coordsStart);
            Debug.Assert(coordsStart != -1 && coordsEnd != -1, "Pairs data not found");
            string pairsString = netData.Substring(coordsStart + 1, coordsEnd - coordsStart - 1);
            string[] pairsStrings = pairsString.Split(' ');
            foreach(string num in pairsStrings) if(int.TryParse(num.Trim(), out int pairData)) Pairs.Add(pairData);
            Debug.Assert(Pairs.Count == numFacets, "Wrong amount of pairs data");

        }

        public Tree ToTree()
        {
            List<int> parentNodes = new List<int>() { 0 };

            int numFacets = 2 * Dimension;
            int netDimension = NetDimension;

            int[] coordParent, coordNode;
            int d2, axisSep, n, p, c;
            for (n = 1; n < numFacets; n++)
            {
                coordNode = Coords[n];
                for (p = 0; p < n; p++)
                {
                    coordParent = Coords[p];
                    d2 = 0;
                    for (c = 0; c < netDimension; c++)
                    {
                        axisSep = coordParent[c] - coordNode[c];
                        d2 += axisSep * axisSep;
                    }

                    if (d2 == 1)
                    {
                        parentNodes.Add(p + 1);
                        break;
                    }
                }
            }

            Debug.Assert(parentNodes.Count == numFacets);
            return new Tree(parentNodes);
        }

        public string Get4DBFacetsData() {
            return string.Join("|", Coords.Select(f => 
                string.Join(",", f.Select(c => c.ToString()))
            ));
        }

        public string Get4DBPairsData() {
            return string.Join(",", Pairs);
        }
    }
}
