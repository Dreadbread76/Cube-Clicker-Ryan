using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour
{
    #region Variables
    [Header("Cube")]
    public GameObject cube;
    public Renderer cubeRend;
    float[] cubeMultiplier = new float[3] { 1f, 1f, 1f };
    public Transform cubeTransform;
    Vector3[] cubeScale = new Vector3[3];

    [Header("Coin")]
    public float coins = 0;
    float timer = 0;
    float coinRate = 1;
    float coinMultiplier;
    float[] sizeCost = new float[3] {10,10,10};
    float[] colourCost = new float[3] { 100, 100, 500 };

    [Header("Textfields")]
    public Text coinText;
    public Text lengthText, heightText, widthText;
    public Text multiplierText;
    public Text lengthCostText, heightCostText, widthCostText;
    public Text blueCostText, redCostText, purpleCostText;

    [Header("Colours")]
    public Button purpleButton;
    public Material[] colours;
    bool[] Purchased = new bool[3];

    #endregion
    #region Start
    void Start()
    {
       

        //GIVE THE CUBES TRANSFORM A NAME
        cubeTransform = cube.transform;

        //GET THE RENDERER TO APPLY COLOURS
        cubeRend = cube.GetComponent<Renderer>();

        //RED AND BLUE HAVEN'T BEEN PURCHASED YET
        Purchased[0] = false;
        Purchased[1] = false;

        //PURPLE IS LOCKED
        purpleButton.interactable = false;

    }
    #endregion
    #region Update
    private void Update()
    {
        #region Coins
        //COIN MULTIPLIER IS ALL SIDES MULTIPLIED
        coinMultiplier = cubeMultiplier[0] * cubeMultiplier[1] * cubeMultiplier[2];

        //COUNTER FOR COINS
        coinText.text = "Cube Coins: $" + coins;

        //REPLENISH COINS EVERY SECOND WITH COIN MUPLTIPLIER APPLIED
        if (timer < coinRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            coins = coins + coinMultiplier;
        }

        #endregion
        #region Scaling Vectors

        //SCALE UP DESIGNATED DIMENSION WITH APPROPRIATE MULTIPLIER
        cubeScale[0] = new Vector3(1f * cubeMultiplier[0], 0, 0);
        cubeScale[1] = new Vector3(0, 1f * cubeMultiplier[1], 0);
        cubeScale[2] = new Vector3(0, 0, 1f * cubeMultiplier[2]);

        #endregion
        #region Textfields

        #region Multiplier Text

        //TEXTFIELDS FOR DIMENSION MULTIPLIERS AND OVERALL MULTIPLIER
        lengthText.text = "Length: " + cubeScale[0].x;
        heightText.text = "Height: " + cubeScale[1].y;
        widthText.text = "Width: " + cubeScale[2].z;
        multiplierText.text = "Coin Multiplier: x" + coinMultiplier;
        #endregion
        #region Scaling Text

        //TEXTFIELDS FOR SCALING BUTTONS AND THEIR COSTS
        lengthCostText.text = "Longer! [$" + sizeCost[0] + "]";
        heightCostText.text = "Taller! [$" + sizeCost[1] + "]";
        widthCostText.text = "Wider! [$" + sizeCost[2] + "]";
        #endregion
        #region Colour Text

        //TEXTFIELDS FOR COLOUR BUTTONS AND THEIR COSTS
        blueCostText.text = "Blue [$" + colourCost[0] + "]";
        redCostText.text = "Red [$" + colourCost[1] + "]";
        purpleCostText.text = "Purple [Locked]";
        #endregion

        #endregion
        #region Unlocks
        //UNLOCK PURPLE IF BLUE AND RED HAVE BEEN PURCHASED
        if (Purchased[0] && Purchased[1])
        {
            UnlockPurple();
        }
        #endregion
    }
    #endregion
    #region Scaling Function
    public void Bigger(int dimensionIndex)
    {
        //FUNCTION FOR SIZE PURCHASE
        if (coins >= sizeCost[dimensionIndex])
        {
            //SCALE THE DIMENSION CORRELATING WITH THE INDEX NUMBER
            cubeTransform.localScale += cubeScale[dimensionIndex];

            //INCREASE MULTIPLIER FOR CORRELATING DIMENSION
            cubeMultiplier[dimensionIndex] = cubeMultiplier[dimensionIndex] += 1;

            //TAKE AWAY COINS
            coins = coins - sizeCost[dimensionIndex];

            //INCREASE COST OF PURCHASE FOR NEXT PURCHASE
            sizeCost[dimensionIndex] = sizeCost[dimensionIndex] * 2;

            Debug.Log(cubeMultiplier);
        }
        
    }
    #endregion
    #region Colour Change Funtion
    public void ColourChange(int colourIndex)
    {
        
        //FUNCTION FOR COLOUR PURCHASE
        if (coins >= colourCost[colourIndex])
        {
            //CHANGE COLOUR TO DESIGNATED INDEX COLOUR
            cubeRend.material = colours[colourIndex];

            //TAKE AWAY COINS
            coins = coins - colourCost[colourIndex];

            //REDUCE COST TO 0
            colourCost[colourIndex] = 0;

            //COLOUR HAS BEEN PURCHASED
            Purchased[colourIndex] = true;
        }
        
    }
    #endregion
    #region Purple
    void UnlockPurple()
    {
        //PURPLE CAN NOW BE PURCHASED 
        purpleButton.interactable = true;

        //CHANGE PURPLE TEXT TO SHOW AVAILIBILITY
        purpleCostText.text = "Purple [$" + colourCost[2] + "]";
    }
    #endregion
}
