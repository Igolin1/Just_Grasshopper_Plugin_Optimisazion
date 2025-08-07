using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using IntraLattice.CORE.Data;
using IntraLattice.Properties;
using Rhino.Collections;
using Rhino.Geometry;

namespace IntraLattice.CORE.Components;

public class BasicBoxComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)2;

	protected override Bitmap Icon => Resources.basicBox;

	public override Guid ComponentGuid => new Guid("{3d9572a6-0783-4885-9b11-df464cf549a7}");

	public BasicBoxComponent()
		: base("Basic Box", "BasicBox", "Generates a lattice box.", "IntraLattice", "Frame")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddGenericParameter("Topology", "Topo", "Unit cell topology", (GH_ParamAccess)0);
		pManager.AddNumberParameter("Cell Size ( x )", "CSx", "Size of unit cell (x)", (GH_ParamAccess)0, 5.0);
		pManager.AddNumberParameter("Cell Size ( y )", "CSy", "Size of unit cell (y)", (GH_ParamAccess)0, 5.0);
		pManager.AddNumberParameter("Cell Size ( z )", "CSz", "Size of unit cell (z)", (GH_ParamAccess)0, 5.0);
		pManager.AddIntegerParameter("Number of Cells ( x )", "Nx", "Number of unit cells (x)", (GH_ParamAccess)0, 5);
		pManager.AddIntegerParameter("Number of Cells ( y )", "Ny", "Number of unit cells (y)", (GH_ParamAccess)0, 5);
		pManager.AddIntegerParameter("Number of Cells ( z )", "Nz", "Number of unit cells (z)", (GH_ParamAccess)0, 5);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Strut curve network", (GH_ParamAccess)1);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Expected O, but got Unknown
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		UnitCell unitCell = new UnitCell();
		double num = 0.0;
		double num2 = 0.0;
		double num3 = 0.0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (!DA.GetData<UnitCell>(0, ref unitCell) || !DA.GetData<double>(1, ref num) || !DA.GetData<double>(2, ref num2) || !DA.GetData<double>(3, ref num3) || !DA.GetData<int>(4, ref num4) || !DA.GetData<int>(5, ref num5) || !DA.GetData<int>(6, ref num6) || !unitCell.isValid || num == 0.0 || num2 == 0.0 || num3 == 0.0 || num4 == 0 || num5 == 0 || num6 == 0)
		{
			return;
		}
		Lattice lattice = new Lattice();
		unitCell = unitCell.Duplicate();
		unitCell.FormatTopology();
		Plane worldXY = Plane.WorldXY;
		Vector3d val = num * ((Plane)(ref worldXY)).XAxis;
		Vector3d val2 = num2 * ((Plane)(ref worldXY)).YAxis;
		Vector3d val3 = num3 * ((Plane)(ref worldXY)).ZAxis;
		float[] array = new float[3] { num4, num5, num6 };
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
						bool flag = unitCell.NodePaths[l][0] > 0 || unitCell.NodePaths[l][1] > 0 || unitCell.NodePaths[l][2] > 0;
						bool flag2 = array2[0] > (double)array[0] || array2[1] > (double)array[1] || array2[2] > (double)array[2];
						if (flag || flag2)
						{
							list.Add(null);
							continue;
						}
						Vector3d val8 = array2[0] * val + array2[1] * val2 + array2[2] * val3;
						LatticeNode item = new LatticeNode(((Plane)(ref worldXY)).Origin + val8);
						list.Add(item);
					}
				}
			}
		}
		lattice.ConformMapping(unitCell, array);
		DA.SetDataList(0, (IEnumerable)lattice.Struts);
	}
}
