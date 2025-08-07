using System.Collections.Generic;
using Rhino.Geometry;

namespace IntraLattice.CORE.Data;

internal class ExoPlate
{
	private double m_offset;

	private Vector3d m_normal;

	private List<Point3d> m_vtc;

	private int m_hullIndex;

	public double Offset
	{
		get
		{
			return m_offset;
		}
		set
		{
			m_offset = value;
		}
	}

	public Vector3d Normal
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_normal;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			m_normal = value;
		}
	}

	public List<Point3d> Vtc
	{
		get
		{
			return m_vtc;
		}
		set
		{
			m_vtc = value;
		}
	}

	public int HullIndex
	{
		get
		{
			return m_hullIndex;
		}
		set
		{
			m_hullIndex = value;
		}
	}

	public ExoPlate()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		m_offset = 0.0;
		m_normal = Vector3d.Unset;
		m_vtc = new List<Point3d>();
		m_hullIndex = 0;
	}

	public ExoPlate(int hullIndex, Vector3d normal)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		m_offset = 0.0;
		m_normal = normal;
		m_vtc = new List<Point3d>();
		m_hullIndex = hullIndex;
	}
}
