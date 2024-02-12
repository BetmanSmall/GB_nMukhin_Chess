using System.Collections.Generic;
using UnityEngine;
namespace ChessGame.Scripts.ChessEngine {
    public class BoardHighlights : MonoBehaviour {
        [SerializeField] private GameObject highlightPrefab;
        private List<GameObject> _highlights;

        private void Start() {
            _highlights = new List<GameObject>();
        }

        private GameObject GetHighLightObject() {
            GameObject go = _highlights.Find(g => !g.activeSelf);
            if (go == null) {
                go = Instantiate(highlightPrefab, gameObject.transform);
                _highlights.Add(go);
            }
            return go;
        }

        public void HighLightAllowedMoves(bool[,] moves) {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (moves[i, j]) {
                        GameObject go = GetHighLightObject();
                        go.SetActive(true);
                        go.transform.position = new Vector3(i + 0.5f, 0.0001f, j + 0.5f);
                    }
                }
            }
        }

        public void HideHighlights() {
            foreach (GameObject go in _highlights) {
                go.SetActive(false);
            }
        }
    }
}