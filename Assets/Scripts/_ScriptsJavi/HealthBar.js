#pragma strict

    var health : float;
    var maxHealth : float;
     
    var adjustment : float= 2.3f;
    private var worldPosition : Vector3= new Vector3();
    private var screenPosition : Vector3= new Vector3();
    private var myTransform : Transform;
    private var myCamera : Camera;
    private var healthBarHeight : int= 5;
    private var healthBarLeft : int= 110;
    private var barTop : int= 1;
    private var myStyle : GUIStyle= new GUIStyle();
     
     
    //assign the camera to a variable so we can raycast from it
    private var myCam : GameObject;
    myCam = GameObject.Find("Main Camera"); //I removed the space from the camera's name in the Unity Inspector, so you will probably need to change this
     
    function Awake()
    {
        myTransform = transform;
        myCamera = Camera.main;
        health = 50; //arbritrarily chosen values to show that this script works
        maxHealth = 100;
    }
     
    function OnGUI()
    {
        worldPosition = new Vector3(myTransform.position.x, myTransform.position.y + adjustment,myTransform.position.z);
        screenPosition = myCamera.WorldToScreenPoint(worldPosition);
       
        //creating a ray that will travel forward from the camera's position   
        var ray = new Ray (myCam.transform.position, transform.forward);
        var hit : RaycastHit;
        var forward = transform.TransformDirection(Vector3.forward);
        var distance = Vector3.Distance(myCam.transform.position, transform.position); //gets the distance between the camera, and the intended target we want to raycast to
       
        //if something obstructs our raycast, that is if our characters are no longer 'visible,' dont draw their health on the screen.
        if (!Physics.Raycast(ray, hit, distance))
        {
            GUI.color = Color.red;
            GUI.HorizontalScrollbar(Rect (screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - barTop, 100, 0), 0, health, 0, maxHealth); //displays a healthbar
           
            GUI.color = Color.white;
            GUI.contentColor = Color.white;                
            GUI.Label(Rect(screenPosition.x - healthBarLeft / 2, Screen.height - screenPosition.y - barTop+5, 100, 100), ""+health+"/"+maxHealth); //displays health in text format
        }
    }
