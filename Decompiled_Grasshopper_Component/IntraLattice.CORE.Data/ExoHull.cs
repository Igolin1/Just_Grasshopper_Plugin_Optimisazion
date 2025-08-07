using System.Collections.Generic;
using Rhino.Geometry;

namespace IntraLattice.CORE.Data;

internal class ExoHull
{
	private Point3d m_point3d;

	private List<int> m_sleeveIndices;

	private List<int> m_plateIndices;

	private double m_avgRadius;

	public Point3d Point3d
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_point3d;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			m_point3d = value;
		}
	}

	public List<int> SleeveIndices
	{
		get
		{
			return m_sleeveIndices;
		}
		set
		{
			m_sleeveIndices = value;
		}
	}

	public List<int> PlateIndices
	{
		get
		{
			return m_plateIndices;
		}
		set
		{
			m_plateIndices = value;
		}
	}

	public double AvgRadius
	{
		get
		{
			return m_avgRadius;
		}
		set
		{
			m_avgRadius = value;
		}
	}

	public ExoHull()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		m_point3d = Point3d.Unset;
		m_sleeveIndices = new List<int>();
		m_plateIndices = new List<int>();
		m_avgRadius = 0.0;
	}

	public ExoHull(Point3d point3d)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		m_point3d = point3d;
		m_sleeveIndices = new List<int>();
		m_plateIndices = new List<int>();
		m_avgRadius = 0.0;
	}
}
