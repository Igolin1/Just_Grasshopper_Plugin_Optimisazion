using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using IntraLattice.CORE.Data;
using IntraLattice.Properties;
using Rhino.Collections;
using Rhino.Geometry;

namespace IntraLattice.CORE.Components;

public class BasicCylinderComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)2;

	protected override Bitmap Icon => Resources.basicCylinder;

	public override Guid ComponentGuid => new Guid("{9f6769c0-dec5-4a0d-8ade-76fca1dfd4e3}");

	public BasicCylinderComponent()
		: base("Basic Cylinder", "BasicCylinder", "Generates a conformal lattice cylinder.", "IntraLattice", "Frame")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddGenericParameter("Topology", "Topo", "Unit cell topology", (GH_ParamAccess)0);
		pManager.AddNumberParameter("Radius", "R", "Radius of cylinder", (GH_ParamAccess)0, 15.0);
		pManager.AddNumberParameter("Height", "H", "Height of cylinder", (GH_ParamAccess)0, 25.0);
		pManager.AddIntegerParameter("Number u", "Nu", "Number of unit cells (axial)", (GH_ParamAccess)0, 5);
		pManager.AddIntegerParameter("Number v", "Nv", "Number of unit cells (theta)", (GH_ParamAccess)0, 15);
		pManager.AddIntegerParameter("Number w", "Nw", "Number of unit cells (radial)", (GH_ParamAccess)0, 4);
		pManager.AddBooleanParameter("Morph", "Morph", "If true, struts are morphed to the space as curves.", (GH_ParamAccess)0, false);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Strut curve network", (GH_ParamAccess)1);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Expected O, but got Unknown
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Expected O, but got Unknown
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		UnitCell unitCell = new UnitCell();
		double num = 0.0;
		double num2 = 0.0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		bool flag = false;
		if (!DA.GetData<UnitCell>(0, ref unitCell) || !DA.GetData<double>(1, ref num) || !DA.GetData<double>(2, ref num2) || !DA.GetData<int>(3, ref num3) || !DA.GetData<int>(4, ref num4) || !DA.GetData<int>(5, ref num5) || !DA.GetData<bool>(6, ref flag) || !unitCell.isValid || num == 0.0 || num2 == 0.0 || num3 == 0 || num4 == 0 || num5 == 0)
		{
			return;
		}
		Lattice lattice = new Lattice();
		DataTree<GeometryBase> val = new DataTree<GeometryBase>();
		Plane worldXY = Plane.WorldXY;
		Cylinder val2 = new Cylinder(new Circle(worldXY, num), num2);
		Surface val3 = (Surface)(object)((Cylinder)(ref val2)).ToNurbsSurface();
		val3 = val3.Transpose();
		LineCurve val4 = new LineCurve(((Plane)(ref worldXY)).Origin, ((Plane)(ref worldXY)).Origin + num2 * ((Plane)(ref worldXY)).ZAxis);
		float[] array = new float[3] { num3, num4, num5 };
		Interval val5 = default(Interval);
		((Interval)(ref val5))._002Ector(0.0, 1.0);
		val3.SetDomain(0, val5);
		val3.SetDomain(1, val5);
		((Curve)val4).Domain = val5;
		unitCell = unitCell.Duplicate();
		unitCell.FormatTopology();
		Point3d val12 = default(Point3d);
		Vector3d[] array3 = default(Vector3d[]);
		Interval val15 = default(Interval);
		Interval val16 = default(Interval);
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
						bool flag2 = unitCell.NodePaths[l][0] > 0 || unitCell.NodePaths[l][1] > 0 || unitCell.NodePaths[l][2] > 0;
						bool flag3 = array2[0] > (double)array[0] || array2[1] > (double)array[1] || array2[2] > (double)array[2];
						if (flag2 || flag3)
						{
							list.Add(null);
							continue;
						}
						Vector3d val10 = num2 * ((Plane)(ref worldXY)).ZAxis * array2[0] / (double)array[0];
						Point3d val11 = ((Plane)(ref worldXY)).Origin + val10;
						val3.Evaluate(array2[0] / (double)array[0], array2[1] / (double)array[1], 2, ref val12, ref array3);
						Vector3d val13 = val12 - val11;
						LatticeNode item = new LatticeNode(val11 + val13 * array2[2] / (double)array[2]);
						list.Add(item);
					}
				}
				if (flag && (float)i < array[0] && (float)j < array[1])
				{
					GH_Path val14 = new GH_Path(new int[2] { i, j });
					((Interval)(ref val15))._002Ector((double)((float)i / array[0]), (double)((float)(i + 1) / array[0]));
					((Interval)(ref val16))._002Ector((double)((float)j / array[1]), (double)((float)(j + 1) / array[1]));
					Surface val17 = val3.Trim(val15, val16);
					Curve val18 = ((Curve)val4).Trim(val15);
					val17.SetDomain(0, val5);
					val17.SetDomain(1, val5);
					val18.Domain = val5;
					val.Add((GeometryBase)(object)val17, val14);
					val.Add((GeometryBase)(object)val18, val14);
				}
			}
		}
		if (flag)
		{
			lattice.MorphMapping(unitCell, val, array);
		}
		else
		{
			lattice.ConformMapping(unitCell, array);
		}
		DA.SetDataList(0, (IEnumerable)lattice.Struts);
	}
}
