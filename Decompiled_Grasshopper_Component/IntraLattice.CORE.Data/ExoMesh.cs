using System;
using System.Collections.Generic;
using IntraLattice.CORE.Helpers;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry;
using Rhino.Geometry.Intersect;

namespace IntraLattice.CORE.Data;

internal class ExoMesh
{
	private List<ExoHull> m_hulls;

	private List<ExoSleeve> m_sleeves;

	private List<ExoPlate> m_plates;

	private Mesh m_mesh;

	public List<ExoHull> Hulls
	{
		get
		{
			return m_hulls;
		}
		set
		{
			m_hulls = value;
		}
	}

	public List<ExoSleeve> Sleeves
	{
		get
		{
			return m_sleeves;
		}
		set
		{
			m_sleeves = value;
		}
	}

	public List<ExoPlate> Plates
	{
		get
		{
			return m_plates;
		}
		set
		{
			m_plates = value;
		}
	}

	public Mesh Mesh
	{
		get
		{
			return m_mesh;
		}
		set
		{
			m_mesh = value;
		}
	}

	public ExoMesh()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		m_hulls = new List<ExoHull>();
		m_sleeves = new List<ExoSleeve>();
		m_plates = new List<ExoPlate>();
		m_mesh = new Mesh();
	}

	public ExoMesh(List<Curve> struts)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		m_hulls = new List<ExoHull>();
		m_sleeves = new List<ExoSleeve>();
		m_plates = new List<ExoPlate>();
		m_mesh = new Mesh();
		double modelAbsoluteTolerance = RhinoDoc.ActiveDoc.ModelAbsoluteTolerance;
		Point3dList nodes = new Point3dList();
		List<IndexPair> nodePairs = new List<IndexPair>();
		struts = FrameTools.CleanNetwork(struts, modelAbsoluteTolerance, out nodes, out nodePairs);
		foreach (Point3d item in (RhinoList<Point3d>)(object)nodes)
		{
			m_hulls.Add(new ExoHull(item));
		}
		IndexPair platePair = default(IndexPair);
		for (int i = 0; i < struts.Count; i++)
		{
			m_sleeves.Add(new ExoSleeve(struts[i], nodePairs[i]));
			List<ExoPlate> plates = m_plates;
			IndexPair val = nodePairs[i];
			plates.Add(new ExoPlate(((IndexPair)(ref val)).I, struts[i].TangentAtStart));
			List<ExoPlate> plates2 = m_plates;
			IndexPair val2 = nodePairs[i];
			plates2.Add(new ExoPlate(((IndexPair)(ref val2)).J, -struts[i].TangentAtEnd));
			((IndexPair)(ref platePair))._002Ector(m_plates.Count - 2, m_plates.Count - 1);
			m_sleeves[i].PlatePair = platePair;
			List<ExoHull> hulls = m_hulls;
			IndexPair val3 = nodePairs[i];
			hulls[((IndexPair)(ref val3)).I].SleeveIndices.Add(i);
			List<ExoHull> hulls2 = m_hulls;
			IndexPair val4 = nodePairs[i];
			hulls2[((IndexPair)(ref val4)).J].SleeveIndices.Add(i);
			List<ExoHull> hulls3 = m_hulls;
			IndexPair val5 = nodePairs[i];
			hulls3[((IndexPair)(ref val5)).I].PlateIndices.Add(((IndexPair)(ref platePair)).I);
			List<ExoHull> hulls4 = m_hulls;
			IndexPair val6 = nodePairs[i];
			hulls4[((IndexPair)(ref val6)).J].PlateIndices.Add(((IndexPair)(ref platePair)).J);
		}
	}

	public bool ComputeOffsets(int nodeIndex, double tol)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Invalid comparison between Unknown and I4
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Invalid comparison between Unknown and I4
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Invalid comparison between Unknown and I4
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Invalid comparison between Unknown and I4
		ExoHull exoHull = Hulls[nodeIndex];
		List<Curve> list = new List<Curve>();
		List<double> list2 = new List<double>();
		List<double> list3 = new List<double>();
		double item = default(double);
		foreach (int sleeveIndex in exoHull.SleeveIndices)
		{
			Curve val = Sleeves[sleeveIndex].Curve.DuplicateCurve();
			Point3d pointAtEnd = val.PointAtEnd;
			if (((Point3d)(ref pointAtEnd)).EpsilonEquals(exoHull.Point3d, 100.0 * tol))
			{
				val.Reverse();
				val.Domain = new Interval(0.0, 1.0);
				list2.Add(Sleeves[sleeveIndex].EndRadius);
			}
			else
			{
				list2.Add(Sleeves[sleeveIndex].StartRadius);
			}
			list.Add(val);
			val.LengthParameter(list2[list2.Count - 1], ref item);
			list3.Add(item);
		}
		double num = 0.0;
		foreach (double item2 in list2)
		{
			double num2 = item2;
			num += num2;
		}
		exoHull.AvgRadius = num / (double)list2.Count;
		bool flag = false;
		int num3 = 0;
		double num4 = list3[0] / 10.0;
		Plane val2 = default(Plane);
		double num5 = default(double);
		double num6 = default(double);
		while (!flag && num3 < 500)
		{
			List<Circle> list4 = new List<Circle>();
			for (int i = 0; i < list.Count; i++)
			{
				list[i].PerpendicularFrameAt(list3[i], ref val2);
				list4.Add(new Circle(val2, list2[i]));
			}
			bool[] array = new bool[list.Count];
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = j + 1; k < list.Count; k++)
				{
					Circle val3 = list4[j];
					PlaneCircleIntersection val4 = Intersection.PlaneCircle(((Circle)(ref val3)).Plane, list4[k], ref num5, ref num6);
					Circle val5 = list4[k];
					PlaneCircleIntersection val6 = Intersection.PlaneCircle(((Circle)(ref val5)).Plane, list4[j], ref num5, ref num6);
					if ((int)val4 == 2 || (int)val4 == 1)
					{
						array[j] = true;
					}
					if ((int)val6 == 2 || (int)val6 == 1)
					{
						array[k] = true;
					}
				}
			}
			flag = true;
			for (int l = 0; l < list.Count; l++)
			{
				if (array[l])
				{
					list3[l] += num4;
					flag = false;
				}
			}
			num3++;
		}
		for (int m = 0; m < list.Count; m++)
		{
			int index = exoHull.PlateIndices[m];
			Plates[index].Offset = 1.05 * list3[m];
		}
		return true;
	}

	public void FixSharpNodes(int nodeIndex, int sides)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		ExoHull exoHull = Hulls[nodeIndex];
		bool flag = true;
		Vector3d val = default(Vector3d);
		foreach (int plateIndex in exoHull.PlateIndices)
		{
			val += Plates[plateIndex].Normal;
		}
		foreach (int plateIndex2 in exoHull.PlateIndices)
		{
			if (Vector3d.VectorAngle(-val, Plates[plateIndex2].Normal) < Math.PI / 2.0)
			{
				flag = false;
			}
		}
		if (flag)
		{
			Plane plane = default(Plane);
			((Plane)(ref plane))._002Ector(exoHull.Point3d - val * exoHull.AvgRadius / (double)exoHull.PlateIndices.Count, -val);
			List<Point3d> collection = MeshTools.CreateKnuckle(plane, sides, exoHull.AvgRadius, 0.0);
			Plates.Add(new ExoPlate(nodeIndex, -val));
			int num = Plates.Count - 1;
			Plates[num].Vtc.AddRange(collection);
			exoHull.PlateIndices.Add(num);
		}
	}

	public Mesh MakeSleeve(int strutIndex, int sides)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		Mesh val = new Mesh();
		ExoSleeve exoSleeve = Sleeves[strutIndex];
		List<ExoPlate> plates = Plates;
		IndexPair platePair = exoSleeve.PlatePair;
		ExoPlate exoPlate = plates[((IndexPair)(ref platePair)).I];
		List<ExoPlate> plates2 = Plates;
		IndexPair platePair2 = exoSleeve.PlatePair;
		ExoPlate exoPlate2 = plates2[((IndexPair)(ref platePair2)).J];
		double offset = exoPlate.Offset;
		double num = 1.0 - exoPlate2.Offset;
		exoPlate.Vtc.Add(exoSleeve.Curve.PointAt(offset));
		exoPlate2.Vtc.Add(exoSleeve.Curve.PointAt(num));
		double avgRadius = exoSleeve.AvgRadius;
		double length = exoSleeve.Curve.GetLength(new Interval(offset, num));
		double num2 = Math.Max(Math.Round(length * 0.5 / avgRadius) * 2.0, 2.0);
		Vector3d tangentAtStart = exoSleeve.Curve.TangentAtStart;
		Plane plane = default(Plane);
		for (int i = 0; (double)i <= num2; i++)
		{
			if (exoSleeve.Curve.IsLinear())
			{
				Point3d val2 = exoPlate.Vtc[0] + tangentAtStart * (length * (double)i / num2);
				plane = new Plane(val2, tangentAtStart);
			}
			else
			{
				double num3 = offset + (double)i / num2 * (num - offset);
				exoSleeve.Curve.PointAt(num3);
				exoSleeve.Curve.PerpendicularFrameAt(num3, ref plane);
			}
			double radius = exoSleeve.StartRadius - (double)i * (exoSleeve.StartRadius - exoSleeve.EndRadius) / num2;
			double startAngle = (double)i * Math.PI / (double)sides;
			List<Point3d> list = MeshTools.CreateKnuckle(plane, sides, radius, startAngle);
			if (i == 0)
			{
				exoPlate.Vtc.AddRange(list);
			}
			if ((double)i == num2)
			{
				exoPlate2.Vtc.AddRange(list);
			}
			val.Vertices.AddVertices((IEnumerable<Point3d>)list);
		}
		for (int j = 0; (double)j < num2; j++)
		{
			for (int k = 0; k < sides; k++)
			{
				int num4 = j * sides + k;
				int num5 = j * sides + k + sides;
				int num6 = j * sides + sides + (k + 1) % sides;
				int num7 = j * sides + (k + 1) % sides;
				val.Faces.AddFace(num4, num5, num7);
				val.Faces.AddFace(num5, num6, num7);
			}
		}
		return val;
	}

	public Mesh MakeConvexHull(int nodeIndex, int sides, double tol, bool cleanPlates)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		Mesh mesh = new Mesh();
		ExoHull exoHull = Hulls[nodeIndex];
		double avgRadius = exoHull.AvgRadius;
		double num = tol * avgRadius / 10.0;
		List<Point3d> list = new List<Point3d>();
		foreach (int plateIndex in exoHull.PlateIndices)
		{
			list.AddRange(Plates[plateIndex].Vtc);
		}
		mesh.Vertices.Add(list[0]);
		mesh.Vertices.Add(list[1]);
		mesh.Vertices.Add(list[2]);
		Plane val = default(Plane);
		((Plane)(ref val))._002Ector(list[0], list[1], list[2]);
		int i;
		for (i = sides + 1; Math.Abs(((Plane)(ref val)).DistanceTo(list[i])) < num; i++)
		{
		}
		mesh.Vertices.Add(list[i]);
		mesh.Faces.AddFace(0, 2, 1);
		mesh.Faces.AddFace(0, 3, 2);
		mesh.Faces.AddFace(0, 1, 3);
		mesh.Faces.AddFace(1, 2, 3);
		list.RemoveAt(i);
		list.RemoveRange(0, 3);
		Plane val3 = default(Plane);
		for (int j = 0; j < list.Count; j++)
		{
			MeshTools.NormaliseMesh(ref mesh);
			List<int> list2 = new List<int>();
			for (int k = 0; k < mesh.Faces.Count; k++)
			{
				Vector3d val2 = list[j] - mesh.Faces.GetFaceCenter(k);
				double num2 = Vector3d.VectorAngle(Vector3d.op_Implicit(mesh.FaceNormals[k]), val2);
				((Plane)(ref val3))._002Ector(mesh.Faces.GetFaceCenter(k), Vector3d.op_Implicit(mesh.FaceNormals[k]));
				if (num2 < Math.PI / 2.0 || Math.Abs(((Plane)(ref val3)).DistanceTo(list[j])) < num)
				{
					list2.Add(k);
				}
			}
			mesh.Faces.DeleteFaces((IEnumerable<int>)list2);
			mesh.Vertices.Add(list[j]);
			List<MeshFace> list3 = new List<MeshFace>();
			for (int l = 0; l < mesh.TopologyEdges.Count; l++)
			{
				if (!mesh.TopologyEdges.IsSwappableEdge(l))
				{
					IndexPair topologyVertices = mesh.TopologyEdges.GetTopologyVertices(l);
					int num3 = mesh.TopologyVertices.MeshVertexIndices(((IndexPair)(ref topologyVertices)).I)[0];
					int num4 = mesh.TopologyVertices.MeshVertexIndices(((IndexPair)(ref topologyVertices)).J)[0];
					list3.Add(new MeshFace(num3, num4, mesh.Vertices.Count - 1));
				}
			}
			mesh.Faces.AddFaces((IEnumerable<MeshFace>)list3);
		}
		MeshTools.NormaliseMesh(ref mesh);
		if (cleanPlates)
		{
			List<int> list4 = new List<int>();
			Point3f val4 = default(Point3f);
			Point3f val5 = default(Point3f);
			Point3f val6 = default(Point3f);
			Point3f val7 = default(Point3f);
			foreach (int plateIndex2 in exoHull.PlateIndices)
			{
				List<Point3f> list5 = MeshTools.Point3dToPoint3f(Plates[plateIndex2].Vtc);
				if (list5.Count < sides + 1)
				{
					continue;
				}
				for (int m = 0; m < mesh.Faces.Count; m++)
				{
					mesh.Faces.GetFaceVertices(m, ref val4, ref val5, ref val6, ref val7);
					int num5 = 0;
					foreach (Point3f item in list5)
					{
						Point3f current3 = item;
						if (((Point3f)(ref current3)).EpsilonEquals(val4, (float)tol) || ((Point3f)(ref current3)).EpsilonEquals(val5, (float)tol) || ((Point3f)(ref current3)).EpsilonEquals(val6, (float)tol))
						{
							num5++;
						}
					}
					if (num5 == 3)
					{
						list4.Add(m);
					}
				}
			}
			list4.Reverse();
			foreach (int item2 in list4)
			{
				mesh.Faces.RemoveAt(item2);
			}
		}
		return mesh;
	}

	public Mesh MakeEndFace(int nodeIndex, int sides)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		Mesh val = new Mesh();
		foreach (Point3d item in Plates[Hulls[nodeIndex].PlateIndices[0]].Vtc)
		{
			val.Vertices.Add(item);
		}
		for (int i = 1; i < sides; i++)
		{
			val.Faces.AddFace(0, i, i + 1);
		}
		val.Faces.AddFace(0, sides, 1);
		return val;
	}
}
