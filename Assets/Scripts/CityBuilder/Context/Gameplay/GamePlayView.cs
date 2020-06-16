using PG.City.Context.Gameplay.elements;
using PG.City.Model.data;
using UnityEngine;
using UnityEngine.UI;

namespace PG.City.Context.Gameplay
{
    public class GamePlayView : MonoBehaviour
    {
        [Header("References")] 
        public Transform ModulesRoot;

        public GridView GridView;

        public Text GameModeText;
        
        public Text GoldText;
        public Text WoodText;
        public Text IronText;

        public Button RegularModeButton;
        public Button BuildModeButton;

        public RectTransform AddModulePanel;
        public Dropdown ModuleSelectionDropDown;
        public EModuleType ModuleType;
        public Button AddModule;

        public GameObject BuildingInfoPanel;
        public Text BuildingName;
        public Button ProduceButton;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}