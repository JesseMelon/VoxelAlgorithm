using Godot;
using System;

public partial class Main : Node3D
{
    public override void _Input(InputEvent theEvent)
    {
		base._Input(theEvent);

		if (theEvent is InputEventMouseMotion mouseMotionEvent)
		{
			if (Input.IsActionPressed("camera_rotate"))
			{
				//rotate camera
				GD.Print("rotating");
			}


        }

		
    }


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		const float SCALE_FACTOR = 1.01f;

		MeshInstance3D cubeMeshInstance3D = GetNode<MeshInstance3D>("Meshes/Cube");

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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
