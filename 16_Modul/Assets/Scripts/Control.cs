using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{  
     
    public static Control instance;

    private Animator _anim;
    private CharacterController cc;
    public Spawner sp1;
    public GameMaster GM;
    private bool _isInMovment = false;
    private float _distance;
    public float _time , timeToRespawn = 2.0f; 
    
    private float  _jumpTimeClip , _tackleClip , _swipeLineClip; // длинна анимации: прыжок , подкат, свайп линии
        
    private float _currentDir = 0f;       
    public float jumpspeed = 15f;
    public float gravity = -9.8f;
    public float terminalvelocity =  -10.0f;
    public float minfall = -1.5f;
    private float _vertSpeed;
    Vector3 _move = Vector3.zero;
    Vector3 StartPlayerTransform;
    private BoxCollider _col1;
    public ParticleSystem headStars;
    public ParticleSystem tacklePaticles;
    public Transform [] currentLine;
    public delegate void DeathAndReloadPosition();
    public event DeathAndReloadPosition DeathPlayer;
    int line = 1;
    int lineToBack = 0;
    bool checkCourutine = false , wallHit = false , isTackleOn = false;
    
    Vector3 moveToBack;
    Vector3 boxCentreNormal = new Vector3(0, .91f, 0), boxCentreTackle = new Vector3(0,.24f,.85f); //положение тригера при перекате
    Vector3 boxHeighNormal = new Vector3(.94f,1.03f, .79f) , boxHeighTackle = new Vector3(.29f,.22f,.32f);    //размер тригера при перекате

    void Awake()
    {   
        if(Control.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Control.instance = this;
        
    }
    
    void Start()
    {   
        transform.position = currentLine[1].position;
        GM = FindObjectOfType<GameMaster>();        
        _anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        _col1 = GetComponent<BoxCollider>();
        _vertSpeed = minfall;
        StartPlayerTransform = transform.position;
    }

   
    void Update()
    {   
        if(GM.CanPlay)
        {
            cc.Move(_move*Time.deltaTime);
        }
        //движение персонажа вперёд + прыжок
        
        //вычисляем скорость анимации 
        RuntimeAnimatorController timeControll = _anim.runtimeAnimatorController;
        for(int i = 0; i <timeControll.animationClips.Length; i ++)
        {
            if(timeControll.animationClips[i].name == "rightClip")
            {
                _swipeLineClip = timeControll.animationClips[i].length;
            }
            if(timeControll.animationClips[i].name == "JumpUse")
            {
                _jumpTimeClip = timeControll.animationClips[i].length;
            }
            if(timeControll.animationClips[i].name == "TackleOne")
            {
                _tackleClip = timeControll.animationClips[i].length;
            }
            
        }
        
        //перемещение 
        float dir = Input.GetAxisRaw("Horizontal");
        if(cc.isGrounded)
        {               
            if(Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpspeed;
                _anim.SetTrigger("Up");                
                         
            }             
            else
            {
                _vertSpeed = minfall;
            }
            if(Input.GetKeyDown(KeyCode.S) && !checkCourutine )
            {
                StartCoroutine(DoTackle());
            }
        }
        else
        {               
            _vertSpeed += gravity * 4 *  Time.deltaTime; 
            if(_vertSpeed<terminalvelocity)
            {
                _vertSpeed = terminalvelocity;
            }
            
        }
        
        if(dir != 0)
        {   
            
            _currentDir += (int)Mathf.Sign(dir); //текущее напраление лево или право           
            
            
            if(!_isInMovment && !checkCourutine && !isTackleOn && GM.CanPlay )
            {    
                
                line = Mathf.Clamp(line, 0 , currentLine.Length);
                moveToBack = currentLine[line].position; //сохраняем позицию если ударяемся
                lineToBack = line; // сохраняем текущуюю линию если ударяемся                         
                if(dir>0 && line !=2)
                {                   
                    _anim.SetTrigger("Right");
                    StartCoroutine(MoveWithCourutine(currentLine[line+=1].position,_swipeLineClip));                
                }
                if(dir<0 && line !=0)
                {                                 
                    _anim.SetTrigger("Left");
                    StartCoroutine(MoveWithCourutine(currentLine[line -=1].position,_swipeLineClip));
                }
                                
                _isInMovment = true;
            }         
            
        }
        else 
        {
            _isInMovment = false;      
            
        }
        
        _move.y = _vertSpeed;
                       
    }  
    
    // смерть игрока
    public void SetGameOn()
    {   
        Debug.Log("вызов");   
        
        if(DeathPlayer != null)
        {
            DeathPlayer();
        } 
        
         SceneManager.LoadScene(1);
        
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("wall"))
        {  
            wallHit = true;            
            StartCoroutine(starsForCrash()); 
           _anim.SetTrigger("Hit");                       
        }
        if(col.CompareTag("Finish"))
        { 
            GM.CanPlay = false;            
        }
    }   

    //партиклы звёзд запуск и стоп
    IEnumerator starsForCrash()
    {
        headStars.Play();
        yield return new WaitForSeconds(.8f);
        headStars.Stop();
    }
    public void ResetPosition()
    {
        transform.position = StartPlayerTransform;
        
    }
    //смена дорожки с помощью корутины
    IEnumerator MoveWithCourutine (Vector3 targetPoint , float duration)
    {   
        
        float elapsed = 0;
        _distance = (transform.position - targetPoint).magnitude;
        _time = duration;
        float speed = _distance/_time;        
        checkCourutine = true; // проверка работает ли сейчас корутин
              
        while(elapsed<duration)
        {               
            
            elapsed +=Time.deltaTime;
            if(wallHit) // возврат на прежнюю линию при ударе
            {
                targetPoint = moveToBack; 
                line = lineToBack;
                wallHit = false;
            }
                        
            transform.position = Vector3.Lerp(transform.position, targetPoint , speed*elapsed);            
                        
            yield return null;
        }
        checkCourutine = false; // перешли на нужную линию 
    }

    //подкат
    IEnumerator DoTackle()
    {   
        isTackleOn = true;
        _anim.SetBool("TackleOn", true);
        _col1.center = boxCentreTackle;
        _col1.size = boxHeighTackle;      
        tacklePaticles.Play();       
        
        yield return new WaitForSeconds (_tackleClip);
        _anim.SetBool("TackleOn", false);
        isTackleOn = false;
        tacklePaticles.Stop();
        _col1.center = boxCentreNormal;
        _col1.size = boxHeighNormal;
    }
}
