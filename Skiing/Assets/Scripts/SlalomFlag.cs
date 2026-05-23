using UnityEngine;

public class SlalomFlag : MonoBehaviour
{
    private enum Direction { Left, Right };
    [SerializeField] private Direction direction;
    private bool passed = false;
    public static event GameManager.TimerPenaltyEvent Penalty;
    [SerializeField] private float penaltyTime = 2.5f;
    [SerializeField] private Material goodMat, badMat;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer =  GetComponent<MeshRenderer>();   
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerControl.Instance.transform.position.z <
           transform.position.z && !passed)
        {
            passed = true;
            Direction passingDir = Direction.Left;
            if(PlayerControl.Instance.transform.position.x>
               transform.position.x)
            {
                passingDir = Direction.Right;
            }
            Debug.Log("player passed flag on " + passingDir);
            if(passingDir != direction)
            {
                Debug.Log("PENALTY");
                Penalty.Invoke(penaltyTime);
                meshRenderer.material = badMat;
            }
            else
            {
                meshRenderer.material = goodMat;
            }
        }
    }
}