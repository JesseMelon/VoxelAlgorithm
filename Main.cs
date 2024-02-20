using Godot;
using System;

public partial class Main : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		Node3D basicCubeNode = GetNode<Node3D>("BasicCube");
		MeshInstance3D cubeMeshInstance3D = GetNode<MeshInstance3D>("BasicCube / Cube");
		if (basicCubeNode != null)
			GD.Print("Got blend node");
		if (cubeMeshInstance3D != null)
			GD.Print("Got mesh node");



		//Mesh cubeMesh = cubeMeshInstance3D.Mesh;

		MeshDataTool mdt = new();

		//mdt.CreateFromSurface((ArrayMesh)cubeMesh, 0);

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
