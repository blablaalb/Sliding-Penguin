using System.Collections.Generic;
using UnityEngine;
using System;

public class PlatformChunkVariationManager : MonoBehaviour
{
    [SerializeField]
    private PlatformChunk[] _chunkVaritations;

    /// <summary>
    /// Amount of variations.
    /// </summary>
    public int PlatformVariationsCount => _chunkVaritations.Length;

    /// <summary>
    /// Disables crrent chunk, instantiates a new one and return reference to it.
    /// </summary>
    /// <returns>Reference to the new instantiated platform chunk.</returns>
    public PlatformChunk SpawnRandomVariation()
    {
        if (PlatformVariationsCount <= 0)
        {
            return this.gameObject.GetComponent<PlatformChunk>();
        }
        int randomIndx = UnityEngine.Random.Range(0, _chunkVaritations.Length);
        PlatformChunk randomChunk = _chunkVaritations[randomIndx];
        randomChunk = Instantiate<PlatformChunk>(randomChunk);
        randomChunk.gameObject.transform.SetParent(transform.parent);
        gameObject.SetActive(false);
        return randomChunk;
    }
}

