using Godot;

public partial class Main : Node3D
{

	private const float _MOUSE_SENSITIVITY = 0.005f;
	private const float _SCROLL_SENSITIVITY = 0.05f;

	private Camera3D camera; //set in ready
	private Node3D cameraPivot;

    public override void _Input(InputEvent theEvent)
    {

		if (theEvent is InputEventMouseMotion mouseMotionEvent)
		{
			if (Input.IsActionPressed("camera_rotate"))
			{
				//rotate camera
				cameraPivot.RotateY(-mouseMotionEvent.Relative.X * _MOUSE_SENSITIVITY);
				cameraPivot.RotateObjectLocal(camera.Basis.X,-mouseMotionEvent.Relative.Y * _MOUSE_SENSITIVITY);
                //camera.Rotation = new Vector3(camera.Rotation.X, camera.Rotation.Y, camera.Rotation.Z);
                cameraPivot.Rotation = new Vector3(Mathf.Clamp(cameraPivot.Rotation.X,-Mathf.Pi / 2, Mathf.Pi / 2), cameraPivot.Rotation.Y, cameraPivot.Rotation.Z);
			}


        }
		if (theEvent is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
			{
				cameraPivot.Scale -= new Vector3(_SCROLL_SENSITIVITY, _SCROLL_SENSITIVITY,_SCROLL_SENSITIVITY) ;
			} 
			else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
			{
                cameraPivot.Scale += new Vector3(_SCROLL_SENSITIVITY, _SCROLL_SENSITIVITY, _SCROLL_SENSITIVITY);
            }
		}
		
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		//consts
		const float SCALE_FACTOR = 1.01f;

		//nodes
		cameraPivot = GetNode<Node3D>("CameraPivot");
		camera = GetNode<Camera3D>("CameraPivot/Camera3D");
		

		MeshInstance3D cubeMeshInstance3D = GetNode<MeshInstance3D>("Props/Cube");



		Mesh cubeMesh = cubeMeshInstance3D.Mesh;

		Vector3[] vertices = cubeMesh.GetFaces();

		ImmediateMesh immediateMesh = new();
		immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
		immediateMesh.SurfaceSetColor(Colors.Honeydew);

		for (int i = 0; i < vertices.Length; i+=3)
		{
			//tri line a-b
			immediateMesh.SurfaceAddVertex(vertices[i]);
            immediateMesh.SurfaceAddVertex(vertices[i + 1]);

			//tri line b-c
            immediateMesh.SurfaceAddVertex(vertices[i + 1]);
            immediateMesh.SurfaceAddVertex(vertices[i + 2]);

			//tri line c-a
            immediateMesh.SurfaceAddVertex(vertices[i + 2]);
            immediateMesh.SurfaceAddVertex(vertices[i]);
        }

		immediateMesh.SurfaceEnd();

		MeshInstance3D immediateMeshInstance = new();
		immediateMeshInstance.Scale = new Vector3(SCALE_FACTOR, SCALE_FACTOR, SCALE_FACTOR);

		immediateMeshInstance.Mesh = immediateMesh;
		cubeMeshInstance3D.AddChild(immediateMeshInstance);

	}

    private void _on_static_body_3d_mouse_entered()
	{
		GD.Print("Mouse Entered");
	}
    private void _on_static_body_3d_mouse_exited()
	{
		GD.Print("Mouse Exited");
	}
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //public override void _Process(double delta)
    //{
    //}
}
