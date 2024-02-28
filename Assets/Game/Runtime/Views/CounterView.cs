using TMPro;
using UnityEngine;

namespace Game.Runtime.Views
{
    public class CounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmp;

        public void SetText(string text)
        {
            _tmp.text = text;
        }
        
    }
}