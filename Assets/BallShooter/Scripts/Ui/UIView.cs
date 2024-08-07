using UnityEngine;

namespace BallShooter.Scripts.Ui
{
    public class UIView : MonoBehaviour
    {
        [field: SerializeField] public GameObject PanelFinish { get; private set; }
        [field: SerializeField] public GameObject PanelDied { get; private set; }
    }
}
