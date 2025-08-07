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

public class ConformSAComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)4;

	protected override Bitmap Icon => Resources.conformSA;

	public override Guid ComponentGuid => new Guid("{e0e8a858-66bd-4145-b173-23dc2e247206}");

	public ConformSAComponent()
		: base("Conform Surface-Axis", "ConformSA", "Generates a conforming lattice between a surface and an axis.", "IntraLattice", "Frame")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddGenericParameter("Topology", "Topo", "Unit cell topology", (GH_ParamAccess)0);
		pManager.AddSurfaceParameter("Surface", "Surf", "Surface to conform to", (GH_ParamAccess)0);
		pManager.AddCurveParameter("Axis", "A", "Axis (may be curved)", (GH_ParamAccess)0);
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
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Expected O, but got Unknown
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Expected O, but got Unknown
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		UnitCell unitCell = new UnitCell();
		Surface val = null;
		Curve val2 = null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		bool flag = false;
		if (!DA.GetData<UnitCell>(0, ref unitCell) || !DA.GetData<Surface>(1, ref val) || !DA.GetData<Curve>(2, ref val2) || !DA.GetData<int>(3, ref num) || !DA.GetData<int>(4, ref num2) || !DA.GetData<int>(5, ref num3) || !DA.GetData<bool>(6, ref flag) || !unitCell.isValid || !((CommonObject)val).IsValid || !((CommonObject)val2).IsValid || num == 0 || num2 == 0 || num3 == 0)
		{
			return;
		}
		Lattice lattice = new Lattice();
		DataTree<GeometryBase> val3 = new DataTree<GeometryBase>();
		float[] array = new float[3] { num, num2, num3 };
		Interval val4 = default(Interval);
		((Interval)(ref val4))._002Ector(0.0, 1.0);
		val.SetDomain(0, val4);
		val.SetDomain(1, val4);
		val2.Domain = val4;
		unitCell = unitCell.Duplicate();
		unitCell.FormatTopology();
		List<double> list = new List<double>(val2.DivideByCount((int)array[0], true));
		_ = list[1];
		_ = list[0];
		if (val2.IsClosed)
		{
			list.Add(0.0);
		}
		Point3d val10 = default(Point3d);
		Vector3d[] array3 = default(Vector3d[]);
		Interval val13 = default(Interval);
		Interval val14 = default(Interval);
		for (int i = 0; (float)i <= array[0]; i++)
		{
			for (int j = 0; (float)j <= array[1]; j++)
			{
				for (int k = 0; (float)k <= array[2]; k++)
				{
					GH_Path val5 = new GH_Path(new int[3] { i, j, k });
					List<LatticeNode> list2 = lattice.Nodes.EnsurePath(val5);
					for (int l = 0; l < ((RhinoList<Point3d>)(object)unitCell.Nodes).Count; l++)
					{
						Point3d val6 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double x = ((Point3d)(ref val6)).X;
						Point3d val7 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double y = ((Point3d)(ref val7)).Y;
						Point3d val8 = ((RhinoList<Point3d>)(object)unitCell.Nodes)[l];
						double z = ((Point3d)(ref val8)).Z;
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
							list2.Add(null);
							continue;
						}
						Point3d val9 = val2.PointAt(list[i] + x / (double)array[0]);
						val.Evaluate(array2[0] / (double)array[0], array2[1] / (double)array[1], 2, ref val10, ref array3);
						Vector3d val11 = val10 - val9;
						LatticeNode item = new LatticeNode(val9 + val11 * array2[2] / (double)array[2]);
						list2.Add(item);
					}
				}
				if (flag && (float)i < array[0] && (float)j < array[1])
				{
					GH_Path val12 = new GH_Path(new int[2] { i, j });
					((Interval)(ref val13))._002Ector((double)((float)i / array[0]), (double)((float)(i + 1) / array[0]));
					((Interval)(ref val14))._002Ector((double)((float)j / array[1]), (double)((float)(j + 1) / array[1]));
					Surface val15 = val.Trim(val13, val14);
					Curve val16 = val2.Trim(val13);
					val15.SetDomain(0, val4);
					val15.SetDomain(1, val4);
					val16.Domain = val4;
					val3.Add((GeometryBase)(object)val15, val12);
					val3.Add((GeometryBase)(object)val16, val12);
				}
			}
		}
		if (flag)
		{
			lattice.MorphMapping(unitCell, val3, array);
		}
		else
		{
			lattice.ConformMapping(unitCell, array);
		}
		DA.SetDataList(0, (IEnumerable)lattice.Struts);
	}
}
