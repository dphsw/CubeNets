using System.Collections.Concurrent;
using System.Diagnostics;
using CubeNetsLibrary;

List<CubeNet> FirschingNets = CubeNet.GetTesseractNets();

List<Tree> Trees = FirschingNets.Select(net => net.ToTree()).ToList();

Debug.Assert(Trees.Count == 261);

ConcurrentDictionary<int, float[]> graphs = new ConcurrentDictionary<int, float[]>();

Console.WriteLine("Starting finding graphs.");

Parallel.ForEach(Enumerable.Range(0, 261), n => {
    graphs.TryAdd(n, Trees[n].FindGraphCoords());
});

Console.WriteLine("Finished finding graphs.");

for (int i = 0; i < 261; i++) {
    string facetData = FirschingNets[i].Get4DBFacetsData();
    string pairsData = FirschingNets[i].Get4DBPairsData();
    string graphData = CoordsTo4DBFormat(graphs[i]);
    string netData = $"{facetData}#{pairsData}#{graphData}";
    Console.WriteLine(netData); // Easy just to copy and paste data from the console to where we need it
}

string CoordsTo4DBFormat(float[] coord){
    return string.Join(",", coord.Select(c => c.ToString("0.00")));
}