using System.Collections.Generic;
using UnityEngine;
namespace ChessGame.Scripts.ChessEngine {
    public class BoardManager : MonoBehaviour {
        public static BoardManager Instance { get; private set; }
        [SerializeField] private BoardHighlights boardHighlights;
        [SerializeField] private List<GameObject> chessmanPrefabs;
        [SerializeField] private bool isWhiteTurn = true;
        private const float TileSize = 1.0f;
        private const float TileOffset = 0.5f;
        private readonly Quaternion _whiteOrientation = Quaternion.Euler(0, 270, 0);
        private readonly Quaternion _blackOrientation = Quaternion.Euler(0, 90, 0);
        private int _selectionX = -1;
        private int _selectionY = -1;
        private bool[,] _allowedMoves;
        private Chessman[,] _сhessmans;
        private List<GameObject> _activeChessman;
        private Chessman _selectedChessman;
        private Material _previousMat;
        [SerializeField] private Material selectedMat;
        private LayerMask _planeLayerMask;
        private Camera _mainCamera;
        [SerializeField] private CameraControl cameraControl;
        public int[] EnPassantMove { get; private set; }
        [SerializeField] private bool _gameStarted;
        public bool GameStarted => _gameStarted;
        [SerializeField] private bool _gamePaused;
        [SerializeField] private MenuCanvas.MenuCanvas menuCanvas;

        private void Start() {
            Instance = this;
            _planeLayerMask = LayerMask.GetMask("ChessPlane");
            _mainCamera = Camera.main;
            EnPassantMove = new int[2] { -1, -1 };
            // SpawnAllChessmans();
            menuCanvas = FindObjectOfType<MenuCanvas.MenuCanvas>();
        }

        private void Update() {
            if (_gameStarted && !_gamePaused) {
                UpdateSelection();
                if (Input.GetMouseButtonDown(0)) {
                    if (_selectionX >= 0 && _selectionY >= 0) {
                        if (_selectedChessman == null) {
                            SelectChessman(_selectionX, _selectionY);
                        } else {
                            MoveChessman(_selectionX, _selectionY);
                        }
                    }
                }
                // if (Input.GetKey("escape")) {
                //     Application.Quit();
                // }
            }
        }

        private void UpdateSelection() {
            Vector3 mPos = Input.mousePosition;
            if (Physics.Raycast(_mainCamera.ScreenPointToRay(mPos), out var hit, 50.0f, _planeLayerMask)) {
                _selectionX = (int)hit.point.x;
                _selectionY = (int)hit.point.z;
            } else {
                _selectionX = -1;
                _selectionY = -1;
            }
        }

        public Chessman GetChessman(int x, int y) {
            return _сhessmans[x, y];
        }

