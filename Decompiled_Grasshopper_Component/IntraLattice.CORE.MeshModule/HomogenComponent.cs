using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.CORE.Data;
using IntraLattice.Properties;
using Rhino;
using Rhino.Geometry;

namespace IntraLattice.CORE.MeshModule;

public class HomogenComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)2;

	protected override Bitmap Icon => Resources.homogen;

	public override Guid ComponentGuid => new Guid("{a51ac688-3afc-48a5-b121-48cecf687eb5}");

	public HomogenComponent()
		: base("Homogen", "Homogen", "Homogeneous solidification of lattice wireframe", "IntraLattice", "Mesh")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Wireframe to thicken", (GH_ParamAccess)1);
		pManager.AddNumberParameter("Radius", "Radius", "Strut Radius", (GH_ParamAccess)0);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddMeshParameter("Mesh", "Mesh", "Thickened wireframe", (GH_ParamAccess)0);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		List<Curve> list = new List<Curve>();
		double num = 0.0;
		if (!DA.GetDataList<Curve>(0, list) || !DA.GetData<double>(1, ref num) || list == null || list.Count == 0 || num <= 0.0)
		{
			return;
		}
		int sides = 6;
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		ExoMesh exoMesh = new ExoMesh(list);
		foreach (ExoSleeve sleefe in exoMesh.Sleeves)
		{
			sleefe.StartRadius = num;
			sleefe.EndRadius = num;
		}
		for (int i = 0; i < exoMesh.Hulls.Count; i++)
		{
			if (exoMesh.Hulls[i].SleeveIndices.Count >= 2)
			{
				exoMesh.ComputeOffsets(i, modelAbsoluteTolerance);
				exoMesh.FixSharpNodes(i, sides);
			}
		}
		for (int j = 0; j < exoMesh.Sleeves.Count; j++)
		{
			Mesh val = exoMesh.MakeSleeve(j, sides);
			exoMesh.Mesh.Append(val);
		}
		for (int k = 0; k < exoMesh.Hulls.Count; k++)
		{
			_ = exoMesh.Hulls[k];
			int count = exoMesh.Hulls[k].PlateIndices.Count;
			if (count < 2)
			{
				Mesh val2 = exoMesh.MakeEndFace(k, sides);
				exoMesh.Mesh.Append(val2);
			}
			else
			{
				Mesh val3 = exoMesh.MakeConvexHull(k, sides, modelAbsoluteTolerance, cleanPlates: true);
				exoMesh.Mesh.Append(val3);
			}
		}
		exoMesh.Mesh.Vertices.CombineIdentical(true, true);
		exoMesh.Mesh.FaceNormals.ComputeFaceNormals();
		exoMesh.Mesh.UnifyNormals();
		exoMesh.Mesh.Normals.ComputeNormals();
		DA.SetData(0, (object)exoMesh.Mesh);
	}
}
