using System.Collections.Concurrent;
using System.Diagnostics;
using CubeNetsLibrary;

List<CubeNet> FirschingNets = CubeNet.GetTesseractNets();

List<Tree> Trees = FirschingNets.Select(net => net.ToTree()).ToList();

Debug.Assert(Trees.Count == 261);

ConcurrentDictionary<int, float[]> graphs = new ConcurrentDictionary<int, float[]>();

Parallel.ForEach(Enumerable.Range(0, 261), n => {
    graphs.TryAdd(n, Trees[n].FindGraphCoords());
});


