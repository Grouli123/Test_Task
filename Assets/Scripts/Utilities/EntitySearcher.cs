using System;
using System.Collections.Generic;
using StaticData.Utilities;
using UnityEngine;

namespace Utilities
{
    public class EntitySearcher : MonoBehaviour
    {
        [SerializeField] private Transform checkPoint;
        [SerializeField] private EntitySearcherStaticData searchData;

        private int searchedCount;
        private Collider[] hits;

        public IEnumerable<Collider> Hits => hits;
        public Collider FirstHit => hits[0];

        private void Awake()
        {
            hits = new Collider[searchData.EntitiesCount];
        }

        public bool IsFound()
        {
            searchedCount = Physics.OverlapSphereNonAlloc(checkPoint.position, searchData.CheckRadius, hits,
                searchData.Mask);
            return searchedCount > 0;
        }
    }
}
