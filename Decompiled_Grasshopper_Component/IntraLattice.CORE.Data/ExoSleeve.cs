using Rhino;
using Rhino.Geometry;

namespace IntraLattice.CORE.Data;

internal class ExoSleeve
{
	private Curve m_curve;

	private IndexPair m_hullPair;

	private IndexPair m_platePair;

	private double m_startRadius;

	private double m_endRadius;

	public Curve Curve
	{
		get
		{
			return m_curve;
		}
		set
		{
			m_curve = value;
		}
	}

	public IndexPair HullPair
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_hullPair;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			m_hullPair = value;
		}
	}

	public IndexPair PlatePair
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_platePair;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			m_platePair = value;
		}
	}

	public double StartRadius
	{
		get
		{
			return m_startRadius;
		}
		set
		{
			m_startRadius = value;
		}
	}

	public double EndRadius
	{
		get
		{
			return m_endRadius;
		}
		set
		{
			m_endRadius = value;
		}
	}

	public double AvgRadius => (StartRadius + EndRadius) / 2.0;

	public ExoSleeve()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		m_curve = null;
		m_hullPair = default(IndexPair);
		m_platePair = default(IndexPair);
		m_startRadius = 0.0;
		m_endRadius = 0.0;
	}

	public ExoSleeve(Curve curve)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		m_curve = curve;
		m_hullPair = default(IndexPair);
		m_platePair = default(IndexPair);
		m_startRadius = 0.0;
		m_endRadius = 0.0;
	}

	public ExoSleeve(Curve curve, IndexPair hullPair)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		m_curve = curve;
		m_hullPair = hullPair;
		m_platePair = default(IndexPair);
		m_startRadius = 0.0;
		m_endRadius = 0.0;
	}
}
