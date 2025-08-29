using MarkusSecundus.Utils.Primitives;
using MarkusSecundus.Utils.Extensions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System.Collections;


namespace MarkusSecundus.Utils.Procgen.Chunking
{
    public interface IChunkInitializer
    {
        public void InitChunk(Vector3Int chunkCoords, ChunkSystem chunkSystem);
        public static void InitAll(GameObject root, Vector3Int chunkCoords, ChunkSystem chunkSystem, bool includeInactive = true)
        {
            foreach (var initializer in root.GetComponentsInChildren<IChunkInitializer>(includeInactive))
                initializer.InitChunk(chunkCoords, chunkSystem);
        }
    }
    public class ChunkSystem : MonoBehaviour
    {
        const string ChunksRootName = "ChunksRoot";
        Transform _chunksRoot;
        Transform GetChunksRoot()
        {
            if (!_chunksRoot)
            {
                _chunksRoot = this.transform.Find(ChunksRootName);
                if (!_chunksRoot)
                {
                    _chunksRoot = new GameObject(ChunksRootName).transform;
                    _chunksRoot.SetParent(this.transform);
                    _chunksRoot.ResetLocalPositionRotationScale();
                }
            }
            return _chunksRoot;
        }

        [field: SerializeField] public Vector3 ChunkDimensions { get; private set; }
        [field: SerializeField] public GameObject ChunkPrefab { get; private set; }
        
        [SerializeField] int _seed=-1;
        [SerializeField] float _chunkTimeToLive_seconds = -0f;

        System.Random _rand;
        public System.Random Rand => _rand ??= (_seed == -1 ? new System.Random() : new System.Random(_seed));

        private void Start()
        {
            GetChunksRoot();
            _chunks.Clear();
            foreach (Transform chunk in _chunksRoot)
                if (TryParseChunkName(chunk.name, out var coords))
                    _chunks[coords] = new ChunkInfo(chunk.gameObject);

            if(_chunkTimeToLive_seconds > Mathf.Epsilon)
            {
                StartCoroutine(chunkKiller());
                IEnumerator chunkKiller()
                {
                    while (true)
                    {
                        yield return new WaitForSeconds(_chunkTimeToLive_seconds * 0.3f); //arbitrary
                        foreach(var idx in _chunks.Keys.ToArray())
                        {
                            var chunk = _chunks[idx];
                            if (chunk.Chunk.IsNil())
                            {
                                _chunks.Remove(idx);
                            }
                            else if(Time.timeAsDouble > (chunk.LastSeenTimestamp + _chunkTimeToLive_seconds))
                            {
                                Destroy(chunk.Chunk);
                                _chunks.Remove(idx);
                            }
                        }
                    }
                }
            }
        }

        struct ChunkInfo
        {
            public ChunkInfo(GameObject chunk) => (Chunk, LastSeenTimestamp) = (chunk, Time.timeAsDouble);

            public GameObject Chunk;
            public double LastSeenTimestamp;
        }

        Dictionary<Vector3Int, ChunkInfo> _chunks = new();
        public GameObject GetChunkFromLocalCoords(Vector3 worldCoords) => GetChunkByIndex(LocalToChunkCoords(worldCoords));
        public GameObject GetChunkFromWorldCoords(Vector3 worldCoords) => GetChunkByIndex(WorldToChunkCoords(worldCoords));
        public GameObject GetChunkByIndex(Vector3Int chunkCoords)
        {
            if (TryGetChunkByIndex(chunkCoords, out var ret))
                return ret;

            var chunk = ChunkPrefab ? Instantiate(ChunkPrefab) : new GameObject();
            chunk.name = GenerateChunkName(chunkCoords);
            chunk.transform.SetParent(GetChunksRoot(), false);
            chunk.transform.localPosition = GetChunkLocalOrigin(chunkCoords);
            chunk.SetActive(true);
            foreach (var toInit in chunk.GetComponentsInChildren<IChunkInitializer>(true))
                toInit.InitChunk(chunkCoords, this);
            _chunks[chunkCoords] = new ChunkInfo(chunk.gameObject);
            return chunk;
        }
        public bool TryGetChunkByIndex(Vector3Int chunkCoords, out GameObject ret)
        {
            ret = default;
            if(_chunks.TryGetValue(chunkCoords, out var info))
            {
                if (info.IsNil())
                {
                    _chunks.Remove(chunkCoords);
                    return false;
                }
                _chunks[chunkCoords] = new ChunkInfo(info.Chunk); //Chunk info is a struct -> to set new timestamp, the whole value must be set anew
                ret = info.Chunk;
                return true;
            }
            return false;
        }
        public Vector3Int WorldToChunkCoords(Vector3 worldCoords) => LocalToChunkCoords(transform.GlobalToLocal(worldCoords));
        public Vector3Int LocalToChunkCoords(Vector3 worldCoords)
            => new Vector3Int((int)(worldCoords.x / ChunkDimensions.x), (int)(worldCoords.y / ChunkDimensions.y), (int)(worldCoords.z / ChunkDimensions.z));

        public Vector3 GetChunkLocalOrigin(Vector3Int chunkCoords) => ChunkDimensions.MultiplyElems(chunkCoords);
        public Vector3 GetChunkWorldOrigin(Vector3Int chunkCoords) => transform.LocalToGlobal(GetChunkLocalOrigin(chunkCoords));




        static string GenerateChunkName(Vector3Int chunkCoords) => $"Chunk({chunkCoords.x}_{chunkCoords.y}_{chunkCoords.z})";
        static readonly Regex ChunkNameParser = new Regex("^Chunk[(](?<xCoord>[-0-9]+)_(?<yCoord>[-0-9]+)_(?<zCoord>[-0-9]+)[)]$");
        static bool TryParseChunkName(string chunkName, out Vector3Int chunkCoords)
        {
            chunkCoords = default;
            var match = ChunkNameParser.Match(chunkName);
            if (!match.Success) return false;
            chunkCoords = new Vector3Int(
                int.Parse(match.Groups["xCoord"].Value),
                int.Parse(match.Groups["yCoord"].Value),
                int.Parse(match.Groups["zCoord"].Value)
            );
            return true;
        }
    }
}