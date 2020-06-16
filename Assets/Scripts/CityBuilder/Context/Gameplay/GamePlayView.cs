using PG.CityBuilder.Context.Gameplay.elements;
using PG.CityBuilder.Model.Data;
using UnityEngine;
using UnityEngine.UI;

namespace PG.CityBuilder.Context.Gameplay
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