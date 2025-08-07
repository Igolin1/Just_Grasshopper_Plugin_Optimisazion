using System;
using Rhino.Geometry;

namespace IntraLattice.CORE.Data;

[Serializable]
public class LatticeNode
{
	private Point3d m_point3d;

	private LatticeNodeState m_state;

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

	public LatticeNodeState State
	{
		get
		{
			return m_state;
		}
		set
		{
			m_state = value;
		}
	}

	public bool IsInside
	{
		get
		{
			if (m_state == LatticeNodeState.Outside)
			{
				return false;
			}
			return true;
		}
	}

	public LatticeNode()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		m_point3d = Point3d.Unset;
		m_state = LatticeNodeState.Inside;
	}

	public LatticeNode(Point3d point3d)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		m_point3d = point3d;
		m_state = LatticeNodeState.Inside;
	}

	public LatticeNode(Point3d point3d, LatticeNodeState state)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		m_point3d = point3d;
		m_state = state;
	}
}
