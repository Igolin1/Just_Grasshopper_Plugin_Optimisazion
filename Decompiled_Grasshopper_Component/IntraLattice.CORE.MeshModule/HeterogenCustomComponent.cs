using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.CORE.Data;
using IntraLattice.Properties;
using Rhino;
using Rhino.Geometry;

namespace IntraLattice.CORE.MeshModule;

public class HeterogenCustomComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)4;

	protected override Bitmap Icon => Resources.heterogenCustom;

	public override Guid ComponentGuid => new Guid("{5fa648cd-af7e-41e5-ac9c-f81bc19466bb}");

	public HeterogenCustomComponent()
		: base("Heterogen Custom", "HeterogenCustom", "Heterogeneous solidification of lattice wireframe", "IntraLattice", "Mesh")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Wireframe to thicken.", (GH_ParamAccess)1);
		pManager.AddNumberParameter("Start Radii", "StartRadii", "Radius at the start of each strut.", (GH_ParamAccess)1);
		pManager.AddNumberParameter("End Radii", "EndRadii", "Radius at the end of each strut.", (GH_ParamAccess)1);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddMeshParameter("Mesh", "Mesh", "Thickened wireframe.", (GH_ParamAccess)0);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		List<Curve> list = new List<Curve>();
		List<double> list2 = new List<double>();
		List<double> list3 = new List<double>();
		if (!DA.GetDataList<Curve>(0, list) || !DA.GetDataList<double>(1, list2) || !DA.GetDataList<double>(2, list3) || list == null || list.Count == 0 || list2 == null || list2.Count == 0 || list3 == null || list3.Count == 0)
		{
			return;
		}
		if (list2.Count != list.Count || list3.Count != list.Count)
		{
			((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)20, "Number of radii in each list must have same number of elements as the struts list.");
			return;
		}
		int sides = 6;
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		ExoMesh exoMesh = new ExoMesh(list);
		for (int i = 0; i < exoMesh.Sleeves.Count; i++)
		{
			exoMesh.Sleeves[i].StartRadius = list2[i];
			exoMesh.Sleeves[i].EndRadius = list3[i];
		}
		for (int j = 0; j < exoMesh.Hulls.Count; j++)
		{
			if (exoMesh.Hulls[j].SleeveIndices.Count >= 2)
			{
				exoMesh.ComputeOffsets(j, modelAbsoluteTolerance);
				exoMesh.FixSharpNodes(j, sides);
			}
		}
		for (int k = 0; k < exoMesh.Sleeves.Count; k++)
		{
			Mesh val = exoMesh.MakeSleeve(k, sides);
			exoMesh.Mesh.Append(val);
		}
		for (int l = 0; l < exoMesh.Hulls.Count; l++)
		{
			_ = exoMesh.Hulls[l];
			int count = exoMesh.Hulls[l].PlateIndices.Count;
			if (count < 2)
			{
				Mesh val2 = exoMesh.MakeEndFace(l, sides);
				exoMesh.Mesh.Append(val2);
			}
			else
			{
				Mesh val3 = exoMesh.MakeConvexHull(l, sides, modelAbsoluteTolerance, cleanPlates: true);
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
