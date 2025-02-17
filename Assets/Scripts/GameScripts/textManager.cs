using TMPro;
using UnityEngine;

namespace GameScripts
{
    public class textManager : MonoBehaviour
    {
    
        public TextMeshProUGUI movingText;
        public TextMeshProUGUI movesLeft;
        public TextMeshProUGUI aimingText;

        public TextMeshProUGUI player1;
        public TextMeshProUGUI player2;
    
        public TextMeshProUGUI pauseTextQuit;
    
        public Color active = Color.green; // Color when the boolean is true
        public Color inactive = Color.black; // Color when the boolean is false

        private float movementsLeft = 3.0f;

        private bool moving = true;
        private bool aiming = false;
        private int player = 1;
    
        void Update()
        {
            player1.text = GameManager.Instance.GetName(1);
            player2.text = GameManager.Instance.GetName(2);

            pauseTextQuit.text = "Press " +
                                 GameManager.Instance.GeneralActions[GeneralAction.Quit].ToString() +
                                 " to exit";
        
            if (player == 1)
            {
                player1.color = active;
                player2.color = inactive;
            }
            else
            {
                player1.color = inactive;
                player2.color = active;
            }

            if (moving)
            {
                movingText.color = active;
                aimingText.color = inactive;
                movesLeft.SetText(movementsLeft.ToString("F2"));
            }
            else if (aiming)
            {
                movingText.color = inactive;
                aimingText.color = active;
            }
        
        
        }

    
        public void setMovingMode(bool b)
        {
            if (b)
            {
                moving = true;
                aiming = false;
            }
            else
            {
                moving = false;
                aiming = true;
            }
        }

        public void setTeam(int t)
        {
            if (t == 1)
            {
                player = 1;
            }
            else if (t == 2)
            {
                player = 2;
            }
        }

        public void setMovesLeft(float moves)
        {
            movementsLeft = moves;
        }
    }
}
