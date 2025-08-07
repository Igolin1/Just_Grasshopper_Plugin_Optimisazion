using System.Drawing;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Rhino;
using Rhino.Collections;
using Rhino.Geometry;

namespace IntraLattice.CORE.Data.GH_Goo;

public class UnitCellGoo : GH_GeometricGoo<UnitCell>, IGH_PreviewData
{
	public override bool IsValid
	{
		get
		{
			if (((GH_GeometricGoo<UnitCell>)this).Value == null)
			{
				return false;
			}
			return base.IsValid;
		}
	}

	public override string IsValidWhyNot
	{
		get
		{
			if (((GH_GeometricGoo<UnitCell>)this).Value.Nodes == null)
			{
				return "Node list empty";
			}
			if (((GH_GeometricGoo<UnitCell>)this).Value.NodePairs == null)
			{
				return "No line";
			}
			return ((GH_Goo<UnitCell>)(object)this).IsValidWhyNot;
		}
	}

	public override string TypeDescription => "LatticeCell Representation";

	public override string TypeName => "LatticeCellGoo";

	public override BoundingBox Boundingbox
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			if (((GH_GeometricGoo<UnitCell>)this).Value == null)
			{
				return BoundingBox.Empty;
			}
			if (((GH_GeometricGoo<UnitCell>)this).Value.Nodes == null)
			{
				return BoundingBox.Empty;
			}
			return ((GH_GeometricGoo<UnitCell>)this).Value.Nodes.BoundingBox;
		}
	}

	public BoundingBox ClippingBox => ((GH_GeometricGoo<UnitCell>)this).Boundingbox;

	public UnitCellGoo()
	{
		((GH_GeometricGoo<UnitCell>)this).Value = new UnitCell();
	}

	public UnitCellGoo(UnitCell cell)
	{
		if (cell == null)
		{
			cell = new UnitCell();
		}
		((GH_GeometricGoo<UnitCell>)this).Value = cell;
	}

	public override IGH_GeometricGoo DuplicateGeometry()
	{
		return (IGH_GeometricGoo)(object)DuplicateGoo();
	}

	public UnitCellGoo DuplicateGoo()
	{
		return new UnitCellGoo((((GH_GeometricGoo<UnitCell>)this).Value == null) ? new UnitCell() : ((GH_GeometricGoo<UnitCell>)this).Value.Duplicate());
	}

	public override string ToString()
	{
		if (((GH_GeometricGoo<UnitCell>)this).Value == null)
		{
			return "Null LatticeCell";
		}
		return ((GH_GeometricGoo<UnitCell>)this).Value.ToString();
	}

	public override BoundingBox GetBoundingBox(Transform xform)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (((GH_GeometricGoo<UnitCell>)this).Value == null)
		{
			return BoundingBox.Empty;
		}
		if (((GH_GeometricGoo<UnitCell>)this).Value.Nodes == null)
		{
			return BoundingBox.Empty;
		}
		return ((GH_GeometricGoo<UnitCell>)this).Value.Nodes.BoundingBox;
	}

	public override object ScriptVariable()
	{
		return ((GH_GeometricGoo<UnitCell>)this).Value;
	}

	public override bool CastTo<Q>(out Q target)
	{
		if (typeof(Q).IsAssignableFrom(typeof(UnitCell)))
		{
			if (((GH_GeometricGoo<UnitCell>)this).Value == null)
			{
				target = default(Q);
			}
			else
			{
				target = (Q)(object)((GH_GeometricGoo<UnitCell>)this).Value;
			}
			return true;
		}
		target = default(Q);
		return false;
	}

	public override bool CastFrom(object source)
	{
		if (source == null)
		{
			return false;
		}
		if (typeof(UnitCell).IsAssignableFrom(source.GetType()))
		{
			((GH_GeometricGoo<UnitCell>)this).Value = (UnitCell)source;
			return true;
		}
		return false;
	}

	public override IGH_GeometricGoo Transform(Transform xform)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (((GH_GeometricGoo<UnitCell>)this).Value == null)
		{
			return null;
		}
		if (((GH_GeometricGoo<UnitCell>)this).Value.Nodes == null)
		{
			return null;
		}
		((GH_Goo<UnitCell>)(object)this).m_value.Nodes.Transform(xform);
		return (IGH_GeometricGoo)(object)this;
	}

	public override IGH_GeometricGoo Morph(SpaceMorph xmorph)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < ((RhinoList<Point3d>)(object)((GH_GeometricGoo<UnitCell>)this).Value.Nodes).Count; i++)
		{
			((RhinoList<Point3d>)(object)((GH_GeometricGoo<UnitCell>)this).Value.Nodes)[i] = xmorph.MorphPoint(((RhinoList<Point3d>)(object)((GH_GeometricGoo<UnitCell>)this).Value.Nodes)[i]);
		}
		return (IGH_GeometricGoo)(object)this;
	}

	public void DrawViewportWires(GH_PreviewWireArgs args)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		if (((GH_GeometricGoo<UnitCell>)this).Value == null)
		{
			return;
		}
		if (((GH_GeometricGoo<UnitCell>)this).Value.Nodes != null)
		{
			foreach (Point3d item in (RhinoList<Point3d>)(object)((GH_GeometricGoo<UnitCell>)this).Value.Nodes)
			{
				args.Pipeline.DrawPoint(item, (Color)args.Color);
			}
		}
		if (((GH_GeometricGoo<UnitCell>)this).Value.NodePairs == null)
		{
			return;
		}
		foreach (IndexPair nodePair in ((GH_GeometricGoo<UnitCell>)this).Value.NodePairs)
		{
			IndexPair current2 = nodePair;
			Point3d val = ((RhinoList<Point3d>)(object)((GH_GeometricGoo<UnitCell>)this).Value.Nodes)[((IndexPair)(ref current2)).I];
			Point3d val2 = ((RhinoList<Point3d>)(object)((GH_GeometricGoo<UnitCell>)this).Value.Nodes)[((IndexPair)(ref current2)).J];
			args.Pipeline.DrawLine(val, val2, (Color)args.Color);
		}
	}

	public void DrawViewportMeshes(GH_PreviewMeshArgs args)
	{
	}
}
