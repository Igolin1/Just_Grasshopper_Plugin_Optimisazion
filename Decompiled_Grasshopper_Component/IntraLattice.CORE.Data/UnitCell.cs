using System;
using System.Collections.Generic;
using IntraLattice.CORE.Helpers;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry;

namespace IntraLattice.CORE.Data;

public class UnitCell
{
	private Point3dList m_nodes;

	private List<IndexPair> m_nodePairs;

	private List<int[]> m_nodePaths;

	public Point3dList Nodes
	{
		get
		{
			return m_nodes;
		}
		set
		{
			m_nodes = value;
		}
	}

	public List<IndexPair> NodePairs
	{
		get
		{
			return m_nodePairs;
		}
		set
		{
			m_nodePairs = value;
		}
	}

	public List<int[]> NodePaths
	{
		get
		{
			return m_nodePaths;
		}
		set
		{
			m_nodePaths = value;
		}
	}

	public bool isValid
	{
		get
		{
			int num = CheckValidity();
			if (num == 1)
			{
				return true;
			}
			return false;
		}
	}

	public UnitCell()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		m_nodes = new Point3dList();
		m_nodePairs = new List<IndexPair>();
		m_nodePaths = new List<int[]>();
	}

	public UnitCell(List<Line> rawCell)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		m_nodes = new Point3dList();
		m_nodePairs = new List<IndexPair>();
		m_nodePaths = new List<int[]>();
		ExtractTopology(rawCell);
		NormaliseTopology();
	}

	public UnitCell Duplicate()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		UnitCell unitCell = new UnitCell();
		foreach (Point3d item in (RhinoList<Point3d>)(object)Nodes)
		{
			((RhinoList<Point3d>)(object)unitCell.Nodes).Add(item);
		}
		foreach (IndexPair nodePair in NodePairs)
		{
			unitCell.NodePairs.Add(nodePair);
		}
		foreach (int[] nodePath in NodePaths)
		{
			unitCell.NodePaths.Add(new int[4]
			{
				nodePath[0],
				nodePath[1],
				nodePath[2],
				nodePath[3]
			});
		}
		return unitCell;
	}

	private void ExtractTopology(List<Line> lines)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		CellTools.FixIntersections(ref lines);
		IndexPair item = default(IndexPair);
		foreach (Line line in lines)
		{
			Line current = line;
			Point3d[] array = (Point3d[])(object)new Point3d[2]
			{
				((Line)(ref current)).From,
				((Line)(ref current)).To
			};
			List<int> list = new List<int>();
			Point3d[] array2 = array;
			foreach (Point3d val in array2)
			{
				int num = Nodes.ClosestIndex(val);
				if (((RhinoList<Point3d>)(object)Nodes).Count != 0)
				{
					Point3d val2 = ((RhinoList<Point3d>)(object)Nodes)[num];
					if (((Point3d)(ref val2)).EpsilonEquals(val, modelAbsoluteTolerance))
					{
						list.Add(num);
						continue;
					}
				}
				((RhinoList<Point3d>)(object)Nodes).Add(val);
				list.Add(((RhinoList<Point3d>)(object)Nodes).Count - 1);
			}
			((IndexPair)(ref item))._002Ector(list[0], list[1]);
			if (NodePairs.Count == 0 || !NodePairs.Contains(item))
			{
				NodePairs.Add(item);
			}
		}
	}

	private void NormaliseTopology()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		Interval val = default(Interval);
		Interval val2 = default(Interval);
		Interval val3 = default(Interval);
		foreach (Point3d item in (RhinoList<Point3d>)(object)Nodes)
		{
			Point3d current = item;
			if (((Point3d)(ref current)).X < ((Interval)(ref val)).T0)
			{
				((Interval)(ref val)).T0 = ((Point3d)(ref current)).X;
			}
			if (((Point3d)(ref current)).X > ((Interval)(ref val)).T1)
			{
				((Interval)(ref val)).T1 = ((Point3d)(ref current)).X;
			}
			if (((Point3d)(ref current)).Y < ((Interval)(ref val2)).T0)
			{
				((Interval)(ref val2)).T0 = ((Point3d)(ref current)).Y;
			}
			if (((Point3d)(ref current)).Y > ((Interval)(ref val2)).T1)
			{
				((Interval)(ref val2)).T1 = ((Point3d)(ref current)).Y;
			}
			if (((Point3d)(ref current)).Z < ((Interval)(ref val3)).T0)
			{
				((Interval)(ref val3)).T0 = ((Point3d)(ref current)).Z;
			}
			if (((Point3d)(ref current)).Z > ((Interval)(ref val3)).T1)
			{
				((Interval)(ref val3)).T1 = ((Point3d)(ref current)).Z;
			}
		}
		Vector3d val4 = default(Vector3d);
		((Vector3d)(ref val4))._002Ector(0.0 - ((Interval)(ref val)).T0, 0.0 - ((Interval)(ref val2)).T0, 0.0 - ((Interval)(ref val3)).T0);
		Nodes.Transform(Transform.Translation(val4));
		Nodes.Transform(Transform.Scale(Plane.WorldXY, 1.0 / ((Interval)(ref val)).Length, 1.0 / ((Interval)(ref val2)).Length, 1.0 / ((Interval)(ref val3)).Length));
	}

	public int CheckValidity()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		Plane[] array = (Plane[])(object)new Plane[2];
		ref Plane reference = ref array[0];
		Point3d val = new Point3d(0.0, 0.0, 0.0);
		Plane worldXY = Plane.WorldXY;
		reference = new Plane(val, ((Plane)(ref worldXY)).ZAxis);
		ref Plane reference2 = ref array[1];
		Point3d val2 = new Point3d(0.0, 0.0, 1.0);
		Plane worldXY2 = Plane.WorldXY;
		reference2 = new Plane(val2, ((Plane)(ref worldXY2)).ZAxis);
		Plane[] array2 = (Plane[])(object)new Plane[2];
		ref Plane reference3 = ref array2[0];
		Point3d val3 = new Point3d(0.0, 0.0, 0.0);
		Plane worldXY3 = Plane.WorldXY;
		reference3 = new Plane(val3, ((Plane)(ref worldXY3)).XAxis);
		ref Plane reference4 = ref array2[1];
		Point3d val4 = new Point3d(1.0, 0.0, 0.0);
		Plane worldXY4 = Plane.WorldXY;
		reference4 = new Plane(val4, ((Plane)(ref worldXY4)).XAxis);
		Plane[] array3 = (Plane[])(object)new Plane[2];
		ref Plane reference5 = ref array3[0];
		Point3d val5 = new Point3d(0.0, 0.0, 0.0);
		Plane worldXY5 = Plane.WorldXY;
		reference5 = new Plane(val5, ((Plane)(ref worldXY5)).YAxis);
		ref Plane reference6 = ref array3[1];
		Point3d val6 = new Point3d(0.0, 1.0, 0.0);
		Plane worldXY6 = Plane.WorldXY;
		reference6 = new Plane(val6, ((Plane)(ref worldXY6)).YAxis);
		bool[] array4 = new bool[3];
		bool[] array5 = array4;
		foreach (Point3d item in (RhinoList<Point3d>)(object)Nodes)
		{
			Point3d current = item;
			_ = Point3d.Unset;
			if (Math.Abs(((Plane)(ref array[0])).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				new Point3d(((Point3d)(ref current)).X, ((Point3d)(ref current)).Y, ((Plane)(ref array[1])).OriginZ);
				array5[0] = true;
			}
			if (Math.Abs(((Plane)(ref array[1])).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				new Point3d(((Point3d)(ref current)).X, ((Point3d)(ref current)).Y, ((Plane)(ref array[0])).OriginZ);
			}
			if (Math.Abs(((Plane)(ref array2[0])).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				new Point3d(((Plane)(ref array2[1])).OriginX, ((Point3d)(ref current)).Y, ((Point3d)(ref current)).Z);
				array5[1] = true;
			}
			if (Math.Abs(((Plane)(ref array2[1])).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				new Point3d(((Plane)(ref array2[0])).OriginX, ((Point3d)(ref current)).Y, ((Point3d)(ref current)).Z);
			}
			if (Math.Abs(((Plane)(ref array3[0])).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				new Point3d(((Point3d)(ref current)).X, ((Plane)(ref array3[1])).OriginY, ((Point3d)(ref current)).Z);
				array5[2] = true;
			}
			if (Math.Abs(((Plane)(ref array3[1])).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				new Point3d(((Point3d)(ref current)).X, ((Plane)(ref array3[0])).OriginY, ((Point3d)(ref current)).Z);
			}
		}
		if (!array5[0] || !array5[1] || !array5[2])
		{
			return 0;
		}
		return 1;
	}

	public void FormatTopology()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		Plane worldXY = Plane.WorldXY;
		((Plane)(ref worldXY)).Translate(new Vector3d(0.0, 0.0, 1.0));
		Plane worldYZ = Plane.WorldYZ;
		((Plane)(ref worldYZ)).Translate(new Vector3d(1.0, 0.0, 0.0));
		Plane worldZX = Plane.WorldZX;
		((Plane)(ref worldZX)).Translate(new Vector3d(0.0, 1.0, 0.0));
		foreach (Point3d item in (RhinoList<Point3d>)(object)Nodes)
		{
			Point3d current = item;
			if (Math.Abs(((Plane)(ref worldXY)).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				if (((Point3d)(ref current)).DistanceTo(new Point3d(1.0, 1.0, 1.0)) < modelAbsoluteTolerance)
				{
					List<int[]> nodePaths = NodePaths;
					int[] array = new int[4] { 1, 1, 1, 0 };
					array[3] = Nodes.ClosestIndex(new Point3d(0.0, 0.0, 0.0));
					nodePaths.Add(array);
				}
				else if (Math.Abs(((Point3d)(ref current)).X - 1.0) < modelAbsoluteTolerance && Math.Abs(((Point3d)(ref current)).Z - 1.0) < modelAbsoluteTolerance)
				{
					NodePaths.Add(new int[4]
					{
						1,
						0,
						1,
						Nodes.ClosestIndex(new Point3d(0.0, ((Point3d)(ref current)).Y, 0.0))
					});
				}
				else if (Math.Abs(((Point3d)(ref current)).Y - 1.0) < modelAbsoluteTolerance && Math.Abs(((Point3d)(ref current)).Z - 1.0) < modelAbsoluteTolerance)
				{
					NodePaths.Add(new int[4]
					{
						0,
						1,
						1,
						Nodes.ClosestIndex(new Point3d(((Point3d)(ref current)).X, 0.0, 0.0))
					});
				}
				else
				{
					NodePaths.Add(new int[4]
					{
						0,
						0,
						1,
						Nodes.ClosestIndex(new Point3d(((Point3d)(ref current)).X, ((Point3d)(ref current)).Y, 0.0))
					});
				}
			}
			else if (Math.Abs(((Plane)(ref worldYZ)).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				if (Math.Abs(((Point3d)(ref current)).X - 1.0) < modelAbsoluteTolerance && Math.Abs(((Point3d)(ref current)).Y - 1.0) < modelAbsoluteTolerance)
				{
					NodePaths.Add(new int[4]
					{
						1,
						1,
						0,
						Nodes.ClosestIndex(new Point3d(0.0, 0.0, ((Point3d)(ref current)).Z))
					});
				}
				else
				{
					NodePaths.Add(new int[4]
					{
						1,
						0,
						0,
						Nodes.ClosestIndex(new Point3d(0.0, ((Point3d)(ref current)).Y, ((Point3d)(ref current)).Z))
					});
				}
			}
			else if (Math.Abs(((Plane)(ref worldZX)).DistanceTo(current)) < modelAbsoluteTolerance)
			{
				NodePaths.Add(new int[4]
				{
					0,
					1,
					0,
					Nodes.ClosestIndex(new Point3d(((Point3d)(ref current)).X, 0.0, ((Point3d)(ref current)).Z))
				});
			}
			else
			{
				NodePaths.Add(new int[4]
				{
					0,
					0,
					0,
					((RhinoList<Point3d>)(object)Nodes).IndexOf(current)
				});
			}
		}
		List<int> list = new List<int>();
		for (int i = 0; i < NodePairs.Count; i++)
		{
			Point3dList nodes = Nodes;
			IndexPair val = NodePairs[i];
			Point3d val2 = ((RhinoList<Point3d>)(object)nodes)[((IndexPair)(ref val)).I];
			Point3dList nodes2 = Nodes;
			IndexPair val3 = NodePairs[i];
			Point3d val4 = ((RhinoList<Point3d>)(object)nodes2)[((IndexPair)(ref val3)).J];
			bool flag = false;
			if (Math.Abs(((Plane)(ref worldXY)).DistanceTo(val2)) < modelAbsoluteTolerance && Math.Abs(((Plane)(ref worldXY)).DistanceTo(val4)) < modelAbsoluteTolerance)
			{
				flag = true;
			}
			if (Math.Abs(((Plane)(ref worldYZ)).DistanceTo(val2)) < modelAbsoluteTolerance && Math.Abs(((Plane)(ref worldYZ)).DistanceTo(val4)) < modelAbsoluteTolerance)
			{
				flag = true;
			}
			if (Math.Abs(((Plane)(ref worldZX)).DistanceTo(val2)) < modelAbsoluteTolerance && Math.Abs(((Plane)(ref worldZX)).DistanceTo(val4)) < modelAbsoluteTolerance)
			{
				flag = true;
			}
			if (flag)
			{
				list.Add(i);
			}
		}
		list.Reverse();
		foreach (int item2 in list)
		{
			NodePairs.RemoveAt(item2);
		}
	}
}