        private void SelectChessman(int x, int y) {
            if (_сhessmans[x, y] == null) return;
            if (_сhessmans[x, y].isWhite != isWhiteTurn) return;
            bool hasAtLeastOneMove = false;
            _allowedMoves = _сhessmans[x, y].PossibleMoves();
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (_allowedMoves[i, j]) {
                        hasAtLeastOneMove = true;
                        i = 8;
                        break;
                    }
                }
            }
            if (!hasAtLeastOneMove) {
                return;
            }
            _selectedChessman = _сhessmans[x, y];
            _previousMat = _selectedChessman.GetComponent<MeshRenderer>().material;
            selectedMat.mainTexture = _previousMat.mainTexture;
            _selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
            boardHighlights.HighLightAllowedMoves(_allowedMoves);
        }

        private void MoveChessman(int x, int y) {
            if (_allowedMoves[x, y]) {
                Chessman c = _сhessmans[x, y];
                if (c != null && c.isWhite != isWhiteTurn) {
                    // Capture a piece
                    if (c.GetType() == typeof(King)) {
                        // End the game
                        EndGame();
                        return;
                    }
                    _activeChessman.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }
                if (x == EnPassantMove[0] && y == EnPassantMove[1]) {
                    if (isWhiteTurn) {
                        c = _сhessmans[x, y - 1];
                    } else {
                        c = _сhessmans[x, y + 1];
                    }
                    _activeChessman.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }
                EnPassantMove[0] = -1;
                EnPassantMove[1] = -1;
                if (_selectedChessman.GetType() == typeof(Pawn)) {
                    if (y == 7) { // White Promotion
                        _activeChessman.Remove(_selectedChessman.gameObject);
                        Destroy(_selectedChessman.gameObject);
                        SpawnChessman(1, x, y, true);
                        _selectedChessman = _сhessmans[x, y];
                    } else if (y == 0) { // Black Promotion
                        _activeChessman.Remove(_selectedChessman.gameObject);
                        Destroy(_selectedChessman.gameObject);
                        SpawnChessman(7, x, y, false);
                        _selectedChessman = _сhessmans[x, y];
                    }
                    EnPassantMove[0] = x;
                    if (_selectedChessman.CurrentY == 1 && y == 3) {
                        EnPassantMove[1] = y - 1;
                    } else if (_selectedChessman.CurrentY == 6 && y == 4) {
                        EnPassantMove[1] = y + 1;
                    }
                }
                _сhessmans[_selectedChessman.CurrentX, _selectedChessman.CurrentY] = null;
                _selectedChessman.transform.position = GetTileCenter(x, y);
                _selectedChessman.SetPosition(x, y);
                _сhessmans[x, y] = _selectedChessman;
                isWhiteTurn = !isWhiteTurn;
                cameraControl.ChangeCameraTransform(isWhiteTurn);
                menuCanvas.SetWhoseMoveNow(isWhiteTurn);
            }
            _selectedChessman.GetComponent<MeshRenderer>().material = _previousMat;
            boardHighlights.HideHighlights();
            _selectedChessman = null;
        }

        private void SpawnChessman(int index, int x, int y, bool isWhite) {
            Vector3 position = GetTileCenter(x, y);
            GameObject go;
            if (isWhite) {
                go = Instantiate(chessmanPrefabs[index], position, _whiteOrientation) as GameObject;
            } else {
                go = Instantiate(chessmanPrefabs[index], position, _blackOrientation) as GameObject;
            }
            go.transform.SetParent(transform);
            _сhessmans[x, y] = go.GetComponent<Chessman>();
            _сhessmans[x, y].SetPosition(x, y);
            _activeChessman.Add(go);
        }

        private Vector3 GetTileCenter(int x, int y) {
            Vector3 origin = Vector3.zero;
            origin.x += (TileSize * x) + TileOffset;
            origin.z += (TileSize * y) + TileOffset;
            return origin;
        }

        private void SpawnAllChessmans() {
            DeSpawnAllChessmans();
            _activeChessman = new List<GameObject>();
            _сhessmans = new Chessman[8, 8];
            /////// White ///////
            // King
            SpawnChessman(0, 3, 0, true);
            // Queen
            SpawnChessman(1, 4, 0, true);
            // Rooks
            SpawnChessman(2, 0, 0, true);
            SpawnChessman(2, 7, 0, true);
            // Bishops
            SpawnChessman(3, 2, 0, true);
            SpawnChessman(3, 5, 0, true);
            // Knights
            SpawnChessman(4, 1, 0, true);
            SpawnChessman(4, 6, 0, true);
            // Pawns
            for (int i = 0; i < 8; i++) {
                SpawnChessman(5, i, 1, true);
            }
            /////// Black ///////
            // King
            SpawnChessman(6, 4, 7, false);
            // Queen
            SpawnChessman(7, 3, 7, false);
            // Rooks
            SpawnChessman(8, 0, 7, false);
            SpawnChessman(8, 7, 7, false);
            // Bishops
            SpawnChessman(9, 2, 7, false);
            SpawnChessman(9, 5, 7, false);
            // Knights
            SpawnChessman(10, 1, 7, false);
            SpawnChessman(10, 6, 7, false);
            // Pawns
            for (int i = 0; i < 8; i++) {
                SpawnChessman(11, i, 6, false);
            }
        }

        private void DeSpawnAllChessmans() {
            if (_activeChessman != null) {
                foreach (GameObject go in _activeChessman) {
                    Destroy(go);
                }
            }
        }

        private void EndGame() {
            string whoWin;
            if (isWhiteTurn) {
                whoWin = "White wins!";
            } else {
                whoWin = "Black wins!";
            }
            Debug.Log(whoWin);
            DeSpawnAllChessmans();
            boardHighlights.HideHighlights();
            _gameStarted = false;
            menuCanvas.GameEnded(whoWin);
        }

        public void StartGame(bool whiteStart) {
            SpawnAllChessmans();
            isWhiteTurn = whiteStart;
            _gameStarted = true;
            _gamePaused = false;
            cameraControl.ChangeCameraTransform(isWhiteTurn);
            menuCanvas.SetWhoseMoveNow(isWhiteTurn);
        }

        public bool PauseOrResumeGame() {
            return PauseOrResumeGame(!_gamePaused);
        }

        private bool PauseOrResumeGame(bool pause) {
            _gamePaused = pause;
            return _gamePaused;
        }
    }
}