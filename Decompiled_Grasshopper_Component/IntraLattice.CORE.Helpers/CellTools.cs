using System.Collections.Generic;
using Rhino;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;

namespace IntraLattice.CORE.Helpers;

public class CellTools
{
	public static void FixIntersections(ref List<Line> lines)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		List<int> list = new List<int>();
		List<Line> list2 = new List<Line>();
		double num = default(double);
		double num2 = default(double);
		for (int i = 0; i < lines.Count; i++)
		{
			for (int j = i + 1; j < lines.Count; j++)
			{
				if (Intersection.LineLine(lines[i], lines[j], ref num, ref num2, modelAbsoluteTolerance, true))
				{
					if (num > modelAbsoluteTolerance && 1.0 - num > modelAbsoluteTolerance && !list.Contains(i))
					{
						Line val = lines[i];
						Point3d from = ((Line)(ref val)).From;
						Line val2 = lines[i];
						list2.Add(new Line(from, ((Line)(ref val2)).PointAt(num)));
						Line val3 = lines[i];
						Point3d val4 = ((Line)(ref val3)).PointAt(num);
						Line val5 = lines[i];
						list2.Add(new Line(val4, ((Line)(ref val5)).To));
						list.Add(i);
					}
					if (num2 > modelAbsoluteTolerance && 1.0 - num2 > modelAbsoluteTolerance && !list.Contains(j))
					{
						Line val6 = lines[j];
						Point3d from2 = ((Line)(ref val6)).From;
						Line val7 = lines[j];
						list2.Add(new Line(from2, ((Line)(ref val7)).PointAt(num2)));
						Line val8 = lines[j];
						Point3d val9 = ((Line)(ref val8)).PointAt(num2);
						Line val10 = lines[j];
						list2.Add(new Line(val9, ((Line)(ref val10)).To));
						list.Add(j);
					}
				}
			}
		}
		list.Sort();
		list.Reverse();
		foreach (int item in list)
		{
			lines.RemoveAt(item);
		}
		lines.AddRange(list2);
	}

	public static void MakeCornerNodes(ref List<Point3d> nodes, double d)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		nodes.Add(new Point3d(0.0, 0.0, 0.0));
		nodes.Add(new Point3d(d, 0.0, 0.0));
		nodes.Add(new Point3d(d, d, 0.0));
		nodes.Add(new Point3d(0.0, d, 0.0));
		nodes.Add(new Point3d(0.0, 0.0, d));
		nodes.Add(new Point3d(d, 0.0, d));
		nodes.Add(new Point3d(d, d, d));
		nodes.Add(new Point3d(0.0, d, d));
	}
}
