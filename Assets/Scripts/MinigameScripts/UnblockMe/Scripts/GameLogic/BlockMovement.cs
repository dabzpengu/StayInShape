using UnityEngine;
using System.Collections;
//Apply this Script On Each Block To Move LEFT,RIGHT,UP,DOWN



[RequireComponent(typeof(BoxCollider2D),typeof(AudioSource))]
public class BlockMovement : MonoBehaviour {

    public bool isthiscurrHintObj;
    
    [HideInInspector]
    public Block thisBlock,previousBlock;
    [HideInInspector]
    public int thisblockId;
    private InputManager inputManager;
    private float Left,Right,Up,Down,offset;
    bool SwipeStart, LerpStart;
    public Vector2 startPos,startMousePos;
    public Vector3 swipeStartPos, swipeEndPos;
    public float startMouseTime,speed,startTime,EndTime,LerpStartTime;
    Vector3 currPos;
    Block[] currblocks;
    AudioSource audioSource;
    // Use this for initialization
    private void Awake()
    {
        inputManager = InputManager.instance;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }
        void OnEnable ()
    {
        SwipeStart = false;
        inputManager.OnStartTouch += On_SwipeStart;
        inputManager.OnEndTouch += On_SwipeEnd;
	}

    void OnDisable()
    {
        inputManager.OnStartTouch -= On_SwipeStart;
        inputManager.OnEndTouch -= On_SwipeEnd;
    }

   
    void On_SwipeStart(Vector2 position, float time)
    {
        
        Ray ray = new Ray(position, Vector3.forward);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == this.transform.gameObject )
            {
                audioSource.clip = UnblockMeGameController.startDrag;
                audioSource.Play();
                print("hit");
                if (!UnblockMeGameController.HintMode || (UnblockMeGameController.HintMode && isthiscurrHintObj))
                {
                    SwipeStart = true;
                    currPos = transform.position;
                    startMouseTime = Time.time;
                    startMousePos = inputManager.TouchPosition();
                    //Count Left,Right,Up,down for selected block
                    Left = currPos.x - UnblockMeGameController.GameBoard.CalculateMovement(thisblockId).Left * UnblockMeGameController.BoxSize / 100f;
                    Right = currPos.x + UnblockMeGameController.GameBoard.CalculateMovement(thisblockId).Right * UnblockMeGameController.BoxSize / 100f;
                    Up = currPos.y + UnblockMeGameController.GameBoard.CalculateMovement(thisblockId).Up * UnblockMeGameController.BoxSize / 100f;
                    Down = currPos.y - UnblockMeGameController.GameBoard.CalculateMovement(thisblockId).Down * UnblockMeGameController.BoxSize / 100f;

                }

            }
        }
        if (offset == 0)
        {
            offset = thisBlock.Length * 0.5f * UnblockMeGameController.BoxSize / 100f;
        }
    }

    void On_SwipeEnd(Vector2 position, float time)
    {

        //Changed value after drag..
        if (SwipeStart && !LerpStart)
        {
            audioSource.clip = UnblockMeGameController.endDrag;
            audioSource.Play();
            int Value;
             if (thisBlock.Orientation==BlockOrientation.Orientation.Horizontal)
             {
                 speed = (inputManager.TouchPosition().x - startMousePos.x) / (Time.time - startMouseTime>0?Time.time - startMouseTime:0.1f ) / 10f;

                 Value = Mathf.RoundToInt(Mathf.Abs(startPos.x - Mathf.Clamp(transform.position.x + speed, Left, Right)) / (UnblockMeGameController.BoxSize / 100f));
               
                swipeStartPos = transform.position;
                LerpStart = true;
                LerpStartTime = Time.time;
              
                previousBlock = thisBlock;
               thisBlock= UnblockMeGameController.GameBoard.AddNewValue(thisblockId, Value > 5 ? 5 : Value);
             }
             else if (thisBlock.Orientation == BlockOrientation.Orientation.Vertical)
             {
                 speed = (inputManager.TouchPosition().y - startMousePos.y) / (Time.time - startMouseTime > 0 ? Time.time - startMouseTime : 0.1f) / 10f;
              
                 LerpStart = true;
                 LerpStartTime = Time.time;
                 Value = Mathf.RoundToInt(Mathf.Abs(startPos.y - Mathf.Clamp(transform.position.y + speed, Down, Up)) / (UnblockMeGameController.BoxSize / 100f));
                 swipeStartPos = transform.position;
              
                 previousBlock = thisBlock;
               thisBlock= UnblockMeGameController.GameBoard.AddNewValue(thisblockId, Value > 5 ? 5 : Value);
             }
             swipeEndPos = UnblockMeGameController.GetBlockPosition(thisBlock);
           

            //change in Position
          
           }
           

    }


    void Update()
    {

        if (SwipeStart && !LerpStart)
        {
            if (thisBlock.Orientation == BlockOrientation.Orientation.Horizontal)
            {
                float leftClamp = Mathf.Max(inputManager.TouchPosition().x - offset, Left);
                float rightClamp = Mathf.Min(inputManager.TouchPosition().x - offset, Right);
                float newX = Mathf.Clamp(transform.position.x + ((inputManager.TouchPosition().x - startMousePos.x) / 30f),
                     leftClamp, rightClamp);
                transform.position = new Vector3(Mathf.Clamp(newX, leftClamp, Right), transform.position.y, 0f);
            }
            else
            {
                float downClamp = Mathf.Max(inputManager.TouchPosition().y + offset, Down);
                float upClamp = Mathf.Min(inputManager.TouchPosition().y + offset, Up);
                float newY = Mathf.Clamp(transform.position.y + ((inputManager.TouchPosition().y - startMousePos.y) / 30f),
                     downClamp, upClamp);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(newY,downClamp,Up), 0f);
            }
        }
        if (LerpStart)
        {
            transform.position = Vector3.Lerp(swipeStartPos, swipeEndPos, (Time.time - LerpStartTime) / 0.2f);
        }
        if(LerpStart && (Time.time - LerpStartTime) > 0.2f)
        {
            LerpStart = false;
            if (transform.position != currPos)
            {
                if(!UnblockMeGameController.HintMode)
                    UnblockMeGameController.GameControllerObj.SetCurrMove();
                int index = thisblockId;
                Block prev = previousBlock;

                UnblockMeGameController.blockdata _blockdata = new UnblockMeGameController.blockdata();
              
              
                _blockdata.index = index;

                _blockdata.block = prev;

             
                
                _blockdata.SolutionBoardNo = UnblockMeGameController.TotalMove;
             
                UnblockMeGameController.blockpositionList.Add(_blockdata);
                if (UnblockMeGameController.GameBoard._blocks[0]._col > 4)
                {
                     UnblockMeGameController.GameControllerObj.LevelClear();
                    
                }
                else if (UnblockMeGameController.HintMode && isthiscurrHintObj)
                {
                    print("HERE ");
                    if (transform.position == UnblockMeGameController.hintObj.transform.position)
                    {

                        isthiscurrHintObj = false;
                        
                          
                            UnblockMeGameController.GameControllerObj.SetCurrMove();
                       
                        

                     

                    }
                    UnblockMeGameController.GameControllerObj.SetHint();

                }
               
          



                 
                   
                
                
               
            }
            SwipeStart = false;
            
        }
    }

   
    
}
