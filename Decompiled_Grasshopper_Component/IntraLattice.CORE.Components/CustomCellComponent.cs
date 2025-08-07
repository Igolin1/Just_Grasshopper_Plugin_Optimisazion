using System;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.CORE.Data;
using IntraLattice.CORE.Data.GH_Goo;
using IntraLattice.Properties;
using Rhino.Geometry;

namespace IntraLattice.CORE.Components;

public class CustomCellComponent : GH_Component
{
	public override GH_Exposure Exposure => (GH_Exposure)4;

	protected override Bitmap Icon => Resources.customCell;

	public override Guid ComponentGuid => new Guid("{93998286-27d4-40a3-8f0e-043de932b931}");

	public CustomCellComponent()
		: base("Custom Cell", "CustomCell", "Pre-processes a custom unit cell by check validity and outputting topology.", "IntraLattice", "Cell")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddCurveParameter("Custom Cell", "L", "Unit cell lines (curves must be linear).", (GH_ParamAccess)1);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddGenericParameter("Topology", "Topo", "Verified unit cell topology", (GH_ParamAccess)0);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		List<Curve> list = new List<Curve>();
		if (!DA.GetDataList<Curve>(0, list))
		{
			return;
		}
		List<Line> list2 = new List<Line>();
		foreach (Curve item in list)
		{
			if (!item.IsLinear())
			{
				((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)20, "All struts must be linear.");
				return;
			}
			list2.Add(new Line(item.PointAtStart, item.PointAtEnd));
		}
		UnitCell unitCell = new UnitCell(list2);
		switch (unitCell.CheckValidity())
		{
		case -1:
			((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)20, "Invalid cell - opposing faces must be identical.");
			return;
		case 0:
			((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)20, "Invalid cell - each face needs at least one node lying on it.");
			return;
		case 1:
			((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)0, "Your cell is valid!");
			break;
		}
		DA.SetData(0, (object)new UnitCellGoo(unitCell));
	}
}
