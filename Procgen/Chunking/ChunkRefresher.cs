using MarkusSecundus.Utils.Extensions;
using MarkusSecundus.Utils.Primitives;
using System.Collections;
using UnityEngine;

namespace MarkusSecundus.Utils.Procgen.Chunking
{
    public class ChunkRefresher : MonoBehaviour
    {
        [SerializeField] float RefreshFrequency_seconds = 0.5f;
        [SerializeField] ChunkSystem ChunkSystem;
        [SerializeField] Interval<Vector3Int> Window = new Interval<Vector3Int>(new Vector3Int(-1, -1, 0), new Vector3Int(1, 1, 0));
        IEnumerator Start()
        {
            while (ChunkSystem != null)
            {
                if (RefreshFrequency_seconds < -0)
                    yield break;

                RefreshChunks();

                yield return (RefreshFrequency_seconds <= 0f)? null: new WaitForSeconds(RefreshFrequency_seconds);
            }
        }
        public void RefreshChunks()
        {
            if (ChunkSystem.IsNil() || !ChunkSystem.isActiveAndEnabled)
                return;

            var chunkCoords = ChunkSystem.WorldToChunkCoords(transform.position);

            for (int x = Window.Min.x; x <= Window.Max.x; ++x)
                for (int y = Window.Min.y; y <= Window.Max.y; ++y)
                    for (int z = Window.Min.z; z <= Window.Max.z; ++z)
                    {
                        ChunkSystem.GetChunkByIndex(chunkCoords + new Vector3Int(x, y, z));
                    }
        }
    }
}
