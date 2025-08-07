using System;
using System.Collections.Generic;
using Rhino.Geometry;

namespace IntraLattice.CORE.Helpers;

public class MeshTools
{
	public static List<Point3d> CreateKnuckle(Plane plane, int sides, double radius, double startAngle)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		List<Point3d> list = new List<Point3d>();
		for (int i = 0; i < sides; i++)
		{
			double num = (double)(i * 2) * Math.PI / (double)sides + startAngle;
			list.Add(((Plane)(ref plane)).PointAt(radius * Math.Cos(num), radius * Math.Sin(num)));
		}
		return list;
	}

	public static List<Point3f> Point3dToPoint3f(List<Point3d> in3d)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		List<Point3f> list = new List<Point3f>();
		foreach (Point3d item in in3d)
		{
			Point3d current = item;
			list.Add(new Point3f((float)((Point3d)(ref current)).X, (float)((Point3d)(ref current)).Y, (float)((Point3d)(ref current)).Z));
		}
		return list;
	}

	public static void NormaliseMesh(ref Mesh mesh)
	{
		if (mesh.SolidOrientation() == -1)
		{
			mesh.Flip(true, true, true);
		}
		mesh.FaceNormals.ComputeFaceNormals();
		mesh.UnifyNormals();
		mesh.Normals.ComputeNormals();
	}
}
