using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using IntraLattice.CORE.Data;
using IntraLattice.CORE.Helpers;
using IntraLattice.Properties;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry;
using Rhino.Runtime;

namespace IntraLattice.CORE.Components;

public class UniformDSComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)8;

	protected override Bitmap Icon => Resources.uniformDS;

	public override Guid ComponentGuid => new Guid("{d242b0c6-83a1-4795-8f8c-a32b1ac85fb3}");

	public UniformDSComponent()
		: base("Uniform DS", "UniformDS", "Generates a uniform lattice within by a design space", "IntraLattice", "Frame")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		pManager.AddGenericParameter("Topology", "Topo", "Unit cell topology", (GH_ParamAccess)0);
		pManager.AddGeometryParameter("Design Space", "DS", "Design Space (Brep or Mesh)", (GH_ParamAccess)0);
		pManager.AddPlaneParameter("Orientation Plane", "Plane", "Lattice orientation plane", (GH_ParamAccess)0, Plane.WorldXY);
		pManager.AddNumberParameter("Cell Size ( x )", "CSx", "Size of unit cell (x)", (GH_ParamAccess)0, 5.0);
		pManager.AddNumberParameter("Cell Size ( y )", "CSy", "Size of unit cell (y)", (GH_ParamAccess)0, 5.0);
		pManager.AddNumberParameter("Cell Size ( z )", "CSz", "Size of unit cell (z)", (GH_ParamAccess)0, 5.0);
		pManager.AddNumberParameter("Tolerance", "Tol", "Smallest allowed strut length", (GH_ParamAccess)0, 0.2);
		pManager.AddBooleanParameter("Strict tolerance", "Strict", "Specifies if we use a strict tolerance.", (GH_ParamAccess)0, false);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Strut curve network", (GH_ParamAccess)1);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Expected O, but got Unknown
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		UnitCell unitCell = new UnitCell();
		GeometryBase designSpace = null;
		Plane unset = Plane.Unset;
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		double num4 = 0.0;
		bool strictlyIn = false;
		if (!DA.GetData<UnitCell>(0, ref unitCell) || !DA.GetData<GeometryBase>(1, ref designSpace) || !DA.GetData<Plane>(2, ref unset) || !DA.GetData<double>(3, ref num) || !DA.GetData<double>(4, ref num2) || !DA.GetData<double>(5, ref num3) || !DA.GetData<double>(6, ref num4) || !DA.GetData<bool>(7, ref strictlyIn) || !unitCell.isValid || !((CommonObject)designSpace).IsValid || !((Plane)(ref unset)).IsValid || num == 0.0 || num2 == 0.0 || num3 == 0.0)
		{
			return;
		}
		if (num4 >= num || num4 >= num2 || num4 >= num3)
		{
			((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)20, "Tolerance parameter cannot be larger than the unit cell dimensions.");
			return;
		}
		int num5 = FrameTools.ValidateSpace(ref designSpace);
		if (num5 == 0)
		{
			((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)20, "Design space must be a closed Brep, Mesh or Surface");
			return;
		}
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		Box val = default(Box);
		designSpace.GetBoundingBox(unset, ref val);
		Point3d[] corners = ((Box)(ref val)).GetCorners();
		Plane val2 = default(Plane);
		((Plane)(ref val2))._002Ector(corners[0], corners[1], corners[3]);
		double num6 = ((Point3d)(ref corners[0])).DistanceTo(corners[1]);
		double num7 = ((Point3d)(ref corners[0])).DistanceTo(corners[3]);
		double num8 = ((Point3d)(ref corners[0])).DistanceTo(corners[4]);
		int num9 = (int)Math.Ceiling(num6 / num);
		int num10 = (int)Math.Ceiling(num7 / num2);
		int num11 = (int)Math.Ceiling(num8 / num3);
		float[] array = new float[3] { num9, num10, num11 };
		Lattice lattice = new Lattice();
		unitCell = unitCell.Duplicate();
		unitCell.FormatTopology();
		Vector3d val3 = num * ((Plane)(ref val2)).XAxis;
		Vector3d val4 = num2 * ((Plane)(ref val2)).YAxis;
		Vector3d val5 = num3 * ((Plane)(ref val2)).ZAxis;
		for (int i = 0; (float)i <= array[0]; i++)
		{
			for (int j = 0; (float)j <= array[1]; j++)
			{
				for (int k = 0; (float)k <= array[2]; k++)
				{
					GH_Path val6 = new GH_Path(new int[3] { i, j, k });
					List<LatticeNode> list = lattice.Nodes.EnsurePath(val6);
					for (int l = 0; l < ((RhinoList<Point3d>)(object)unitCell.Nodes).Count; l++)
					{
						Point3d val7 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double x = ((Point3d)(ref val7)).X;
						Point3d val8 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double y = ((Point3d)(ref val8)).Y;
						Point3d val9 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double z = ((Point3d)(ref val9)).Z;
						double[] array2 = new double[3]
						{
							(double)i + x,
							(double)j + y,
							(double)k + z
						};
						bool flag = unitCell.NodePaths[l][0] > 0 || unitCell.NodePaths[l][1] > 0 || unitCell.NodePaths[l][2] > 0;
						bool flag2 = array2[0] > (double)array[0] || array2[1] > (double)array[1] || array2[2] > (double)array[2];
						if (flag || flag2)
						{
							list.Add(null);
							continue;
						}
						Vector3d val10 = array2[0] * val3 + array2[1] * val4 + array2[2] * val5;
						LatticeNode latticeNode = new LatticeNode(((Plane)(ref val2)).Origin + val10);
						if (FrameTools.IsPointInside(designSpace, latticeNode.Point3d, num5, modelAbsoluteTolerance, strictlyIn))
						{
							latticeNode.State = LatticeNodeState.Inside;
						}
						else
						{
							latticeNode.State = LatticeNodeState.Outside;
						}
						list.Add(latticeNode);
					}
				}
			}
		}
		lattice.UniformMapping(unitCell, designSpace, num5, array, num4);
		DA.SetDataList(0, (IEnumerable)lattice.Struts);
	}
}
