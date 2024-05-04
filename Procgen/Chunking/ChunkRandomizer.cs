using MarkusSecundus.Utils.Behaviors.Automatization;
using UnityEngine;

namespace MarkusSecundus.Utils.Procgen.Chunking
{
    public interface IRandomizedChunkInitializer
    {
        public void InitChunk(System.Random random, Vector3Int chunkCoords, ChunkSystem chunkSystem);
        public static void InitAll(GameObject root, System.Random random, Vector3Int chunkCoords, ChunkSystem chunkSystem, bool includeInactive = true)
        {
            foreach (var initializer in root.GetComponentsInChildren<IRandomizedChunkInitializer>(includeInactive))
                initializer.InitChunk(random, chunkCoords, chunkSystem);
        }
    }
    public class ChunkRandomizer : MonoBehaviour, IChunkInitializer
    {
        public void InitChunk(Vector3Int chunkCoords, ChunkSystem chunkSystem)
        {
            var seed = _someArbitraryHashFunc(chunkSystem.Rand.Next(), chunkCoords.x, chunkCoords.y, chunkCoords.z);
            var rand = new System.Random(seed);
            IRandomizer.RandomizeAll(this.gameObject, rand);
            IRandomizedChunkInitializer.InitAll(this.gameObject, rand, chunkCoords, chunkSystem);
        }

        int _someArbitraryHashFunc(int a, int b, int c, int d) 
            => a ^ ((b* 3733 + c) * (c ^ 212868329 + 7) + (d * 5427887));
    }
}
