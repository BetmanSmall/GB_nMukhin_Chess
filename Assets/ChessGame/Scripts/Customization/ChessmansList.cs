using System.Collections.Generic;
using UnityEngine;
namespace ChessGame.Scripts.Customization {
    [System.Serializable]
    public class ChessmansList {
        public Vector3 rotation;
        public float customObjectSize;
        [SerializeField] public List<ChessmanTypeAndGameObject> list;
        [System.Serializable]
        public class ChessmanTypeAndGameObject {
            public ChessmanType chessmanType;
            public GameObject gameObjectPrefab;
        }
    }
}