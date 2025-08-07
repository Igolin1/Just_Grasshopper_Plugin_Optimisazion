using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.CORE.Data;
using IntraLattice.CORE.Data.GH_Goo;
using IntraLattice.CORE.Helpers;
using IntraLattice.Properties;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry;

namespace IntraLattice.CORE.Components;

public class PresetCellComponent : GH_Component
{
	private GH_Document GrasshopperDocument;

	private IGH_Component Component;

	public override GH_Exposure Exposure => (GH_Exposure)2;

	protected override Bitmap Icon => Resources.presetCell;

	public override Guid ComponentGuid => new Guid("{508cc705-bc5b-42a9-8100-c1e364f3b83d}");

	public PresetCellComponent()
		: base("Preset Cell", "PresetCell", "Built-in selection of unit cell topologies.", "IntraLattice", "Cell")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddIntegerParameter("Cell Tye", "Type", "Unit cell topology type", (GH_ParamAccess)0, 0);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddGenericParameter("Topology", "Topo", "Unit cell topology", (GH_ParamAccess)0);
		pManager.AddLineParameter("Lines", "L", "Optional output so you can modify the unit cell lines. Pass through the CustomCell component when you're done.", (GH_ParamAccess)1);
		pManager.HideParameter(1);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		Component = (IGH_Component)(object)this;
		GrasshopperDocument = ((GH_DocumentObject)this).OnPingDocument();
		if (Component.Params.Input[0].SourceCount == 0)
		{
			InputTools.TopoSelect(ref Component, ref GrasshopperDocument, 0, 11f);
		}
		int num = 0;
		if (!DA.GetData<int>(0, ref num))
		{
			return;
		}
		List<Line> rawCell = new List<Line>();
		double d = 5.0;
		switch (num)
		{
		case 0:
			rawCell = GridLines(d);
			break;
		case 1:
			rawCell = XLines(d);
			break;
		case 2:
			rawCell = StarLines(d);
			break;
		case 3:
			rawCell = CrossLines(d);
			break;
		case 4:
			rawCell = TesseractLines(d);
			break;
		case 5:
			rawCell = VintileLines(d);
			break;
		case 6:
			rawCell = OctetLines(d);
			break;
		case 7:
			rawCell = DiamondLines(d);
			break;
		case 8:
			rawCell = Honeycomb(d);
			break;
		case 9:
			rawCell = AuxeticHoneycomb(d);
			break;
		}
		UnitCell unitCell = new UnitCell(rawCell);
		if (!unitCell.isValid)
		{
			((GH_ActiveObject)this).AddRuntimeMessage((GH_RuntimeMessageLevel)20, "Invalid cell - this is embarassing.");
		}
		List<Line> list = new List<Line>();
		foreach (IndexPair nodePair in unitCell.NodePairs)
		{
			IndexPair current = nodePair;
			list.Add(new Line(((RhinoList<Point3d>)(object)unitCell.Nodes)[((IndexPair)(ref current)).I], ((RhinoList<Point3d>)(object)unitCell.Nodes)[((IndexPair)(ref current)).J]));
		}
		DA.SetData(0, (object)new UnitCellGoo(unitCell));
		DA.SetDataList(1, (IEnumerable)list);
	}

	private List<Line> GridLines(double d)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> nodes = new List<Point3d>();
		CellTools.MakeCornerNodes(ref nodes, d);
		int[] array = new int[3] { 1, 3, 4 };
		foreach (int index in array)
		{
			list.Add(new Line(nodes[0], nodes[index]));
		}
		int[] array2 = new int[3] { 1, 3, 6 };
		foreach (int index2 in array2)
		{
			list.Add(new Line(nodes[2], nodes[index2]));
		}
		int[] array3 = new int[3] { 1, 4, 6 };
		foreach (int index3 in array3)
		{
			list.Add(new Line(nodes[5], nodes[index3]));
		}
		int[] array4 = new int[3] { 3, 4, 6 };
		foreach (int index4 in array4)
		{
			list.Add(new Line(nodes[7], nodes[index4]));
		}
		return list;
	}

	private List<Line> XLines(double d)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> nodes = new List<Point3d>();
		CellTools.MakeCornerNodes(ref nodes, d);
		list.Add(new Line(nodes[0], nodes[6]));
		list.Add(new Line(nodes[1], nodes[7]));
		list.Add(new Line(nodes[3], nodes[5]));
		list.Add(new Line(nodes[2], nodes[4]));
		return list;
	}

	private List<Line> StarLines(double d)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> nodes = new List<Point3d>();
		CellTools.MakeCornerNodes(ref nodes, d);
		list.Add(new Line(nodes[0], nodes[6]));
		list.Add(new Line(nodes[1], nodes[7]));
		list.Add(new Line(nodes[3], nodes[5]));
		list.Add(new Line(nodes[2], nodes[4]));
		int[] array = new int[3] { 1, 3, 4 };
		foreach (int index in array)
		{
			list.Add(new Line(nodes[0], nodes[index]));
		}
		int[] array2 = new int[3] { 1, 3, 6 };
		foreach (int index2 in array2)
		{
			list.Add(new Line(nodes[2], nodes[index2]));
		}
		int[] array3 = new int[3] { 1, 4, 6 };
		foreach (int index3 in array3)
		{
			list.Add(new Line(nodes[5], nodes[index3]));
		}
		int[] array4 = new int[3] { 3, 4, 6 };
		foreach (int index4 in array4)
		{
			list.Add(new Line(nodes[7], nodes[index4]));
		}
		return list;
	}

	private List<Line> DiamondLines(double d)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> list2 = new List<Point3d>();
		list2.Add(new Point3d(0.0, 0.0, 0.0));
		list2.Add(new Point3d(0.0, d / 2.0, d / 2.0));
		list2.Add(new Point3d(d / 2.0, 0.0, d / 2.0));
		list2.Add(new Point3d(d / 2.0, d / 2.0, 0.0));
		list2.Add(new Point3d(d / 4.0, d / 4.0, d / 4.0));
		list.Add(new Line(list2[4], list2[0]));
		list.Add(new Line(list2[4], list2[1]));
		list.Add(new Line(list2[4], list2[2]));
		list.Add(new Line(list2[4], list2[3]));
		List<Line> list3 = new List<Line>(list);
		Line item = default(Line);
		foreach (Line item4 in list)
		{
			Line current = item4;
			((Line)(ref item))._002Ector(((Line)(ref current)).From, ((Line)(ref current)).To);
			((Line)(ref item)).Transform(Transform.Translation(d / 2.0, d / 2.0, 0.0));
			list3.Add(item);
		}
		Line item2 = default(Line);
		foreach (Line item5 in list)
		{
			Line current2 = item5;
			((Line)(ref item2))._002Ector(((Line)(ref current2)).From, ((Line)(ref current2)).To);
			((Line)(ref item2)).Transform(Transform.Rotation(Math.PI / 2.0, list2[4]));
			((Line)(ref item2)).Transform(Transform.Translation(d / 2.0, d / 2.0, d / 2.0));
			list3.Add(item2);
		}
		Line item3 = default(Line);
		foreach (Line item6 in list)
		{
			Line current3 = item6;
			((Line)(ref item3))._002Ector(((Line)(ref current3)).From, ((Line)(ref current3)).To);
			((Line)(ref item3)).Transform(Transform.Rotation(Math.PI / 2.0, list2[4]));
			((Line)(ref item3)).Transform(Transform.Translation(0.0, 0.0, d / 2.0));
			list3.Add(item3);
		}
		list.AddRange(list3);
		return list;
	}

	private List<Line> CrossLines(double d)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> nodes = new List<Point3d>();
		CellTools.MakeCornerNodes(ref nodes, d);
		int[] array = new int[2] { 5, 7 };
		foreach (int index in array)
		{
			list.Add(new Line(nodes[0], nodes[index]));
			list.Add(new Line(nodes[2], nodes[index]));
		}
		int[] array2 = new int[2] { 4, 6 };
		foreach (int index2 in array2)
		{
			list.Add(new Line(nodes[1], nodes[index2]));
			list.Add(new Line(nodes[3], nodes[index2]));
		}
		int[] array3 = new int[4] { 0, 1, 4, 5 };
		foreach (int num in array3)
		{
			list.Add(new Line(nodes[num], nodes[num + 2]));
		}
		return list;
	}

	private List<Line> TesseractLines(double d)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> nodes = new List<Point3d>();
		CellTools.MakeCornerNodes(ref nodes, d);
		nodes.Add(new Point3d(d / 4.0, d / 4.0, d / 4.0));
		nodes.Add(new Point3d(3.0 * d / 4.0, d / 4.0, d / 4.0));
		nodes.Add(new Point3d(3.0 * d / 4.0, 3.0 * d / 4.0, d / 4.0));
		nodes.Add(new Point3d(d / 4.0, 3.0 * d / 4.0, d / 4.0));
		nodes.Add(new Point3d(d / 4.0, d / 4.0, 3.0 * d / 4.0));
		nodes.Add(new Point3d(3.0 * d / 4.0, d / 4.0, 3.0 * d / 4.0));
		nodes.Add(new Point3d(3.0 * d / 4.0, 3.0 * d / 4.0, 3.0 * d / 4.0));
		nodes.Add(new Point3d(d / 4.0, 3.0 * d / 4.0, 3.0 * d / 4.0));
		int[] array = new int[3] { 1, 3, 4 };
		foreach (int num in array)
		{
			list.Add(new Line(nodes[0], nodes[num]));
			list.Add(new Line(nodes[8], nodes[num + 8]));
		}
		int[] array2 = new int[3] { 1, 3, 6 };
		foreach (int num2 in array2)
		{
			list.Add(new Line(nodes[2], nodes[num2]));
			list.Add(new Line(nodes[10], nodes[num2 + 8]));
		}
		int[] array3 = new int[3] { 1, 4, 6 };
		foreach (int num3 in array3)
		{
			list.Add(new Line(nodes[5], nodes[num3]));
			list.Add(new Line(nodes[13], nodes[num3 + 8]));
		}
		int[] array4 = new int[3] { 3, 4, 6 };
		foreach (int num4 in array4)
		{
			list.Add(new Line(nodes[7], nodes[num4]));
			list.Add(new Line(nodes[15], nodes[num4 + 8]));
		}
		for (int m = 0; m < 8; m++)
		{
			list.Add(new Line(nodes[m], nodes[m + 8]));
		}
		return list;
	}

	private List<Line> VintileLines(double d)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> list2 = new List<Point3d>();
		double[] array = new double[2] { 0.0, d };
		foreach (double num in array)
		{
			list2.Add(new Point3d(0.0, d / 4.0, num));
			list2.Add(new Point3d(0.0, 3.0 * d / 4.0, num));
			list2.Add(new Point3d(d / 4.0, d, num));
			list2.Add(new Point3d(3.0 * d / 4.0, d, num));
			list2.Add(new Point3d(d, 3.0 * d / 4.0, num));
			list2.Add(new Point3d(d, d / 4.0, num));
			list2.Add(new Point3d(3.0 * d / 4.0, 0.0, num));
			list2.Add(new Point3d(d / 4.0, 0.0, num));
		}
		double[] array2 = new double[2]
		{
			d / 4.0,
			3.0 * d / 4.0
		};
		foreach (double num2 in array2)
		{
			list2.Add(new Point3d(0.0, d / 2.0, num2));
			list2.Add(new Point3d(d / 2.0, d, num2));
			list2.Add(new Point3d(d, d / 2.0, num2));
			list2.Add(new Point3d(d / 2.0, 0.0, num2));
		}
		double[] array3 = new double[2]
		{
			d / 4.0,
			3.0 * d / 4.0
		};
		foreach (double num3 in array3)
		{
			list2.Add(new Point3d(d / 2.0, num3, d / 2.0));
		}
		double[] array4 = new double[2]
		{
			d / 4.0,
			3.0 * d / 4.0
		};
		foreach (double num4 in array4)
		{
			list2.Add(new Point3d(num4, d / 2.0, d / 2.0));
		}
		int[] array5 = new int[3] { 0, 1, 26 };
		foreach (int index in array5)
		{
			list.Add(new Line(list2[16], list2[index]));
		}
		int[] array6 = new int[3] { 2, 3, 25 };
		foreach (int index2 in array6)
		{
			list.Add(new Line(list2[17], list2[index2]));
		}
		int[] array7 = new int[3] { 4, 5, 27 };
		foreach (int index3 in array7)
		{
			list.Add(new Line(list2[18], list2[index3]));
		}
		int[] array8 = new int[3] { 6, 7, 24 };
		foreach (int index4 in array8)
		{
			list.Add(new Line(list2[19], list2[index4]));
		}
		int[] array9 = new int[3] { 8, 9, 26 };
		foreach (int index5 in array9)
		{
			list.Add(new Line(list2[20], list2[index5]));
		}
		int[] array10 = new int[3] { 10, 11, 25 };
		foreach (int index6 in array10)
		{
			list.Add(new Line(list2[21], list2[index6]));
		}
		int[] array11 = new int[3] { 12, 13, 27 };
		foreach (int index7 in array11)
		{
			list.Add(new Line(list2[22], list2[index7]));
		}
		int[] array12 = new int[3] { 14, 15, 24 };
		foreach (int index8 in array12)
		{
			list.Add(new Line(list2[23], list2[index8]));
		}
		int[] array13 = new int[6] { 1, 3, 5, 9, 11, 13 };
		foreach (int num12 in array13)
		{
			list.Add(new Line(list2[num12], list2[num12 + 1]));
		}
		int[] array14 = new int[2] { 24, 25 };
		foreach (int index9 in array14)
		{
			list.Add(new Line(list2[26], list2[index9]));
			list.Add(new Line(list2[27], list2[index9]));
		}
		list.Add(new Line(list2[0], list2[7]));
		list.Add(new Line(list2[8], list2[15]));
		return list;
	}

	private List<Line> OctetLines(double d)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> nodes = new List<Point3d>();
		CellTools.MakeCornerNodes(ref nodes, d);
		nodes.Add(new Point3d(d, d / 2.0, d / 2.0));
		nodes.Add(new Point3d(d / 2.0, d, d / 2.0));
		nodes.Add(new Point3d(0.0, d / 2.0, d / 2.0));
		nodes.Add(new Point3d(d / 2.0, 0.0, d / 2.0));
		nodes.Add(new Point3d(d / 2.0, d / 2.0, 0.0));
		nodes.Add(new Point3d(d / 2.0, d / 2.0, d));
		int[] array = new int[8] { 0, 1, 2, 3, 8, 9, 10, 11 };
		foreach (int index in array)
		{
			list.Add(new Line(nodes[12], nodes[index]));
		}
		int[] array2 = new int[8] { 4, 5, 6, 7, 8, 9, 10, 11 };
		foreach (int index2 in array2)
		{
			list.Add(new Line(nodes[13], nodes[index2]));
		}
		int[] array3 = new int[4] { 0, 3, 4, 7 };
		foreach (int index3 in array3)
		{
			list.Add(new Line(nodes[10], nodes[index3]));
		}
		int[] array4 = new int[6] { 2, 3, 6, 7, 8, 10 };
		foreach (int index4 in array4)
		{
			list.Add(new Line(nodes[9], nodes[index4]));
		}
		int[] array5 = new int[4] { 1, 2, 5, 6 };
		foreach (int index5 in array5)
		{
			list.Add(new Line(nodes[8], nodes[index5]));
		}
		int[] array6 = new int[6] { 0, 1, 4, 5, 8, 10 };
		foreach (int index6 in array6)
		{
			list.Add(new Line(nodes[11], nodes[index6]));
		}
		return list;
	}

	private List<Line> Honeycomb(double d)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> list2 = new List<Point3d>();
		for (int i = 0; i < 2; i++)
		{
			double num = 3.0 * d * (double)i;
			list2.Add(new Point3d(2.25 * d, num, 2.0 * d));
			list2.Add(new Point3d(2.25 * d, num, d));
			list2.Add(new Point3d(0.75 * d, num, 2.0 * d));
			list2.Add(new Point3d(0.75 * d, num, d));
			list2.Add(new Point3d(0.0, num, 0.0));
			list2.Add(new Point3d(0.0, num, 0.5 * d));
			list2.Add(new Point3d(0.0, num, 2.5 * d));
			list2.Add(new Point3d(0.0, num, 3.0 * d));
			list2.Add(new Point3d(1.5 * d, num, 0.0));
			list2.Add(new Point3d(1.5 * d, num, 0.5 * d));
			list2.Add(new Point3d(1.5 * d, num, 2.5 * d));
			list2.Add(new Point3d(1.5 * d, num, 3.0 * d));
			list2.Add(new Point3d(3.0 * d, num, 0.0));
			list2.Add(new Point3d(3.0 * d, num, 0.5 * d));
			list2.Add(new Point3d(3.0 * d, num, 2.5 * d));
			list2.Add(new Point3d(3.0 * d, num, 3.0 * d));
		}
		for (int j = 0; j < 2; j++)
		{
			int num2 = j * 16;
			int[] array = new int[3] { 2, 5, 9 };
			foreach (int num3 in array)
			{
				list.Add(new Line(list2[3 + num2], list2[num3 + num2]));
			}
			int[] array2 = new int[3] { 0, 9, 13 };
			foreach (int num4 in array2)
			{
				list.Add(new Line(list2[1 + num2], list2[num4 + num2]));
			}
			int[] array3 = new int[3] { 0, 2, 11 };
			foreach (int num5 in array3)
			{
				list.Add(new Line(list2[10 + num2], list2[num5 + num2]));
			}
			list.Add(new Line(list2[6 + num2], list2[7 + num2]));
			list.Add(new Line(list2[14 + num2], list2[15 + num2]));
			list.Add(new Line(list2[4 + num2], list2[5 + num2]));
			list.Add(new Line(list2[8 + num2], list2[9 + num2]));
			list.Add(new Line(list2[13 + num2], list2[12 + num2]));
			list.Add(new Line(list2[num2], list2[14 + num2]));
			list.Add(new Line(list2[2 + num2], list2[6 + num2]));
		}
		for (int n = 0; n < 16; n++)
		{
			list.Add(new Line(list2[n], list2[n + 16]));
		}
		return list;
	}

	private List<Line> AuxeticHoneycomb(double d)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		List<Line> list = new List<Line>();
		List<Point3d> list2 = new List<Point3d>();
		for (int i = 0; i < 2; i++)
		{
			double num = 3.0 * d * (double)i;
			list2.Add(new Point3d(2.25 * d, num, 2.5 * d));
			list2.Add(new Point3d(2.25 * d, num, 0.5 * d));
			list2.Add(new Point3d(0.75 * d, num, 2.5 * d));
			list2.Add(new Point3d(0.75 * d, num, 0.5 * d));
			list2.Add(new Point3d(0.0, num, 0.0));
			list2.Add(new Point3d(0.0, num, d));
			list2.Add(new Point3d(0.0, num, 2.0 * d));
			list2.Add(new Point3d(0.0, num, 3.0 * d));
			list2.Add(new Point3d(1.5 * d, num, 0.0));
			list2.Add(new Point3d(1.5 * d, num, d));
			list2.Add(new Point3d(1.5 * d, num, 2.0 * d));
			list2.Add(new Point3d(1.5 * d, num, 3.0 * d));
			list2.Add(new Point3d(3.0 * d, num, 0.0));
			list2.Add(new Point3d(3.0 * d, num, d));
			list2.Add(new Point3d(3.0 * d, num, 2.0 * d));
			list2.Add(new Point3d(3.0 * d, num, 3.0 * d));
		}
		for (int j = 0; j < 2; j++)
		{
			int num2 = j * 16;
			int[] array = new int[3] { 2, 5, 9 };
			foreach (int num3 in array)
			{
				list.Add(new Line(list2[3 + num2], list2[num3 + num2]));
			}
			int[] array2 = new int[3] { 0, 9, 13 };
			foreach (int num4 in array2)
			{
				list.Add(new Line(list2[1 + num2], list2[num4 + num2]));
			}
			int[] array3 = new int[3] { 0, 2, 11 };
			foreach (int num5 in array3)
			{
				list.Add(new Line(list2[10 + num2], list2[num5 + num2]));
			}
			list.Add(new Line(list2[6 + num2], list2[7 + num2]));
			list.Add(new Line(list2[14 + num2], list2[15 + num2]));
			list.Add(new Line(list2[4 + num2], list2[5 + num2]));
			list.Add(new Line(list2[8 + num2], list2[9 + num2]));
			list.Add(new Line(list2[13 + num2], list2[12 + num2]));
			list.Add(new Line(list2[num2], list2[14 + num2]));
			list.Add(new Line(list2[2 + num2], list2[6 + num2]));
		}
		for (int n = 0; n < 16; n++)
		{
			list.Add(new Line(list2[n], list2[n + 16]));
		}
		return list;
	}
}
