using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Grasshopper;
using Grasshopper.Kernel.Data;
using Rhino;
using Rhino.Collections;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;
using Rhino.Runtime;

namespace IntraLattice.CORE.Data;

public class Lattice
{
	private DataTree<LatticeNode> m_nodes;

	private List<Curve> m_struts;

	public DataTree<LatticeNode> Nodes
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

	public List<Curve> Struts
	{
		get
		{
			return m_struts;
		}
		set
		{
			m_struts = value;
		}
	}

	public Lattice()
	{
		m_nodes = new DataTree<LatticeNode>();
		m_struts = new List<Curve>();
	}

	public Lattice Duplicate()
	{
		using MemoryStream memoryStream = new MemoryStream();
		if (GetType().IsSerializable)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, this);
			memoryStream.Position = 0L;
			return (Lattice)binaryFormatter.Deserialize(memoryStream);
		}
		return null;
	}

	public void ConformMapping(UnitCell cell, float[] N)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Expected O, but got Unknown
		for (int i = 0; (float)i <= N[0]; i++)
		{
			for (int j = 0; (float)j <= N[1]; j++)
			{
				for (int k = 0; (float)k <= N[2]; k++)
				{
					foreach (IndexPair nodePair in cell.NodePairs)
					{
						IndexPair current = nodePair;
						int[] array = cell.NodePaths[((IndexPair)(ref current)).I];
						int[] array2 = cell.NodePaths[((IndexPair)(ref current)).J];
						GH_Path val = new GH_Path(new int[3]
						{
							i + array[0],
							j + array[1],
							k + array[2]
						});
						GH_Path val2 = new GH_Path(new int[3]
						{
							i + array2[0],
							j + array2[1],
							k + array2[2]
						});
						if (!Nodes.PathExists(val) || !Nodes.PathExists(val2))
						{
							continue;
						}
						LatticeNode latticeNode = Nodes[val, array[3]];
						LatticeNode latticeNode2 = Nodes[val2, array2[3]];
						if (latticeNode != null && latticeNode2 != null)
						{
							LineCurve val3 = new LineCurve(latticeNode.Point3d, latticeNode2.Point3d);
							if (val3 != null && ((CommonObject)val3).IsValid)
							{
								Struts.Add((Curve)(object)val3);
							}
						}
					}
				}
			}
		}
	}

	public void MorphMapping(UnitCell cell, DataTree<GeometryBase> spaceTree, float[] N)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Invalid comparison between Unknown and I4
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Invalid comparison between Unknown and I4
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Invalid comparison between Unknown and I4
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		Line val6 = default(Line);
		Point3d val7 = default(Point3d);
		Vector3d[] array3 = default(Vector3d[]);
		Point3d val9 = default(Point3d);
		Vector3d[] array4 = default(Vector3d[]);
		for (int i = 0; (float)i <= N[0]; i++)
		{
			for (int j = 0; (float)j <= N[1]; j++)
			{
				for (int k = 0; (float)k <= N[2]; k++)
				{
					foreach (IndexPair nodePair in cell.NodePairs)
					{
						IndexPair current = nodePair;
						int[] array = cell.NodePaths[((IndexPair)(ref current)).I];
						int[] array2 = cell.NodePaths[((IndexPair)(ref current)).J];
						GH_Path val = new GH_Path(new int[3]
						{
							i + array[0],
							j + array[1],
							k + array[2]
						});
						GH_Path val2 = new GH_Path(new int[3]
						{
							i + array2[0],
							j + array2[1],
							k + array2[2]
						});
						if (!Nodes.PathExists(val) || !Nodes.PathExists(val2))
						{
							continue;
						}
						LatticeNode latticeNode = Nodes[val, array[3]];
						LatticeNode latticeNode2 = Nodes[val2, array2[3]];
						if (latticeNode == null || latticeNode2 == null)
						{
							continue;
						}
						GH_Path val3 = (((float)i == N[0] && (float)j == N[1]) ? new GH_Path(new int[2]
						{
							i - 1,
							j - 1
						}) : (((float)i == N[0]) ? new GH_Path(new int[2]
						{
							i - 1,
							j
						}) : (((float)j != N[1]) ? new GH_Path(new int[2] { i, j }) : new GH_Path(new int[2]
						{
							i,
							j - 1
						}))));
						GeometryBase val4 = spaceTree[val3, 0];
						GeometryBase val5 = spaceTree[val3, 1];
						int num = 16;
						List<Point3d> list = new List<Point3d>();
						((Line)(ref val6))._002Ector(((RhinoList<Point3d>)(object)cell.Nodes)[((IndexPair)(ref current)).I], ((RhinoList<Point3d>)(object)cell.Nodes)[((IndexPair)(ref current)).J]);
						for (int l = 0; l <= num; l++)
						{
							list.Add(((Line)(ref val6)).PointAt((double)l / (double)num));
						}
						List<Point3d> list2 = new List<Point3d>();
						foreach (Point3d item2 in list)
						{
							Point3d current2 = item2;
							double num2 = ((Point3d)(ref current2)).X;
							double num3 = ((Point3d)(ref current2)).Y;
							if ((float)i == N[0])
							{
								num2 = 1.0 - num2;
							}
							if ((float)j == N[1])
							{
								num3 = 1.0 - num3;
							}
							((Surface)val4).Evaluate(num2, num3, 0, ref val7, ref array3);
							Vector3d val8 = Vector3d.Unset;
							ObjectType objectType = val5.ObjectType;
							if ((int)objectType != 1)
							{
								if ((int)objectType != 4)
								{
									if ((int)objectType == 8)
									{
										((Surface)val5).Evaluate(num2, num3, 0, ref val9, ref array4);
										val8 = val9 - val7;
									}
								}
								else
								{
									val8 = ((Curve)val5).PointAt(num2) - val7;
								}
							}
							else
							{
								val8 = ((Point)val5).Location - val7;
							}
							Point3d item = val7 + val8 * ((double)k + ((Point3d)(ref current2)).Z) / (double)N[2];
							list2.Add(item);
						}
						Curve val10 = Curve.CreateInterpolatedCurve((IEnumerable<Point3d>)list2, 3);
						if (val10 != null && ((CommonObject)val10).IsValid)
						{
							Struts.Add(val10);
						}
					}
				}
			}
		}
	}

	public void UniformMapping(UnitCell cell, GeometryBase designSpace, int spaceType, float[] N, double minStrutLength)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Expected O, but got Unknown
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Expected O, but got Unknown
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Expected O, but got Unknown
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Expected O, but got Unknown
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		int[] array5 = default(int[]);
		for (int i = 0; (float)i <= N[0]; i++)
		{
			for (int j = 0; (float)j <= N[1]; j++)
			{
				for (int k = 0; (float)k <= N[2]; k++)
				{
					foreach (IndexPair nodePair in cell.NodePairs)
					{
						IndexPair current = nodePair;
						int[] array = cell.NodePaths[((IndexPair)(ref current)).I];
						int[] array2 = cell.NodePaths[((IndexPair)(ref current)).J];
						GH_Path val = new GH_Path(new int[3]
						{
							i + array[0],
							j + array[1],
							k + array[2]
						});
						GH_Path val2 = new GH_Path(new int[3]
						{
							i + array2[0],
							j + array2[1],
							k + array2[2]
						});
						if (!Nodes.PathExists(val) || !Nodes.PathExists(val2))
						{
							continue;
						}
						LatticeNode latticeNode = Nodes[val, array[3]];
						LatticeNode latticeNode2 = Nodes[val2, array2[3]];
						if (latticeNode == null || latticeNode2 == null)
						{
							continue;
						}
						Curve item = (Curve)new LineCurve(latticeNode.Point3d, latticeNode2.Point3d);
						if (latticeNode.IsInside && latticeNode2.IsInside)
						{
							Struts.Add(item);
						}
						else
						{
							if (!latticeNode.IsInside && !latticeNode2.IsInside)
							{
								continue;
							}
							Point3d[] array3 = null;
							Curve[] array4 = null;
							LineCurve val3 = null;
							switch (spaceType)
							{
							case 1:
								val3 = new LineCurve(latticeNode.Point3d, latticeNode2.Point3d);
								Intersection.CurveBrep((Curve)(object)val3, (Brep)designSpace, modelAbsoluteTolerance, ref array4, ref array3);
								break;
							case 2:
								val3 = new LineCurve(latticeNode.Point3d, latticeNode2.Point3d);
								array3 = Intersection.MeshLine((Mesh)designSpace, val3.Line, ref array5);
								break;
							case 3:
								array4 = null;
								val3 = new LineCurve(latticeNode.Point3d, latticeNode2.Point3d);
								Intersection.CurveBrep((Curve)(object)val3, ((Surface)designSpace).ToBrep(), modelAbsoluteTolerance, ref array4, ref array3);
								break;
							}
							LineCurve val4 = null;
							if (array3.Length > 0)
							{
								val4 = AddTrimmedStrut(latticeNode, latticeNode2, array3[0], minStrutLength);
								if (val4 != null)
								{
									Struts.Add((Curve)(object)val4);
								}
							}
							else if (array4 != null && array4.Length > 0)
							{
								Struts.Add(array4[0]);
							}
						}
					}
				}
			}
		}
	}

	public LineCurve AddTrimmedStrut(LatticeNode node1, LatticeNode node2, Point3d intersectionPt, double minStrutLength)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		new LineCurve(new Line(node1.Point3d, node2.Point3d), 0.0, 1.0);
		if (node1.IsInside)
		{
			double num = ((Point3d)(ref intersectionPt)).DistanceTo(node1.Point3d);
			if (num > minStrutLength)
			{
				Nodes.Add(new LatticeNode(intersectionPt, LatticeNodeState.Boundary));
				return new LineCurve(node1.Point3d, intersectionPt);
			}
			node1.State = LatticeNodeState.Boundary;
		}
		if (node2.IsInside)
		{
			double num2 = ((Point3d)(ref intersectionPt)).DistanceTo(node2.Point3d);
			if (num2 > minStrutLength)
			{
				Nodes.Add(new LatticeNode(intersectionPt, LatticeNodeState.Boundary));
				return new LineCurve(node2.Point3d, intersectionPt);
			}
			node2.State = LatticeNodeState.Boundary;
		}
		return null;
	}
}
