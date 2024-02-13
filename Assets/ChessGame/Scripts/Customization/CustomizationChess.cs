using System.Collections.Generic;
using ChessGame.Scripts.ChessEngine;
using UnityEngine;
namespace ChessGame.Scripts.Customization {
    public class CustomizationChess : MonoBehaviour {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private List<ChessmansList> chessmansLists = new List<ChessmansList>();
        [SerializeField] private List<Dictionary<ChessmanType, GameObject>> chessmanDictionaryList;

        private void Start() {
            boardManager = GetComponent<BoardManager>();
            chessmanDictionaryList = new List<Dictionary<ChessmanType, GameObject>>();
            foreach (ChessmansList chessmansList in chessmansLists) {
                Dictionary<ChessmanType, GameObject> dictionary = new Dictionary<ChessmanType, GameObject>();
                foreach (ChessmansList.ChessmanTypeAndGameObject chessmanTypeAndGameObject in chessmansList.list) {
                    dictionary.Add(chessmanTypeAndGameObject.chessmanType, chessmanTypeAndGameObject.gameObjectPrefab);
                }
                chessmanDictionaryList.Add(dictionary);
            }
        }

        public void ChangeChessSkins(int index) {
            if (chessmansLists.Count > index) {
                Dictionary<ChessmanType, GameObject> dictionary = chessmanDictionaryList[index];
                List<Chessman> activeChessmans = boardManager.GetActiveChessmans();
                foreach (Chessman activeChessman in activeChessmans) {
                    MeshFilter oldMeshFilter = activeChessman.GetComponentInChildren<MeshFilter>();
                    MeshRenderer oldMeshRenderer = activeChessman.GetComponentInChildren<MeshRenderer>();
                    if (dictionary.TryGetValue(activeChessman.chessmanType, out GameObject newGameObject)) {
                        MeshFilter newMeshFilter = newGameObject.GetComponentInChildren<MeshFilter>();
                        MeshRenderer newMeshRenderer = newGameObject.GetComponentInChildren<MeshRenderer>();
                        oldMeshFilter.mesh = newMeshFilter.sharedMesh;
                        oldMeshRenderer.material = newMeshRenderer.sharedMaterial;
                    }
                    float size = chessmansLists[index].customObjectSize;
                    activeChessman.gameObject.transform.localScale = Vector3.one*size;
                    Vector3 rotation = chessmansLists[index].rotation;
                    if (rotation != Vector3.zero) {
                        activeChessman.gameObject.transform.rotation = Quaternion.Euler(rotation);
                    }
                }
            }
        }
    }
}