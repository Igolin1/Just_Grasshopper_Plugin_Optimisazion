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
using Rhino.Runtime;

namespace IntraLattice.CORE.Components;

public class ConformSPComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)4;

	protected override Bitmap Icon => Resources.conformSP;

	public override Guid ComponentGuid => new Guid("{27cbc46a-3ef6-4f00-9a66-d6afd6b7b2fe}");

	public ConformSPComponent()
		: base("Conform Surface-Point", "ConformSP", "Generates a conforming lattice between a surface and a point.", "IntraLattice", "Frame")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddGenericParameter("Topology", "Topo", "Unit cell topology", (GH_ParamAccess)0);
		pManager.AddSurfaceParameter("Surface", "Surf", "Surface to conform to", (GH_ParamAccess)0);
		pManager.AddPointParameter("Point", "Pt", "Point", (GH_ParamAccess)0);
		pManager.AddIntegerParameter("Number u", "Nu", "Number of unit cells (u)", (GH_ParamAccess)0, 5);
		pManager.AddIntegerParameter("Number v", "Nv", "Number of unit cells (v)", (GH_ParamAccess)0, 5);
		pManager.AddIntegerParameter("Number w", "Nw", "Number of unit cells (w)", (GH_ParamAccess)0, 5);
		pManager.AddBooleanParameter("Morph", "Morph", "If true, struts are morphed to the space as curves.", (GH_ParamAccess)0, false);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Strut curve network", (GH_ParamAccess)1);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Expected O, but got Unknown
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		UnitCell unitCell = new UnitCell();
		Surface val = null;
		Point3d unset = Point3d.Unset;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		bool flag = false;
		if (!DA.GetData<UnitCell>(0, ref unitCell) || !DA.GetData<Surface>(1, ref val) || !DA.GetData<Point3d>(2, ref unset) || !DA.GetData<int>(3, ref num) || !DA.GetData<int>(4, ref num2) || !DA.GetData<int>(5, ref num3) || !DA.GetData<bool>(6, ref flag) || !unitCell.isValid || !((CommonObject)val).IsValid || !((Point3d)(ref unset)).IsValid || num == 0 || num2 == 0 || num3 == 0)
		{
			return;
		}
		Lattice lattice = new Lattice();
		DataTree<GeometryBase> val2 = new DataTree<GeometryBase>();
		float[] array = new float[3] { num, num2, num3 };
		Interval val3 = default(Interval);
		((Interval)(ref val3))._002Ector(0.0, 1.0);
		val.SetDomain(0, val3);
		val.SetDomain(1, val3);
		unitCell = unitCell.Duplicate();
		unitCell.FormatTopology();
		Point3d val9 = default(Point3d);
		Vector3d[] array3 = default(Vector3d[]);
		Interval val12 = default(Interval);
		Interval val13 = default(Interval);
		for (int i = 0; (float)i <= array[0]; i++)
		{
			for (int j = 0; (float)j <= array[1]; j++)
			{
				for (int k = 0; (float)k <= array[2]; k++)
				{
					GH_Path val4 = new GH_Path(new int[3] { i, j, k });
					List<LatticeNode> list = lattice.Nodes.EnsurePath(val4);
					for (int l = 0; l < ((RhinoList<Point3d>)(object)unitCell.Nodes).Count; l++)
					{
						Point3d val5 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double x = ((Point3d)(ref val5)).X;
						Point3d val6 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double y = ((Point3d)(ref val6)).Y;
						Point3d val7 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double z = ((Point3d)(ref val7)).Z;
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
						Point3d val8 = unset;
						val.Evaluate(array2[0] / (double)array[0], array2[1] / (double)array[1], 2, ref val9, ref array3);
						Vector3d val10 = val9 - val8;
						LatticeNode item = new LatticeNode(val8 + val10 * array2[2] / (double)array[2]);
						list.Add(item);
					}
				}
				if (flag && (float)i < array[0] && (float)j < array[1])
				{
					GH_Path val11 = new GH_Path(new int[2] { i, j });
					((Interval)(ref val12))._002Ector((double)((float)i / array[0]), (double)((float)(i + 1) / array[0]));
					((Interval)(ref val13))._002Ector((double)((float)j / array[1]), (double)((float)(j + 1) / array[1]));
					Surface val14 = val.Trim(val12, val13);
					Point val15 = new Point(unset);
					val14.SetDomain(0, val3);
					val14.SetDomain(1, val3);
					val2.Add((GeometryBase)(object)val14, val11);
					val2.Add((GeometryBase)(object)val15, val11);
				}
			}
		}
		if (flag)
		{
			lattice.MorphMapping(unitCell, val2, array);
		}
		else
		{
			lattice.ConformMapping(unitCell, array);
		}
		DA.SetDataList(0, (IEnumerable)lattice.Struts);
	}
}
