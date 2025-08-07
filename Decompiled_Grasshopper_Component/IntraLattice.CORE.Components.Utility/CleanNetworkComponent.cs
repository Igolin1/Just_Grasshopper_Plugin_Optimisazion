using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Grasshopper.Kernel;
using IntraLattice.CORE.Helpers;
using IntraLattice.Properties;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry;

namespace IntraLattice.CORE.Components.Utility;

public class CleanNetworkComponent : GH_Component
{
	protected override Bitmap Icon => Resources.cleanNetwork;

	public override Guid ComponentGuid => new Guid("{8b3a2f8c-3a76-4b19-84b9-f3eea80010ea}");

	public CleanNetworkComponent()
		: base("Clean Network", "CleanNetwork", "Removes duplicate curves from a network, within specified tolerance.", "IntraLattice", "Utils")
	{
	}

	protected override void RegisterInputParams(GH_InputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Strut network to clean.", (GH_ParamAccess)1);
		pManager.AddNumberParameter("Tolerance", "Tol", "Tolerance for combining nodes.", (GH_ParamAccess)0);
	}

	protected override void RegisterOutputParams(GH_OutputParamManager pManager)
	{
		pManager.AddCurveParameter("Struts", "Struts", "Cleaned curve network.", (GH_ParamAccess)1);
		pManager.AddPointParameter("Nodes", "Nodes", "List of unique nodes.", (GH_ParamAccess)1);
		pManager.AddIntegerParameter("CurveStart", "I", "Index in 'Nodes' for the start of each curve in 'Struts'.", (GH_ParamAccess)1);
		pManager.AddIntegerParameter("CurveEnd", "J", "Index in 'Nodes' for the end of each curve in 'Struts'.", (GH_ParamAccess)1);
	}

	protected override void SolveInstance(IGH_DataAccess DA)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		List<Curve> list = new List<Curve>();
		double num = 0.0;
		if (!DA.GetDataList<Curve>(0, list) || !DA.GetData<double>(1, ref num) || list == null || list.Count == 1 || num < 0.0)
		{
			return;
		}
		Point3dList nodes = new Point3dList();
		List<IndexPair> nodePairs = new List<IndexPair>();
		list = FrameTools.CleanNetwork(list, num, out nodes, out nodePairs);
		List<int> list2 = new List<int>();
		List<int> list3 = new List<int>();
		foreach (IndexPair item in nodePairs)
		{
			IndexPair current = item;
			list2.Add(((IndexPair)(ref current)).I);
			list3.Add(((IndexPair)(ref current)).J);
		}
		DA.SetDataList(0, (IEnumerable)list);
		DA.SetDataList(1, (IEnumerable)nodes);
		DA.SetDataList(2, (IEnumerable)list2);
		DA.SetDataList(3, (IEnumerable)list3);
	}
}
