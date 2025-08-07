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

public class ConformSSComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)4;

	protected override Bitmap Icon => Resources.conformSS;

	public override Guid ComponentGuid => new Guid("{ac0814b4-00e7-4efb-add5-e845a831c6da}");

	public ConformSSComponent()
		: base("Conform Surface-Surface", "ConformSS", "Generates a conforming lattice between two surfaces.", "IntraLattice", "Frame")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddGenericParameter("Topology", "Topo", "Unit cell topology", (GH_ParamAccess)0);
		pManager.AddSurfaceParameter("Surface 1", "S1", "First bounding surface", (GH_ParamAccess)0);
		pManager.AddSurfaceParameter("Surface 2", "S2", "Second bounding surface", (GH_ParamAccess)0);
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
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Expected O, but got Unknown
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Expected O, but got Unknown
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		UnitCell unitCell = new UnitCell();
		Surface val = null;
		Surface val2 = null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		bool flag = false;
		if (!DA.GetData<UnitCell>(0, ref unitCell) || !DA.GetData<Surface>(1, ref val) || !DA.GetData<Surface>(2, ref val2) || !DA.GetData<int>(3, ref num) || !DA.GetData<int>(4, ref num2) || !DA.GetData<int>(5, ref num3) || !DA.GetData<bool>(6, ref flag) || !unitCell.isValid || !((CommonObject)val).IsValid || !((CommonObject)val2).IsValid || num == 0 || num2 == 0 || num3 == 0)
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
		val2.SetDomain(0, val4);
		val2.SetDomain(1, val4);
		unitCell = unitCell.Duplicate();
		unitCell.FormatTopology();
		Point3d val9 = default(Point3d);
		Vector3d[] array3 = default(Vector3d[]);
		Point3d val10 = default(Point3d);
		Vector3d[] array4 = default(Vector3d[]);
		Interval val13 = default(Interval);
		Interval val14 = default(Interval);
		for (int i = 0; (float)i <= array[0]; i++)
		{
			for (int j = 0; (float)j <= array[1]; j++)
			{
				for (int k = 0; (float)k <= array[2]; k++)
				{
					GH_Path val5 = new GH_Path(new int[3] { i, j, k });
					List<LatticeNode> list = lattice.Nodes.EnsurePath(val5);
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
							list.Add(null);
							continue;
						}
						val.Evaluate(array2[0] / (double)array[0], array2[1] / (double)array[1], 2, ref val9, ref array3);
						val2.Evaluate(array2[0] / (double)array[0], array2[1] / (double)array[1], 2, ref val10, ref array4);
						Vector3d val11 = val10 - val9;
						LatticeNode item = new LatticeNode(val9 + val11 * array2[2] / (double)array[2]);
						list.Add(item);
					}
				}
				if (flag && (float)i < array[0] && (float)j < array[1])
				{
					GH_Path val12 = new GH_Path(new int[2] { i, j });
					((Interval)(ref val13))._002Ector((double)((float)i / array[0]), (double)((float)(i + 1) / array[0]));
					((Interval)(ref val14))._002Ector((double)((float)j / array[1]), (double)((float)(j + 1) / array[1]));
					Surface val15 = val.Trim(val13, val14);
					Surface val16 = val2.Trim(val13, val14);
					val15.SetDomain(0, val4);
					val15.SetDomain(1, val4);
					val16.SetDomain(0, val4);
					val16.SetDomain(1, val4);
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
