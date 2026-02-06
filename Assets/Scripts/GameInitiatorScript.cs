using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameInitiatorScript : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Light2D _mainDirectionalLight;
    [SerializeField] private GameObject _backgroundScreen;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _dog;
    [SerializeField] private GameObject _box;
    [SerializeField] private GameObject _square;
    [SerializeField] private GameObject _circle;
    [SerializeField] private GameObject _factory;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private MaterialManager _materialManager;

    public static GameInitiatorScript Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    private async void Start()
    {
        BindObjects();
        await InitializeObjects();
        await CreateObjects();
    }

    private void BindObjects()
    {
        _loadingScreen = Instantiate(_loadingScreen);
        _mainCamera = Instantiate(_mainCamera);
        _mainDirectionalLight = Instantiate(_mainDirectionalLight);
        _inputHandler = Instantiate(_inputHandler);
        _materialManager = Instantiate(_materialManager);
    }

    private async UniTask InitializeObjects()
    {
        await UniTask.Delay(1000);
        _loadingScreen.SetActive(false);
    }

    private async UniTask CreateObjects()
    {
        _backgroundScreen = Instantiate(_backgroundScreen);
        //_dog = Instantiate(_dog);
        //_box = Instantiate(_box);
        //_square = Instantiate(_square);
        //_circle = Instantiate(_circle);
        _factory = Instantiate(_factory);   
    }
}
