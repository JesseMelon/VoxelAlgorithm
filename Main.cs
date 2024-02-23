using Godot;

public partial class Main : Node3D
{

	private const float _MOUSE_SENSITIVITY = 0.005f;
	private const float _SCROLL_SENSITIVITY = 0.05f;

	private Camera3D camera; //set in ready
	private Node3D cameraPivot;

    public override void _Input(InputEvent theEvent)
    {
		
		if (theEvent is InputEventMouseMotion inputEventMouseMotion)
		{	
			//middle mouse button
			if (Input.IsActionPressed("camera_rotate"))
			{
				//rotate camera
				cameraPivot.RotateY(-inputEventMouseMotion.Relative.X * _MOUSE_SENSITIVITY);
				cameraPivot.RotateObjectLocal(camera.Basis.X,-inputEventMouseMotion.Relative.Y * _MOUSE_SENSITIVITY);
                cameraPivot.Rotation = new Vector3(Mathf.Clamp(cameraPivot.Rotation.X,-Mathf.Pi / 2, Mathf.Pi / 2), cameraPivot.Rotation.Y, cameraPivot.Rotation.Z);
			}


        }
		if (theEvent is InputEventMouseButton mouseButtonEvent)
		{
			//scrolling
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
		StandardMaterial3D material = new();

		material.ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded;
		material.VertexColorUseAsAlbedo = true; //commment and uncomment below to remove override and apply yellow via material
		//material.AlbedoColor = Colors.Brown; 

		immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
		immediateMesh.SurfaceSetColor(Colors.Yellow); //use this line with alternate colours to change w/out need of new material


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
		immediateMesh.SurfaceSetMaterial(0,material);

		MeshInstance3D immediateMeshInstance = new();
		immediateMeshInstance.Scale = new Vector3(SCALE_FACTOR, SCALE_FACTOR, SCALE_FACTOR);

		immediateMeshInstance.Mesh = immediateMesh;
		cubeMeshInstance3D.AddChild(immediateMeshInstance);

		ImmediateMesh axisImmediateMesh = new();
		StandardMaterial3D axisMaterial = new();

		axisMaterial.ShadingMode = BaseMaterial3D.ShadingModeEnum.Unshaded;
		axisMaterial.VertexColorUseAsAlbedo = true;
		axisImmediateMesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
		axisImmediateMesh.SurfaceSetColor(Colors.Red);

		const float axisLength = 10f;

		axisImmediateMesh.SurfaceAddVertex(new Vector3(0, 0, 0));
		axisImmediateMesh.SurfaceAddVertex(new Vector3(axisLength, 0, 0));


		axisImmediateMesh.SurfaceEnd();
		axisImmediateMesh.SurfaceSetMaterial(0,axisMaterial);
		MeshInstance3D axisMeshInstance = new();
		axisMeshInstance.Mesh = axisImmediateMesh;
		AddChild(axisMeshInstance);
		

	}

	//signals
    private void _on_static_body_3d_mouse_entered()
	{
		GD.Print("Mouse Entered");
	}
    private void _on_static_body_3d_mouse_exited()
	{
		GD.Print("Mouse Exited");
	}
	private void _on_static_body_3d_input_event(Camera3D camera , InputEvent theEvent, Vector3 clickPosition, Vector3 clickNormal, int shapeIndex)
	{
		switch (theEvent)
		{
			case InputEventMouseButton inputEventMouseButton:
				
				switch (inputEventMouseButton.ButtonIndex)
				{
					case MouseButton.Left:
						if (inputEventMouseButton.IsPressed())
							GD.Print("Clicked at" + clickPosition);
						break;
					case MouseButton.Right:
						break;
				}
				break;
		
		}
	}


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //public override void _Process(double delta)
    //{
    //}
}
