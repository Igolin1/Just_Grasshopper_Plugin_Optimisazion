using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Expressions;
using IntraLattice.CORE.Data;
using IntraLattice.Properties;
using Rhino;
using Rhino.Geometry;

namespace IntraLattice.CORE.MeshModule;

public class HeterogenGradientComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)4;

	protected override Bitmap Icon => Resources.heterogenGradient;

	public override Guid ComponentGuid => new Guid("{a5e48dd2-8467-4991-95b1-15d29524de3e}");

	public HeterogenGradientComponent()
		: base("Heterogen Gradient", "HeterogenGradient", "Heterogeneous solidification (thickness gradient) of lattice wireframe", "IntraLattice", "Mesh")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Wireframe to thicken", (GH_ParamAccess)1);
		pManager.AddTextParameter("Gradient String", "Grad", "The spatial gradient as an expression string", (GH_ParamAccess)0, "0");
		pManager.AddNumberParameter("Maximum Radius", "Rmax", "Maximum radius in gradient", (GH_ParamAccess)0, 0.5);
		pManager.AddNumberParameter("Minimum Radius", "Rmin", "Minimum radius in gradient", (GH_ParamAccess)0, 0.2);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddMeshParameter("Mesh", "Mesh", "Thickened wireframe", (GH_ParamAccess)0);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		List<Curve> list = new List<Curve>();
		string text = null;
		double num = 0.0;
		double num2 = 0.0;
		if (!DA.GetDataList<Curve>(0, list) || !DA.GetData<string>(1, ref text) || !DA.GetData<double>(2, ref num) || !DA.GetData<double>(3, ref num2) || list == null || list.Count == 0 || num <= 0.0 || num2 <= 0.0)
		{
			return;
		}
		int sides = 6;
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		ExoMesh exoMesh = new ExoMesh(list);
		BoundingBox val = default(BoundingBox);
		foreach (ExoSleeve sleefe in exoMesh.Sleeves)
		{
			BoundingBox boundingBox = ((GeometryBase)sleefe.Curve).GetBoundingBox(Plane.WorldXY);
			((BoundingBox)(ref val)).Union(boundingBox);
		}
		Point3d max = ((BoundingBox)(ref val)).Max;
		double x = ((Point3d)(ref max)).X;
		Point3d min = ((BoundingBox)(ref val)).Min;
		double num3 = x - ((Point3d)(ref min)).X;
		Point3d max2 = ((BoundingBox)(ref val)).Max;
		double y = ((Point3d)(ref max2)).Y;
		Point3d min2 = ((BoundingBox)(ref val)).Min;
		double num4 = y - ((Point3d)(ref min2)).Y;
		Point3d max3 = ((BoundingBox)(ref val)).Max;
		double z = ((Point3d)(ref max3)).Z;
		Point3d min3 = ((BoundingBox)(ref val)).Min;
		double num5 = z - ((Point3d)(ref min3)).Z;
		text = GH_ExpressionSyntaxWriter.RewriteForEvaluator(text);
		foreach (ExoSleeve sleefe2 in exoMesh.Sleeves)
		{
			List<ExoHull> hulls = exoMesh.Hulls;
			IndexPair hullPair = sleefe2.HullPair;
			ExoHull exoHull = hulls[((IndexPair)(ref hullPair)).I];
			GH_ExpressionParser val2 = new GH_ExpressionParser();
			Point3d point3d = exoHull.Point3d;
			double x2 = ((Point3d)(ref point3d)).X;
			Point3d min4 = ((BoundingBox)(ref val)).Min;
			val2.AddVariable("x", (x2 - ((Point3d)(ref min4)).X) / num3);
			Point3d point3d2 = exoHull.Point3d;
			double y2 = ((Point3d)(ref point3d2)).Y;
			Point3d min5 = ((BoundingBox)(ref val)).Min;
			val2.AddVariable("y", (y2 - ((Point3d)(ref min5)).Y) / num4);
			Point3d point3d3 = exoHull.Point3d;
			double z2 = ((Point3d)(ref point3d3)).Z;
			Point3d min6 = ((BoundingBox)(ref val)).Min;
			val2.AddVariable("z", (z2 - ((Point3d)(ref min6)).Z) / num5);
			sleefe2.StartRadius = num2 + val2.Evaluate(text)._Double * (num - num2);
			val2.ClearVariables();
			List<ExoHull> hulls2 = exoMesh.Hulls;
			IndexPair hullPair2 = sleefe2.HullPair;
			exoHull = hulls2[((IndexPair)(ref hullPair2)).J];
			Point3d point3d4 = exoHull.Point3d;
			double x3 = ((Point3d)(ref point3d4)).X;
			Point3d min7 = ((BoundingBox)(ref val)).Min;
			val2.AddVariable("x", (x3 - ((Point3d)(ref min7)).X) / num3);
			Point3d point3d5 = exoHull.Point3d;
			double y3 = ((Point3d)(ref point3d5)).Y;
			Point3d min8 = ((BoundingBox)(ref val)).Min;
			val2.AddVariable("y", (y3 - ((Point3d)(ref min8)).Y) / num4);
			Point3d point3d6 = exoHull.Point3d;
			double z3 = ((Point3d)(ref point3d6)).Z;
			Point3d min9 = ((BoundingBox)(ref val)).Min;
			val2.AddVariable("z", (z3 - ((Point3d)(ref min9)).Z) / num5);
			sleefe2.EndRadius = num2 + val2.Evaluate(text)._Double * (num - num2);
			val2.ClearVariables();
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
			Mesh val3 = exoMesh.MakeSleeve(j, sides);
			exoMesh.Mesh.Append(val3);
		}
		for (int k = 0; k < exoMesh.Hulls.Count; k++)
		{
			_ = exoMesh.Hulls[k];
			int count = exoMesh.Hulls[k].PlateIndices.Count;
			if (count < 2)
			{
				Mesh val4 = exoMesh.MakeEndFace(k, sides);
				exoMesh.Mesh.Append(val4);
			}
			else
			{
				Mesh val5 = exoMesh.MakeConvexHull(k, sides, modelAbsoluteTolerance, cleanPlates: true);
				exoMesh.Mesh.Append(val5);
			}
		}
		exoMesh.Mesh.Vertices.CombineIdentical(true, true);
		exoMesh.Mesh.FaceNormals.ComputeFaceNormals();
		exoMesh.Mesh.UnifyNormals();
		exoMesh.Mesh.Normals.ComputeNormals();
		DA.SetData(0, (object)exoMesh.Mesh);
	}
}
