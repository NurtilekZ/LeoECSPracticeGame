using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime.Views
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _desciptionTmp;
        [SerializeField] private TMP_Text _buttonTmp;
        [SerializeField] private GameObject _root;
        [field: SerializeField] public Button Button { get; private set; }

        public void SetDescription(string text)
        {
            _desciptionTmp.text = text;
        }

        public void SetButtonText(string text)
        {
            _buttonTmp.text = text;
        }

        public void SetActive(bool state)
        {
            _root.SetActive(state);
        }
    }
}