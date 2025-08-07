using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry;
using Rhino.Runtime;

namespace IntraLattice.CORE.Helpers;

public class FrameTools
{
	public static List<Curve> CleanNetwork(List<Curve> inputStruts, double tol)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		Point3dList nodes = new Point3dList();
		List<IndexPair> nodePairs = new List<IndexPair>();
		return CleanNetwork(inputStruts, tol, out nodes, out nodePairs);
	}

	public static List<Curve> CleanNetwork(List<Curve> inputStruts, double tol, out Point3dList nodes)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		nodes = new Point3dList();
		List<IndexPair> nodePairs = new List<IndexPair>();
		return CleanNetwork(inputStruts, tol, out nodes, out nodePairs);
	}

	public static List<Curve> CleanNetwork(List<Curve> inputStruts, double tol, out Point3dList nodes, out List<IndexPair> nodePairs)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		nodes = new Point3dList();
		nodePairs = new List<IndexPair>();
		List<Curve> list = new List<Curve>();
		IndexPair item = default(IndexPair);
		for (int i = 0; i < inputStruts.Count; i++)
		{
			Curve val = inputStruts[i];
			val.Domain = new Interval(0.0, 1.0);
			double num = Math.Max(tol, 100.0 * RhinoDoc.ActiveDoc.ModelAbsoluteTolerance);
			if (val == null || !((CommonObject)val).IsValid || val.IsShort(num))
			{
				continue;
			}
			Point3d[] array = (Point3d[])(object)new Point3d[2] { val.PointAtStart, val.PointAtEnd };
			List<int> list2 = new List<int>();
			for (int j = 0; j < 2; j++)
			{
				Point3d val2 = array[j];
				int num2 = nodes.ClosestIndex(val2);
				if (((RhinoList<Point3d>)(object)nodes).Count != 0 && ((Point3d)(ref val2)).EpsilonEquals(((RhinoList<Point3d>)(object)nodes)[num2], tol))
				{
					list2.Add(num2);
					continue;
				}
				((RhinoList<Point3d>)(object)nodes).Add(val2);
				list2.Add(((RhinoList<Point3d>)(object)nodes).Count - 1);
			}
			bool flag = false;
			((IndexPair)(ref item))._002Ector(list2[0], list2[1]);
			int num3 = nodePairs.IndexOf(item);
			if (nodePairs.Count != 0 && num3 != -1)
			{
				Curve val3 = list[num3];
				Point3d val4 = val.PointAt(0.5);
				Point3d val5 = val3.PointAt(0.5);
				if (((Point3d)(ref val4)).EpsilonEquals(val5, tol))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				nodePairs.Add(item);
				val.Domain = new Interval(0.0, 1.0);
				list.Add(val);
			}
		}
		return list;
	}

	public static int ValidateSpace(ref GeometryBase designSpace)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Invalid comparison between Unknown and I4
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Invalid comparison between Unknown and I4
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		int result = 0;
		if ((int)designSpace.ObjectType == 16)
		{
			result = 1;
		}
		else if ((int)designSpace.ObjectType == 32 && ((Mesh)designSpace).IsClosed)
		{
			result = 2;
		}
		else if ((int)designSpace.ObjectType == 8 && ((Surface)designSpace).IsSolid)
		{
			result = 3;
		}
		return result;
	}

	public static bool IsPointInside(GeometryBase geometry, Point3d testPoint, int spaceType, double tol, bool strictlyIn)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		switch (spaceType)
		{
		case 1:
			result = ((Brep)geometry).IsPointInside(testPoint, tol, strictlyIn);
			break;
		case 2:
			result = ((Mesh)geometry).IsPointInside(testPoint, tol, strictlyIn);
			break;
		case 3:
			result = ((Surface)geometry).ToBrep().IsPointInside(testPoint, tol, strictlyIn);
			break;
		}
		return result;
	}

	public static double DistanceTo(GeometryBase geometry, Point3d testPoint, int spaceType)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		double result = 0.0;
		switch (spaceType)
		{
		case 1:
		{
			Point3d val = ((Brep)geometry).ClosestPoint(testPoint);
			result = ((Point3d)(ref testPoint)).DistanceTo(val);
			break;
		}
		case 2:
		{
			Point3d val = ((Mesh)geometry).ClosestPoint(testPoint);
			result = ((Point3d)(ref testPoint)).DistanceTo(val);
			break;
		}
		case 3:
		{
			Point3d val = ((Surface)geometry).ToBrep().ClosestPoint(testPoint);
			result = ((Point3d)(ref testPoint)).DistanceTo(val);
			break;
		}
		}
		return result;
	}
}
