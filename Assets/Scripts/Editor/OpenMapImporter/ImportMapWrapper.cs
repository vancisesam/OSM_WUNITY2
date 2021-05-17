using System.Collections.Generic;
using UnityEngine;


/*
    Copyright (c) 2018 Sloan Kelly

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

internal sealed class ImportMapWrapper
{

    private ImportMapDataEditorWindow _window;
    private string _mapFile;
    public static Transform _obstacles;

    private static Transform _walkable;
    private static Transform Walkable {
        get {
            if(_walkable == null) {
                _walkable = CreateParent("Walkable");
            }
            return _walkable;
        }
    }
    private static Transform _notWalkable;
    private static Transform NotWalkable {
        get {
            if (_notWalkable == null) {
                _notWalkable = CreateParent("NotWalkable");
            }
            return _notWalkable;
        }
    }
    public static Dictionary<OsmWay.OSMStructreType, Transform> structureParents;
    public static Transform GetParentForOSMStructureType(OsmWay.OSMStructreType structureType) {
        if (structureParents == null) structureParents = new Dictionary<OsmWay.OSMStructreType, Transform>();
        if (!structureParents.ContainsKey(structureType) ) {
            var parent = CreateParent(structureType.ToString());
            switch (structureType) {
                case OsmWay.OSMStructreType.Grass:
                    parent.SetParent(Walkable);
                    break;
                case OsmWay.OSMStructreType.Water:
                case OsmWay.OSMStructreType.Building:
                    parent.SetParent(NotWalkable);
                    break;
            }
        }
        return structureParents[structureType];
    }

    public ImportMapWrapper(ImportMapDataEditorWindow window, string mapFile)
                            
    {
        _window = window;
        _mapFile = mapFile;
        
    }

    public void Import()
    {
        
        var mapReader = new MapReader();
        mapReader.Read(_mapFile);

        //Transform Landuse = CreateParent("Landuse");
        //Transform Water = CreateParent("Water");
        //Transform Residential = CreateParent("Residential");
        //Transform Pedestrian = CreateParent("Pedestrian");


        var buildingMaker = new BuildingMaker(mapReader);
        var roadMaker = new WayMaker(mapReader);
        var FlatMaker = new FlatMaker(mapReader);

        

        Process(buildingMaker, "Importing buildings");
        Process(roadMaker, "Importing roads");
        Process(FlatMaker, "Importing Flat things");

        var walkableMesh = new List<OsmWay>();
        buildingMaker.Map.ways.FindAll((w) => { return "iswalkable" }));
        walkableMesh.AddRange(buildingMaker.Map.ways.FindAll((w) => { return ?? })); //roads
        walkableMesh.AddRange(buildingMaker.Map.ways.FindAll((w) => { return ?? })); //flats

        

        Combine.CombineMeshes(walkableMesh);

    }

    private void Process(BaseInfrastructureMaker maker, string progressText)
    {
        float nodeCount = maker.NodeCount;
        var progress = 0f;

     foreach (var node in maker.Process())
        {
           progress = node / nodeCount;
            _window.UpdateProgress(progress, progressText, false);
        }
        _window.UpdateProgress(0, string.Empty, true);
    }



    public static Transform CreateParent(string name)
    {

        GameObject go = new GameObject(name);

        return go.transform;
    }


}
